using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using LPD_DEMO;

namespace LprTest {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main1() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
