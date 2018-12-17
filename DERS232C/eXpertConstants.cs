using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C
{
    public static class eXpertConstants
    {
        //Horizontal size of graphical logo in pixels.
        public static readonly short BITMAP_X = 384;


        //Vertical size of graphical logo in pixels
        public static readonly short BITMAP_Y = 144;

        //Number of payment types
        public static readonly short PAY_MAX_CNT = 5;

        //Number of tax groups
        public static readonly short TAX_MAX_CNT = 5;

        //Number of symbols per line
        public static readonly short CHARS_PER_LINE = 32;

        //Number of symbols of comment line
        public static readonly short COMMENT_LEN = 28;

        //Number of symbols for names (operators, PLUs, departments).
        public static readonly short NAME_LEN = 10;

        //Number of symbols of MRC of FD
        public static readonly short MACHNO_LEN = 10;

        //Number of symbols of FM number
        public static readonly short MNO_LEN = 10;

        //Number of symbols of registration numberr
        public static readonly short REGNO_LEN = 15;

        //Number of departments
        public static readonly short DEPT_MAX_CNT = 50;

        //Number of PLUs
        public static readonly short PLU_MAX_CNT = 8000;

        //Number of operators
        public static readonly short OPER_MAX_CNT= 20;

        //Number of symbols of payment names
        public static readonly short PAYNAME_LEN = 10;


    }
}
