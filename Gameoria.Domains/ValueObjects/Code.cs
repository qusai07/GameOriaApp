using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Domains.ValueObjects
{
    public class Code : IEquatable<Code>
    {
        public string Value { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public bool IsUsed { get; private set; }
        public DateTime? UsedAt { get; private set; }

        private Code(string value, DateTime expiresAt)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Code value cannot be empty", nameof(value));

            if (expiresAt <= DateTime.UtcNow)
                throw new ArgumentException("Expiration date must be in the future", nameof(expiresAt));

            Value = value;
            ExpiresAt = expiresAt;
            IsUsed = false;
        }

        // Factory methods
        public static Code Generate(int length = 16, int validityInDays = 365)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var code = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return new Code(code, DateTime.UtcNow.AddDays(validityInDays));
        }

        public static Code CreateFromValue(string value, DateTime expiresAt)
        {
            return new Code(value, expiresAt);
        }

        // Business logic
        public void MarkAsUsed()
        {
            if (IsUsed)
                throw new InvalidOperationException("Code is already used");

            if (IsExpired())
                throw new InvalidOperationException("Code is expired");

            IsUsed = true;
            UsedAt = DateTime.UtcNow;
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow > ExpiresAt;
        }

        public bool IsValid()
        {
            return !IsUsed && !IsExpired();
        }

        public TimeSpan GetTimeUntilExpiration()
        {
            return ExpiresAt - DateTime.UtcNow;
        }

        // Equality members
        public bool Equals(Code? other)
        {
            if (other is null) return false;
            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Code other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        // Format methods
        public string ToFormattedString(string format = "XXXX-XXXX-XXXX-XXXX")
        {
            if (Value.Length != format.Count(c => c == 'X'))
                throw new InvalidOperationException("Format pattern does not match code length");

            var result = format;
            var valueIndex = 0;
            for (var i = 0; i < format.Length; i++)
            {
                if (format[i] == 'X')
                {
                    result = result.Remove(i, 1).Insert(i, Value[valueIndex].ToString());
                    valueIndex++;
                }
            }
            return result;
        }
    }
}
