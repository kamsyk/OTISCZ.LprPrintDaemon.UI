using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

using OTISCZ.Security;

namespace OTISCZ.LprPrintDaemon {
    public class AppXmlSettings {
        #region Constants
        public const string LPD_SERVER = "LpdServerIpName";
        public const string PRINTER_NAME = "PrinterName";
        public const string SOURCE_FOLDER = "SourcePdfFolder";
        private const string ARCHIVE_FOLDER = "ArchivePdfFolder";
        private const string ALLOW_MOVE_TO_ARCHIVE = "AllowMoveToArchive";
        //private const string PCL_CONVERTER_PATH = "PclConverterPath";
        private const string FOXIT_PATH = "FoxitPath";
        private const string ALLOW_PCL_CONVERTION = "AllowPclConvertion";
        private const string PRINT_USER_NAME = "PrintUserName";
        private const string PRINT_USER_PASSWORD = "PrintUserPassword";
        private const string PRINT_USER_DOMAIN = "PrintUserDomain";
        private const string FOLDER_USER_NAME = "FolderUserName";
        private const string FOLDER_USER_PASSWORD = "FolderUserPassword";
        private const string FOLDER_USER_DOMAIN = "FolderUserDomain";
        private const string ALLOW_PRINT_USER_IDENTITY = "AllowPrintUserIdentity";
        private const string ALLOW_FOLDER_USER_IDENTITY = "AllowFolderUserIdentity";
        private const string CHECK_PERIOD_MINUTES = "CheckPeriodInMinutes";
        private const string ADMIN_MAILS = "AdminMails";
        //private const string IS_CHECKING_FOLDER = "IsCheckingFolder";
        private const string ATTRIBUTE_NAME = "name";
        private const string DES_PASSWORD = "r44D&.dsS5_qiweu&(lweqdweqo";
        #endregion

        #region Properties
        private string m_app_config_file {
            get {
                Assembly pbAssembly = Assembly.GetEntryAssembly();
                if (pbAssembly == null) return null;
                if (Assembly.GetEntryAssembly().Location == null) return null;

                string configFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string configFileFullPath = new DirectoryInfo(configFilePath).FullName;
                configFilePath = Path.Combine(configFileFullPath, "OTISCZ.Baan.InvoicePrintService.exe.config");

                return configFilePath;
            }
        }



        private string m_LpdServerIpName = "";
        public string LpdServerIpName {
            get {
                return m_LpdServerIpName;
            }
            set {
                m_LpdServerIpName = value;
                SetUserSettings(AppXmlSettings.LPD_SERVER, value.ToString());
            }
        }

        private string m_PrinterName = "";
        public string PrinterName {
            get {
                return m_PrinterName;
            }
            set {
                m_PrinterName = value;
                SetUserSettings(AppXmlSettings.PRINTER_NAME, value.ToString());
            }
        }

        private string m_SourcePdfFolderName = "";
        public string SourcePdfFolderName {
            get {
                return m_SourcePdfFolderName;
            }
            set {
                m_SourcePdfFolderName = value;
                SetUserSettings(AppXmlSettings.SOURCE_FOLDER, value.ToString());
            }
        }

        private string m_ArchivePdfFolderName = "";
        public string ArchivePdfFolderName {
            get {
                return m_ArchivePdfFolderName;
            }
            set {
                m_ArchivePdfFolderName = value;
                SetUserSettings(AppXmlSettings.ARCHIVE_FOLDER, value.ToString());
            }
        }

        private bool m_IsAllowMoveToArchive = false;
        public bool IsAllowMoveToArchive {
            get {
                return m_IsAllowMoveToArchive;
            }
            set {
                if (value) {
                    SetUserSettings(AppXmlSettings.ALLOW_MOVE_TO_ARCHIVE, "1");
                } else {
                    SetUserSettings(AppXmlSettings.ALLOW_MOVE_TO_ARCHIVE, "0");
                }
            }
        }

        private bool m_IsAllowPclConvertion = false;
        public bool IsAllowPclConvertion {
            get {
                return m_IsAllowPclConvertion;
            }
            set {
                if (value) {
                    SetUserSettings(AppXmlSettings.ALLOW_PCL_CONVERTION, "1");
                } else {
                    SetUserSettings(AppXmlSettings.ALLOW_PCL_CONVERTION, "0");
                }
            }
        }

