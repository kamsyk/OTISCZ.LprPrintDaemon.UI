using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace OTISCZ.Baan.InvoicePrintService {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
#if DEBUG
            new PdfPrint().InvoicePrint();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new BaanInvoicePrint() 
			};
            
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
