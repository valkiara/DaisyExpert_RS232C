using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DERS232C;
using System.Threading;
using System.Globalization;

namespace DER232CTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            DaisyExpert xPrinter = new DaisyExpert("COM3");
            //Sales Sale = new Sales("COM3");

            xPrinter.Sales.StartOfNonFiscalReceipt();
            xPrinter.Sales.PrintNonFiscalText("glea");
            var aa = xPrinter.Sales.EndOfNonFiscalReceipt();

            var aaa = xPrinter.Information.GetDateAndTimeInformation();
            //xPrinter.GetDateAndTimeInformation();

        }
    }
}
