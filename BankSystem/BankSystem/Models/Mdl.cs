using System;

namespace BankSystem.Services
{
    [Serializable]
    public class Mdl : Currency
    {
        public Mdl() : base(Convert.ToDouble(GetCurencyRate("MDLUSD").Result))
        {

        }
    }
}