        //private string m_PclConverterPath = "";
        //public string PclConverterPath {
        //    get {
        //        return m_PclConverterPath;
        //    }
        //    set {
        //        m_PclConverterPath = value;
        //        SetUserSettings(AppXmlSettings.PCL_CONVERTER_PATH, value.ToString());
        //    }
        //}

        private string m_FoxitPath = "";
        public string FoxitPath {
            get {
                return m_FoxitPath;
            }
            set {
                m_FoxitPath = value;
                SetUserSettings(AppXmlSettings.FOXIT_PATH, value.ToString());
            }
        }

        private string m_PrintUserName = "";
        public string PrintUserName {
            get {
                return m_PrintUserName;
            }
            set {
                m_PrintUserName = value;
                SetUserSettings(AppXmlSettings.PRINT_USER_NAME, value.ToString());
            }
        }

        private string m_PrintUserPassword = "";
        public string PrintUserPassword {
            get {
                if (m_PrintUserPassword.Length == 0) {
                    return "";
                }
                return Des.Decrypt(m_PrintUserPassword, DES_PASSWORD);
                //return m_PrintUserPassword;
            }
            set {
                m_PrintUserPassword = value;
                string encryptPwd = Des.Encrypt(value.ToString(), DES_PASSWORD);
                SetUserSettings(AppXmlSettings.PRINT_USER_PASSWORD, encryptPwd);
            }
        }

        private string m_PrintUserDomain = "";
        public string PrintUserDomain {
            get {
                return m_PrintUserDomain;
            }
            set {
                m_PrintUserDomain = value;
                SetUserSettings(AppXmlSettings.PRINT_USER_DOMAIN, value.ToString());
            }
        }

        private bool m_IsDefaultIdentity = false;
        public bool IsDefaultIdentity {
            get {
                return m_IsDefaultIdentity;
            }
            set {
                m_IsDefaultIdentity = value;
                if (value) {
                    SetUserSettings(AppXmlSettings.ALLOW_PRINT_USER_IDENTITY, "0");
                } else {
                    SetUserSettings(AppXmlSettings.ALLOW_PRINT_USER_IDENTITY, "1");    
                }
            }
        }

        private string m_FolderUserName = "";
        public string FolderUserName {
            get {
                return m_FolderUserName;
            }
            set {
                m_FolderUserName = value;
                SetUserSettings(AppXmlSettings.FOLDER_USER_NAME, value.ToString());
            }
        }

        private string m_FolderUserPassword = "";
        public string FolderUserPassword {
            get {
                if (m_FolderUserPassword.Length == 0) {
                    return "";
                }
                return Des.Decrypt(m_FolderUserPassword, DES_PASSWORD);
                //return m_PrintUserPassword;
            }
            set {
                m_FolderUserPassword = value;
                string encryptPwd = Des.Encrypt(value.ToString(), DES_PASSWORD);
                SetUserSettings(AppXmlSettings.FOLDER_USER_PASSWORD, encryptPwd);
            }
        }

        private string m_FolderUserDomain = "";
        public string FolderUserDomain {
            get {
                return m_FolderUserDomain;
            }
            set {
                m_FolderUserDomain = value;
                SetUserSettings(AppXmlSettings.FOLDER_USER_DOMAIN, value.ToString());
            }
        }

        private bool m_IsFolderDefaultIdentity = false;
        public bool IsFolderDefaultIdentity {
            get {
                return m_IsFolderDefaultIdentity;
            }
            set {
                m_IsFolderDefaultIdentity = value;
                if (value) {
                    SetUserSettings(AppXmlSettings.ALLOW_FOLDER_USER_IDENTITY, "0");
                } else {
                    SetUserSettings(AppXmlSettings.ALLOW_FOLDER_USER_IDENTITY, "1");
                }
            }
        }

        //private bool m_IsCheckingFolder = false;
        //public bool IsCheckingFolder {
        //    get {
        //        return m_IsCheckingFolder;
        //    }
        //    set {
        //        m_IsCheckingFolder = value;
        //        if (value) {
        //            SetUserSettings(AppXmlSettings.IS_CHECKING_FOLDER, "1");
        //        } else {
        //            SetUserSettings(AppXmlSettings.IS_CHECKING_FOLDER, "0");
        //        }
        //    }
        //}

        private int m_CheckPeriodInMinutes = 10;
        public int CheckPeriodInMinutes {
            get {
                return m_CheckPeriodInMinutes;
            }
            set {
                m_CheckPeriodInMinutes = value;
                SetUserSettings(AppXmlSettings.CHECK_PERIOD_MINUTES, m_CheckPeriodInMinutes.ToString());
                
            }
        }

