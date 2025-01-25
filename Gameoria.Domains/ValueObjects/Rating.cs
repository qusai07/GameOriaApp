using Gameoria.Domains.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameoria.Domains.Exceptions;
namespace Gameoria.Domains.ValueObjects
{
    public class Rating : IEquatable<Rating>
    {
        public int Value { get; private set; }
        public string? Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private const int MinRating = 1;
        private const int MaxRating = 5;

        public Rating(int value, string? comment = null)
        {
            if (value < MinRating || value > MaxRating)
                throw new ArgumentException($"Rating must be between {MinRating} and {MaxRating}", nameof(value));

            Value = value;
            Comment = comment;
            CreatedAt = DateTime.UtcNow;
        }

        // Factory methods
        public static Rating Create(int value, string? comment = null)
        {
            return new Rating(value, comment);
        }

        // Helper methods
        public bool IsPositive() => Value >= 4;
        public bool IsNeutral() => Value == 3;
        public bool IsNegative() => Value <= 2;

        public string GetStarRepresentation()
        {
            return new string('★', Value) + new string('☆', MaxRating - Value);
        }

        // Equality members
        public bool Equals(Rating? other)
        {
            if (other is null) return false;
            return Value == other.Value &&
                   Comment == other.Comment &&
                   CreatedAt == other.CreatedAt;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Rating other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Comment, CreatedAt);
        }

        public override string ToString()
        {
            return $"{Value}/5 {GetStarRepresentation()} {(Comment != null ? $"- {Comment}" : "")}";
        }
    }
}
