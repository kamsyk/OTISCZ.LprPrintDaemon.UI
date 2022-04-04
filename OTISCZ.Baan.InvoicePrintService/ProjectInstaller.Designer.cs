namespace OTISCZ.Baan.InvoicePrintService {
    partial class ProjectInstaller {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.BipProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BaanInvoicePrintInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BipProcessInstaller
            // 
            this.BipProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BipProcessInstaller.Password = null;
            this.BipProcessInstaller.Username = null;
            // 
            // BaanInvoicePrintInstaller
            // 
            this.BaanInvoicePrintInstaller.Description = "Send Baan PDF invoices to Printer";
            this.BaanInvoicePrintInstaller.DisplayName = "Baan Invoice Print";
            this.BaanInvoicePrintInstaller.ServiceName = "BaanInvoicePrint";
            this.BaanInvoicePrintInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BipProcessInstaller,
            this.BaanInvoicePrintInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BipProcessInstaller;
        private System.ServiceProcess.ServiceInstaller BaanInvoicePrintInstaller;
    }
}