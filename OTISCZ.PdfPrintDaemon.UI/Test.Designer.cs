namespace OTISCZ.LprPrintDaemon.UI {
    partial class Test {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerId = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPrinterName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbPdfSourceFolder = new System.Windows.Forms.GroupBox();
            this.btnSourceFolderLookup = new System.Windows.Forms.Button();
            this.txtSourceFolder = new System.Windows.Forms.TextBox();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.txtPrintInfo = new System.Windows.Forms.TextBox();
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnArchiveFolderLookup = new System.Windows.Forms.Button();
            this.txtArchiveFolder = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.errPrint = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGswin32 = new System.Windows.Forms.Button();
            this.txtGhostscript = new System.Windows.Forms.TextBox();
            this.ofdGswin32c = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.gbPdfSourceFolder.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errPrint)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "LPD host Server IP address (name):";
            // 
            // txtServerId
            // 
            this.txtServerId.Location = new System.Drawing.Point(188, 19);
            this.txtServerId.Name = "txtServerId";
            this.txtServerId.Size = new System.Drawing.Size(289, 20);
            this.txtServerId.TabIndex = 1;
            this.txtServerId.TextChanged += new System.EventHandler(this.txtServerId_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPrinterName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtServerId);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 79);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LPR parameters";
            // 
            // txtPrinterName
            // 
            this.txtPrinterName.Location = new System.Drawing.Point(188, 46);
            this.txtPrinterName.Name = "txtPrinterName";
            this.txtPrinterName.Size = new System.Drawing.Size(289, 20);
            this.txtPrinterName.TabIndex = 3;
            this.txtPrinterName.TextChanged += new System.EventHandler(this.txtPrinterName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Printer Name:";
            // 
            // gbPdfSourceFolder
            // 
            this.gbPdfSourceFolder.Controls.Add(this.btnSourceFolderLookup);
            this.gbPdfSourceFolder.Controls.Add(this.txtSourceFolder);
            this.gbPdfSourceFolder.Location = new System.Drawing.Point(1, 142);
            this.gbPdfSourceFolder.Name = "gbPdfSourceFolder";
            this.gbPdfSourceFolder.Size = new System.Drawing.Size(547, 51);
            this.gbPdfSourceFolder.TabIndex = 3;
            this.gbPdfSourceFolder.TabStop = false;
            this.gbPdfSourceFolder.Text = "PDF source folder";
            // 
            // btnSourceFolderLookup
            // 
            this.btnSourceFolderLookup.Location = new System.Drawing.Point(486, 17);
            this.btnSourceFolderLookup.Name = "btnSourceFolderLookup";
            this.btnSourceFolderLookup.Size = new System.Drawing.Size(32, 23);
            this.btnSourceFolderLookup.TabIndex = 1;
            this.btnSourceFolderLookup.Text = "...";
            this.btnSourceFolderLookup.UseVisualStyleBackColor = true;
            this.btnSourceFolderLookup.Click += new System.EventHandler(this.btnSourceFolderLookup_Click);
            // 
            // txtSourceFolder
            // 
            this.txtSourceFolder.Location = new System.Drawing.Point(13, 20);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.ReadOnly = true;
            this.txtSourceFolder.Size = new System.Drawing.Size(464, 20);
            this.txtSourceFolder.TabIndex = 0;
            this.txtSourceFolder.TextChanged += new System.EventHandler(this.txtSourceFolder_TextChanged);
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.txtPrintInfo);
            this.gbLog.Location = new System.Drawing.Point(1, 296);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(547, 218);
            this.gbLog.TabIndex = 4;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "PrintLog";
            // 
            // txtPrintInfo
            // 
            this.txtPrintInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrintInfo.Location = new System.Drawing.Point(3, 16);
            this.txtPrintInfo.Multiline = true;
            this.txtPrintInfo.Name = "txtPrintInfo";
            this.txtPrintInfo.ReadOnly = true;
            this.txtPrintInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPrintInfo.Size = new System.Drawing.Size(541, 199);
            this.txtPrintInfo.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnArchiveFolderLookup);
            this.groupBox2.Controls.Add(this.txtArchiveFolder);
            this.groupBox2.Location = new System.Drawing.Point(4, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(547, 51);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PDF archive folder";
            // 
            // btnArchiveFolderLookup
            // 
            this.btnArchiveFolderLookup.Location = new System.Drawing.Point(486, 19);
            this.btnArchiveFolderLookup.Name = "btnArchiveFolderLookup";
            this.btnArchiveFolderLookup.Size = new System.Drawing.Size(32, 23);
            this.btnArchiveFolderLookup.TabIndex = 1;
            this.btnArchiveFolderLookup.Text = "...";
            this.btnArchiveFolderLookup.UseVisualStyleBackColor = true;
            this.btnArchiveFolderLookup.Click += new System.EventHandler(this.btnArchiveFolderLookup_Click);
            // 
            // txtArchiveFolder
            // 
            this.txtArchiveFolder.Location = new System.Drawing.Point(13, 20);
            this.txtArchiveFolder.Name = "txtArchiveFolder";
            this.txtArchiveFolder.ReadOnly = true;
            this.txtArchiveFolder.Size = new System.Drawing.Size(464, 20);
            this.txtArchiveFolder.TabIndex = 0;
            this.txtArchiveFolder.TextChanged += new System.EventHandler(this.txtArchiveFolder_TextChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrint.Location = new System.Drawing.Point(97, 256);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(138, 34);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnClose.Location = new System.Drawing.Point(315, 256);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 34);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errPrint
            // 
            this.errPrint.ContainerControl = this;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGswin32);
            this.groupBox3.Controls.Add(this.txtGhostscript);
            this.groupBox3.Location = new System.Drawing.Point(1, 85);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(547, 51);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ghostscript path (gswin32c.exe)";
            // 
            // btnGswin32
            // 
            this.btnGswin32.Location = new System.Drawing.Point(486, 17);
            this.btnGswin32.Name = "btnGswin32";
            this.btnGswin32.Size = new System.Drawing.Size(32, 23);
            this.btnGswin32.TabIndex = 1;
            this.btnGswin32.Text = "...";
            this.btnGswin32.UseVisualStyleBackColor = true;
            this.btnGswin32.Click += new System.EventHandler(this.btnGswin32_Click);
            // 
            // txtGhostscript
            // 
            this.txtGhostscript.Location = new System.Drawing.Point(13, 20);
            this.txtGhostscript.Name = "txtGhostscript";
            this.txtGhostscript.ReadOnly = true;
            this.txtGhostscript.Size = new System.Drawing.Size(464, 20);
            this.txtGhostscript.TabIndex = 0;
            this.txtGhostscript.TextChanged += new System.EventHandler(this.txtGhostscript_TextChanged);
            // 
            // ofdGswin32c
            // 
            this.ofdGswin32c.FileName = "gswin32c.exe";
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 517);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.gbPdfSourceFolder);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Test";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LPR Daemon Print Test";
            this.Load += new System.EventHandler(this.Test_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbPdfSourceFolder.ResumeLayout(false);
            this.gbPdfSourceFolder.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errPrint)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPrinterName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbPdfSourceFolder;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.Button btnSourceFolderLookup;
        private System.Windows.Forms.TextBox txtSourceFolder;
        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnArchiveFolderLookup;
        private System.Windows.Forms.TextBox txtArchiveFolder;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errPrint;
        private System.Windows.Forms.TextBox txtPrintInfo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGswin32;
        private System.Windows.Forms.TextBox txtGhostscript;
        private System.Windows.Forms.OpenFileDialog ofdGswin32c;
    }
}

