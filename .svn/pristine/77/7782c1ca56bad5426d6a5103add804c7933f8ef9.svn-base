using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OTISCZ.Baan.InvoicePrint.Setup {
    public partial class LogView : Form {
        #region Properties
        private string m_FileName = null;
        #endregion

        #region Constructor
        public LogView(string logFile) {
            InitializeComponent();
            m_FileName = logFile;
            this.Text += " " + logFile;
        }
        #endregion

        #region Methods
        private void LoadInit() {
            this.Cursor = Cursors.WaitCursor;
            try {
                using (StreamReader srFile = new StreamReader(m_FileName, true)) {
                    txtLog.Text = srFile.ReadToEnd();
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                this.Cursor = Cursors.Default;
            }
        }

        private void ResizeForm() {
            this.txtLog.Width = this.ClientSize.Width - 2 * txtLog.Left;
            btnClose.Left = this.ClientSize.Width / 2 - btnClose.Width / 2;

            txtLog.Height = this.ClientSize.Height - btnClose.Height - 2 * txtLog.Top;
            btnClose.Top = txtLog.Top + txtLog.Height + 5;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void LogView_Load(object sender, EventArgs e) {
            LoadInit();
        }
                
        private void LogView_Shown(object sender, EventArgs e) {
            ResizeForm();
        }

        private void LogView_Resize(object sender, EventArgs e) {
            ResizeForm();
        }
    }
}
