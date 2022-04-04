using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Security.Principal;
using System.Reflection;
using System.Threading;
using System.Drawing.Printing;
using System.Security;

using OTISCZ.LprPrintDaemon;
using OTISCZ.OtisUser;

namespace OTISCZ.Baan.InvoicePrintService {
    public class PdfPrint {
        #region Constants
        private const string EVENT_LOG_SOURCE = "Baan Invoice Print Service";
        private const string TEMP_FOLDER = "TempInvoice";
        private const string LOG_FOLDER = "Log";
        #endregion

        #region Static Properties
        private static bool m_IsCheckingFolder = false;
        public static bool IsCheckingFolder {
            get { return m_IsCheckingFolder; }
            set { m_IsCheckingFolder = value; }
        }
        #endregion

        #region Properties
        private AppXmlSettings m_AppXmlSettings;

        private string m_HomeFolder {
            get { return new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName; }
        }

        private string m_TempFolder {
            get { return Path.Combine(m_HomeFolder, TEMP_FOLDER); }
        }

        private string m_LogFolder {
            get { 
                string strFolder = Path.Combine(m_HomeFolder, LOG_FOLDER); 
                if(!Directory.Exists(strFolder)) {
                    Directory.CreateDirectory(strFolder);    
                }

                return strFolder;
            }
        }
        #endregion

        #region Constructor
        public PdfPrint() {
            m_AppXmlSettings = new AppXmlSettings();
        }
        #endregion

        #region Methods
        public void InvoicePrint() {
            if (m_IsCheckingFolder) {
                return;
            }

            try {
                m_IsCheckingFolder = true;

                string[] files = null;
                WindowsImpersonationContext wic = null;
                try {
                    if (!m_AppXmlSettings.IsFolderDefaultIdentity) {
                        wic = new UserClass().ImpersonateUser(
                            m_AppXmlSettings.FolderUserName,
                            m_AppXmlSettings.FolderUserDomain,
                            m_AppXmlSettings.FolderUserPassword);
                    }
                    files = Directory.GetFiles(m_AppXmlSettings.SourcePdfFolderName, "*.pdf");
                } catch (Exception ex) {
                    throw ex;
                } finally {
                    if (wic != null) {
                        wic.Undo();
                    }
                }

                if (files == null || files.Length == 0) {
                    return;
                }

                //printer test
                if (!IsPrinterReady(m_AppXmlSettings.PrinterName)) { 
                    string currUser = WindowsIdentity.GetCurrent().Name;
                    if (!m_AppXmlSettings.IsDefaultIdentity) {
                        currUser = m_AppXmlSettings.PrintUserName;
                    }
                    string errMsg = "Printer '" + m_AppXmlSettings.PrinterName + "' is not available for the user '" + currUser + "'";
                    throw new Exception(errMsg);
                }

                foreach (string origFileName in files) {
                    try {
                        LogMsg("--------------------------------"); 
                        LogMsg(origFileName + " was found");   
                     
                        string tmpPrintUserName = null;
                        string tmpPrintUserPassword = null;
                        string tmpPrintUserDomain = null;
                        if (!m_AppXmlSettings.IsDefaultIdentity) {
                            tmpPrintUserName = m_AppXmlSettings.PrintUserName;
                            tmpPrintUserPassword = m_AppXmlSettings.PrintUserPassword;
                            tmpPrintUserDomain = m_AppXmlSettings.PrintUserDomain;
                        }

                        string localFileName = CopyFileToTempFolder(origFileName);

                        //string pclConverter = null;
                        //if (m_AppXmlSettings.IsAllowPclConvertion) {
                        //    pclConverter = m_AppXmlSettings.PclConverterPath;  
                        //}

                        //string printResult = new Lpr().PrintFile(
                        //    m_AppXmlSettings.LpdServerIpName,
                        //    m_AppXmlSettings.PrinterName,
                        //    pclConverter,
                        //    tmpPrintUserName,
                        //    tmpPrintUserPassword,
                        //    tmpPrintUserDomain,
                        //    origFileName,
                        //    localFileName);

                        string printResult = FoxitPrint(
                            m_AppXmlSettings.FoxitPath, 
                            localFileName,
                            m_AppXmlSettings.PrinterName,
                            tmpPrintUserName,
                            tmpPrintUserDomain,
                            tmpPrintUserPassword);
                        
                        if (printResult == null) {
                            string succMsg = origFileName + " was sent to printer successfully with user account " + WindowsIdentity.GetCurrent().Name;
                            EventLog.WriteEntry(EVENT_LOG_SOURCE, succMsg, EventLogEntryType.Information);
                            LogMsg(succMsg);
                            
                            if (m_AppXmlSettings.IsAllowMoveToArchive) {
                                MoveFile(origFileName, m_AppXmlSettings.ArchivePdfFolderName);
                                LogMsg(origFileName + " was moved to archive " + m_AppXmlSettings.ArchivePdfFolderName);
                            } else {
                                File.Delete(origFileName);
                                LogMsg(origFileName + " was deleted");
                            }

                            
                        } else {
                            LogError(new Exception(printResult), origFileName);
                            RenameFileToErr(origFileName);
                        }
                    } catch (Exception exc) {
                        LogError(exc, origFileName);
                        RenameFileToErr(origFileName);
                    }
                }

                ClearTempFolder();
            } catch (Exception ex) {
                LogError(ex);
            } finally {
                m_IsCheckingFolder = false;

            }
        }

