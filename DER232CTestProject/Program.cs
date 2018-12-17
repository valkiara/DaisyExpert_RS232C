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
            DEClass xPrinter = new DEClass("COM3");
            //xPrinter.SendCommand(55, "");
            //xPrinter.DisplayDateAndTime();
            //xPrinter.StartOfNonFiscalReceipt();
            //var x = xPrinter.PaperCut("1");

            //var x = xPrinter.GetTaxRates(DateTime.Now.AddYears(-2), DateTime.Now);
            var x = xPrinter.DiagnosticInformation();
            //var x = BYTEN.SetFlags(176, 3);
            //xPrinter.PrintNonFiscalText("საპ ბიზნეს 1");
            //xPrinter.FDStatus();
            //for (int i = 0; i < 10; i++)
            //{
            //    xPrinter.PrintNonFiscalText("საპ ბიზნეს 1");
            //    xPrinter.GetDateAndTimeInformation();
            //    Thread.Sleep(1000);
            //}\
            //Console.WriteLine(xPrinter.GetDateAndTimeInformation() ); 
            //xPrinter.PrintSystemParameters();


        }
    }
}
