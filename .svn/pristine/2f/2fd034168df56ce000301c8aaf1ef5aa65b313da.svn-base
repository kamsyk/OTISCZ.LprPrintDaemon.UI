using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.Management;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;

using OTISCZ.OtisUser;
using OTISCZ.LprPrintDaemon;


namespace OTISCZ.Baan.InvoicePrint.Setup {
    public partial class BaanInvoicePrint : Form {
        #region Constants
        private const string BAAN_PRINT_SERVICE_MAME = "BaanInvoicePrint";
        private const int TIMEOUT_IN_MILISECONDS = 30000;
        private const string LOG_FOLDER = "Log";
        #endregion

        #region Properties
        private ServiceController m_BaanPrintService {
            get { return new ServiceController(BAAN_PRINT_SERVICE_MAME); } 
        }

        private AppXmlSettings m_AppXmlSettings;

        private string _m_serviceIndentity = null;
        private string m_serviceIndentity {
            get {
                if (_m_serviceIndentity == null) {
                    _m_serviceIndentity = GetServiceUserIdentity();
                }

                return _m_serviceIndentity;
            }
            set {
                _m_serviceIndentity = value;   
            }
        }

        private string m_HomeFolder {
            get { return new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName; }
        }

        private string m_LogFolder {
            get {
                string strFolder = Path.Combine(m_HomeFolder, LOG_FOLDER);
                if (!Directory.Exists(strFolder)) {
                    Directory.CreateDirectory(strFolder);
                }

                return strFolder;
            }
        }
        #endregion
        
        #region Constructor
        public BaanInvoicePrint() {
            InitializeComponent();
            m_AppXmlSettings = new AppXmlSettings();
        }
        #endregion

        #region Methods
        private string StopService() {
            Cursor = Cursors.WaitCursor;
            try {
                if (m_BaanPrintService.Status == ServiceControllerStatus.Stopped) {
                    return null;
                }

                TimeSpan timeout = TimeSpan.FromMilliseconds(TIMEOUT_IN_MILISECONDS);

                m_BaanPrintService.Stop();
                m_BaanPrintService.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                
                DisplayServiceStatus();

                return null;
            } catch(Exception ex) {
                DisplayErrorMsg(ex);
                DisplayServiceStatus();
                return ex.ToString();
            } finally {
                Cursor = Cursors.Default;
            }
        }

        private string StartService() {
            Cursor = Cursors.WaitCursor;
            try {
                if (m_BaanPrintService.Status == ServiceControllerStatus.Running) {
                    return null;
                }

                TimeSpan timeout = TimeSpan.FromMilliseconds(TIMEOUT_IN_MILISECONDS);

                m_BaanPrintService.Start();
                m_BaanPrintService.WaitForStatus(ServiceControllerStatus.Running, timeout);
                DisplayServiceStatus();

                return null;
            } catch (Exception ex) {
                DisplayErrorMsg(ex);
                DisplayServiceStatus();
                return ex.ToString();
            } finally {
                Cursor = Cursors.Default;
            }
        }
                
