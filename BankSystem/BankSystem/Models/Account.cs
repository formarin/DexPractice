using BankSystem.Services;
using System;

namespace BankSystem.Models
{
    [Serializable]
    public class Account
    {
        public Currency CurrencyType { get; set; }
        public double CurrencyAmount { get; set; }
    }
}
