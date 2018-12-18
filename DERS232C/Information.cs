using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public class Information : Communicator
    {
        public Information(string portName) : base(portName)
        {
        }

        /// <summary>
        /// 62 (3Eh) GET DATE AND TIME INFORMATION
        ///Data field: No data.
        ///Date and time information is received from FD.
        /// </summary>
        /// <returns>String Reply: { DD–MM–YY}{Space}{HH:MM:SS}</returns>
        public Reply GetDateAndTimeInformation()
        {
            return SendCommand(62, "");
        }

        /// <summary>
        /// 74 (4Ah) FD STATUS
        /// Data field: No data
        /// Reply: {S0}{S1}{S2}{S3}{S4}{S5}
        /// Sn Status Byte N.
        /// </summary>
        /// <returns></returns>
        public Reply FDStatus()
        {
            return SendCommand(74);
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
        public Reply CurrentNetTotalsSum(string Data)
        {
            return SendCommand(65, Data);
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
        public Reply FinalFiscalRecord(string Data)
        {
            return SendCommand(64, Data);
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
        public Reply FreeFiscalRecords()
        {
            return SendCommand(68);
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
        public Reply FiscalReportStatus(string Data)
        {
            return SendCommand(76, Data);
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
        public Reply DiagnosticInformation(string Calculate = "1")
        {
            return SendCommand(90, "");
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
        public Reply CurrentTaxRates()
        {
            return SendCommand(97);
        }

        /// <summary>
        /// 113 (71h) FINAL DOCUMENT NUMBER
        /// Data field: No data
        /// Reply: DocNo
        /// DocNo Number of last printed document.
        /// </summary>
        /// <returns></returns>
        public Reply FinalDocumentNumber()
        {
            return SendCommand(113);
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
        public Reply InformationIncludedInTheReceipt()
        {
            return SendCommand(103);
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
        public Reply PrintInformationAboutCurrentDay(string Data)
        {
            return SendCommand(110, Data);
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
        public Reply InformationAboutOperators(string Data)
        {
            return SendCommand(112, Data);
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
        public Reply DataFromFMByMember(string Data)
        {
            return SendCommand(114, Data);
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
        public Reply InformationFromFMByDate(string Data)
        {
            return SendCommand(146, Data);
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
        public Reply GetConstantValues()
        {
            return SendCommand(128);
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


    }
}
