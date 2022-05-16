using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public class NextMenuNotSetException : Exception
    {
        public NextMenuNotSetException()
        {
        }

        public NextMenuNotSetException(string message) : base(message)
        {
        }
    }
}
