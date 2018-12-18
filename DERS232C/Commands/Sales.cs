using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public class Sales : Communicator
    {
        public Sales(string portName) : base(portName)
        {
        }




        /// <summary>
        /// 52 (34h) SALE AND DISPLAY
        ///558Data field: [Text1]{Tab}{TaxGroup}{[Sign]Price}[*QTY][, Percent][$Netto]
        ///558Reply: No data
        ///558Information for data fields.their function and conditions through which FD will complete successfully this command –see description of command 49(31h).
        /// </summary>
        public void SaleAndDisplay(string Data)
        {
            SendCommand(52, Data);
        }

        /// <summary>
        /// 49 (31h) SALE
        /// Data field: [{Text1}][{CR}{Text2}]{Tabulator}{TaxGr}{[Sign]Price} [*{QTY}][,Percent][$Netto]
        /// Reply: No data
        /// Text 1 Text that describes the sale
        /// CR One byte, value 0Аh(divider).
        /// Text 2 Additional text which describes the sale.
        /// Tab One byte, value 09h (divider).
        /// TaxGr This is 1 symbol,that shows tax group(Space or one of capital latin letters ‘A’,’B’,’C’,’D’).
        /// Sign One byte with value‘+’ or ‘–‘. If there is Sign= ‘–’, a correction of the last sale with the same price, quantitity and tax group is made.Besides: Percent and N etto ///are ignored.
        /// Price This is unit price, including no more than 8 digits
        /// QTY It is not compulsury. .Its length is up to 8 digits. (up to 3 after the decimal point).It indicates sale quantity.It is 1.000 by default.
        /// Percent This is Optional parameter, showing the value of surcharge or discount(according to the sign) in percentige over the current sum.Acceptable values are from 9 9.99 ///% to 99.99 %. 2 digits after the decimal point are acceptable.
        /// $Netto This is Optional parameter indicating the value of the net surcharge or discount ( according to the sign) over current sale.
        /// The FD prints the sale name with price and code of tax group(quantity if is set)
        /// In case of discount or surcharge, the print is on a separate line.
        /// 
        ///     The FD doesn't react to the command if:
        ///  There is no open fiscal receipt;
        ///  Maximum number of sales is reached for 1 fiscal receipt;
        ///  A payment has been started in the current fiscal receipt;
        ///  Overflowed EJT,if FD works with EJT;
        ///  Fileds Percent and Netto are simultenuosly set;
        ///  After the execution an overflow of some of the reports would happen;
        ///  It comes out a negative sum, which is a result from discounts or surcharges within the receipt.
        ///  The indicated tax group is forbidden for sale
        /// Note: The indicated texts are “cut” on the right side if they are longer than #COMMENT_LEN#
        /// </summary>
        public void Sale(string Data)
        {
            SendCommand(49, Data);
        }



        /// <summary>
        /// 38 (26h) START OF NON-FISCAL RECEIPT
        /// Number of all issued receipts (fiscal and non-fiscal) since the last balance at the end of the day till now (4 bytes).
        /// Data field: No data
        /// Reply: Allreceipt
        /// The SLAVE does not react to the command if:
        /// There is open non-fiscal receipt;
        /// There is open fiscal receipt
        /// The built-in RTC is not set to the correct time
        /// </summary>
        public void StartOfNonFiscalReceipt()
        {
            var x = SendCommand(38, "");
        }

        /// <summary>
        /// 42 (2Ah) PRINT NON-FISCAL TEXT
        ///  Data field: Text
        ///  Reply: No data
        ///  Text Text
        ///  The SLAVE does not react to the command if:
        ///   There is no open non-fiscal receipt;
        ///   There is open fiscal receipt.
        ///  Note: The text will be “cut” on the right side if it is longer than #COMMENT_LEN#
        /// </summary>
        public void PrintNonFiscalText(string text4Print)
        {
            SendCommand(42, text4Print);
        }

        /// <summary>
        /// 39 (27h) END OF NON-FISCAL RECEIPT
        /// Data field: No data
        /// Reply: Allreceipt
        /// Allreceipt Number of all issued receipts(fiscal and non-fiscal) since the last balance at the end of the day till now(4 bytes).
        /// The SLAVE does not react to the command if:
        /// There is no open non-fiscal receipt;
        /// There is open fiscal receipt.
        /// </summary>
        /// <returns>String Allreceipt</returns>
        public Reply EndOfNonFiscalReceipt()
        {
            return SendCommand(39);
        }

        /// <summary>
        /// 10
        ///  48 (30h) START OF FISCAL RECEIPT
        ///  Data field: {OperatorNum },{Password}
        ///  Reply: Allreceipt, FiscReceipt
        ///  OperatorNum Operator number – 1 to #OPER_MAX_CNT#.
        ///  Password Operator password – up to 6 digits
        ///  Allreceipt Number of all issued receipts(non-fiscal and fiscal) since the last balance at the end of the day till now(4 bytes)
        ///  FiscReceipt Number of all issued fiscal receipts since the last balance at the end of the day till now(4 bytes)
        ///  Operation of FD:
        ///   Prints HEADER and REGNO
        ///   Prints Operator name and number;
        ///  FD does not react to the command if:
        ///   There is open fiscal or non-fiscal receipt;
        ///   Overflowed fiscal memory;
        ///   Overflowed or erased EJT, when the SLAVE operates with EJT
        ///   There is no number or password of operator;
        ///   Password is invalid;
        ///   The built-in RTC is not set to the correct time;.
        ///  Attention: If FD has not been fiscalized, you are allowed to open a
        /// </summary>
        /// <returns></returns>
        public Reply StartOfFiscalReceipt(string Data)
        {
            return SendCommand(48, Data);
        }

        /// <summary>
        /// 58 (3Ah) SALE BY PLUs
        /// Data field: {[Sign]PLU} [*{QTY}][, Percent][@Price][$Netto]
        /// Reply: No data
        /// Sign 1byte with value ‘+’ or ‘–‘. If Sign = ’–’, then a correction of the last sale with the same price, quantity and number of PLU is made as well as i gnoring ///fields Percent and Netto.
        /// PLU PLU number(serial).
        /// QTY It is not mandatory.Its length is up to 8 digits. (up to 3 after the decimal point).It indicates sale quantity.It is 1.000 by default.
        /// Percent This is optional parameter, specifying the value of surcharge or discount(according to the sign) in percentage over the current sum.Acceptable values a re ///from 99.99 % to 99.99 %. 2 digits after the decimal point are acceptable.
        /// $Netto This is Optional parameter indicating the value of the net surcharge or discount ( according to the sign) over current sale.
        /// Price Optional parameter, indicating free PLU price during sale
        /// The SLAVE doesn't react to the command if:
        ///  There is no open fiscal receipt;
        ///  Maximum number of sales is reached for 1 fiscal receipt;
        ///  A payment has been started in the current fiscal receipt;
        ///  Overflowed EJT, when FD works with EJT;
        ///  Fileds Percent and Netto are simultaneously set;
        ///  After the execution an overflow of some of the reports would happen;
        ///  PLU with this number is not programmed
        /// </summary>
        public void SaleByPLUs(string Data)
        {
            SendCommand(58, Data);
        }


        /// <summary>
        /// 51 (33h) SUBTOTAL
        /// Data field: {Print}{Display}[, Percent]
        /// Reply: SubTotal,TaxFree,Tax1,Tax2,Tax3, Tax4
        /// Print Value 0 (not printing) or 1 (printing) SubTotal sum
        /// Display Value 0 or 1. Indicates whether FD must show SubTotal sum on the external display.
        /// Percent This is Optional parameter, specifying the value of surcharge or discount(according to the sign) in percentage over the accumulated sum by the moment.
        /// SubTotal Amount till the moment for the current FISCAL RECEIPT – 10 symbols.
        /// Tax_Free Total of Non-Taxable sales
        /// Tax1 Total of Taxable A sales
        /// Tax2 Total of Taxable B sales
        /// Tax3 Total of Taxable C sales
        /// Tax4 Total of Taxable D sales
        /// This command calculates the sum of all sales, which are registered into the fiscal receipt by now.If discount or surcharge is entered, it is printed on a separate line.The calculated current sum and all accumulated sums of tax groups for the moment are transmitted to the PC.
        /// </summary>
        /// <returns></returns>
        public Reply Subtotal(string Data)
        {
            return SendCommand(51, Data);
        }

        /// <summary>
        /// 53 (35h) TOTAL SUM (TOTAL)
        ///Data field: [{Text1}][{CR}{Text2}]{Tabulator}[[{Payment}] {Amount}]Reply: {PaidCode }{Amount}
        ///Text1 First line for printing– text
        ///CR One byte,value 0Ah(delimiter).
        ///Text2 Second line for printing –text(.see Table 7)
        ///Tab One byte,value 09h(delimiter).
        ///Payment Optional parameter, specifying method of payment.If you haven't chosen parameter nor Amount, the SLAVE usually enacts total payment in cash.
        ///Payment modes:
        ///“P” – In cash;
        ///“N” – payment 1
        ///“C” – payment 2
        ///“D” or “U” – payment 3
        ///“E” or “B” – payment 4
        ///Amount Sum of payment(up to 8 meaning digits)..
        ///PaidCode 1 byte – result of the command.
        ///“F” – error
        ///“I” – if the sum by a certain tax group is negative.
        ///“D” – if the sum, which is paid is less or equal to the Total sum of the receipt.
        ///“R” –if the sum, which is paid is greater than Total sum.
        ///“E” – negative SubTotal.
        ///.
        ///Amount Change(according to PaidCode).
        ///The SLAVE doesn't react to the command if:
        /// There is no open fiscal receipt;
        ///Note: The indicated texts are “cut” on the right side if they are longer than #COMMENT_LEN#
        ///Attention: when the command is completed successfully, FD will not allow making sales within this receipt.Error codes “E” and “I” are not valid for Gerogia.
        ///</summary>
        /// <returns></returns>
        public Reply TotalSum(string Data)
        {
            return SendCommand(53, Data);
        }

        /// <summary>
        /// 54 (36h) PRINT FISCAL TEXT
        /// Data field: Text
        /// Reply: No data
        /// Text Text for printing
        /// Fiscal receipt must be open in order to print the text.
        /// Note: The indicated texts are “cut” on the right side if they are longer than #COMMENT_LEN#
        /// </summary>
        public void PrintFiscalText(string Data)
        {
            SendCommand(54, Data);
        }

        /// <summary>
        /// 130 (82h) CANCEL RECEIPT
        /// Data field: No data
        /// Reply: Allreceipt, FiscReceipt
        /// Allreceipt Number of all issued receipts from the last balance of the day till now.
        /// FiscReceipt Number of all issued fiscal receipts from the last balance of the day till now.
        /// When receiving this command FD:
        /// 1. makes correction to all sales in the fiscal receipt;
        /// 2. completes operation payment in cash for 0.00;
        /// 3. closes fiscal receipt
        /// </summary>
        /// <returns></returns>
        public Reply CancelReceipt()
        {
            return SendCommand(130);
        }

        /// <summary>
        /// 56 (38h) END OF FISCAL RECEIPT
        /// Data field: No data
        /// Reply: Allreceipt, FiscReceipt
        /// Allreceipt Number of all issued receipts(non-fiscal and fiscal) since the last balance at the end of the day till now
        /// 13
        /// FiscReceipt Number of all issued fiscal receipts since the last balance at the end of the day till now
        /// The SLAVE doesn't react to the command if:
        ///  There is no open fiscal receipt;
        ///  The command"TOTAL SUM"(53) is not executed
        ///  The amount of TOTAL SUM(53) is less than Total Sum of the receipt.
        /// </summary>
        /// <returns></returns>
        public Reply EndOfFiscalText()
        {
            return SendCommand(56);
        }

        /// <summary>
        /// 109 (6Dh) PRINT DUPLICATE DOCUMENT
        ///        Data field: {Count}[, DocNo]
        ///    Reply: No data
        //DocNo Optional parameter, defines the number of the document of which a duplicate should be printed.If this field is not set, FD will print a copy of the last document printed.
        /// </summary>
        /// <param name="Data"></param>
        public void PrintDuplicateDocument(string Data)
        {
            SendCommand(109, Data);
        }

    }
}
