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

            xPrinter.StartOfNonFiscalReceipt();
            xPrinter.PrintNonFiscalText("glea");
            var aa = xPrinter.EndOfNonFiscalReceipt();


        }
    }
}
