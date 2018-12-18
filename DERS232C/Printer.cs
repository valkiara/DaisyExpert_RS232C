using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public class Printer : Communicator
    {
        public Printer(string portName) : base(portName)
        {
        }

        /// <summary>
        /// 44 (2Ch) PAPER FEED
        /// Data field: [Lines]
        /// Reply: No data
        /// Lines Number of lines by which the paper will be moved.
        /// </summary>
        /// <returns></returns>
        public void PaperFeed(string Data)
        {
            SendCommand(44, Data);
        }

        /// <summary>
        /// PAPER CUT
        /// Data field: [Mode]
        /// Reply: Code
        /// Mode Optional parameter indicating cutting type. 1 = full cut, 2 = partial cut.
        /// Code One symbol indicating the result:
        /// “P” – command is successful.
        /// “F” – command is not successful.
        /// </summary>
        /// <param name="Code"></param>
        public Reply PaperCut(string Code)
        {
            return SendCommand(45, Code);
        }
    }
}
