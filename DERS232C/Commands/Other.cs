using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public class Other : Communicator
    {
        public Other(string portName) : base(portName)
        {
        }

        /// <summary>
        /// 70 (46h) SERVICE INPUT SUM (R/A) AND SERVICE OUTPUT(P/O) SUM
        /// Data field: {Amount }[,{Text1}[{CR}{Text2}]]
        /// Reply: Code,CashSum,ServInput,ServOutput
        /// Amount Registration sum(9 symbols). According to the sign of the digit it is executing ServInput or ServOutput.
        /// Text 1 Commentary text.
        /// CR One byte with value 0Аh (delimiter).
        /// Text 2 Commentary text.
        /// TAB One byte with value 09h (delimiter).
        /// Code “P” – The command was executed successfully.
        /// “F” – The command was cancelled.
        /// CashSum CashSum available. The amount increases not only by using this command but also by
        /// every payment in cash.
        /// ServInput Sum of all commands for the day “ServInput ”
        /// ServOutput Sum of all commands“for the day ServOutput ”.
        /// Note: The indicated texts are “cut” on the right side if they are longer than #COMMENT_LEN#
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Reply ServiceInputSumAndServiceOutPutSum(string Data)
        {
            return SendCommand(70, Data);
        }


        /// <summary>
        /// 63 (3Fh) DISPLAY DATE AND TIME
        /// Data field: No data.
        /// Reply : No data.Current date and time are displayed in format : DD-MM-YYYY HH:MM.
        /// </summary>
        public void DisplayDateAndTime()
        {
            SendCommand(63, "");
        }
    }


}
