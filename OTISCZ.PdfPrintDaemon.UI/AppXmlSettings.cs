using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

namespace OTISCZ.LprPrintDaemon.UI {
    public class AppXmlSettings {
        #region Constants
        public const string LPD_SERVER = "LpdServerIpName";
        public const string PRINTER_NAME = "PrinterName";
        public const string SOURCE_FOLDER = "SourcePdfFolder";
        private const string ARCHIVE_FOLDER = "ArchivePdfFolder";
        private const string GHOST_SCRIPT_PATH = "GhostscriptPath";
        private const string ATTRIBUTE_NAME = "name";
        #endregion

        #region Properties
        private string m_app_config_file {
            get {
                Assembly pbAssembly = Assembly.GetEntryAssembly();
                if (pbAssembly == null) return null;
                if (Assembly.GetEntryAssembly().Location == null) return null;

                string configFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string configFileFullPath = new DirectoryInfo(configFilePath).FullName;
                configFilePath = Path.Combine(configFileFullPath, "OTISCZ.LprPrintDaemon.UI.exe.config");

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

        private string m_GhostSriptPath = "";
        public string GhostSriptPath {
            get {
                return m_GhostSriptPath;
            }
            set {
                m_GhostSriptPath = value;
                SetUserSettings(AppXmlSettings.GHOST_SCRIPT_PATH, value.ToString());
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

            XmlNode xmlArchiveghostscriptExe = GetDaemonNode(xmlDoc, GHOST_SCRIPT_PATH);
            if (xmlArchiveghostscriptExe != null) {
                XmlNode xmlValue = xmlArchiveghostscriptExe.FirstChild;
                m_GhostSriptPath = xmlValue.InnerText;
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
                throw new Exception("Baan Invoice Print XML Setup file " + m_app_config_file + " was not found");
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
                        if (appNode.Name == "OTISCZ.LprPrintDaemon.Properties.Settings") {
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
