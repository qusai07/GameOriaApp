
namespace GameOria.Domains.Exceptions
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
