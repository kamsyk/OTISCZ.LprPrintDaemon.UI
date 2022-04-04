using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using OTISCZ.LprPrintDaemon;

namespace OTISCZ.LprPrintDaemon.UI {
    public partial class Test : Form {
        #region Properties
        private bool m_IsLoading = false;
        private AppXmlSettings m_AppXmlSettings = new AppXmlSettings();
        #endregion

        #region Constructor
        public Test() {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void LoadInit() {
            m_IsLoading = true;
            txtArchiveFolder.Text = m_AppXmlSettings.ArchivePdfFolderName;
            txtPrinterName.Text = m_AppXmlSettings.PrinterName;
            txtServerId.Text = m_AppXmlSettings.LpdServerIpName;
            txtSourceFolder.Text = m_AppXmlSettings.SourcePdfFolderName;
            txtGhostscript.Text = m_AppXmlSettings.GhostSriptPath;
            m_IsLoading = false;
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

        private void SetGhostscriptexeFile() {
            if (txtGhostscript.Text.Trim().Length > 0) {
                ofdGswin32c.FileName = txtGhostscript.Text;
            } else {
                ofdGswin32c.FileName = "gswin32c.exe";
            }

            DialogResult dr = ofdGswin32c.ShowDialog();
            if (dr == DialogResult.OK) {
                txtGhostscript.Text = ofdGswin32c.FileName;
            }
        }

        private bool IsParamsValid() {
            errPrint.Clear();

            bool bIsValid = true;

            if (txtServerId.Text.Length == 0) {
                errPrint.SetError(txtServerId, "Enter IP address or name of the LPD server");
                bIsValid = false;
            }

            if (txtPrinterName.Text.Length == 0) {
                errPrint.SetError(txtPrinterName, "Enter Printer Name");
                bIsValid = false;
            }

            if (txtArchiveFolder.Text.Length == 0) {
                errPrint.SetError(btnArchiveFolderLookup, "Select Archive PDF files Folder");
                bIsValid = false;
            }

            if (txtSourceFolder.Text.Length == 0) {
                errPrint.SetError(btnSourceFolderLookup, "Select Source PDF files Folder");
                bIsValid = false;
            }

            return bIsValid;
        }

        private void PrintPdfFiles() {
            this.Cursor = Cursors.WaitCursor;
            try {
                txtPrintInfo.Text = "";

                if (!IsParamsValid()) {
                    MessageBox.Show("Fix the parameters", "Error - Cannot be printed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                LogPrintItem("Print started");
                string[] files = Directory.GetFiles(m_AppXmlSettings.SourcePdfFolderName, "*.pdf");
                if (files.Length == 0) {
                    LogPrintItem("No PDF file was found in the '" + m_AppXmlSettings.SourcePdfFolderName + "'");
                    return;
                }

                bool bOK = true;
                Lpr lpr = new Lpr();
                for (int i = 0; i < files.Length; i++) {
                    try {
                        string result = lpr.PrintFile(
                            m_AppXmlSettings.LpdServerIpName,
                            m_AppXmlSettings.PrinterName,
                            m_AppXmlSettings.GhostSriptPath,
                            null,
                            null,
                            null,
                            files[i],
                            null);
                        if (result == null) {
                            //result = lpr.MoveFile(files[i], m_AppXmlSettings.ArchivePdfFolderName);
                        }
                        if (result == null) {
                            LogPrintItem(files[i] + " - OK");
                        } else {
                            bOK = false;
                            LogPrintItem(files[i] + " -> ERROR -> ERROR CODE " + result);
                        }
                    } catch (Exception ex) {
                        LogPrintItem("-------------------------------------------------");
                        LogPrintItem(files[i] + " ERROR - " + ex.ToString());
                        LogPrintItem("-------------------------------------------------");
                    }
                }

                if (bOK) {
                    LogPrintItem("Print finished successfully");
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                this.Cursor = Cursors.Default;
            }
        }

        private void LogPrintItem(string msg) {
            txtPrintInfo.Text = DateTime.Now + " -> " + msg + "\r\n" + txtPrintInfo.Text;
        }
        #endregion

        private void btnSourceFolderLookup_Click(object sender, EventArgs e) {
            SetSourceFolder();  
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e) {
            PrintPdfFiles();
        }

        private void Test_Load(object sender, EventArgs e) {
            LoadInit();
        }

        private void txtServerId_TextChanged(object sender, EventArgs e) {
            if (m_IsLoading) {
                return;
            }
            m_AppXmlSettings.LpdServerIpName = txtServerId.Text;
        }

        private void txtPrinterName_TextChanged(object sender, EventArgs e) {
            if (m_IsLoading) {
                return;
            }
            m_AppXmlSettings.PrinterName = txtPrinterName.Text;
        }

        private void txtSourceFolder_TextChanged(object sender, EventArgs e) {
            if (m_IsLoading) {
                return;
            }
            m_AppXmlSettings.SourcePdfFolderName = txtSourceFolder.Text;
        }

        private void txtArchiveFolder_TextChanged(object sender, EventArgs e) {
            if (m_IsLoading) {
                return;
            }
            m_AppXmlSettings.ArchivePdfFolderName = txtArchiveFolder.Text;
        }

        private void btnArchiveFolderLookup_Click(object sender, EventArgs e) {
            SetArchiveFolder();  
        }

        private void btnGswin32_Click(object sender, EventArgs e) {
            SetGhostscriptexeFile();
        }

        private void txtGhostscript_TextChanged(object sender, EventArgs e) {
            if (m_IsLoading) {
                return;
            }
            m_AppXmlSettings.GhostSriptPath = txtGhostscript.Text;
        }

       
    }
}
