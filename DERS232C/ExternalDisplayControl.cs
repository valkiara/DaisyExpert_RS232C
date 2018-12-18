using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public class ExternalDisplayControl : Communicator
    {
        public ExternalDisplayControl(string portName) : base(portName)
        {
        }

        /// <summary>
        /// A commad to clear the display is sent.
        /// Note 1: If there is an open fiscal receipt, the data that FD sends to display depends on the value of the relevant system parameter – for further details see “User’s manual”
        /// Note 2: This command is supported only by FD with external display. The display must be turned on and connected to the FD.
        /// </summary>
        public void ClearDisplay()
        {
            SendCommand(33, "");
        }


        /// <summary>
        /// Note 1: The text will be “cut” on the right side, if it is longer than the value of the relevant system parameter – for further details see “User’s manual”
        /// Note 2: The command will not be completed if the value of the relevant system parameter is less than 2– for further details see “User’s manual”
        /// Note 3: If there is an open fiscal receipt, the data which FD sends to display depends on the value of the relevant system parameter – for further details see “User’s manual”
        ///  Note 4: This command is supported only by FD with external display.The display must be turned on and connected to the FD.
        /// </summary>
        /// <param name="Text">The text which will be displayed on line 2 of the display.</param>
        public void DisplayTextAtLine2(string Text)
        {
            SendCommand(23, Text);
        }
    }


}
