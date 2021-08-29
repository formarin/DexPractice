using System;

namespace BankSystem.Services
{
    [Serializable]
    public class Eur : Currency
    {
        public Eur() : base(Convert.ToDouble(GetCurencyRate("EURUSD").Result))
        {

        }
    }
}
