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
    public class SerialController
    {

        private SerialPort Port { get; set; }

        private byte SOH = Convert.ToByte(Convert.ToInt32("01", 16));
        private byte SEQ;
        private byte POST_AMBLE = Convert.ToByte(Convert.ToInt32("05", 16));
        private byte POST_AMBLE_2 = Convert.ToByte(Convert.ToInt32("04", 16));
        private byte ETX = Convert.ToByte(Convert.ToInt32("03", 16));

        private byte xSEQ;
        private byte xCMD;
        private string xData;

        public SerialController(SerialPort xPort)
        {
            Port = xPort;
        }
        


        /// <summary>
        /// Senc command, gadaecema int, brdzanebis kodi da optional parameti Data
        /// </summary>
        /// <param name="CMD"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string SendCommand(byte CMD, string Data = "", byte exSEQ = 0)
        {
            xCMD = CMD;
            if (!Port.IsOpen)
            {
                Port.Open();
            }

            if (exSEQ == 0)
            {
                SEQ = GetRandomNumber();
                xSEQ = SEQ;
            }
            else
            {
                SEQ = exSEQ;
            }

            Port.DiscardInBuffer();
            Port.DiscardOutBuffer();
            if (string.IsNullOrEmpty(Data))
            {
                byte[] by = Write(CMD);
                Port.Write(by, 0, by.Length);
            }
            else
            {
                xData = Data;
                byte[] FormASCII = Encoding.Unicode.GetBytes(Data);

                byte[] list = FormASCII.Where((value, index) => index % 2 == 0).ToArray();

                byte[] by = Write(CMD, list);
                Port.Write(by, 0, by.Length);
            }


            Thread.Sleep(500);

            var x = Read(xCMD, xData);

            Port.Close();
            return x;
        }

        private string Read(byte CMD, string xData)
        {

            int SYNCount = 0;
            byte[] Baf = new byte[Port.ReadBufferSize];
            List<byte> lstB = new List<byte>();

            int bytesToRead = 0;
            bytesToRead = Port.Read(Baf, 0, Baf.Length);
            int readByte = Baf[0];


            while (readByte != 1)
            {
                if (readByte == 21) //NAK
                {
                    Console.WriteLine("NAK");
                    SendCommand(xCMD, xData, SEQ);
                }
                else if (readByte == 22) //SYN
                {
                    SYNCount++;
                    Console.WriteLine("SynCount: " + SYNCount + " Byte[0] = " + Baf[0] + " Bytes to read: " + bytesToRead);
                    Thread.Sleep(60);
                }
                bytesToRead = Port.Read(Baf, 0, Baf.Length);
                readByte = Baf[0];
            }


            Console.WriteLine("Out of While: " + " Byte[0] = " + readByte + " Bytes to read: " + bytesToRead);

            if (bytesToRead < 5)
            {
                Port.Close();
                return "Error: ByteCOunt is less then 5";
            }

            char R_SOH = Convert.ToChar(Baf[0]);
            char R_LEN = Convert.ToChar(Baf[1]);
            char R_SEQ = Convert.ToChar(Baf[2]);
            char R_CMD = Convert.ToChar(Baf[3]);
            char R_ETX = Convert.ToChar(Baf[bytesToRead - 1]);

            Byte[] BCCCheckX = new byte[4];
            BCCCheckX[0] = Baf[bytesToRead - 5];
            BCCCheckX[1] = Baf[bytesToRead - 4];
            BCCCheckX[2] = Baf[bytesToRead - 3];
            BCCCheckX[3] = Baf[bytesToRead - 2];

            if (R_SEQ != xSEQ)
            {
                //TODO:
                Console.WriteLine("Error: Sent REQ does not match REQ");
                return "Error: Sent REQ does not match REQ";
            }
            if (R_CMD != xCMD)
            {
                //TODO:
                Console.WriteLine("Error: Sent CMD does not match CMD");
                return "Error: Sent CMD does not match CMD";
            }
            if (R_ETX != 3)
            {
                //TODO:
                Port.Close();
                Console.WriteLine("NOT ETX!");
                return "Error: NOT ETX!";
            }

            //SendCommand(xCMD, xData, SEQ);

            int px;

            for (px = 4; px <= 200 + 4; px++)
            {
                if (Baf[px] == 0x4)
                    break; // TODO: might not be correct. Was : Exit For
                lstB.Add(Baf[px]);
            }

            List<Byte> BCCLIST = new List<byte>();

            for (int xp = px + 1; xp <= px + 6; xp++)
            {
                if (Baf[xp] == 0x5)
                    break; // TODO: might not be correct. Was : Exit For
                BCCLIST.Add((Baf[xp]));
            }

            if (!CheckBCC(lstB.ToArray(), BCCLIST.ToArray(), R_LEN, R_CMD, R_SEQ, BCCCheckX))
            {
                //TODO:
                //return "Error: Sent CMD does not match CMD";
                Console.WriteLine("Error: BCC check failed!!!!");
                
                return "Error: BCC check failed!";
                // SendCommand(xCMD, xData, SEQ);
            }
            else
            {
                Console.WriteLine("BCC Check: True;");
            }

            List<BYTEN> statusCodes = new List<BYTEN>();
            for (int i = 1; i < BCCLIST.Count + 1; i++)
            {
                statusCodes.Add(BYTEN.SetFlags(BCCLIST[i - 1], i - 1));
            }


           
            string st = "";
            foreach (byte bb in lstB)
            {
                st = st + Convert.ToChar(bb);
            }

            string converted = Encoding.UTF8.GetString(lstB.ToArray(), 0, lstB.Count);
            Debug.WriteLine("Conveted: " + converted + " SYNCOunt: ");
            //Console.WriteLine("Conveted: " + converted + " SYNCOunt: ");

            return converted;

        }


        private byte GetRandomNumber(double minimum = 32, double maximum = 255)
        {
            Random random = new Random();
            var x = random.NextDouble() * (maximum - minimum) + minimum;
            return Convert.ToByte(x);
        }

        //BCC check. control sum(0000h-FFFFh). Sum of data bytes from position 2 to position 6. The control sum is transferred in ASCІІ type (12АВ is transferred as 31h 32h 3Аh 3Вh).
        private bool CheckBCC(byte[] DATA, byte[] BCCLst, int LEN, int SEQ, int CMD, Byte[] BCCCheckX)
        {

            Byte[] By = new Byte[4];

            int bCal = 0;

            foreach (Byte bb in DATA)
            {
                bCal += bb;
            }

            int BCCCal = 0;
            foreach (Byte bb in BCCLst)
            {
                BCCCal += bb;
            }


            int V_Int = LEN + SEQ + POST_AMBLE + CMD + bCal + POST_AMBLE_2 + BCCCal;



            string reString = GetBCCStrHex(V_Int);

            By[0] = Convert.ToByte(Convert.ToInt32("3" + reString[0], 16));
            By[1] = Convert.ToByte(Convert.ToInt32("3" + reString[1], 16));
            By[2] = Convert.ToByte(Convert.ToInt32("3" + reString[2], 16));
            By[3] = Convert.ToByte(Convert.ToInt32("3" + reString[3], 16));

            return By.SequenceEqual(BCCCheckX);
        }


        /// <summary>
        /// Write, amzadebs brdzanebas gasagzavnad
        /// </summary>
        /// <param name="CMD"></param>
        /// <param name="DATA"></param>
        /// <returns></returns>
        private Byte[] Write(Byte CMD, Byte[] DATA = null)
        {
            byte LEN;
            #region if
            if (DATA == null)
            {

                LEN = Convert.ToByte(Convert.ToByte(Convert.ToInt32("04", 16) + Convert.ToInt32("20", 16)));

                Byte[] By = new Byte[10];
                By[0] = SOH;
                By[1] = LEN;
                By[2] = SEQ;
                By[3] = CMD;
                By[4] = POST_AMBLE;
                //By[4] = Convert.ToByte(Convert.ToInt32("0", 16));

                int V_Int = LEN + SEQ + POST_AMBLE + CMD;

                string reString = GetBCCStrHex(V_Int);

                By[5] = Convert.ToByte(Convert.ToInt32("3" + reString[0], 16));
                By[6] = Convert.ToByte(Convert.ToInt32("3" + reString[1], 16));
                By[7] = Convert.ToByte(Convert.ToInt32("3" + reString[2], 16));
                By[8] = Convert.ToByte(Convert.ToInt32("3" + reString[3], 16));
                By[9] = ETX;

                return By;
            }
            #endregion
            #region else
            else
            {

                string ForLen = (DATA.Length + 4 + 32).ToString();
                LEN = Convert.ToByte(Convert.ToByte(ForLen));

                Byte[] By = new Byte[10 + DATA.Length];

                By[0] = SOH;
                By[1] = LEN;
                By[2] = SEQ;
                By[3] = CMD;

                int II = 4;
                int bCal = 0;

                foreach (Byte bb in DATA)
                {
                    By[II] = bb;
                    bCal += bb;
                    II += 1;
                }

                By[II] = POST_AMBLE;
                II += 1;

                int V_Int = LEN + SEQ + POST_AMBLE;
                V_Int = V_Int + CMD;
                V_Int = V_Int + bCal;


                string reString = GetBCCStrHex(V_Int);

                By[II] = Convert.ToByte(Convert.ToInt32("3" + reString[0], 16));
                II += 1;
                By[II] = Convert.ToByte(Convert.ToInt32("3" + reString[1], 16));
                II += 1;
                By[II] = Convert.ToByte(Convert.ToInt32("3" + reString[2], 16));
                II += 1;
                By[II] = Convert.ToByte(Convert.ToInt32("3" + reString[3], 16));
                II += 1;
                By[II] = ETX;

                return By;
            }
            #endregion
        }

        private string GetBCCStrHex(int V_Int)
        {
            string strHex = V_Int.ToString("X");
            string reString = strHex;

            if (strHex.Length == 1)
            {
                reString = "000" + strHex;
            }
            if (strHex.Length == 2)
            {
                reString = "00" + strHex;
            }
            if (strHex.Length == 3)
            {
                reString = "0" + strHex;
            }

            return reString;
        }
    }
}
