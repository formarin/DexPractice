using BankSystem.Services;

namespace BankSystem.Models
{
    public class Account
    {
        public Currency CurrencyType { get; set; }
        public double CurrencyAmount { get; set; }
    }
}
