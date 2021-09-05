using BankSystem.Services;
using System;

namespace BankSystem.Models
{
    [Serializable]
    public class Rub : Currency
    {
        public Rub() : base(0.014/*Convert.ToDouble(GetCurencyRate("RUBUSD").Result)*/)
        {

        }
    }
}