        private void LoadInit() {
            
            //txtServerId.Text = m_AppXmlSettings.LpdServerIpName;
            txtPrinterName.Text = m_AppXmlSettings.PrinterName;

            ckbMoveToArchive.Checked = m_AppXmlSettings.IsAllowMoveToArchive;
            //ckbConvertToPCL.Checked = m_AppXmlSettings.IsAllowPclConvertion;
            btnArchiveFolderLookup.Enabled = ckbMoveToArchive.Checked;
            txtArchiveFolder.Enabled = btnArchiveFolderLookup.Enabled;
            //btnConverter.Enabled = ckbConvertToPCL.Checked;
            
            //txtPclConverter.Text = m_AppXmlSettings.PclConverterPath;
            txtFoxitPath.Text = m_AppXmlSettings.FoxitPath;
            txtSourceFolder.Text = m_AppXmlSettings.SourcePdfFolderName;
            txtArchiveFolder.Text = m_AppXmlSettings.ArchivePdfFolderName;

            txtUserNamePrint.Text = m_AppXmlSettings.PrintUserName;
            txtPasswordPrint.Text = m_AppXmlSettings.PrintUserPassword;
            txtDomainPrint.Text = m_AppXmlSettings.PrintUserDomain;

            txtUserNameFolder.Text = m_AppXmlSettings.FolderUserName;
            txtPasswordFolder.Text = m_AppXmlSettings.FolderUserPassword;
            txtDomainFolder.Text = m_AppXmlSettings.FolderUserDomain;
                        
            numMinutes.Value = m_AppXmlSettings.CheckPeriodInMinutes;

            ckbDefaultUser.Checked = m_AppXmlSettings.IsDefaultIdentity;
            ckbDefaultUserFolder.Checked = m_AppXmlSettings.IsFolderDefaultIdentity;

            txtAdminMails.Text = m_AppXmlSettings.AdminMails;

            DisplayServiceStatus();
            RefreshDisplayedIdentity();
        }

        private void RefreshDisplayedIdentity() {
            m_serviceIndentity = null;

            lblServiceIdentity.Text = m_serviceIndentity;
            ckbDefaultUser.Text = "Default Service Identity (" + m_serviceIndentity + ")";
            ckbDefaultUserFolder.Text = "Default Service Identity (" + m_serviceIndentity + ")";
            if (ckbDefaultUser.Checked) {
                lblPrintUserName.Text = m_serviceIndentity;
            } else {
                lblPrintUserName.Text = txtDomainPrint.Text.Trim() + "\\" + txtUserNamePrint.Text.Trim();
            }
        }

        private void DisplayServiceStatus() {
            if (m_BaanPrintService.Status == ServiceControllerStatus.Running) {
                btnStartService.Enabled = false;
                btnStopService.Enabled = true;
            } else {
                btnStartService.Enabled = true;
                btnStopService.Enabled = false;
            }

            lblServiceStatus.Text = BAAN_PRINT_SERVICE_MAME + " Service Status: " + m_BaanPrintService.Status.ToString();      
        }

