using System;

namespace BankSystem.Exceptions
{
    public class AgeLimitsExeption : Exception
    {
        public AgeLimitsExeption(string message) : base(message)
        {

        }
    }
}