using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Principal;
using System.Threading;

using OTISCZ.OtisUser;

namespace OTISCZ.LprPrintDaemon {
    public class Lpr {
        #region Properties
        private string m_LprExePath {
            get {
                return Path.Combine(Environment.SystemDirectory, "lpr.exe");
            }
        }
        #endregion

        #region Methods
        public string PrintFile(
            string lpdServerIpName, 
            string printerName, 
            string pclConverterPath, 
            string printUserName,
            string printUserPassword,
            string printUserDomain,
            string origfilePath,
            string localFilePath) {

            try {
                string printFileName = localFilePath;
                if (pclConverterPath != null) {
                    string pclFilePath = ConvertPdfToPcl(localFilePath, pclConverterPath);
                    if (pclFilePath == null) {
                        return "PCL Conversion Failed";
                    }
                    printFileName = pclFilePath;
                } 

                string jobId = Guid.NewGuid().ToString();

                LprClient lprClient = new LprClient(lpdServerIpName, printerName, printUserName, printUserDomain);
                lprClient.LPR(printFileName, true);
                Thread.Sleep(500);
                lprClient.LPQ(false);
                Thread.Sleep(500);
                lprClient.LPRM(jobId);
                Thread.Sleep(500);
                if (lprClient.ErrorMsg != null && lprClient.ErrorMsg.Trim().Length > 0) {
                    throw new Exception("LPR Client error:" + lprClient.ErrorMsg);
                }

                /*
                //WindowsImpersonationContext wic = null;
                try {
                    //if (printUserName != null && printUserName.Trim().Length > 0) {
                    //    wic = new UserClass().ImpersonateUser(
                    //        printUserName,
                    //        printUserDomain,
                    //        printUserPassword);
                    //}
                                                                                
                    ProcessStartInfo siLpr = new ProcessStartInfo();
                    siLpr.Arguments = " -S " + "\"" + lpdServerIpName + "\"" + " -P " + "\"" + printerName + "\"" + " " + "\"" + pclFilePath + "\"";
                    siLpr.FileName = m_LprExePath;
                    siLpr.UseShellExecute = false;
                    siLpr.WorkingDirectory = new FileInfo(m_LprExePath).DirectoryName;
                    siLpr.CreateNoWindow = true;
                    if (printUserName != null && printUserName.Trim().Length > 0) {
                        siLpr.UserName = printUserName;
                        siLpr.Password = GetPassword(printUserPassword);
                        siLpr.Domain = printUserDomain;
                    }

                    Process pLpr = Process.Start(siLpr);
                    while (!pLpr.HasExited) {
                    }

                    if (pLpr.ExitCode != 0) {
                        return "LPR Print failed with code:" + pLpr.ExitCode.ToString();
                    }
                } catch (Exception ex) {
                    return ex.ToString();
                } finally {
                    //if (wic != null) {
                    //    wic.Undo();
                    //} 
                }*/

                //File.Delete(pclFilePath);
                                             

                // acrord32.exe /t filePath "RICOH_Terminal"
                                
                return null;
            } catch (Exception ex) {
                return ex.ToString();
                
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

        public string ConvertPdfToPcl(string filePath, string pdf2PclExeFilePath) {
            try {
                string sourcePdfFile = filePath;
                string destinPclFile = sourcePdfFile.ToLower().Replace(".pdf", ".pcl");

                ProcessStartInfo siPdf2Pcl = new ProcessStartInfo();
                //siPdf2Pcl.Arguments = "-sDEVICE=pxlcolor -dQuiet -dBATCH -dNOPAUSE -dNOPROMPT -sOutputFile=" + destinPclFile + " " + filePath;
                siPdf2Pcl.Arguments = "\"" + sourcePdfFile + "\"" + " " + "\"" + destinPclFile + "\"";
                siPdf2Pcl.FileName = pdf2PclExeFilePath;
                siPdf2Pcl.UseShellExecute = false;

                Process pLpr = Process.Start(siPdf2Pcl);
                while (!pLpr.HasExited) {
                }

                if (pLpr.ExitCode != 0) {
                    return null;    
                }
                                
                return destinPclFile;

                /*
                 * C:\Program Files\gs\gs9.02\bin>gswin32c -dNODISPLAY -dBATCH -sPSFile=c:\temp\fil
e.ps C:\Temp\PdfArchive\lsoft_27May_081741.pdf
                 * 
                 * 
                 * C:\Program Files\gs\gs9.02\bin>gswin32c -sDEVICE=ps2write -dQuiet -dBATCH -dNOPA
USE -dNOPROMPT -sOutputFile=c:\temp\out.ps  C:\Temp\PdfArchive\lsoft_27May_08174
1.pdf
                 * 
                 * 
                 * */
            } catch (Exception ex) {
                throw ex;

            }    
        }


        //public string MoveFile(string sourceFile, string destinFolder) {
        //    try {
        //        FileInfo fiSource = new FileInfo(sourceFile);
        //        string destinFileName = Path.Combine(destinFolder, fiSource.Name);

        //        File.Move(sourceFile, destinFileName);

        //        return null;
        //    } catch (Exception ex) {
        //        return ex.ToString();
        //    }
        //}
        #endregion
    }
}