        private void DisplayErrorMsg(Exception ex) {
            MessageBox.Show("The following error occured:" + ex.ToString(), BAAN_PRINT_SERVICE_MAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SaveSettings() {
            try {
                if (!IsParamsInOrder()) {
                    DialogResult dr = MessageBox.Show(
                        "Settings are not correct. If you save them the application will not work correctly.\r\nAre you sure to save the incorrect data?", 
                        "Wrong Data", 
                        MessageBoxButtons.YesNoCancel, 
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);
                    if (dr != System.Windows.Forms.DialogResult.Yes) {
                        return;
                    }
                }

                m_AppXmlSettings.LpdServerIpName = txtServerId.Text;
                m_AppXmlSettings.PrinterName = txtPrinterName.Text;
                //m_AppXmlSettings.PclConverterPath = txtPclConverter.Text;
                m_AppXmlSettings.FoxitPath = txtFoxitPath.Text;
                m_AppXmlSettings.SourcePdfFolderName = txtSourceFolder.Text;
                m_AppXmlSettings.ArchivePdfFolderName = txtArchiveFolder.Text;
                m_AppXmlSettings.CheckPeriodInMinutes = Convert.ToInt32(numMinutes.Value);
                m_AppXmlSettings.IsDefaultIdentity = ckbDefaultUser.Checked;
                m_AppXmlSettings.PrintUserName = txtUserNamePrint.Text;
                m_AppXmlSettings.PrintUserPassword = txtPasswordPrint.Text;
                m_AppXmlSettings.PrintUserDomain = txtDomainPrint.Text;
                m_AppXmlSettings.IsFolderDefaultIdentity = ckbDefaultUserFolder.Checked;
                m_AppXmlSettings.FolderUserName = txtUserNameFolder.Text;
                m_AppXmlSettings.FolderUserPassword = txtPasswordFolder.Text;
                m_AppXmlSettings.FolderUserDomain = txtDomainFolder.Text;
                m_AppXmlSettings.AdminMails = txtAdminMails.Text;
                m_AppXmlSettings.IsAllowPclConvertion = ckbConvertToPCL.Checked;
                m_AppXmlSettings.IsAllowMoveToArchive = ckbMoveToArchive.Checked;

                if (m_BaanPrintService.Status == ServiceControllerStatus.Running) {
                    StopService();
                    StartService();
                }

                RefreshDisplayedIdentity();

                MessageBox.Show("Setting were saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                DisplayErrorMsg(ex);
            }
        }

        private void SetSourceFolder() {
            DialogResult dr = fbdFolder.ShowDialog();
            if (dr == DialogResult.OK) {
                txtSourceFolder.Text = fbdFolder.SelectedPath;
            }
        }

        private void SetArchiveFolder() {
            DialogResult dr = fbdFolder.ShowDialog();
            if (dr == DialogResult.OK) {
                txtArchiveFolder.Text = fbdFolder.SelectedPath;
            }
        }

        private void EnablePrintUserIdentity() {
            if (ckbDefaultUser.Checked) {
                txtUserNamePrint.Enabled = false;
                txtPasswordPrint.Enabled = false;
                txtDomainPrint.Enabled = false;
            } else {
                txtUserNamePrint.Enabled = true;
                txtPasswordPrint.Enabled = true;
                txtDomainPrint.Enabled = true;
                if (txtDomainPrint.Text.Trim().Length == 0) {
                    txtDomainPrint.Text = "EUOTIS";
                }
            }   
        }

        private void EnableFolderUserIdentity() {
            if (ckbDefaultUserFolder.Checked) {
                txtUserNameFolder.Enabled = false;
                txtPasswordFolder.Enabled = false;
                txtDomainFolder.Enabled = false;
            } else {
                txtUserNameFolder.Enabled = true;
                txtPasswordFolder.Enabled = true;
                txtDomainFolder.Enabled = true;
                if (txtDomainFolder.Text.Trim().Length == 0) {
                    txtDomainFolder.Text = "EUOTIS";    
                }
            }
        }

        private void EnablePclConvertion() {
            if (ckbConvertToPCL.Checked) {
                btnConverter.Enabled = true;
            } else {
                btnConverter.Enabled = false;
            }
        }

        private void EnableArchiveMove() {
            if (ckbMoveToArchive.Checked) {
                btnArchiveFolderLookup.Enabled = true;
                txtArchiveFolder.Enabled = true;
            } else {
                btnArchiveFolderLookup.Enabled = false;
                txtArchiveFolder.Enabled = false;
            }
        }

        private void OpenUrlHyperlink() {
            ProcessStartInfo sInfo = new ProcessStartInfo(btnConverterUrl.Text);
            Process.Start(sInfo);

        }

        private void OpenFoxitUrlHyperlink() {
            ProcessStartInfo sInfo = new ProcessStartInfo(btnFoxitLink.Text);
            Process.Start(sInfo);

        }

        private void SetPclConverterExeFile() {
            if (txtPclConverter.Text.Trim().Length > 0) {
                ofdConverter.FileName = txtPclConverter.Text;
            } else {
                ofdConverter.FileName = "pdf2vec.exe";
            }

            DialogResult dr = ofdConverter.ShowDialog();
            if (dr == DialogResult.OK) {
                txtPclConverter.Text = ofdConverter.FileName;
            }
        }

        private void SetFoxitExeFile() {
            if (txtFoxitPath.Text.Trim().Length > 0) {
                ofdConverter.FileName = txtFoxitPath.Text;
            } else {
                ofdConverter.FileName = "foxitreader.exe";
            }

            DialogResult dr = ofdConverter.ShowDialog();
            if (dr == DialogResult.OK) {
                txtFoxitPath.Text = ofdConverter.FileName;
            }
        }

        private bool IsParamsInOrder() {
            errPrint.Clear();

            bool bOk = true;

            //if (txtPrinterName.Text.Trim().Length == 0) {
            //    errPrint.SetError(txtPrinterName, "Enter Printer Name");
            //    bOk = false;
            //}

            //if (txtServerId.Text.Trim().Length == 0) {
            //    errPrint.SetError(txtServerId, "Enter LDP Host Server IP address or Name");
            //    bOk = false;
            //}

            if (txtFoxitPath.Text.Trim().Length == 0) {
                errPrint.SetError(btnFoxitLookup, "Set FoxitReader.exe path");
                bOk = false;
            } else {
                if (!File.Exists(txtFoxitPath.Text)) {
                    errPrint.SetError(btnFoxitLookup, "File '" + txtFoxitPath.Text + "' does not exist");
                    bOk = false;
                }
            }

            if (txtSourceFolder.Text.Trim().Length == 0) {
                errPrint.SetError(btnSourceFolderLookup, "Set Source PDF Folder");
                bOk = false;
            } else {
                if (!Directory.Exists(txtSourceFolder.Text)) {
                    errPrint.SetError(btnSourceFolderLookup, "Folder '" + txtSourceFolder.Text + "' does not exist");
                    bOk = false;
                }
            }

            if (ckbMoveToArchive.Checked) {
                if (txtArchiveFolder.Text.Trim().Length == 0) {
                    errPrint.SetError(btnArchiveFolderLookup, "Set Archive Folder");
                    bOk = false;
                } else {
                    if (!Directory.Exists(txtArchiveFolder.Text)) {
                        errPrint.SetError(btnArchiveFolderLookup, "Folder '" + txtArchiveFolder.Text + "' does not exist");
                        bOk = false;
                    }   
                }
            }

            if (!ckbDefaultUser.Checked) {
                if (txtUserNamePrint.Text.Trim().Length == 0) {
                    errPrint.SetError(txtUserNamePrint, "Set User Name");
                    bOk = false;
                }

                if (txtPasswordPrint.Text.Trim().Length == 0) {
                    errPrint.SetError(txtPasswordPrint, "Set Password");
                    bOk = false;
                }

                if (txtDomainPrint.Text.Trim().Length == 0) {
                    errPrint.SetError(txtDomainPrint, "Set Domain");
                    bOk = false;
                }

                if (bOk) {
                    
                    if (!ckbDefaultUser.Checked) {
                        //if (!IsUserExists(txtUserNamePrint.Text.Trim(), txtDomainPrint.Text.Trim())) {
                        //    bOk = false;
                        //    MessageBox.Show("Print User Identity cannot be set. Wrong User Name or Domain", "Cannot Set User Identity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        
                        //}

                        UserClass otUser = new UserClass();
                        WindowsImpersonationContext wic = null;
                        this.Cursor = Cursors.WaitCursor;
                        try {
                            wic = otUser.ImpersonateUser(
                                txtUserNamePrint.Text.Trim(),
                                txtDomainPrint.Text.Trim(),
                                txtPasswordPrint.Text.Trim());
                        } catch {
                            bOk = false;
                            string msg = "Print User Identity cannot be set. Wrong User Name, Password or Domain";
                            errPrint.SetError(txtUserNamePrint, msg);
                            errPrint.SetError(txtDomainPrint, msg);
                            errPrint.SetError(txtPasswordPrint, msg);
                            MessageBox.Show(msg, "Cannot Set User Identity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        } finally {
                            this.Cursor = Cursors.Default;
                            if (wic != null) {
                                wic.Undo();
                            }

                        }
                    }

                    if (!ckbDefaultUserFolder.Checked) {
                        UserClass otUser = new UserClass();

                        WindowsImpersonationContext wic = null;
                        this.Cursor = Cursors.WaitCursor;
                        try {
                            wic = otUser.ImpersonateUser(
                                txtUserNameFolder.Text.Trim(),
                                txtDomainFolder.Text.Trim(),
                                txtPasswordFolder.Text.Trim());
                        } catch {
                            bOk = false;
                            string msg = "Source and Archive Folders User Identity cannot be set. Wrong User Name, Password or Domain";
                            errPrint.SetError(txtUserNameFolder, msg);
                            errPrint.SetError(txtDomainFolder, msg);
                            errPrint.SetError(txtPasswordFolder, msg);
                            MessageBox.Show(msg, "Cannot Set User Identity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        } finally {
                            this.Cursor = Cursors.Default;
                            if (wic != null) {
                                wic.Undo();
                            }

                        }
                    }
                }
            }

            if (bOk && !ckbDefaultUser.Checked) {
                RefreshDisplayedIdentity();
                if (txtPrinterName.Text.Trim().Length == 0) {
                    errPrint.SetError(txtPrinterName, "Enter Printer Name");
                    bOk = false;
                } else {
                    if (!IsPrinterOK()) {
                        errPrint.SetError(btnPrinter, "Not available printer for print user '" + lblPrintUserName.Text + "'");
                        bOk = false;    
                    }
                }
            }

            return bOk;
        }

        private bool IsPrinterOK() {
            UserClass otUser = new UserClass();
            WindowsImpersonationContext wic = null;
            
            try {
                if (!ckbDefaultUser.Checked) {
                    wic = otUser.ImpersonateUser(
                        txtUserNamePrint.Text.Trim(),
                        txtDomainPrint.Text.Trim(),
                        txtPasswordPrint.Text.Trim());
                }

                foreach (string tmpPrinterName in PrinterSettings.InstalledPrinters) {
                    if (txtPrinterName.Text.Trim().ToLower() == tmpPrinterName.Trim().ToLower()) {
                        return true;
                    }
                }

                return false;
            } catch {
                return false;                
            } finally {
                if (wic != null) {
                    wic.Undo();
                }

            }
        }

        private void GetPrinter() {
            WindowsImpersonationContext wic = null;

            try {
                if (ckbDefaultUser.Checked) {
                    //if (m_serviceIndentity.Trim().ToLower() == "localsystem") {
                    MessageBox.Show(
                        "The print is running as '" + m_serviceIndentity + "' user account. Select only printers which are available for the user.",
                        "Notification",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    //}
                } else {
                    wic = new UserClass().ImpersonateUser(
                                txtUserNamePrint.Text.Trim(),
                                txtDomainPrint.Text.Trim(),
                                txtPasswordPrint.Text.Trim());   
                }

                DialogResult dr = prnPrinter.ShowDialog();
                if (dr == DialogResult.OK) {
                    txtPrinterName.Text = prnPrinter.PrinterSettings.PrinterName;
                }

                


            } catch (Exception ex) {
                throw ex;
            } finally {
                if (wic != null) {
                    wic.Undo();
                }
            }
        }

        private string GetServiceUserIdentity() {
            string objPath = string.Format("Win32_Service.Name='{0}'", BAAN_PRINT_SERVICE_MAME);

            ManagementObject service = new ManagementObject(new ManagementPath(objPath));
            object invokeResult = service.GetPropertyValue("startname");

            return invokeResult.ToString();
            
            
        }

        private void SetServiceIdentity() {
            ServiceIdentity frmServiceIdentity = new ServiceIdentity(m_serviceIndentity);
            DialogResult dr = frmServiceIdentity.ShowDialog();

            bool bRunService = (m_BaanPrintService.Status == ServiceControllerStatus.Running);

            if (dr == DialogResult.OK) {
                this.Cursor = Cursors.WaitCursor;
                try {
                    string userName = frmServiceIdentity.UserName;
                    string domain = frmServiceIdentity.Domain;
                    string password = frmServiceIdentity.Password;
                    
                    string objPath = string.Format("Win32_Service.Name='{0}'", BAAN_PRINT_SERVICE_MAME);

                    ManagementObject service = new ManagementObject(new ManagementPath(objPath));
                    object[] wmiParams = new object[11];

                    if (frmServiceIdentity.IsPredefinedAccount) {
                        wmiParams[6] = userName;
                        wmiParams[7] = "";
                    } else {
                        string domainUserName = userName;
                        if (domain != null && domain.Trim().Length > 0) {
                            domainUserName = domain + "\\" + userName;
                        }
                        wmiParams[6] = domainUserName;
                        wmiParams[7] = password;
                    }

                    if (bRunService) {
                        string retStop = StopService();
                        if (retStop != null) {
                            throw new Exception(retStop);
                        }
                    }

                    object oInvokeResult = service.InvokeMethod("Change", wmiParams);
                    int iResult = 0;
                    if (oInvokeResult != null) {
                        iResult = Convert.ToInt32(oInvokeResult);
                    }

                    if (iResult != 0) {
                        throw new Exception("Windows Service Identity change failed with error code " + iResult);
                    }

                    if (bRunService) {
                        //string retStop = StopService();
                        //if (retStop != null) {
                        //    throw new Exception(retStop);
                        //}

                        string retStart = StartService();
                        if (retStart != null) {
                            throw new Exception(retStart);
                        }
                        
                    }
                                        
                    RefreshDisplayedIdentity();

                    MessageBox.Show("Baan Print Service Identity was set successfully", "Identity Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } catch (Exception ex) {
                    MessageBox.Show("Baan Print Service Identity setting failed.\r\n" + ex.ToString() , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } finally {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void DisplayLog() {
            if (Directory.GetFiles(m_LogFolder).Length == 0) {
                MessageBox.Show("No Log file was found, try to check Event Viewer.", "No Log File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ofdLog.FileName = "";
            ofdLog.InitialDirectory = m_LogFolder;
            ofdLog.Filter = "Log files (*.log)|*.log";


            DialogResult dr = ofdLog.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK) {
                string logFileName = ofdLog.FileName;
                LogView logView = new LogView(logFileName);
                logView.ShowDialog();
            }

            
        }
        #endregion


        private bool IsUserExists(string userName, string domain) {
            PrincipalContext pc = new PrincipalContext(ContextType.Domain);

            UserPrincipal up = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, userName);
            return (up != null);
        }
        
        private void BaanInvoicePrint_Load(object sender, EventArgs e) {
            LoadInit();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            SaveSettings();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnStartService_Click(object sender, EventArgs e) {
            StartService();
        }

        private void btnStopService_Click(object sender, EventArgs e) {
            StopService();
        }
               
        private void btnSourceFolderLookup_Click(object sender, EventArgs e) {
            SetSourceFolder();
        }

        private void btnArchiveFolderLookup_Click(object sender, EventArgs e) {
            SetArchiveFolder();
        }

        private void ckbDefaultUser_CheckedChanged(object sender, EventArgs e) {
            EnablePrintUserIdentity();
        }

        private void btnConverterUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            OpenUrlHyperlink();

        }

        private void btnConverter_Click(object sender, EventArgs e) {
            SetPclConverterExeFile();
        }

        private void ckbDefaultUserFolder_CheckedChanged(object sender, EventArgs e) {
            EnableFolderUserIdentity();
        }

        private void ckbConvertToPCL_CheckedChanged(object sender, EventArgs e) {
            EnablePclConvertion();
        }

        private void ckbMoveToArchive_CheckedChanged(object sender, EventArgs e) {
            EnableArchiveMove();
        }

        private void btnPrinter_Click(object sender, EventArgs e) {
            GetPrinter();
        }
               
        private void btnFoxitLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            OpenFoxitUrlHyperlink();
        }

        private void btnServiceIdentity_Click(object sender, EventArgs e) {
            SetServiceIdentity();
        }

        private void btnFoxitLookup_Click(object sender, EventArgs e) {
            SetFoxitExeFile();
        }

        private void btnDisplayLog_Click(object sender, EventArgs e) {
            DisplayLog();
        }

        
        
    }
}
