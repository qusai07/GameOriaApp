using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Exceptions
{
    public abstract class DomainException : Exception
    {
        public string Code { get; }

        public DomainException()
        { }

        public DomainException(string message)
            : base(message)
        { }

        public DomainException(string message, string code)
            : base(message)
        {
            Code = code;
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public DomainException(string message, string code, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }

        public virtual IDictionary<string, object[]> GetErrors()
        {
            return new Dictionary<string, object[]>
            {
                { "DomainError", new object[] { Message } }
            };
        }
    }
}
