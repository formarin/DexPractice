using System;

namespace BankSystem.Models
{
    public interface IPerson
    {
        public string PassportId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
