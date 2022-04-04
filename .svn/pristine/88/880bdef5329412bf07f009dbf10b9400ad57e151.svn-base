using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

using OTISCZ.LprPrintDaemon;

namespace OTISCZ.Baan.InvoicePrintService {
    public partial class BaanInvoicePrint : ServiceBase {
        #region Properties
        private Timer m_Timer;
        private AppXmlSettings m_AppXmlSettings;
        #endregion

        #region Constructor
        public BaanInvoicePrint() {
            InitializeComponent();
            m_AppXmlSettings = new AppXmlSettings();
        }
        #endregion

        #region Methods
        private void SetTimer() {
            TimerCallback tmrCallBack = new TimerCallback(m_Timer_TimerCallback);
            m_Timer = new Timer(tmrCallBack);
            m_Timer.Change(new TimeSpan(0, 0, 1), new TimeSpan(0, m_AppXmlSettings.CheckPeriodInMinutes, 0));

        }

        private void m_Timer_TimerCallback(object state) {
            m_Timer.Change(Timeout.Infinite, Timeout.Infinite);
            new PdfPrint().InvoicePrint();
            m_Timer.Change(new TimeSpan(0, m_AppXmlSettings.CheckPeriodInMinutes, 0), new TimeSpan(0, m_AppXmlSettings.CheckPeriodInMinutes, 0));
        }

        
        #endregion

        #region Service Events
        protected override void OnStart(string[] args) {
            PdfPrint.IsCheckingFolder = false;
            SetTimer();
        }

        protected override void OnStop() {
        }
        #endregion
    }
}