        private string FoxitPrint(
            string foxitExeFilePath, 
            string sourcePdfFile, 
            string printerName,
            string userName,
            string domain,
            string password) {
            try {

                string foxitParams = " -t" + " " + 
                    "\"" + sourcePdfFile + "\"" + " " + 
                    "\"" + printerName + "\"";
                
                if (userName != null && userName.Trim().Length > 0) {
                    string commandLine = foxitExeFilePath + foxitParams;

                    new OtProcess().CreateProcessWithLogon(
                        commandLine,
                        userName,
                        domain,
                        password);
                    
                } else {
                    ProcessStartInfo siFoxit = new ProcessStartInfo();
                    siFoxit.Arguments = foxitParams;
                    siFoxit.FileName = foxitExeFilePath;
                    siFoxit.UseShellExecute = false;
                    siFoxit.WorkingDirectory = Directory.GetCurrentDirectory();
                    siFoxit.CreateNoWindow = true;

                    Process pFoxit = Process.Start(siFoxit);
                    while (!pFoxit.HasExited) {
                    }

                    if (pFoxit.ExitCode != 0) {
                        return "FOXIT Print failed with code:" + pFoxit.ExitCode.ToString();
                    }
                }

                LogMsg(sourcePdfFile + " was send to Foxit to be printed " + foxitExeFilePath + " " + foxitParams);
                
                return null;
            } catch (Exception ex) {
                if(ex is OtAccessDeniedException) {
                    string msg = "Foxit error :" + ex.ToString();
                    msg += "Current user " + new UserClass().GetCurrentUserName() + " have no sufficient rights to run the FoxitReader.exe " +
                        " with identity of the '" + domain + "\\" + userName +"'";
                    return msg; 
                } else {
                    return "Foxit error :" + ex.ToString();
                }
            } 
        }

        private SecureString GetPassword(string plainPassword) {
            char[] PasswordChars = plainPassword.ToCharArray();
            SecureString ssPassword = new SecureString();
            foreach (char c in PasswordChars) {
                ssPassword.AppendChar(c);
            }

            return ssPassword;

        }

        private bool IsPrinterReady(string printerName) {
            WindowsImpersonationContext wic = null;
            try {
                if (!m_AppXmlSettings.IsDefaultIdentity) {
                    wic = new UserClass().ImpersonateUser(
                        m_AppXmlSettings.PrintUserName,
                        m_AppXmlSettings.PrintUserDomain,
                        m_AppXmlSettings.PrintUserPassword);
                }
                
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;

                return printDocument.PrinterSettings.IsValid;
                //if (!printDocument.PrinterSettings.IsValid) {
                //    return false;
                //}


                //foreach (string tmpPrinterName in PrinterSettings.InstalledPrinters) {
                //    if (printerName.Trim().ToLower() == tmpPrinterName.Trim().ToLower()) {
                //        return true;
                //    }
                //}

                //return false;
            } catch (Exception ex) {
                throw new Exception("Cannot check the printer. " + ex.ToString());
            } finally {
                if(wic != null) {
                    wic.Undo();
                }
            }
                        
        }

       

        private string CopyFileToTempFolder(string fileName) {
            ClearTempFolder();
            //if (!Directory.Exists(m_TempFolder)) {
            //    Directory.CreateDirectory(m_TempFolder);
            //}

            //string[] files = Directory.GetFiles(m_TempFolder);
            //foreach (string file in files) {
            //    try {
            //        File.Delete(file);
            //    } catch (Exception ex) {
            //        LogError(ex);
            //    }
            //}

            WindowsImpersonationContext wic = null;
            try {
                if (!m_AppXmlSettings.IsFolderDefaultIdentity) {
                    wic = new UserClass().ImpersonateUser(
                        m_AppXmlSettings.FolderUserName,
                        m_AppXmlSettings.FolderUserDomain,
                        m_AppXmlSettings.FolderUserPassword);
                }

                byte[] byteContent = null;
                
                FileInfo fi = new FileInfo(fileName);
                FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                int fileLength = Convert.ToInt32(fi.Length);
                byteContent = br.ReadBytes(fileLength);
                br.Close();
                fs.Close();
                if (wic != null) {
                    wic.Undo();
                    wic = null;
                }

                string localFileName = Path.Combine(m_TempFolder, fi.Name);
                FileStream fsl = new FileStream(localFileName, FileMode.CreateNew, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fsl);
                bw.Write(byteContent);
                bw.Close();
                fsl.Close();

                return localFileName;
                
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (wic != null) {
                    wic.Undo();
                }
            }


        }

