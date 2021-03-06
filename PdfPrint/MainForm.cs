using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PdfPrint {
    public partial class MainForm : Form {
        private AppXmlSettings m_AppXmlSettings;

        private string _m_foxitExeFilePath = null;
        private string m_foxitExeFilePath {
            get {
                if (_m_foxitExeFilePath == null) {
                    Assembly pbAssembly = Assembly.GetEntryAssembly();
                    if (pbAssembly == null) return null;
                    if (Assembly.GetEntryAssembly().Location == null) return null;

                    string configFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    string configFileFullPath = new DirectoryInfo(configFilePath).FullName;

                    string foxitPath = Path.Combine(configFileFullPath, "Foxit");
                    _m_foxitExeFilePath = Path.Combine(foxitPath, "FoxitReader.exe");
                }

                return _m_foxitExeFilePath;
            }    
        }

        Thread m_tPrint;

        public MainForm() {
            InitializeComponent();
            m_AppXmlSettings = new AppXmlSettings();
        }


        private void GetPrinter() {
            try {
               
                DialogResult dr = prnPrinter.ShowDialog();
                if (dr == DialogResult.OK) {
                    txtPrinterName.Text = prnPrinter.PrinterSettings.PrinterName;
                }


            } catch (Exception ex) {
                throw ex;
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

        private void SaveSourceFolderSettings() {
            try {
               
                
                m_AppXmlSettings.SourcePdfFolderName = txtSourceFolder.Text;
                
                
            } catch (Exception ex) {
                throw ex;
            }
        }

        private void SaveArchiveFolderSettings() {
            try {

                
                m_AppXmlSettings.ArchivePdfFolderName = txtArchiveFolder.Text;

            } catch (Exception ex) {
                throw ex;
            }
        }

        private void SavePrinterSettings() {
            try {

                m_AppXmlSettings.PrinterName = txtPrinterName.Text;
                

            } catch (Exception ex) {
                throw ex;
            }
        }

        private void LoadInit() {
                       
            txtPrinterName.Text = m_AppXmlSettings.PrinterName;
            txtSourceFolder.Text = m_AppXmlSettings.SourcePdfFolderName;
            txtArchiveFolder.Text = m_AppXmlSettings.ArchivePdfFolderName;

            
        }

        private void PrintThread() {
            if (!IsValid()) {
                return;
            }

#if DEBUG
            Print();
#else
            m_tPrint = new Thread(Print);
            m_tPrint.Start();
#endif
        }

        private void Print() {
            

            btnPrint.Enabled = false;
            try {


                string[] files = Directory.GetFiles(txtSourceFolder.Text, "*.pdf");
                string[] sortFiles = files.OrderBy(x => x).ToArray();

                if (files.Length == 0) {
                    MessageBox.Show("No PDF was found", "No PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnPrint.Enabled = true;
                    
                }

                bool isError = false;
                foreach (string file in sortFiles) {
                    FileInfo fi = new FileInfo(file);
                    
                    if (!IsPdfCorrect(file)) {
                        txtProcess.Text += "*************************************************************************************************************************************" + Environment.NewLine;
                        txtProcess.Text += DateTime.Now + ": " + fi.Name + ": " + "PDF file cannot be opened, it could be corrupted" + Environment.NewLine;
                        txtProcess.Text += "*************************************************************************************************************************************" + Environment.NewLine;
                        
                        isError = true;
                        continue;
                    }

                    if (fi.Extension.ToLower() != ".pdf") {
                        txtProcess.Text += DateTime.Now + ": " + fi.Name + ": " + " is not a PDF file it was skipped" + Environment.NewLine;
                        continue;
                    }

                    string errMsg = PrintPdf(file);
                    if (errMsg == null) {
                        txtProcess.Text += DateTime.Now + ": " + fi.Name + " was sent to printer " + Environment.NewLine;
                    } else {
                        txtProcess.Text += "*************************************************************************************************************************************" + Environment.NewLine;
                        txtProcess.Text += DateTime.Now + ": " + errMsg + Environment.NewLine;
                        txtProcess.Text += "*************************************************************************************************************************************" + Environment.NewLine;
                        
                        isError = true;
                        continue;
                    }
                     
                    if (ckbMoveToArchive.Checked) {
                        try {
                            string archiveFileName = Path.Combine(txtArchiveFolder.Text, fi.Name);
                            int iIndex = 1;
                            while (File.Exists(archiveFileName)) {
                                archiveFileName = Path.Combine(txtArchiveFolder.Text, fi.Name.Substring(0, fi.Name.Length - 4) + "_" + iIndex + ".pdf");
                                iIndex++;
                            }
                            File.Move(file, archiveFileName);
                            //txtProcess.Text = DateTime.Now + ": " + fi.Name + " was moved to archive folder" + Environment.NewLine + txtProcess.Text ;
                        } catch (Exception exMove) {
                            txtProcess.Text += DateTime.Now + ": " + exMove.Message + Environment.NewLine;
                            MessageBox.Show("Error " + file + " was not moved to archive folder. " + exMove.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //btnPrint.Enabled = true;
                            return;
                        }
                    } 
                }

                txtProcess.Text += DateTime.Now + ": " + "Print was finished " + Environment.NewLine ;

                if (isError) {
                    MessageBox.Show("Some files were not printed because of errors, they remain in the Source folder", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } else {
                    MessageBox.Show("Printed successfully", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show("Error occured. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (m_tPrint != null && m_tPrint.IsAlive) {
                    m_tPrint.Abort();
                }
                
            } finally {
                btnPrint.Enabled = true;
            }
        }

        private bool IsPdfCorrect(string strPdfPath) {
            try {
                PdfReader reader = new PdfReader(strPdfPath);
                //using (FileStream stream1 = File.Open(strPdfPath, FileMode.OpenOrCreate)) {
                //}
            } catch {
                return false;
            }

            return true;
        }

        private string PrintPdf(string sourcePdfFile) {


            string foxitParams = " -t" + " " +
                    "\"" + sourcePdfFile + "\"" + " " +
                    "\"" + txtPrinterName.Text + "\"";

            ProcessStartInfo siFoxit = new ProcessStartInfo();
            siFoxit.Arguments = foxitParams;
            siFoxit.FileName = m_foxitExeFilePath;
            siFoxit.UseShellExecute = false;
            siFoxit.WorkingDirectory = Directory.GetCurrentDirectory();
            siFoxit.CreateNoWindow = true;

            Process pFoxit = Process.Start(siFoxit);

            while (!pFoxit.HasExited) {
            }

            if (pFoxit.ExitCode == 0) {
                return null;
            } else { 
                return "FOXIT Print failed with code:" + pFoxit.ExitCode.ToString();
            }
        }

        private bool IsPrinterReady(string printerName) {
           
            try { 
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;

                return printDocument.PrinterSettings.IsValid;
               
            } catch (Exception ex) {
                throw new Exception("Cannot check the printer. " + ex.ToString());
            } 

        }

        private bool IsValid() {
            bool isValid = true;

            errProvider.Clear();

            if (String.IsNullOrEmpty(txtSourceFolder.Text)) {
                isValid = false;
                errProvider.SetError(txtSourceFolder, "Enter Value");
            }

            if (String.IsNullOrEmpty(txtArchiveFolder.Text)) {
                isValid = false;
                errProvider.SetError(txtArchiveFolder, "Enter Value");
            }

            if (String.IsNullOrEmpty(txtPrinterName.Text)) {
                isValid = false;
                errProvider.SetError(txtPrinterName, "Enter Value");
            }

            if (!Directory.Exists(txtSourceFolder.Text)) {
                isValid = false;
                errProvider.SetError(txtSourceFolder, "Directory does not exist");
            }

            if (!Directory.Exists(txtArchiveFolder.Text)) {
                isValid = false;
                errProvider.SetError(txtArchiveFolder, "Directory does not exist");
            }

            if (!IsPrinterReady(txtPrinterName.Text)) {
                isValid = false;
                errProvider.SetError(txtPrinterName, "Printer '" + txtPrinterName.Text + "' is not available");
                
            }

            return isValid;
        }

        

        private void MainForm_Load(object sender, EventArgs e) {
            LoadInit();
        }

        private void btnPrinter_Click(object sender, EventArgs e) {
            GetPrinter();
        }

        private void btnSorceFolder_Click(object sender, EventArgs e) {
            SetSourceFolder();
        }

        private void btnArchiveFolder_Click(object sender, EventArgs e) {
            SetArchiveFolder();
        }

        private void txtSourceFolder_TextChanged(object sender, EventArgs e) {
            SaveSourceFolderSettings();
        }

        private void txtArchiveFolder_TextChanged(object sender, EventArgs e) {
            SaveArchiveFolderSettings();
        }

        private void txtPrinterName_TextChanged(object sender, EventArgs e) {
            SavePrinterSettings();
        }

        private void btnPrint_Click(object sender, EventArgs e) {
            PrintThread();
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (m_tPrint != null && m_tPrint.IsAlive) {
                m_tPrint.Abort();
            }
        }
    }
}
