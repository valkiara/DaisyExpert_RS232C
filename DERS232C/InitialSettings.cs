using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public class InitialSettings : Communicator
    {
        public InitialSettings(string portName) : base(portName)
        {
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
        public Reply EnteringTaxAutherityPassword(string Data)
        {
            return SendCommand(192, Data);
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
        public Reply PrintingOptions(string Data)
        {
            return SendCommand(43, Data);
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
        public Reply TaxRatesProgramming(string Data)
        {
            return SendCommand(96, Data);
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
        public Reply SetSystemParameters(string Data)
        {
            return SendCommand(150, Data);
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
        public Reply ProgrammingErasingAndReadingDataForPLUs(string Data)
        {
            return SendCommand(107, Data);
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
        public Reply ProgrammingDepartments(string Data)
        {
            return SendCommand(131, Data);
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
        public Reply ProgrammingText(string Data)
        {
            return SendCommand(149, Data);
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
        public Reply SetPaymentCurrencies(string Data)
        {
            return SendCommand(151, Data);
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
        public Reply RestoreDefaultPasswords()
        {
            return SendCommand(229);
        }

    }
}