        private void ClearTempFolder() {
            if (!Directory.Exists(m_TempFolder)) {
                Directory.CreateDirectory(m_TempFolder);
            }

            string[] files = Directory.GetFiles(m_TempFolder);
            foreach (string file in files) {
                try {
                    int iTryCount = 0;
                    while (iTryCount < 3) {
                        try {
                            File.Delete(file);
                            iTryCount = 3;
                        } catch(Exception ex) {
                            Thread.Sleep(500);
                            iTryCount++;
                            if (iTryCount == 3) {
                                LogError(ex);
                            }
                        }
                    }
                } catch (Exception ex) {
                    LogError(ex);
                }
            }    
        }

        private void LogError(Exception ex, string fileName) {
            string tmpFile = "";
            if (fileName != null) {
                tmpFile = " file: " + fileName + " - "; 
            }
            EventLog.WriteEntry(EVENT_LOG_SOURCE, tmpFile + ex.ToString(), EventLogEntryType.Error);
            LogMsg("ERROR!!! " + tmpFile + ex.ToString()); 
            if (m_AppXmlSettings.AdminMails != null && m_AppXmlSettings.AdminMails.Trim().Length > 0) {
                WsOtMail.OtWsMail otMail = new WsOtMail.OtWsMail();
                otMail.SendMail(
                    "OTIS_APP_CZ@otis.com",
                    m_AppXmlSettings.AdminMails,
                    null,
                    EVENT_LOG_SOURCE,
                    "An error occured in the Baan Invoice Printing Service: " + tmpFile + ex.ToString(),
                    null,
                    (int)MailPriority.High);
            }
        }

        private void LogError(Exception ex) {
            LogError(ex, null);
        }

        private void MoveFile(string sourceFile, string destinFolder) {
            WindowsImpersonationContext wic = null;
            try {
                if (!m_AppXmlSettings.IsFolderDefaultIdentity) {
                    wic = new UserClass().ImpersonateUser(
                            m_AppXmlSettings.FolderUserName,
                            m_AppXmlSettings.FolderUserDomain,
                            m_AppXmlSettings.FolderUserPassword);
                    
                }

                FileInfo fiSource = new FileInfo(sourceFile);
                string destinFileName = Path.Combine(destinFolder, fiSource.Name);
                int iIndex = 1;
                while (File.Exists(destinFileName)) {
                    string pureFileNameWoExtent = fiSource.Name.Substring(0, fiSource.Name.Length - fiSource.Extension.Length - 1);
                    destinFileName = Path.Combine(destinFolder, pureFileNameWoExtent + "_" + iIndex + "." + fiSource.Extension);
                    iIndex++;
                }

                File.Move(sourceFile, destinFileName);
                                
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (wic != null) {
                    wic.Undo();
                }    
            }
        }

        private void RenameFileToErr(string sourceFile) {
            WindowsImpersonationContext wic = null;
            try {
                if (!m_AppXmlSettings.IsFolderDefaultIdentity) {
                    wic = new UserClass().ImpersonateUser(
                            m_AppXmlSettings.FolderUserName,
                            m_AppXmlSettings.FolderUserDomain,
                            m_AppXmlSettings.FolderUserPassword);
                }

                FileInfo fiSource = new FileInfo(sourceFile);
                string errFileName = fiSource.Name + ".err";
                int iIndex = 1;
                while (File.Exists(errFileName)) {
                    errFileName = fiSource.Name + "_" + iIndex + ".err";
                    iIndex++;    
                }

                File.Move(sourceFile, errFileName);

            } catch (Exception ex) {
                throw ex;
            } finally {
                if (wic != null) {
                    wic.Undo();
                }
            }
        }

        private void LogMsg(string msg) { 
            string logPureFileName = DateTime.Now.ToString("yyyyMM") + ".log";
            string logFileName = Path.Combine(m_LogFolder, logPureFileName);

            msg = DateTime.Now + ": " + msg;

            using (StreamWriter swFile = new StreamWriter(logFileName, true)) {
                swFile.WriteLine(msg);
            }

        }
        #endregion
    }
}
