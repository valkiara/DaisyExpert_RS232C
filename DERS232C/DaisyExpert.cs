using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public class DaisyExpert
    {
        public Sales Sales { get; set; }
        public Information Information { get; set; }
        public BalanceAtTheEndOfTheDay BalanceAtTheEndOfTheDay { get; set; }
        public ExternalDisplayControl ExternalDisplayControl { get; set; }
        public InitialSettings InitialSettings { get; set; }
        public Other Other { get; set; }
        public Printer Printer { get; set; }
        public Reports Reports { get; set; }


        public DaisyExpert(string PortName)
        {
            Sales = new Sales(PortName);
            Information = new Information(PortName);
            BalanceAtTheEndOfTheDay = new BalanceAtTheEndOfTheDay(PortName);
            ExternalDisplayControl = new ExternalDisplayControl(PortName);
            InitialSettings = new InitialSettings(PortName);
            Other = new Other(PortName);
            Printer = new Printer(PortName);
            Reports = new Reports(PortName);
        }
    }
}
