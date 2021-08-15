using System;

namespace BankSystem.Models
{
    public class Employee : IPerson
    {
        public int PassportId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Employee))
            {
                return false;
            }
            return ((Employee)obj).PassportId == PassportId;
        }

        public static bool operator ==(Employee employee1, Employee employee2)
        {
            return employee1.Equals(employee2);
        }
        public static bool operator !=(Employee employee1, Employee employee2)
        {
            return !employee1.Equals(employee2);
        }

        public override int GetHashCode()
        {
            return PassportId;
        }
    }
}
