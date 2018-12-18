using DERS232C.Exceptions;
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
    class SerialController
    {

        private SerialPort Port { get; set; }

        private byte SOH = Convert.ToByte(Convert.ToInt32("01", 16));
        private byte POST_AMBLE = Convert.ToByte(Convert.ToInt32("05", 16));
        private byte POST_AMBLE_2 = Convert.ToByte(Convert.ToInt32("04", 16));
        private byte ETX = Convert.ToByte(Convert.ToInt32("03", 16));

        List<BYTEN> StatusCodes;

        public SerialController(SerialPort xPort)
        {
            Port = xPort;
        }

        /// <summary>
        /// Send command, gadaecema int, brdzanebis kodi da optional parameti Data
        /// </summary>
        /// <param name="CMD"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Reply ExecuteCMD(byte CMD, string Data = "", byte SEQ = 0)
        {
            if(!Port.IsOpen)
            {
                Port.Open();
                Port.DiscardInBuffer();
                Port.DiscardOutBuffer();
            }

            
            string result = string.Empty;

            if(SEQ == 0)
            {
                SEQ = GetRandomNumber();
            }

            byte[] by = PrepareCommand(CMD, SEQ, Data);

            Port.Write(by, 0, by.Length);

            Thread.Sleep(500);


            List<byte> lstB = new List<byte>();

            byte[] Baf = new byte[Port.ReadBufferSize];
            int bytesToRead = Port.Read(Baf, 0, Baf.Length);

            int firstByte = Baf[0];


            if (firstByte == 21) //NAK
            {
                ExecuteCMD(CMD, Data, SEQ);
            }
            else if (firstByte == 22) //SYN
            {
                while (firstByte  != 1)
                {
                    Baf = new byte[Port.ReadBufferSize];
                    bytesToRead = Port.Read(Baf, 0, Baf.Length);
                    Thread.Sleep(60);

                    firstByte = Baf[0];
                }
            }


            if (ValidateMessage(Baf, bytesToRead, SEQ, CMD, lstB))
            {
                result = Encoding.UTF8.GetString(lstB.ToArray(), 0, lstB.Count);
            }
            else
            {
                throw new CorruptedMessageException();
            }

            Port.Close();

            return new Reply
            {
                Result = result,
                StatusCodes = StatusCodes
            };
        }



        internal bool ValidateMessage(byte[] Baf, int bytesToRead, byte SEQ, byte CMD, List<byte> lstB)
        {
            char R_SOH = Convert.ToChar(Baf[0]);
            char R_LEN = Convert.ToChar(Baf[1]);
            char R_SEQ = Convert.ToChar(Baf[2]);
            char R_CMD = Convert.ToChar(Baf[3]);
            char R_ETX = Convert.ToChar(Baf[bytesToRead - 1]);

            byte[] BCCCheckX = new byte[4];
            BCCCheckX[0] = Baf[bytesToRead - 5];
            BCCCheckX[1] = Baf[bytesToRead - 4];
            BCCCheckX[2] = Baf[bytesToRead - 3];
            BCCCheckX[3] = Baf[bytesToRead - 2];

            int px;

            for (px = 4; px <= 200 + 4; px++)
            {
                if(Baf[px] == 0x4)
                    break; // TODO: might not be correct. Was : Exit For
                lstB.Add(Baf[px]);
            }

            List<byte> BCCLIST = new List<byte>();

            for (int xp = px + 1; xp <= px + 6; xp++)
            {
                if(Baf[xp] == 0x5)
                   break; // TODO: might not be correct. Was : Exit For
                BCCLIST.Add((Baf[xp]));
            }

            if (!CheckBCC(lstB.ToArray(), BCCLIST.ToArray(), R_LEN, R_CMD, R_SEQ, BCCCheckX))
            {
                return false;
            }
          

            if (R_SEQ != SEQ || R_CMD != CMD || R_ETX != 3)
            {
                return false;
            }
            else
            {
                StatusCodes = new List<BYTEN>();
                for (int i = 1; i < BCCLIST.Count + 1; i++)
                {
                    StatusCodes.Add(BYTEN.SetFlags(BCCLIST[i - 1], i - 1));
                }

                return true;
            }
        }

        internal byte[] PrepareCommand(byte CMD, byte SEQ, string Data = "")
        {
            byte[] by;

            if (string.IsNullOrEmpty(Data))
            {
                by = Write(CMD, SEQ);
            }
            else
            {
                byte[] FormASCII = Encoding.Unicode.GetBytes(Data);
                byte[] list = FormASCII.Where((value, index) => index % 2 == 0).ToArray();
                by = Write(CMD, SEQ, list);
            }

            return by;
        }
       

        internal byte GetRandomNumber(double minimum = 32, double maximum = 255)
        {
            Random random = new Random();
            var x = random.NextDouble() * (maximum - minimum) + minimum;
            return Convert.ToByte(x);
        }

        //BCC check. control sum(0000h-FFFFh). Sum of data bytes from position 2 to position 6. The control sum is transferred in ASCІІ type (12АВ is transferred as 31h 32h 3Аh 3Вh).
        private bool CheckBCC(byte[] DATA, byte[] BCCLst, int LEN, int SEQ, int CMD, byte[] BCCCheckX)
        {
            byte[] By = new byte[4];

            int bCal = 0;

            foreach (byte bb in DATA)
            {
                bCal += bb;
            }

            int BCCCal = 0;
            foreach (byte bb in BCCLst)
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
        private byte[] Write(byte CMD, byte SEQ, byte[] DATA = null)
        {
            byte LEN;
            #region if
            if (DATA == null)
            {

                LEN = Convert.ToByte(Convert.ToByte(Convert.ToInt32("04", 16) + Convert.ToInt32("20", 16)));

                byte[] By = new byte[10];
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

                byte[] By = new byte[10 + DATA.Length];

                By[0] = SOH;
                By[1] = LEN;
                By[2] = SEQ;
                By[3] = CMD;

                int II = 4;
                int bCal = 0;

                foreach (byte bb in DATA)
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
