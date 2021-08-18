using System;

namespace BankSystem.Services
{
    [Serializable]
    public class Currency
    {
        public double Rate { get; set; }
        public Currency(double rate)
        {
            Rate = rate;
        }
    }
}
