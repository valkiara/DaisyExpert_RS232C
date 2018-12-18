using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DERS232C
{

    public abstract class Communicator
    {

        internal SerialCom xSerial { get; set; }
        private SerialController xController { get; set; }


        public Communicator(string portName)
        {
            xSerial = new SerialCom(portName);
            xController = new SerialController(xSerial.Port);
        }

        internal Reply SendCommand(byte CMD, string Data = "")
        {
            return xController.ExecuteCMD(CMD, Data);
        }

        
    }
}
