using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Exceptions
{
    public class LibraryBaseException : Exception
    {
        public LibraryBaseException()
        {
            
        }
        public LibraryBaseException(string message) :base(message)
        {
            
        }
        public LibraryBaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
