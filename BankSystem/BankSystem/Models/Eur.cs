using BankSystem.Services;
using System;

namespace BankSystem.Models
{
    [Serializable]
    public class Eur : Currency
    {
        public Eur() : base(1.19/*Convert.ToDouble(GetCurencyRate("EURUSD").Result)*/)
        {

        }
    }
}
