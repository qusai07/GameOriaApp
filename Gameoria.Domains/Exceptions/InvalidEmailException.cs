using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public string AttemptedValue { get; }

        public InvalidEmailException(string attemptedValue)
            : base($" Your Email '{attemptedValue}' Not true")
        {
            AttemptedValue = attemptedValue;
        }
    }
}
