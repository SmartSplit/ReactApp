using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ApiCallException : Exception
    {
        public ApiCallException()
        {

        }

        public ApiCallException(string message)
        : base(message)
    {
        }

        public ApiCallException(string message, Exception inner)
        : base(message, inner)
    {
        }
    }
}
