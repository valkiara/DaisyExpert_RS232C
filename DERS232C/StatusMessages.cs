using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{


    class StatusMessages
    {
        


        public void GetStatusMessage(int code, int byteNum)
        {

            string BinaryCode = Convert.ToString(code, 2);
            //switch (byteNum)
            //{
            //    case 1:
            //        foreach (char bit in BinaryCode)
            //        {
                        
            //        }
            //    default:
            //        break;
            //}
        }
    }

    class StatusFlag
    {
        public byte BYTENO { get; set; }
        public string  GeneralFunction{ get; set; }
        public bool Flag { get; set; }
    }

    public class BYTEN
    {
        public int ByteN { get; set; }
        StatusFlag Seven;
        StatusFlag Six;
        StatusFlag Five;
        StatusFlag Four;
        StatusFlag Three;
        StatusFlag Two;
        StatusFlag One;
        StatusFlag Zero;

        public static BYTEN SetFlags(int code, int byteNum)
        {
            string BinaryCode = Convert.ToString(code, 2);
            BYTEN xByte = new BYTEN();

            switch (byteNum)
            {
                case 0:
                    xByte = new BYTEN
                    {
                        ByteN = byteNum,
                        Seven = new StatusFlag
                        {
                            BYTENO = 7,
                            GeneralFunction = "Reserved",
                            Flag = BinaryCode.ElementAt(0) == '1' ? true : false
                        },
                        Six = new StatusFlag
                        {
                            BYTENO = 6,
                            GeneralFunction = "Reserved",
                            Flag = BinaryCode.ElementAt(1) == '1' ? true : false
                        },
                        Five = new StatusFlag
                        {
                            BYTENO = 5,
                            GeneralFunction = "OR of all errors with *from bytes 0, 1, 2 – general error",
                            Flag = BinaryCode.ElementAt(2) == '1' ? true : false
                        },
                        Four = new StatusFlag
                        {
                            BYTENO = 4,
                            GeneralFunction = "Printing mechanism error",
                            Flag = BinaryCode.ElementAt(3) == '1' ? true : false
                        },
                        Three = new StatusFlag
                        {
                            BYTENO = 3,
                            GeneralFunction = "No external display",
                            Flag = BinaryCode.ElementAt(4) == '1' ? true : false
                        },
                        Two = new StatusFlag
                        {
                            BYTENO = 2,
                            GeneralFunction = "Date and time are not set",
                            Flag = BinaryCode.ElementAt(5) == '1' ? true : false
                        },
                        One = new StatusFlag
                        {
                            BYTENO = 1,
                            GeneralFunction = "Invalid command",
                            Flag = BinaryCode.ElementAt(6) == '1' ? true : false
                        },
                        Zero = new StatusFlag
                        {
                            BYTENO = 0,
                            GeneralFunction = "Syntactic error",
                            Flag = BinaryCode.ElementAt(7) == '1' ? true : false
                        }
                    };
                    break;
                case 1:
                    xByte = new BYTEN
                    {
                        ByteN = byteNum,
                        Seven = new StatusFlag
                        {
                            BYTENO = 7,
                            GeneralFunction = "Reserved",
                            Flag = BinaryCode.ElementAt(0) == '1' ? true : false
                        },
                        Six = new StatusFlag
                        {
                            BYTENO = 6,
                            GeneralFunction = "Wrong password",
                            Flag = BinaryCode.ElementAt(1) == '1' ? true : false
                        },
                        Five = new StatusFlag
                        {
                            BYTENO = 5,
                            GeneralFunction = "Error in cutter",
                            Flag = BinaryCode.ElementAt(2) == '1' ? true : false
                        },
                        Four = new StatusFlag
                        {
                            BYTENO = 4,
                            GeneralFunction = "Not used",
                            Flag = BinaryCode.ElementAt(3) == '1' ? true : false
                        },
                        Three = new StatusFlag
                        {
                            BYTENO = 3,
                            GeneralFunction = "Not used",
                            Flag = BinaryCode.ElementAt(4) == '1' ? true : false
                        },
                        Two = new StatusFlag
                        {
                            BYTENO = 2,
                            GeneralFunction = "Reset memory",
                            Flag = BinaryCode.ElementAt(5) == '1' ? true : false
                        },
                        One = new StatusFlag
                        {
                            BYTENO = 1,
                            GeneralFunction = "Prohibited command in current mode",
                            Flag = BinaryCode.ElementAt(6) == '1' ? true : false
                        },
                        Zero = new StatusFlag
                        {
                            BYTENO = 0,
                            GeneralFunction = "Overflow of sum fields",
                            Flag = BinaryCode.ElementAt(7) == '1' ? true : false
                        }
                    };
                    break;
                case 2:
                    xByte = new BYTEN
                    {
                        ByteN = byteNum,
                        Seven = new StatusFlag
                        {
                            BYTENO = 7,
                            GeneralFunction = "Reserved",
                            Flag = BinaryCode.ElementAt(0) == '1' ? true : false
                        },
                        Six = new StatusFlag
                        {
                            BYTENO = 6,
                            GeneralFunction = "Print of document is allowed",
                            Flag = BinaryCode.ElementAt(1) == '1' ? true : false
                        },
                        Five = new StatusFlag
                        {
                            BYTENO = 5,
                            GeneralFunction = "non-fiscal receipt is opened",
                            Flag = BinaryCode.ElementAt(2) == '1' ? true : false
                        },
                        Four = new StatusFlag
                        {
                            BYTENO = 4,
                            GeneralFunction = "Insufficient paper in EJT",
                            Flag = BinaryCode.ElementAt(3) == '1' ? true : false
                        },
                        Three = new StatusFlag
                        {
                            BYTENO = 3,
                            GeneralFunction = "Fiscal receipt is opened",
                            Flag = BinaryCode.ElementAt(4) == '1' ? true : false
                        },
                        Two = new StatusFlag
                        {
                            BYTENO = 2,
                            GeneralFunction = "No paper (control tape)",
                            Flag = BinaryCode.ElementAt(5) == '1' ? true : false
                        },
                        One = new StatusFlag
                        {
                            BYTENO = 1,
                            GeneralFunction = "Not enough paper",
                            Flag = BinaryCode.ElementAt(6) == '1' ? true : false
                        },
                        Zero = new StatusFlag
                        {
                            BYTENO = 0,
                            GeneralFunction = "No paper",
                            Flag = BinaryCode.ElementAt(7) == '1' ? true : false
                        }
                    };
                    break;
                case 3: //TODO
                    //int ErrCode = Convert.ToInt32(BinaryCode.Substring(1, BinaryCode.Length - 1), 2);
                    xByte = new BYTEN
                    {
                        ByteN = byteNum,
                        Seven = new StatusFlag
                        {
                            BYTENO = 7,
                            GeneralFunction = "Reserved",
                            Flag = BinaryCode.ElementAt(0) == '1' ? true : false
                        },
                        Six = new StatusFlag
                        {
                            BYTENO = 6,
                           // GeneralFunction = ErrorCodes.ContainsKey(ErrCode)? ErrorCodes[ErrCode].Description : ""

                        }

                       
                        
                    };
                    break;
                case 4:
                    xByte = new BYTEN
                    {
                        ByteN = byteNum,
                        Seven = new StatusFlag
                        {
                            BYTENO = 7,
                            GeneralFunction = "Reserved",
                            Flag = BinaryCode.ElementAt(0) == '1' ? true : false
                        },
                        Six = new StatusFlag
                        {
                            BYTENO = 6,
                            GeneralFunction = "Not used",
                            Flag = BinaryCode.ElementAt(1) == '1' ? true : false
                        },
                        Five = new StatusFlag
                        {
                            BYTENO = 5,
                            GeneralFunction = "OR of all errors with*from bytes 4 and 5– general error",
                            Flag = BinaryCode.ElementAt(2) == '1' ? true : false
                        },
                        Four = new StatusFlag
                        {
                            BYTENO = 4,
                            GeneralFunction = "Fiscal memory full",
                            Flag = BinaryCode.ElementAt(3) == '1' ? true : false
                        },
                        Three = new StatusFlag
                        {
                            BYTENO = 3,
                            GeneralFunction = "Room for less than 50 records in fiscal memory",
                            Flag = BinaryCode.ElementAt(4) == '1' ? true : false
                        },
                        Two = new StatusFlag
                        {
                            BYTENO = 2,
                            GeneralFunction = "Invalid record in fiscal memory",
                            Flag = BinaryCode.ElementAt(5) == '1' ? true : false
                        },
                        One = new StatusFlag
                        {
                            BYTENO = 1,
                            GeneralFunction = "Not used",
                            Flag = BinaryCode.ElementAt(6) == '1' ? true : false
                        },
                        Zero = new StatusFlag
                        {
                            BYTENO = 0,
                            GeneralFunction = "Record error in fiscal memory",
                            Flag = BinaryCode.ElementAt(7) == '1' ? true : false
                        }
                    };
                    break;
                case 5:
                    xByte = new BYTEN
                    {
                        ByteN = byteNum,
                        Seven = new StatusFlag
                        {
                            BYTENO = 7,
                            GeneralFunction = "Reserved",
                            Flag = BinaryCode.ElementAt(0) == '1' ? true : false
                        },
                        Six = new StatusFlag
                        {
                            BYTENO = 6,
                            GeneralFunction = "FM ready",
                            Flag = BinaryCode.ElementAt(1) == '1' ? true : false
                        },
                        Five = new StatusFlag
                        {
                            BYTENO = 5,
                            GeneralFunction = "MRC is programmed",
                            Flag = BinaryCode.ElementAt(2) == '1' ? true : false
                        },
                        Four = new StatusFlag
                        {
                            BYTENO = 4,
                            GeneralFunction = "Tax rate is programmed",
                            Flag = BinaryCode.ElementAt(3) == '1' ? true : false
                        },
                        Three = new StatusFlag
                        {
                            BYTENO = 3,
                            GeneralFunction = "Fiscal device in use",
                            Flag = BinaryCode.ElementAt(4) == '1' ? true : false
                        },
                        Two = new StatusFlag
                        {
                            BYTENO = 2,
                            GeneralFunction = "Reserved",
                            Flag = BinaryCode.ElementAt(5) == '1' ? true : false
                        },
                        One = new StatusFlag
                        {
                            BYTENO = 1,
                            GeneralFunction = "Not used",
                            Flag = BinaryCode.ElementAt(6) == '1' ? true : false
                        },
                        Zero = new StatusFlag
                        {
                            BYTENO = 0,
                            GeneralFunction = "FM overflowed",
                            Flag = BinaryCode.ElementAt(7) == '1' ? true : false
                        }
                    };
                    break;
                default:
                    break;
            }

            return xByte;
        }



        #region ErrorCodes
        public static Dictionary<int, ErrorCode> ErrorCodes = new Dictionary<int, ErrorCode>
        {
            {1, new ErrorCode
                {
                    No = 1,
                    Text = "OVERFLOW",
                    Description = "This operation will lead to overflow"
                }
            },
            {3, new ErrorCode
                {
                    No = 3,
                    Text = "OFL RECEIPT",
                    Description = "You are not authorized to do more transactions in this receipt"
                }
            },
            {4, new ErrorCode
                {
                    No = 4,
                    Text = "OFL.NUM PAYM.",
                    Description = "You are not authorized to make more payments in this receipt"
                }
            },
            {6, new ErrorCode
                {
                    No = 6,
                    Text = "BEGIN PAYMENT",
                    Description = "Attempt to begin a sale after a payment has been started"
                }
            },
            {8, new ErrorCode
                {
                    No = 8,
                    Text = "",
                    Description = "Forbidden for sale VAT group"
                }
            },
            {11, new ErrorCode
                {
                    No = 11,
                    Text = "TOO MANY DOTS",
                    Description = "More than one decimal point has been entered."
                }
            },
            {12, new ErrorCode
                {
                    No = 12,
                    Text = "",
                    Description = "More than one sign '+' or '-'has been entered."
                }
            },
            {13, new ErrorCode
                {
                    No = 13,
                    Text = "",
                    Description = "The symbol '+'or '-' is not in first position"
                }
            },
            {14, new ErrorCode
                {
                    No = 14,
                    Text = "WRONG SYMBOL",
                    Description = "For example, barcode which include not only digits"
                }
            },
            {15, new ErrorCode
                {
                    No = 15,
                    Text = "MANY DOTSYMBOLS",
                    Description = "Too many symbols after the decimal point than acceptable"
                }
            },
            {16, new ErrorCode
                {
                    No = 16,
                    Text = "MANY SYMBOLS",
                    Description = "Too many symbol have been entered than acceptable"
                }
            },
            {20, new ErrorCode
                {
                    No = 20,
                    Text = "INCORRECT KEY",
                    Description = "In this case, you have chosen the wrong key."
                }
            },
            {21, new ErrorCode
                {
                    No = 21,
                    Text = "INCORRECT VALUE",
                    Description = "The value is out of the acceptable range"
                }
            },
            {22, new ErrorCode
                {
                    No = 22,
                    Text = "FORBIDDEN OPER.",
                    Description = "See system parameter 10"
                }
            },
            {23, new ErrorCode
                {
                    No = 23,
                    Text = "FORBIDDEN VOID",
                    Description = "Operation “VOID” is impossible"
                }
            },
            {24, new ErrorCode
                {
                    No = 24,
                    Text = "WRONG OPERATION",
                    Description = "Attempt to make deep void to non-existing transaction"
                }
            },
            {25, new ErrorCode
                {
                    No = 25,
                    Text = "WRONG OPERATION",
                    Description = "Attempt to make payment before receipt is opened."
                }
            },
            {26, new ErrorCode
                {
                    No = 26,
                    Text = "NO STOCK",
                    Description = "Attempt to sale a PLU with a quantity bigger than its stock"
                }
            },
            {27, new ErrorCode
                {
                    No = 27,
                    Text = "ERROR SCALES",
                    Description = "Incorrect communication with electronic scales"
                }
            },
            {29, new ErrorCode
                {
                    No = 29,
                    Text = "NO NAME",
                    Description = "Attempt to sale an ITEM without a PLU name"
                }
            },
            {30, new ErrorCode
                {
                    No = 30,
                    Text = "FULL FM",
                    Description = "Fiscal memory is full"
                }
            },
            {41, new ErrorCode
                {
                    No = 41,
                    Text = "INCORR.BARCODE",
                    Description = "Incorrect barcode (wrong control sum)"
                }
            },
            {42, new ErrorCode
                {
                    No = 42,
                    Text = "ZERO BARCODE",
                    Description = "Attempt to make a sale or report with zero barcode"
                }
            },
            {43, new ErrorCode
                {
                    No = 43,
                    Text = "BARC.FORBIDDEN",
                    Description = "Attempt to program or report with a weighted barcode"
                }
            },
            {44, new ErrorCode
                {
                    No = 44,
                    Text = "BC NOT FOUND",
                    Description = "Attempt to sale or report with a non-programmed barcode"
                }
            },
            {45, new ErrorCode
                {
                    No = 45,
                    Text = "BC DUPLICATED",
                    Description = "Attempt to program an already existing barcode"
                }
            },
            {51, new ErrorCode
                {
                    No = 51,
                    Text = "",
                    Description = "The transmitted string from the PC during erasing the EJT is not correct"
                }
            },
            {61, new ErrorCode
                {
                    No = 61,
                    Text = "WRONG SD CARD",
                    Description = "Check if the card is inserted correctly. Exit and enter again in mode \"Operation with SD card\""
                }
            },
            {62, new ErrorCode
                {
                    No = 62,
                    Text = "FILE NOT FOUND",
                    Description = "File with this name is not found"
                }
            },
            {63, new ErrorCode
                {
                    No = 63,
                    Text = "FILE ERROR",
                    Description = "Operation with this file can not be executed."
                }
            },
            {66, new ErrorCode
                {
                    No = 66,
                    Text = "",
                    Description = "Incorrect password"
                }
            },
            {70, new ErrorCode
                {
                    No = 70,
                    Text = "FM NOT EXIST",
                    Description = "Fiscal memory does not exist"
                }
            },
            {71, new ErrorCode
                {
                    No = 71,
                    Text = "SUBSTITUTION FM",
                    Description = "!!! Incorrect data in FM !!!!"
                }
            },
            {72, new ErrorCode
                {
                    No = 72,
                    Text = "ERR RECORD FM",
                    Description = "!!! Error in FM record !!!!"
                }
            },
            {75, new ErrorCode
                {
                    No = 75,
                    Text = "",
                    Description = "Wrong range for fiscal report"
                }
            },
            {81, new ErrorCode
                {
                    No = 81,
                    Text = "OFL.DAILY REP.",
                    Description = "The daily financial report is overflowed"
                }
            },
            {82, new ErrorCode
                {
                    No = 82,
                    Text = "OVERFLOW 24h",
                    Description = "More than24 hours without a daily report"
                }
            },
            {83, new ErrorCode
                {
                    No = 83,
                    Text = "OFL.OPER.REP.",
                    Description = "The report by operators is overflowed"
                }
            },
            {84, new ErrorCode
                {
                    No = 84,
                    Text = "OFL.PLU REP.",
                    Description = "The report by PLU's is overflowed"
                }
            },
            {88, new ErrorCode
                {
                    No = 88,
                    Text = "OVERFLOW EJT",
                    Description = "The EJT is overflowed"
                }
            },
            {91, new ErrorCode
                {
                    No = 91,
                    Text = "NEED DAILY REP.",
                    Description = "The daily financial report must be zeroed before executing the requested operation."
                }
            },
            {92, new ErrorCode
                {
                    No = 92,
                    Text = "NEED OPER. REP.",
                    Description = "The report by operators must be zeroed before executing the requested operation."
                }
            },
            {93, new ErrorCode
                {
                    No = 93,
                    Text = "NEED PLU REPORT",
                    Description = "The report by PLUs must be zeroed before executing the requested operation."
                }
            },
            {97, new ErrorCode
                {
                    No = 97,
                    Text = "CHANGE FORBIDDEN",
                    Description = "It is forbidden to change the value of this field."
                }
            },
            {108, new ErrorCode
                {
                    No = 108,
                    Text = "WRONG PASSWORD",
                    Description = "3 consecutive attempts to enter a wrong password."
                }
            } 
	#endregion
        };



    }

    public class ErrorCode
    {
        public int No { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }

    

}
