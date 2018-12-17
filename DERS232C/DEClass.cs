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

    public class DEClass
    {

        private SerialCom xSerial { get; set; }
        private SerialController xController { get; set; }

        public DEClass(string portName)
        {
            xSerial = new SerialCom(portName);
            xController = new SerialController(xSerial.Port);
        }
        

        internal string SendCommand(byte CMD, string Data = "")
        {
            return xController.SendCommand(CMD, Data);
        }


        /// <summary>
        /// Add Item
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Description"></param>
        /// <param name="Price"></param>
        /// <param name="VATGroup"></param>
        /// <returns></returns>
        public string AddItem(int ID, string Description, double Price, string VATGroup)
        {
            return this.SendCommand(0x6b, "P" + VATGroup + ID + "," + Price + "," + Description);
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
        /// PAPER CUT
        /// Data field: [Mode]
        /// Reply: Code
        /// Mode Optional parameter indicating cutting type. 1 = full cut, 2 = partial cut.
        /// Code One symbol indicating the result:
        /// “P” – command is successful.
        /// “F” – command is not successful.
        /// </summary>
        /// <param name="Code"></param>
        public string PaperCut(string Code)
        {
            return SendCommand(45, Code);
        }


        /// <summary>
        /// 62 (3Eh) GET DATE AND TIME INFORMATION
        ///Data field: No data.
        ///Date and time information is received from FD.
        /// </summary>
        /// <returns>String Reply: { DD–MM–YY}{Space}{HH:MM:SS}</returns>
        public string GetDateAndTimeInformation()
        {
            return SendCommand(62, "");
        }

        /// <summary>
        /// 50 (32h) GET TAX RATES
        /// Data field: [{StartDate},{EndDate}]
        /// Reply: Code,Tax1,Tax2,Tax3,Tax4,Tax5,Date
        /// StartDate StartDate of the period(DDMMYY) – 6 bytes.
        /// EndDate EndDate of the period(DDMMYY) – 6 bytes.
        /// Code One byte with value "F" – error or "P" – data is found in FM.
        /// Tax1 VAT rate by tax group 0
        /// Tax2 VAT rate by tax group A
        /// Tax3 VAT rate by tax group B
        /// Tax4 VAT rate by tax group C
        /// Tax5 VAT rate by tax group D
        /// Date Date on which rates are recorded in format DDMMYY
        /// FD prints a report for service records in FM for the indicated period.
        /// </summary>
        /// <param name="startDate">StartDate StartDate of the period(DDMMYY) – 6 bytes.</param>
        /// <param name="endDate">EndDate EndDate of the period(DDMMYY) – 6 bytes.</param>
        /// <returns></returns>
        public string GetTaxRates(DateTime startDate, DateTime endDate)
        {
            return SendCommand(50, startDate.ToString("ddMMyy") + endDate.ToString("ddMMyy"));
        }

        /// <summary>
        /// 90 (5Ah) DIAGNOSTIC INFORMATION
        /// Data field: {Calculate}
        /// Reply: {FirmwareRev}{Space}{FirmwareDate}{Space}{FirmwareTime},{ChekSum},{Sw},{Country},{SerNum},{FMNo}
        /// Calculate If “1”, it is calculating the control sum of EPROM(1 byte).
        /// FirmwareRev Version of Firmware(4 symbols).
        /// Space Space20h(1 byte).
        /// FirmwareDate Date of Firmware DDMМYY(6 byte).
        /// Space Space 20h(1 byte).
        /// FirmwareTime Time of Firmware HHMM(4 byte).
        /// ChekSum ChekSum of EPROM(4 byte hexadecimal string).
        /// Sw Switches.(4 digits)
        /// Country Number of country, for Georgia = 8
        /// SerNum MRC of FD(#MACHNO_LEN# symbols).
        /// FMNo Fiscal memory number of FD (#FMNO_LEN# symbols).
        /// </summary>
        /// <returns>Reply: {FirmwareRev}{Space}{FirmwareDate}{Space}{FirmwareTime},{ChekSum},{Sw},{Country},{SerNum},{FMNo}</returns>
        public string DiagnosticInformation(string Calculate = "1")
        {
            return SendCommand(90, "");
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
        public string EndOfNonFiscalReceipt()
        {
            return SendCommand(39);
        }

        /// <summary>
        /// 43 (2Bh) CLICHÉ AND PRINTING OPTIONS
        /// Data field: {Option}{Text}
        /// Reply: According to data field
        /// Option 1 symbol, which meaning is:
        /// “0”(30Hex) to “=”(3DHex) – number of line which is set(HEADER lines are with numbers from 0 to 7, аnd for FOOTER – 8 to 13).
        /// “P” – Set Print options.
        /// “L” – printing of graphic logo.
        /// “C” – automatic paper cutting at the end of the document (if FD has a cutter).
        /// “А” – detailed print of client receipt.
        /// “I” –gives the opportunity to read the values, set earlier by this command.The next symbol after letter “I”follows only one symbol which coincides with the ///ones mentioned above.
        /// Text Text
        /// If {Option} is digit from “0” to “;” – the text of the the relevant line.
        /// Note: The text will be cut on the right side if it is longer than #CHARS_PER_LINE#
        /// If {Option} = “P” – 4 symbols with value “0”=disables or “1”= enables the corresponding option.
        /// Options according to symbols:
        /// [1] A blank line will be printed after HEADER
        /// [2] A blank line will be printed after REGNO
        /// [3] A blank line will be printed after FOOTER
        /// [4] Delimiter line will be printed before total sum
        /// If{ Option} = “L” – One symbol with value “0” or “1”, which forbids or allows the printing of graphical logo.
        /// If {Option} = “A” – One symbol with value “0”= forbids or “1”= allows detailed printing of client receipt.
        /// If
        /// { Option } = “C” – One symbol with value “0”= forbids automatic cutting or “1”= allows fully cutting at the end of the receipt; or “2” = allows partial cutting at the end of the receipt.
        /// </summary>
        /// <returns></returns>
        public string PrintingOptions(string Data)
        {
            return SendCommand(43, Data);
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
        public string StartOfFiscalReceipt(string Data)
        {
            return SendCommand(48, Data);
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
        /// 50 (32h) GET TAX RATES
        /// Data field: [{StartDate},{EndDate}]
        /// Reply: Code,Tax1,Tax2,Tax3,Tax4,Tax5,Date
        /// StartDate StartDate of the period(DDMMYY) – 6 bytes.
        /// EndDate EndDate of the period(DDMMYY) – 6 bytes.
        /// Code One byte with value "F" – error or "P" – data is found in FM.
        /// Tax1 VAT rate by tax group 0
        /// Tax2 VAT rate by tax group A
        /// Tax3 VAT rate by tax group B
        /// Tax4 VAT rate by tax group C
        /// Tax5 VAT rate by tax group D
        /// Date Date on which rates are recorded in format DDMMYY
        /// FD prints a report for service records in FM for the indicated period.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string GetTaxRates(string Data)
        {
            return SendCommand(50, Data);
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
        public string Subtotal(string Data)
        {
            return SendCommand(51, Data);
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
        public string TotalSum(string Data)
        {
            return SendCommand(52, Data);
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
        public string EndOfFiscalText()
        {
            return SendCommand(56);
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
        /// 61 (3Dh) SET DATE AND TIME
        /// Data field: {DD–MM–YY}{space}{HH:MM[:SS]}
        /// Reply: No data
        /// Attention:You can't enter date which is earlier than the last date stored in the fiscal memory or the last document, saved in EJT.
        /// </summary>
        public void SetDateAndTime(string Data)
        {
            SendCommand(61, Data);
        }



        /// <summary>
        /// 64 (40h) FINAL FISCAL RECORD
        /// Data field: [Type]
        /// Reply: Number,SpaceGr,Tax1,Tax2,Tax3,Tax4,Tax5,Date
        /// Type Not compulsory parameter indicating type of the returned data: “T”- amount with VAT(total) and “N”-amount without VAT(net). By default is“N”
        /// Number Number of the last fiscal record.
        /// SpaceGr Session Non-Taxable sales Total.
        /// Tax1 Session Taxable1 sales Total
        /// Tax2 Session Taxable2 sales Total
        /// Tax3 Session Taxable3 sales Total
        /// Tax4 Session Taxable4 sales Total
        /// Tax5 Session Taxable5 sales Total
        /// Date Date of the last fiscal record(6 symbols DDMMYY).
        /// This command returns information from FD for the last daily financial report stored in the fiscal memory
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string FinalFiscalRecord(string Data)
        {
            return SendCommand(64, Data);
        }


        /// <summary>
        /// 65 (41h) CURRENT NET/TOTAL SUMS
        /// Data field: [Type]
        /// Reply: SpaceGr,Tax1,Tax2,Tax3, Tax4,Tax5
        /// Type
        /// Not compulsory parameter indicating type of the returned data: “T”- amount with VAT(total) and “N”-amount without VAT(net). By default is“N”
        /// SpaceGr Session Non-Taxable sales Total.
        /// Tax1 Session Taxable1 sales Total
        /// Tax2 Session Taxable2 sales Total
        /// Tax3 Session Taxable3 sales Total
        /// Tax4 Session Taxable4 sales Total
        /// Tax5 Session Taxable5 sales Total
        /// The command returns the accumulated sums from the last balance of the day to the moment by all tax groups.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string CurrentNetTotalsSum(string Data)
        {
            return SendCommand(65, Data);
        }


        /// <summary>
        /// 68 (44h) FREE FISCAL RECORDS
        /// Data field: No data
        /// Reply: Logical, Physical
        /// Logical Number of all free blocks for record of the daily reports in the fiscal memory
        /// Physical Repeats the former record.
        /// Returns information for the number of free records in fiscal memory.
        /// </summary>
        /// <returns></returns>
        public string FreeFiscalRecords()
        {
            return SendCommand(68);
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
        public string DisplayFinancialreportWithOrWithoutClearing(string Data)
        {
            return SendCommand(69, Data);
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
        public string ServiceInputSumAndServiceOutPutSum(string Data)
        {
            return SendCommand(70, Data);
        }

        /// <summary>
        /// 71 (47h) PRINT DIAGNOSTIC INFORMATION
        // Data field: No data
        //  Reply: No data
        //  A service receipt with diagnostic information is printed.It includes:
        //   Date and version of software;
        //   Firmware control sum;
        //   Serial port communication speed;
        //   Swithes;
        //   Other information
        /// </summary>
        public void PrintDiagnosticInformation()
        {
            SendCommand(71);
        }


        /// <summary>
        /// 73 (49h) REPORT FROM FM BY NUMBER
        /// Data field: {StartNum},{EndNum}
        /// Reply: No data
        /// StartNum Start Number of fiscal record with length 4 symbols.
        /// EndNum End Number of fiscal record with length 4 symbols.
        /// FD is printing detailed report of the fiscal memory(FM).
        /// Note: This command is not allowed, if ther is not entered TAX Authority Password(see command C0Hex)
        /// </summary>
        /// <param name="Data"></param>
        public void ReportFromFMByMember(string Data)
        {
            SendCommand(73, Data);
        }

        /// <summary>
        /// 74 (4Ah) FD STATUS
        /// Data field: No data
        /// Reply: {S0}{S1}{S2}{S3}{S4}{S5}
        /// Sn Status Byte N.
        /// </summary>
        /// <returns></returns>
        public string FDStatus()
        {
            return SendCommand(74);
        }


        /// <summary>
        /// 76 (4Ch) FISCAL REPORT STATUS
        /// Data field: [Option]
        /// Reply: Open,Items,Amount[, Tender, Remainder]
        /// Option “T” – The command returns information for the last made payment.
        /// Open “0” – There is no open receipt
        /// “1” – There is open fiscal receipt.
        /// Items Number of sales, which have been made and registered in the current or the final fiscal receipt.
        /// Amount Total sum of the final fiscal receipt (10 digits).
        /// Tender The amount included in the current receipt or in the final receipt..
        /// Remainder The amount due to be paid in the current or final receipt.
        /// This command enables PC application to determine the status of FD and if it is necessary to restore and complete a fiscal operation, which is interrupted by emergency, for example in case of power supply failure.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string FiscalReportStatus(string Data)
        {
            return SendCommand(76, Data);
        }


        /// <summary>
        /// 79 (4Fh) BRIEF REPORT FROM FM BY DATE
        ///        Data field: {StartDate},{EndDate}
        ///Reply: No data
        ///StartDate StartDate with length 6 symbols(DDMMYY)
        ///EndDate EndDate with length 6 symbols(DDMMYY)
        ///The command executes summary report by date from FM.
        ///Note: This command is not allowed, if ther is not entered TAX Authority Password(see command C0Hex)
        /// </summary>
        /// <param name="Data"></param>
        public void BreifReportFromFMByDate(string Data)
        {
            SendCommand(79, Data);
        }


        /// <summary>
        /// 94 (5Eh) REPORT FROM FM BY DATES
        /// Data field: {StartDate},{EndDate}
        /// Reply: No data
        /// StartDate StartDate of fiscal record(6 symbols DDMMYY).
        /// EndDate EndDate of fiscal record(6 symbols DDMMYY).
        /// After using this command, the FD prints Detailed Financial Report for the period from date to date.
        /// Note: This command is not allowed, if ther is not entered TAX Authority Password (see command C0Hex)
        /// </summary>
        /// <param name="Data"></param>
        public void ReportFromFMByDates(string Data)
        {
            SendCommand(94, Data);
        }

        /// <summary>
        /// 95 (5Fh) BRIEF REPORT FROM FM BY NUMBER
        /// Data field: {StartNum},{EndNum}
        /// Reply: No data
        /// StartNum Start Number of fiscal record.
        /// EndNum End Number of fiscal record
        /// Brief report from FM is executed by intially set Start Number of fiscal record and End Number of fiscal record.
        /// Note: This command is not allowed, if ther is not entered TAX Authority Password (see command C0Hex)
        /// </summary>
        /// <param name="Data"></param>
        public void BreifReportFromFMByNumber(string Data)
        {
            SendCommand(95, Data);
        }

        /// <summary>
        /// 96 (60h) TAX RATES PROGRAMMING
        /// Data field: {Tax1},{Tax2},{Tax3},{Tax4},{Tax5}
        /// Reply: Code
        /// Tax1 Tax rate by tax group with code Space(number up to 2 signs after decimal point)
        /// Tax1 Tax rate by tax group with code A(number up to 2 signs after decimal point)
        /// Tax2 Tax rate by tax group with code B(number up to 2 signs after decimal point)
        /// Tax4 Tax rate by tax group with code C(number up to 2 signs after decimal point)
        /// Tax5 Tax rate by tax group with code D(number up to 2 signs after decimal point)
        /// Code 1 byte, including
        /// “P“ – no error.
        /// “F” – error
        /// Attention:The tax rate values are from 0.00 to 99.99. Acceptable value is “-1”, which means that the relevant tax group will not be permitted for sale.If all 5 values haven’t been set, the rest rates will keep the value which they have had before the execution of this command..
        /// </summary>
        public string TaxRatesProgramming(string Data)
        {
            return SendCommand(96, Data);
        }

        /// <summary>
        /// 97 (61h) CURRENT TAX RATES
        /// Data field: No data
        /// Reply: Tax1,Tax2,Tax3,Tax4,Tax5
        /// Tax 1 Tax rate Space
        /// Tax 2 Tax rate A
        /// Tax 3 Tax rate B
        /// Tax 4 Tax rate C
        /// Tax 5 Tax rate D
        /// 18
        /// Receiving information about the current tax rates.
        /// </summary>
        /// <returns></returns>
        public string CurrentTaxRates()
        {
            return SendCommand(97);
        }


        /// <summary>
        /// 101 (65h) SET OPERATOR PASSWORD
        /// Data field: {OperatorNumber},{OldPsw},{NewPsw}
        /// Reply: No data
        /// OperatorNumber Number of operator from 1 to #OPER_MAX_CNT#
        /// OldPsw Old password
        /// NewPsw New password.
        /// Passwords can be entered maximum for #OPER_MAX_CNT# operators. The command must be transmitted while opening FISCAL RECEIPT.
        /// Attention:
        /// 1. If it is set password 0, this operator is not allowed to work.
        /// 2. It is forbidden to enter one and the same different nonzero passwords for two or more different operators.
        /// Passwords by default: see "User’s manual"
        /// </summary>
        /// <param name="Data"></param>
        public void SetOperatorPassword(string Data)
        {
            SendCommand(101, Data);
        }


        /// <summary>
        /// 102 (66h) SET OPERATOR’S NAME
        /// Data field: {OperatorNum},{Password},{Name}
        /// Reply: No data
        /// OperatorNum Number of operator from 1 to #OPER_MAX_CNT#.
        /// Password Password.
        /// Name Name of operator . It is "cut" on the right side, if it is longer than #NAME_LEN# symbols.
        /// Operator name is programmed.His name and number are printed at the beginning of each FISCAL RECEIPT.
        /// Attention: If the operator has made operations (has issued at least one document), his name can not be changed until the sales by this operator are cleared.
        /// </summary>
        /// <param name="Data"></param>
        public void SetOperatorName(string Data)
        {
            SendCommand(102, Data);
        }

        /// <summary>
        /// 103 (67h) INFORMATION INCLUDED IN THE RECEIPT
        /// Data field: No data
        /// Reply:
        /// CanVoid,TaxFree,Tax1,Tax2,Tax3,Tax4,Tax5,InvoiceFlag,InvoiceNo
        /// CanVoid Indicating the possibility to make corrections. [0/1].
        /// Tax_Free Total of Non-Taxable sales
        /// Tax1 Total of Taxable Space sales
        /// Tax2 Total of Taxable A sales
        /// Tax3 Total of Taxable B sales
        /// Tax4 Total of Taxable C sales
        /// Tax5 Total of Taxable D sales
        /// InvoiceFlag Flag whether is open a detailed fiscal receipt(invoice). For Georgia = 0
        /// InvoiceNo 10 digits 0
        /// This command indicates if it is possible to correct registered sales and gives information for the accumulated sums by tax groups.
        /// </summary>
        /// <returns></returns>
        public string InformationIncludedInTheReceipt()
        {
            return SendCommand(103);
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


        /// <summary>
        /// 105 (69h) REPORT BY OPERATORS
        /// Data field: No data
        /// Reply: No data
        /// FD executes X report by operator
        /// </summary>
        public void ReportByOperators()
        {
            SendCommand(105);
        }


        /// <summary>
        /// 107 (6Bh) PROGRAMMING, ERASING AND READING DATA FOR PLUs
        ///        Data field: {Item}[Data]
        ///    Reply: Code[, Data]
        ///    Code 1 symbol indicating the result:
        ///“P” – valid command.
        ///“F” – invalid command.
        ///Item Setting the required operation.Acceptable values are: “P’, “D”, “R”, “F”, “N”.
        ///“P” – Plu’s programming
        ///Data field: { P}{TaxGroup
        ///}{PLUNum},{Price},{Name}[{CR}{BARCODE}
        ///[, Dept[, FracQty[, StockQty]]]]
        ///TaxGroup TaxGroup(Space,’A’,’B’,’C’,’D’).
        ///PLUNum Serial PLU’s number.
        ///Price Unit price of PLU (up to 8 digits).
        ///Name Name of PLU.
        ///CR 1byte with value 0Ah.
        ///BARCODE Barcode of PLU. Transmission of up to13 bytes in ASCII format (33h 38h 30h 30h 30h 30h 31h 31h 30h 31h 38h 31h 35h).
        ///Dept Number of department to which the PLU refers.
        ///FracQty Is is not used.For compatibility set 1
        ///StockQty PLU on stock
        ///After Code the number of free PLU is returned.
        ///Attention: Tax group of PLU is not kept in the record for every PLU, but is taken from the linked to it department.This leads to the following features:
        ///- If the PC sets tax group and department, FD will ignore the set tax group from the PC.
        ///- If during programming a new PLU, PC sets a tax group without a department, FD will “attach" PLU to a service department, and will forbid the sale of this PLU manual.
        ///- If you reprogram the tax group of the department , you automatically re-program the tax groups of all PLUs attached to this department.
        ///“D” – Deleting PLUs..
        ///                                                                                          Data field: {D}{A|PLU|PLU1,PLU2}
        ///A Deleting all PLUs with zero accumulated sums
        ///PLU Deleting the indicated number of PLU, if there is no accumulated sums.
        ///PLU1,PLU2 Deleting PLUs in the increment period with no accumulated sums.
        ///When the command is successfully completed”P” is returned and number of free PLUs
        ///“R” – reading PLU data.
        ///Data field: {R}{PLU}
        ///Reply: {{P},{PLU},{Time},{TaxGroup},{UnitPrice},{SaleQty},{SaleAmount},{RefQty},{RefAmount},
        ///{Name}{CR}{BARCODE},Dept,FracQty,StockQty
        ///PLU Number of PLU
        ///Time Date and time of the last PLU programming
        ///TaxGroup TaxGroup(1 byte).
        ///UnitPrice Unit price.
        ///SaleQty Accumulated quantity for sales.
        ///SaleAmount Accumulated sale sum.
        ///SaleQty Accumulated quantity for refund.
        ///SaleAmount Accumulated refund sum.
        ///Name PLU name.
        ///CR 1byte with value 0Ah (delimiter).
        ///BARCODE ASCII string,length up to 13 digits.
        ///Dept Number of department to which the PLU refers.
        ///FracQty It is not used.Always equal to 1.
        ///StockQty Stock quantity of PLU
        ///If the indicated number of PLU is beyond of the acceptable values ( from 1 to до #PLU_MAX_CNT#), one byte “F” is returned.
        ///“F” – retrieves data about the first found programmed or sold PLU.
        ///Data field: { F}
        ///[Type]
        ///Type - Type - Type of searching:
        ///0 - /Default value/ -searching the first programmed PLU.It influences the following commands “N”.
        ///1 – searching the first PLU by which sales are registrated.It influences the following commands “N”.
        ///“N” – returns data for the first found programmed or sold PLU(according to the type of command “F”)
        ///Data field: {F}
        ///The reply of subcommands “F” и “N” is analogical to subcommand “R”.
        ///The HOST uses the last 2 for extraction of data for all programmed/sold PLUs.First you have to set subcommand “F” and then subcommand ”N” till Reply ”F”is received..This means that the last PLU has been read.

        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string ProgrammingErasingAndReadingDataForPLUs(string Data)
        {
            return SendCommand(107, Data);
        }

        /// <summary>
        /// 108 (6Ch)DETAILED DAILY REPORT WITH PRINTING OF PLUs
        /// Data field: {Option}
        /// Reply: Closure, SpaceGr,Tax1,Tax2,Tax3,Tax4,Tax5
        /// The command is analogical to “DAILY FINANCIAL REPORT” (69), but at the very start of the report there is printing of sales by PLUs.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string DetailedDailyReportWithPrintingOfPLUs(string Data)
        {
            return SendCommand(108, Data);
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


        /// <summary>
        /// 110 (6Eh) INFORMATION ABOUT CURRENT DAY
        /// Data field: [All]
        /// Reply: Cash, Pay1, ....PayX, ZRepNo, DocNo, InvoiceNo
        /// All Optional parameter(1 byte).If it set equal to "A", FD returns informationby all
        /// types of payments(#PAY_MAX_CNT#), if not - by 4
        /// Cash Payment in cash.
        /// Pay1 Sum Payment 1.
        /// .........
        /// .........
        /// PayX SumPayment X
        /// ZRepNo Number of the last Daily financial report written in FM.
        /// DocNo Number of next document.
        /// Receipt Number of next invoice(10 digits 0).
        /// When this command is received FD returns information about the daily amount distribution by types of payment.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string PrintInformationAboutCurrentDay(string Data)
        {
            return SendCommand(110, Data);
        }


        /// <summary>
        /// 111 (6Fh) REPORT BY PLUs
        /// Data field: {RepType}
        /// Reply: Code
        ///    RepType Specifies the type of printed information.Possible values:
        ///“0” – FD is printing X report by PLUs.
        ///    “1” – FD is printing all programmed PLUs
        ///“Z” – FD is printing Z report by PLUs
        ///    If it is not indicated in other way, value “0” is set.
        ///    Code One byte with the following options:
        ///"P" – when the operation is successfully completed
        ///"F" - when the operation is unsuccessfully completed
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string ReportByPLUs(string Data)
        {
            return SendCommand(111, Data);
        }


        /// <summary>
        /// 112 (70h) INFORMATION ABOUT OPERATORS
        /// Data field: Operator
        /// Reply: Receipts,Total,Discount,Surcharge,Void,Name
        /// Operator Indicating operator number from 1 to #OPER_MAX_CNT#.
        /// Receipts Indicating number of fiscal receipts which were issued by a certain operator.
        /// Total Indicating number of sales and total sum divided by ‘;’.
        /// Discount Number of discounts and total amount of discounts , divided by ‘;’.
        /// Surcharge Number of surcharges and total amount of surcharges , divided by ‘;’.
        /// Void Number of corrections and total amount, divided by ‘;’.
        /// Name Operator name.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string InformationAboutOperators(string Data)
        {
            return SendCommand(112, Data);
        }

        /// <summary>
        /// 113 (71h) FINAL DOCUMENT NUMBER
        /// Data field: No data
        /// Reply: DocNo
        /// DocNo Number of last printed document.
        /// </summary>
        /// <returns></returns>
        public string FinalDocumentNumber()
        {
            return SendCommand(113);
        }

        /// <summary>
        /// 114 (72h) DATA FROM FM BY NUMBER
        /// Data field: {FiscNum}[,{Options}[, FiscNum1]]
        ///Reply: Code,Tax1,Tax2,Tax3,Tax4,Tax5
        ///FiscNum number of fiscal record(start date).
        ///Options Specifies the type of the received data(1 byte):
        ///= ‘0’ – returning the sums by tax groups.
        ///= ‘1’ – returning the net sums by tax groups.
        ///= ‘2’ – returning the accumulated taxes by tax groups.
        ///= ‘3’ – returning tax bases.
        ///= ‘4’ – returning the sums by tax groups for the indicated period
        ///= ‘5’ – returning the net sums by tax groups for the indicated period.
        ///= ‘6’ – returning the accumulated taxes by tax groups for the indicated period.
        ///FiscNum1 Number of fiscal record (end date) for check up “4”, ”5” and “6”. For check up 0, 1, 2 и 3, the field must be empty.
        ///Code 1 byte with value:
        ///“P” – valid data;
        ///“F” – invalid control sum of the record;
        ///“E” – the chosen record is empty in FM
        ///.
        ///Tax1,…,Tax5 Sum or percentage according to Options.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string DataFromFMByMember(string Data)
        {
            return SendCommand(114, Data);
        }


        /// <summary>
        /// 115 (73h) LOADING of GRAPHIC LOGO
        /// Data field: {Number},{Data}
        /// Reply: No data
        /// Number Indicates the number of bitmap lines,which are in process of programming.Values from 0 to(#BITMAP_Y# - 1).
        /// Data Graphical Logo data in pixels.Set in hexadecimal type. 2 symbols for each data byte (B8h  42h 38h). The maximumu number set pixels is #BITMAP_X#. By using this ///command, a graphical Logo is programmed (BMP) upon request of the customer.
        /// Graphical Logoprinting is activated by command(43).
        /// Attention: In order to determine the content of whole graphical logo, the command must be executed #BITMAP_Y# times (once for each line).
        /// </summary>
        /// <param name="Data"></param>
        public void LoadingGraphicalLogo(string Data)
        {
            SendCommand(115, Data);
        }



        /// <summary>
        /// 128 (80h) GET CONSTANT VALUES
        /// Data field: No data
        /// Reply: {P1},{ P2},{......},{P25}
        /// P1 Horizontal size of Graphical Logo in pixels.
        /// P2 Vertical size of Graphical Logo in pixels..
        /// P3 Number of payment types
        /// P4 Number of tax group.
        /// P5 Letter for non-taxable items (= 20h)
        /// P6 It is not used for Georgia
        /// P7 Symbol concerning first tax group
        /// P8 Dimension of inner arithmetics
        /// P9 Number of symbols per line..
        /// P10 Number of symbols per comment line
        /// 23
        /// Р11 Length of names (operators, PLUs, departments).
        /// Р12 Length(number of symbols)of the MRC of FD
        /// Р13 Length(number of symbols)of the Fiscal Memory Number
        /// Р15 Length(number of symbols)of REGNO
        /// Р16 Number of departments.
        /// Р17 Number of PLUs.
        /// Р18 Flag of stock field, described in PLU (0,1).
        /// Р19 Flag of barcode field,described in PLU(0,1).
        /// Р20 Number of stock groups.
        /// Р21 Number of operators..
        /// Р22 Length of the payment names
        /// </summary>
        /// <returns></returns>
        public string GetConstantValues()
        {
            return SendCommand(128);
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
        public string CancelReceipt()
        {
            return SendCommand(130);
        }


        /// <summary>
        ///131 (83h) PROGRAM DEPARTMENTS
        /// Data field: {Item}[Data]
        /// Reply: Code[, Data]
        /// Code 1 symbol, indicating the result::
        /// “P” – valid command.
        /// “F” – invalid command.
        /// Item Specifies the type of required operation.Acceptable values: “P” and“R”.
        /// “P” – programming departments
        /// Data field: { P}{DeptNum
        /// },{Name}{CR}{TaxGroup},{Price},{MaxDigits}
        /// DeptNum Number of department(from 1 to #DEPT_MAX_CNT#)
        /// Name Name of department.It is "cut" at the right side, if it is longer than #NAME_LEN#symbols.
        /// CR 1 byte with value 0Ah.
        /// TaxGroup Tax group. 1 byte of (Space,’A’,’B’,’C’,’D’)
        /// Price Unit price of department(up to 8 digits).It is not used.For compatibility send 0.
        /// MaxDigits Maximum digits of entered free price
        /// Attention: You can not change the name or tax group by department, to which thre are sales without issueing the corresponding Z reports.Check the error number(status byte/// 3), and issue the necessary "Z" report
        /// “R” – Reading data about department.
        /// Data field: { R}{DeptNum}
        /// Reply: {P},{DeptNum},{Name}{CR}{TaxGroup},{Price},{Amount},
        /// {Total},{PerAmount},{PerTotal},{RefAmount},{RefTotal},{MaxDigits}
        ///     The information is anallogical to the command for programming departments.
        /// Amount Accumulated quantity at sale by this department
        /// Total Accumulated amount from registrated sale by this department.
        /// PerAmount For Ethiopia = 0;..
        /// PerTotal For Ethiopia = 0;..
        /// RefAmount Accumulated quantity at refund by this department (For Georgia = 0)
        /// RefTotal Accumulated amount from registrated refunds by this department (For Georgia = 0)
        /// If the indicated number of department is not found, FD returns Code = ‘F”.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string ProgrammingDepartments(string Data)
        {
            return SendCommand(131, Data);
        }


        /// <summary>
        /// 146 (92h) INFORMATION FROM FM BY DATE
        /// Data field: {FiscDateBgn}[,{Options}[, FiscDateEnd]]
        /// Reply: Code, Tax1,Tax2,Tax3,Tax4,Tax5
        /// FiscDateBgn Date of fiscal record(start of period) in format DDMMYY.
        /// Options Determines type of the received information (1 byte):
        /// = ‘0’ – returns the sums by tax groups.
        /// = ‘1’ – returns the net sums by tax groups.
        /// = ‘2’ – returns the accumulated taxes by tax groups.
        /// = ‘3’ – returns tax rates.
        /// = ‘4’ – returns the sums by tax groups for the indicated period.
        /// = ‘5’ – returns the net sums by tax groups for the indicated period.
        /// = ‘6’ – returns the accumulated taxes for the indicated period.
        /// FiscDateEnd Date of fiscal record (end of period) in format DDMMYY for reference “4”, ”5” and “6”. For reference 0, 1, 2 and 3 this field must be empty.
        /// Code One byte with value:
        /// “P” – correct data;
        /// “F” – incorrect control sum of record;
        /// “E” – in FM there is no data for the indicated period.
        /// Tax1,…, Tax5 Sum or percentage depending on Options.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string InformationFromFMByDate(string Data)
        {
            return SendCommand(146, Data);
        }


        /// <summary>
        /// 149 (95h) PROGRAMMING TEXT
        /// Data field: {Item}[Data]
        ///    Reply: [Data]
        ///    Item Determines the type of the required operation..Acceptable values: “P” and “R”.
        ///This command is used for programming and reading text
        ///“P” – programming text.
        ///Data field: { P}{Number
        ///},{Text}
        ///“R” – reading text.
        ///Data field: { R}{Number}
        ///Reply {Text}
        ///Number Number, which meaning is:
        ///40 to 53 – (Number – 40) – number of HEADER line/ FOOTER line
        ///The text is limited to #CHARS_PER_LINE# symbols
        ///60 to 64 – name of payment
        ///The text is limited to #PAYNAME_LEN#symbols
        ///600 to 610 – commentary lines
        ///The text is limited to #COMMENT_LEN# symbols
        ///Text Text
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string ProgrammingText(string Data)
        {
            return SendCommand(149, Data);
        }


        /// <summary>
        /// 150 (96h) SET SYSTEM PARAMETERS
        ///    Data field: {Item}[Data]
        ///    Reply: [Data]
        ///    Item Determines the type of the required operation..Acceptable values: “P’ and “R”.
        ///“P” – programming value parameters.
        ///    Data field: { P}{Number},{Value}
        ///“R” – programming value parameters.
        ///Data field: {R}{Number}
        ///Reply {Number},{Value}
        ///Number Number of system parameter(see User’s manual).
        ///Value Parameter value.The specific meaning depends on the type of parameter.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string SetSystemParameters(string Data)
        {
           return SendCommand(150, Data);
        }


        /// <summary>
        /// 151 (97h) SET PAYMENTS (CURRENCIES)
        /// Data field: {Item}{Data}
        /// Reply: {Data}
        /// Item Determines the type of the required operation.Acceptable values: “P” and “R”.
        /// “P” – programming name and currency rate of payment.
        /// Data field: { P}{Number},{Name}{TAB}{Rate}
        /// “R” –reading parameter value.
        /// Data field: {R}{Number}
        /// Reply {Number},{Name}{TAB}{Rate}
        /// Number Payment number(from 1 to #PAY_MAX_CNT#)
        /// Name Payment name. It is "cut" at the right side, if it is longer than #PAYNAME_LEN# symbols.
        /// TAB Delimiter (ASCII 09)
        /// Rate Currency rate.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string SetPaymentCurrencies(string Data)
        {
            return SendCommand(151, Data);
        }


        /// <summary>
        /// 153(99h) SEND REPORTS IN TEXT TYPE
        ///Data field: No data
        ///Reply: No data
        ///After receiving this command, if within 5 seconds FD does not receive any new command related to report, then the indicated report is completed, and instead of printing FD sends the data by RS line by line and after every line waits for confirmation from the PC that it is accepted.
        ///Data structure from FD to PC:
        /// <SUB><SndLineNo><TAB><Font><TAB><Text><LF><CR>
        /// or
        /// <SUB><TAB><Font><LF><CR>
        /// <SUB> One byte with value 1Ah
        /// <SndLineNo> Serial number of line in the report
        /// <TAB> Delimiter.One byte with value 09h
        /// 26
        /// <Font> Type of data, one symbol with possible values:
        /// “N”,”B”or”E”
        /// “N”(Normal) – normal print.Field<Text> contains #CHARS_PER_LINE# symbols
        /// ”B”(Bold) – print with double width of letters.The field<Text> contains (#CHARS_PER_LINE#/2) symbols
        /// ”E”(Empty) – empty line(end of data transerring). No field<Text>
        /// <Text> Text on the corresponding line
        /// <LF> One byte with value 0Dh
        /// <CR> One byte with value 0Ah
        /// After sending every line FD waits for confirmation from PC for its accepting. As unpacked one byte code message with code 1Ah.
        /// When there is no confirmation, as well as when turning off the power supply, FD stops sending data to PC.
        /// List of commands for reports,
        /// To which FD conforms with the preliminary accepted command 99h
        /// 50 (32h) RECEIVING TAX RATES
        /// 73 (49h) REPORT FROM FM BY NUMBER
        /// 79 (4Fh) BRIEF REPORT FROM FM BY DATE
        /// 94 (5Eh) REPORT FROM FM BY DATE
        /// 95 (5Fh) BRIEF REPORT FROM FM BY NUMBER
        /// 105 (69h) REPORT BY OPERATORS
        /// 111 (6Fh) REPORT BY PLU
        /// Note: FD will return error, if will set RepType = “Z”, it means., it can’t be completed Z report by PLUs
        /// 165 (A5h) REPORT BY DEPARTMENTS
        /// Note: FD will return error if it is set RepType = “Z”, т.е., it means., it can’t be completed periodical Z report by departments
        /// 166 (A6h) PRINTING SYSTEM PARAMETERS
        /// After sending all lines from the report, FD replies to the PC with the corresponding reply command.
        /// For example:
        /// In order to receive system parameters by RS, you have to send command 99h, followed by command A6h
        /// In order to receive by RS brief report from FM from 1 January 2008 to 31 December 2009, you have to send command 99h, followed by command 4Fh with data 010107,311208
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string SendReportsInTextType(string Data)
        {
            return SendCommand(153, Data);
        }


        /// <summary>
        /// 165 (A5h) REPORT BY DEPARTMENTS
        ///        Data field: [RepType]
        ///        Reply: Code
        ///        RepType Determines the type of the printed information.
        ///        Acceptable values:
        ///“0” – FD prints X report by departments.
        ///“1” – FD prints the programmed data for all departments
        ///“X” FD prints periodical X report by departments.
        ///“Z” – FD prints periodical Z report by departments.
        ///If it is not indicated value “0” is accepted.
        ///Code One byte with the following options:
        ///"P" –when the operation is completed successfully
        ///"F" - when the operation is not completed successfully
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string ReportByDepartments(string Data)
        {
            return SendCommand(165, Data);
        }


        /// <summary>
        /// 166 (A6h) PRINT SYSTEM PARAMETERS
        /// Data field: No data
        /// Reply: Code
        /// Code One byte with the following options:
        /// "P" –when the operation is completed successfully
        /// "F" - when the operation is not completed successfully
        /// FD prints number, name and system paraters’ values.
        /// </summary>
        /// <returns></returns>
        public string PrintSystemParameters()
        {
           return SendCommand(166);
        }


        /// <summary>
        /// 176(B0h) PRINT CURRENT TAX RATES
        ///        Data field: No data
        ///Reply: No data
        ///FD issues a service document, containig current tax rates values.
        /// </summary>
        public void PrintCurrentTaxRates()
        {
            SendCommand(176);
        }

        /// <summary>
        /// 192 (C0h) ENTERING TAX AUTHORITY PASSWORD
        ///Data field: [TaxAuthorityPassword]
        ///Reply: Code
        ///TaxAuthorityPassword Tax Authority Password
        ///Code “P” = Pass if password is correct, or “F” if password is not correct.If a wrong password is entered 3 times in a row, FD needs to be restarted (turned off and turned on again) to continue working.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string EnteringTaxAutherityPassword(string Data)
        {
            return SendCommand(192, Data);
        }


        /// <summary>
        /// 229(E5h) RESTORE DEFAULT PASSWORDS
        ///        Data field: No data
        ///Reply: Code
        ///Code One symbol which indicates the result:
        ///“P” – the command is completed successfully.
        ///“F” – the command is completed unsuccessfully.
        ///The command is used to restore the operators’ passwords.It can be completed only if JUMPER2 is put.
        /// </summary>
        /// <returns></returns>
        public string RestoreDefaultPasswords()
        {
            return SendCommand(229);
        }
    }
}
