using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public class BalanceAtTheEndOfTheDay : Communicator
    {
        public BalanceAtTheEndOfTheDay(string portName) : base(portName)
        {
        }

        /// <summary>
        /// 69 (45h) DAILY FINANCIAL REPORT WITH OR WITHOUT CLEARING
        /// Data field: [[Item] Option]
        /// Reply: Closure, SpaceGr,Tax1,Tax2,Tax3, Tax4,Tax5
        /// Item Optional parameter, indicating type of record.If it is not entered, it is = ‘0’by default.
        /// = “0” or “1” – executing daily report with clearing(Z report). The report ends with sign “FISCAL RECEIPT” or “NON-FISCAL RECEIPT” depending on whether FD is f iscalized.
        /// = “2” or “3” – executing daily report without clearing(X report).
        /// = “5” Retrieves information for number of free lines in EJT.
        /// 15
        /// Option Optional parameter.If it is chosen value "N", when issuing daily financial report Z,data is not cleared by operators.
        /// Closure Number of fiscal record (4 symbols).
        /// SpaceGr Session Non-Taxable sales Total.
        /// Tax1 Session Taxable1 sales Total
        /// Tax2 Session Taxable2 sales Total
        /// Tax3 Session Taxable3 sales Total
        /// Tax4 Session Taxable4 sales Total
        /// Tax5 Session Taxable5 sales Total
        /// </summary>
        /// <param name="Data"></param>
        public Reply DisplayFinancialreportWithOrWithoutClearing(string Data)
        {
            return SendCommand(69, Data);
        }

        /// <summary>
        /// 108 (6Ch)DETAILED DAILY REPORT WITH PRINTING OF PLUs
        /// Data field: {Option}
        /// Reply: Closure, SpaceGr,Tax1,Tax2,Tax3,Tax4,Tax5
        /// The command is analogical to “DAILY FINANCIAL REPORT” (69), but at the very start of the report there is printing of sales by PLUs.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Reply DetailedDailyReportWithPrintingOfPLUs(string Data)
        {
            return SendCommand(108, Data);
        }

        /// <summary>
        /// 104 (68h) RESET SALES BY OPERATORS
        /// Data field: Operator,Password
        /// Reply: No data
        /// Operator: Number of operator : from 0 to #OPER_MAX_CNT#
        /// Password: Operator password
        /// When the number of operator and password are correctly entered, FD clears the accumulated data from sales for the particular operator.When entering operator 0 and password of supervisor, Z report is made for all operators.
        /// </summary>
        /// <param name="Data"></param>
        public void ResetSalesByOperators(string Data)
        {
            SendCommand(104);
        }
    }
}
