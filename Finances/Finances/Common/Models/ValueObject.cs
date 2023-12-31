﻿namespace Finances.Common.Models
{
    public abstract class ValueObject<T>
         where T : ValueObject<T>
    {
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            if (valueObject == null)
            {
                return false;
            }

            return EqualsCore(valueObject);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        private bool EqualsCore(ValueObject<T> other)
        {
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }
    }
}
