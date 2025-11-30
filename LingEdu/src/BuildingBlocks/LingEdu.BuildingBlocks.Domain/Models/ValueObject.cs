using System.Collections.Generic;
using System.Linq;

namespace LingEdu.BuildingBlocks.Domain.Models
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is not ValueObject other)
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Where(x => x is not null)
                .Aggregate(0, (hash, obj) => HashCode.Combine(hash, obj!));
        }
    }
}
