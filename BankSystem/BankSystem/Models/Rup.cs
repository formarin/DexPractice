using System;

namespace BankSystem.Services
{
    [Serializable]
    public class Rup : Currency
    {
        public Rup() : base(0.062)
        {

        }
    }
}
