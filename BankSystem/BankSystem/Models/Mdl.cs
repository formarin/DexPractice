using BankSystem.Services;
using System;

namespace BankSystem.Models
{
    [Serializable]
    public class Mdl : Currency
    {
        public Mdl() : base(0.056/*Convert.ToDouble(GetCurencyRate("MDLUSD").Result)*/)
        {

        }
    }
}
