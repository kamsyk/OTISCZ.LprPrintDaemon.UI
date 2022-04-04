//
// name: 	LPR_DEMO
// author: 	rob tillaart
// version:	1.02
//
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using LPD;

namespace LPD_DEMO {
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form {
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox QueueName;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox HostName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox textBox1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1() {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.HostName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.QueueName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(8, 128);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(560, 208);
            this.textBox1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 12;
            this.label4.Text = "UserName";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(416, 96);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(40, 21);
            this.textBox2.TabIndex = 4;
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(128, 56);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(240, 21);
            this.UserName.TabIndex = 11;
            this.UserName.Text = "syka";
            // 
            // HostName
            // 
            this.HostName.Location = new System.Drawing.Point(128, 8);
            this.HostName.Name = "HostName";
            this.HostName.Size = new System.Drawing.Size(240, 21);
            this.HostName.TabIndex = 7;
            this.HostName.Text = "10.68.33.53";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "LPR";
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "HostName";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "QueueName";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(464, 88);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 32);
            this.button4.TabIndex = 6;
            this.button4.Text = "Restart";
            this.button4.Click += new System.EventHandler(this.Button4Click);
            // 
            // QueueName
            // 
            this.QueueName.Location = new System.Drawing.Point(128, 32);
            this.QueueName.Name = "QueueName";
            this.QueueName.Size = new System.Drawing.Size(240, 21);
            this.QueueName.TabIndex = 8;
            this.QueueName.Text = "RICOH_Terminal";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(376, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "jobID";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(128, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "LPQ";
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(256, 88);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 32);
            this.button3.TabIndex = 3;
            this.button3.Text = "LPRM";
            this.button3.Click += new System.EventHandler(this.Button3Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(578, 344);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.QueueName);
            this.Controls.Add(this.HostName);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Simple LPR example";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label label4;
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.Run(new Form1());
        }

        // LPR
        void Button1Click(object sender, System.EventArgs e) {
            Printer printer1 = null;
            
            if ((HostName.Text == "") || (QueueName.Text == "") || (UserName.Text == "")) {
                MessageBox.Show("Please fill in host, queue and username");
                return;
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                textBox1.Text = DateTime.Now.ToString() + " busy";
                printer1 = new Printer(HostName.Text, QueueName.Text, UserName.Text);
                printer1.LogFile = @"c:\lpr.log";

                string fname = openFileDialog1.FileName;
                if (fname.EndsWith(".ps") || fname.EndsWith(".pdf") || fname.EndsWith(".pcl")) {
                    printer1.LPR(fname, false);
                    textBox1.Text = DateTime.Now.ToString();
                    textBox1.Text += printer1.ErrorMsg;
                } else {
                    // error message
                    textBox1.Text = "File does not end with .ps or .pdf.";
                }
            }

            //LPQ
            if ((HostName.Text == "") || (QueueName.Text == "") || (UserName.Text == "")) {
                MessageBox.Show("Please fill in host, queue and username");
                return;
            }
            textBox1.Text = DateTime.Now.ToString() + " busy";
            //Printer printer1 = new Printer(HostName.Text, QueueName.Text, UserName.Text);
            textBox1.Text = DateTime.Now.ToString();
            textBox1.Text += printer1.LPQ(false);

            //LPRM
            if ((HostName.Text == "") || (QueueName.Text == "") || (UserName.Text == "")) {
                MessageBox.Show("Please fill in host, queue and username");
                return;
            }
            textBox1.Text = DateTime.Now.ToString() + " busy";
            //Printer printer1 = new Printer(HostName.Text, QueueName.Text, UserName.Text);
            printer1.LPRM(textBox2.Text);
            textBox1.Text = DateTime.Now.ToString() + " done";
        }

        // LPQ        
        void Button2Click(object sender, System.EventArgs e) {
            if ((HostName.Text == "") || (QueueName.Text == "") || (UserName.Text == "")) {
                MessageBox.Show("Please fill in host, queue and username");
                return;
            }
            textBox1.Text = DateTime.Now.ToString() + " busy";
            Printer printer1 = new Printer(HostName.Text, QueueName.Text, UserName.Text);
            textBox1.Text = DateTime.Now.ToString();
            textBox1.Text += printer1.LPQ(false);
        }

        // LPRM
        void Button3Click(object sender, System.EventArgs e) {
            if ((HostName.Text == "") || (QueueName.Text == "") || (UserName.Text == "")) {
                MessageBox.Show("Please fill in host, queue and username");
                return;
            }
            textBox1.Text = DateTime.Now.ToString() + " busy";
            Printer printer1 = new Printer(HostName.Text, QueueName.Text, UserName.Text);
            printer1.LPRM(textBox2.Text);
            textBox1.Text = DateTime.Now.ToString() + " done";
        }

        // Restart
        void Button4Click(object sender, System.EventArgs e) {
            if ((HostName.Text == "") || (QueueName.Text == "") || (UserName.Text == "")) {
                MessageBox.Show("Please fill in host, queue and username");
                return;
            }
            textBox1.Text = DateTime.Now.ToString() + " busy";
            Printer printer1 = new Printer(HostName.Text, QueueName.Text, UserName.Text);
            printer1.Restart();
            textBox1.Text = DateTime.Now.ToString() + printer1.ErrorMsg;
        }
    }
}
