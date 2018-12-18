using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public class Reports : Communicator
    {
        public Reports(string portName) : base(portName)
        {
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
        public Reply GetTaxRates(string Data)
        {
            return SendCommand(50, Data);
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
        public Reply ReportByPLUs(string Data)
        {
            return SendCommand(111, Data);
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
        public Reply GetTaxRates(DateTime startDate, DateTime endDate)
        {
            return SendCommand(50, startDate.ToString("ddMMyy") + endDate.ToString("ddMMyy"));
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
        public Reply ReportByDepartments(string Data)
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
        public Reply PrintSystemParameters()
        {
            return SendCommand(166);
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
        public Reply SendReportsInTextType(string Data)
        {
            return SendCommand(153, Data);
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
    }
}
