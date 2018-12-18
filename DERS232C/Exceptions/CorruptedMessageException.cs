using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DERS232C.Exceptions
{
    public class CorruptedMessageException : Exception
    {
        public CorruptedMessageException() : base()
        {

        }

        public CorruptedMessageException(string message): base(message)
        {

        }

        public CorruptedMessageException(string message, Exception innerException): base(message, innerException)
        {

        }
    }
}
