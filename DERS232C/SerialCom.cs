using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    /*
Abbreviations:
“SOH”- start of packed message
“LEN” – total number of bytes from position 2 to position 8, plus fixed offset of 20h(for example lenght 0х01 is transmitted as 21h).
“SEQ” – serial number of packet.This is the number of the packet transmitted by HOST.
“CMD” – code of command.This is the command number, transmitted by HOST.In case of syntactic error or invalid command the corresponding status byte is set in the reply and then a packed message with zero data length is returned.
“DATA” – data, according to the command.If there is no data, the length field is zero.In case of syntactic error or invalid command the corresponding status byte is set in the reply and then a packed message with zero data length is returned.
“STATUS” – current status field of SLAVE(see Table1 to Table 6).
“BCC” – control sum(0000h - FFFFh).Sum of data bytes from position 2 to position 8.The control sum is transferred in ASCІІ type(12АВ is transferred as 31h 32h 3А 3В).
“ETX” – end of packed message

Abbreviations:
    FD Fiscal device
    PC Personal computer. The usage in this documentation is not just the computer hardware itself, but also includes the running software application on hardware machine, which realize the communication with FD.
    FM Fiscal memory
    MRC Machine Registration Code
    TIN Tax Identification Number
*/
    public class SerialCom
    {
       
        public SerialPort Port { get; set; }


        public SerialCom(string Name, StopBits StopBits = StopBits.One, int Baudrate = 9600, int DataBits = 8, Parity Parity = Parity.None, Handshake Handshake = Handshake.None)
        {
            Port = new SerialPort(Name);
            Port.StopBits = StopBits;
            Port.BaudRate = Baudrate;
            Port.DataBits = DataBits;
            Port.Parity = Parity;
            Port.Handshake = Handshake;
            Port.DiscardNull = false;
            Port.Encoding = Encoding.Unicode;
        }
    }
}