        private string m_AdminMails = "";
        public string AdminMails {
            get {
                return m_AdminMails;
            }
            set {
                m_AdminMails = value;
                SetUserSettings(AppXmlSettings.ADMIN_MAILS, value);
            }
        }
        #endregion

        #region Constructor
        public AppXmlSettings() {
            XmlDocument xmlDoc = GetConfigXmlDoc();
            if (xmlDoc == null) return;

            XmlNode xmlServer = GetDaemonNode(xmlDoc, LPD_SERVER);
            if (xmlServer != null) {
                XmlNode xmlValue = xmlServer.FirstChild;
                m_LpdServerIpName = xmlValue.InnerText;
            }

            XmlNode xmlPrinterName = GetDaemonNode(xmlDoc, PRINTER_NAME);
            if (xmlPrinterName != null) {
                XmlNode xmlValue = xmlPrinterName.FirstChild;
                m_PrinterName = xmlValue.InnerText;
            }

            XmlNode xmlSourcePdfFolderName = GetDaemonNode(xmlDoc, SOURCE_FOLDER);
            if (xmlSourcePdfFolderName != null) {
                XmlNode xmlValue = xmlSourcePdfFolderName.FirstChild;
                m_SourcePdfFolderName = xmlValue.InnerText;
            }

            XmlNode xmlArchivePdfFolderName = GetDaemonNode(xmlDoc, ARCHIVE_FOLDER);
            if (xmlArchivePdfFolderName != null) {
                XmlNode xmlValue = xmlArchivePdfFolderName.FirstChild;
                m_ArchivePdfFolderName = xmlValue.InnerText;
            }

            //XmlNode xmlPclConverterPath = GetDaemonNode(xmlDoc, PCL_CONVERTER_PATH);
            //if (xmlPclConverterPath != null) {
            //    XmlNode xmlValue = xmlPclConverterPath.FirstChild;
            //    m_PclConverterPath = xmlValue.InnerText;
            //}

            XmlNode xmlFoxitPath = GetDaemonNode(xmlDoc, FOXIT_PATH);
            if (xmlFoxitPath != null) {
                XmlNode xmlValue = xmlFoxitPath.FirstChild;
                m_FoxitPath = xmlValue.InnerText;
            }

            XmlNode xmlFolderUserName = GetDaemonNode(xmlDoc, FOLDER_USER_NAME);
            if (xmlFolderUserName != null) {
                XmlNode xmlValue = xmlFolderUserName.FirstChild;
                m_FolderUserName = xmlValue.InnerText;
            }

            XmlNode xmlFolderUserPassword = GetDaemonNode(xmlDoc, FOLDER_USER_PASSWORD);
            if (xmlFolderUserPassword != null) {
                XmlNode xmlValue = xmlFolderUserPassword.FirstChild;
                m_FolderUserPassword = xmlValue.InnerText;
            }

            XmlNode xmlFolderUserDomain = GetDaemonNode(xmlDoc, FOLDER_USER_DOMAIN);
            if (xmlFolderUserDomain != null) {
                XmlNode xmlValue = xmlFolderUserDomain.FirstChild;
                m_FolderUserDomain = xmlValue.InnerText;
            }

            XmlNode xmlDefaultUserFolderIdentity = GetDaemonNode(xmlDoc, ALLOW_FOLDER_USER_IDENTITY);
            if (xmlDefaultUserFolderIdentity != null) {
                XmlNode xmlValue = xmlDefaultUserFolderIdentity.FirstChild;
                m_IsFolderDefaultIdentity = (xmlValue.InnerText != "1");
            }

            XmlNode xmlPrintUserName = GetDaemonNode(xmlDoc, PRINT_USER_NAME);
            if (xmlPrintUserName != null) {
                XmlNode xmlValue = xmlPrintUserName.FirstChild;
                m_PrintUserName = xmlValue.InnerText;
            }

            XmlNode xmlPrintUserPassword = GetDaemonNode(xmlDoc, PRINT_USER_PASSWORD);
            if (xmlPrintUserPassword != null) {
                XmlNode xmlValue = xmlPrintUserPassword.FirstChild;
                m_PrintUserPassword = xmlValue.InnerText;
            }

            XmlNode xmlPrintUserDomain = GetDaemonNode(xmlDoc, PRINT_USER_DOMAIN);
            if (xmlPrintUserDomain != null) {
                XmlNode xmlValue = xmlPrintUserDomain.FirstChild;
                m_PrintUserDomain = xmlValue.InnerText;
            }

            XmlNode xmlDefaultUserIdentity = GetDaemonNode(xmlDoc, ALLOW_PRINT_USER_IDENTITY);
            if (xmlDefaultUserIdentity != null) {
                XmlNode xmlValue = xmlDefaultUserIdentity.FirstChild;
                m_IsDefaultIdentity = (xmlValue.InnerText != "1");
            }

            //XmlNode xmlIsCheckingFolder = GetDaemonNode(xmlDoc, IS_CHECKING_FOLDER);
            //if (xmlIsCheckingFolder != null) {
            //    XmlNode xmlValue = xmlIsCheckingFolder.FirstChild;
            //    m_IsCheckingFolder = (xmlValue.InnerText == "1");
            //}

            XmlNode xmlCheckFolderPeriod = GetDaemonNode(xmlDoc, CHECK_PERIOD_MINUTES);
            if (xmlCheckFolderPeriod != null) {
                XmlNode xmlValue = xmlCheckFolderPeriod.FirstChild;
                if (xmlValue.InnerText.Trim().Length > 0) {
                    m_CheckPeriodInMinutes = Convert.ToInt32(xmlValue.InnerText);
                }
            }

            XmlNode xmlAdminMails = GetDaemonNode(xmlDoc, ADMIN_MAILS);
            if (xmlAdminMails != null) {
                XmlNode xmlValue = xmlAdminMails.FirstChild;
                m_AdminMails = xmlValue.InnerText;
            }

            XmlNode xmlAllowPclConvertion = GetDaemonNode(xmlDoc, ALLOW_PCL_CONVERTION);
            if (xmlAllowPclConvertion != null) {
                XmlNode xmlValue = xmlAllowPclConvertion.FirstChild;
                m_IsAllowPclConvertion = (xmlValue.InnerText == "1");
            }

            XmlNode xmlAllowPMoveToArchive = GetDaemonNode(xmlDoc, ALLOW_MOVE_TO_ARCHIVE);
            if (xmlAllowPMoveToArchive != null) {
                XmlNode xmlValue = xmlAllowPMoveToArchive.FirstChild;
                m_IsAllowMoveToArchive = (xmlValue.InnerText == "1");
            }
        }
        #endregion

