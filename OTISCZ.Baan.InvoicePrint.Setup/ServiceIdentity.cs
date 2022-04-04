using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;

using OTISCZ.OtisUser;

namespace OTISCZ.Baan.InvoicePrint.Setup {
    public partial class ServiceIdentity : Form {
        #region Properties
        public string UserName {
            get { return GetUserName(); }
        }

        public string Domain {
            get { return GetDomain(); }
        }

        public string Password {
            get { return GetPassword(); }
        }

        public bool IsPredefinedAccount {
            get { return GetIsPredefinedAccount(); }
        }

        private string m_ServiceAccount;
        public string ServiceAccount {
            get { return m_ServiceAccount; }
            set { m_ServiceAccount = value; }
        }
        #endregion

        #region Constructor
        public ServiceIdentity(string serviceAccount) {
            InitializeComponent();
            m_ServiceAccount = serviceAccount;
        }
        #endregion

        #region Methods
        private string GetUserName() {
            
            if (ckbLocalsystem.Checked) {
                return "LocalSystem";
            }

            //if (ckbNetworkSystem.Checked) {
            //    return "NetworkSystem";
            //}

            return txtUserName.Text; ;
        }

        private string GetDomain() {

            if (ckbLocalsystem.Checked) {
                return "";
            }

            //if (ckbNetworkSystem.Checked) {
            //    return "";
            //}

            return txtDomain.Text;
        }

        private string GetPassword() {

            if (ckbLocalsystem.Checked) {
                return "";
            }

            //if (ckbNetworkSystem.Checked) {
            //    return "";
            //}

            return txtPassword.Text;
        }

        private bool GetIsPredefinedAccount() {

            if (ckbLocalsystem.Checked) {
                return true;
            }

            //if (ckbNetworkSystem.Checked) {
            //    return true;
            //}

            return false;
        }

        private bool IsUserAccountOK() {
            errIdentity.Clear();
            
            bool bOk = true;

            if (ckbOtherUser.Checked) {
                UserClass otUser = new UserClass();

                WindowsImpersonationContext wic = null;
                this.Cursor = Cursors.WaitCursor;
                try {
                    wic = otUser.ImpersonateUser(
                        txtUserName.Text.Trim(),
                        txtDomain.Text.Trim(),
                        txtPassword.Text.Trim());
                } catch {
                    bOk = false;
                    string msg = "Identity cannot be set. Wrong User Name, Password or Domain";
                    errIdentity.SetError(txtUserName, msg);
                    errIdentity.SetError(txtDomain, msg);
                    errIdentity.SetError(txtPassword, msg);
                    MessageBox.Show(msg, "Cannot Set User Identity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } finally {
                    this.Cursor = Cursors.Default;
                    if (wic != null) {
                        wic.Undo();
                    }

                }
            }

            return bOk;
        }

        private void LoadInit() {
            if (m_ServiceAccount.Trim().ToLower() == "localsystem") {
                ckbLocalsystem.Checked = true;
                ckbOtherUser.Checked = false;
            } else {
                ckbLocalsystem.Checked = false;
                ckbOtherUser.Checked = true;
            }    
        }

        private void EnableDisableLocalSystem() {
            if (ckbLocalsystem.Checked) {
                ckbOtherUser.Checked = false;
                txtDomain.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
            } else {
                ckbOtherUser.Checked = true;
                txtDomain.Enabled = true;
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                if (m_ServiceAccount.Trim().ToLower() != "localsystem") {
                    string[] serviceAccountParts = m_ServiceAccount.Split('\\');
                    if (serviceAccountParts.Length > 1) {
                        txtUserName.Text = serviceAccountParts[1];
                        txtDomain.Text = serviceAccountParts[0];
                    } else {
                        txtUserName.Text = serviceAccountParts[0];
                    }
                }

                if (txtDomain.Text.Trim().Length == 0) {
                    txtDomain.Text = "CZOTIS";
                }
            }
        }

        private void EnableDisableOtherUser() {
            if (ckbOtherUser.Checked) {
                ckbLocalsystem.Checked = false;
                ckbOtherUser.Checked = true;
                txtDomain.Enabled = true;
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                if (m_ServiceAccount.Trim().ToLower() != "localsystem") {
                    string[] serviceAccountParts = m_ServiceAccount.Split('\\');
                    if (serviceAccountParts.Length > 1) {
                        txtUserName.Text = serviceAccountParts[1];
                        txtDomain.Text = serviceAccountParts[0];
                    } else {
                        txtUserName.Text = serviceAccountParts[0];
                    }
                }

                if (txtDomain.Text.Trim().Length == 0) {
                    txtDomain.Text = "CZOTIS";
                }
            } else {
                ckbLocalsystem.Checked = true;
                txtDomain.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
            }
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e) {
            if (!IsUserAccountOK()) {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ServiceIdentity_Load(object sender, EventArgs e) {
            LoadInit();
        }

        private void ckbLocalsystem_CheckedChanged(object sender, EventArgs e) {
            EnableDisableLocalSystem();
        }

        private void ckbOtherUser_CheckedChanged(object sender, EventArgs e) {
            EnableDisableOtherUser();
        }
    }
}
