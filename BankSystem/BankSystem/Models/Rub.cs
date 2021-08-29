using System;

namespace BankSystem.Services
{
    [Serializable]
    public class Rub : Currency
    {
        public Rub() : base(Convert.ToDouble(GetCurencyRate("RUBUSD").Result))
        {

        }
    }
}