        #region Methods
        private void SetUserSettings(string setName, string strValue) {
            XmlDocument xmlDoc = GetConfigXmlDoc();
            XmlNode xmlAppNode = GetDaemonNode(xmlDoc, setName);
            if (xmlAppNode != null) {
                XmlNode xmlValue = xmlAppNode.FirstChild;
                xmlValue.InnerText = strValue;
            }
            xmlDoc.Save(m_app_config_file);
        }

        private XmlDocument GetConfigXmlDoc() {
            if (m_app_config_file == null) return null;
            if (!File.Exists(m_app_config_file)) {
                throw new Exception("Baan Print Service XML Setup file " + m_app_config_file + " was not found");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_app_config_file);

            return xmlDoc;

        }

        public XmlNode GetDaemonAppRootNode(XmlDocument xmlDoc) {
            XmlElement rootElement = xmlDoc.DocumentElement;
            if (rootElement == null) return null;
            XmlNodeList fileNodes = rootElement.ChildNodes;
            for (int i = 0; i < fileNodes.Count; i++) {
                XmlNode fileNode = fileNodes[i];
                if (fileNode.Name == "applicationSettings") {
                    XmlNodeList appNodes = fileNode.ChildNodes;
                    for (int j = 0; j < appNodes.Count; j++) {
                        XmlNode appNode = appNodes[j];
                        if (appNode.Name == "OTISCZ.Baan.InvoicePrintService.Properties.Settings") {
                            return appNode;
                        }
                    }
                }
            }

            return null;
        }

        private XmlNode GetDaemonNode(XmlDocument xmlDoc, string nodeName) {
            XmlNode phoneAppNode = GetDaemonAppRootNode(xmlDoc);
            if (phoneAppNode == null) return null;

            XmlNodeList phoneNodes = phoneAppNode.ChildNodes;
            for (int i = 0; i < phoneNodes.Count; i++) {
                for (int attIndex = 0; attIndex < phoneNodes[i].Attributes.Count; attIndex++) {
                    if (phoneNodes[i].Attributes[attIndex].Name == ATTRIBUTE_NAME) {
                        if (phoneNodes[i].Attributes[attIndex].Value == nodeName) {
                            return phoneNodes[i];
                        }
                    }
                }

            }

            return null;
        }
        #endregion
    }
}
