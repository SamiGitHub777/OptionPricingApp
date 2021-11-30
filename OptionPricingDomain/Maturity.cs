using System;

namespace OptionPricingDomain
{
    public class Maturity
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }

        public Maturity(int year, int month, int day)
        {
            IsMaturityValid(year, month, day);
            this.Year = year;
            this.Month = month;
            this.Day = day;
        }

        private void IsMaturityValid(int year, int month, int day)
        {
            if (year < DateTime.Today.Year)
            {
                throw new ArgumentException("Year cannot be in the past");
            }
            var d = new DateTime(year, month, day);
            if (d.CompareTo(DateTime.Now) <= 0)
            {
                throw new ArgumentException("Maturity cannot be in the past");
            }
        }

        public bool Equals(Maturity that)
        {
            if (that == null)
                return false;

            if (ReferenceEquals(that, this))
                return false;

            if (that.GetType() != this.GetType())
                return false;

            return this.Year.Equals(that.Year)
                && this.Month == that.Month
                && this.Day == that.Day;
        }
        public override int GetHashCode()
        {
            return this.Year.GetHashCode()
                 ^ this.Month.GetHashCode()
                 ^ this.Day.GetHashCode();
        }

        public DateTime ToDateTime()
        {
            return new DateTime(this.Year, this.Month, this.Day);
        }
    }
}
