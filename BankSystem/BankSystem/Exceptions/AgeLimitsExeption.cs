using System;

namespace BankSystem.Exceptions
{
    class AgeLimitsExeption : Exception
    {
        public AgeLimitsExeption(string message) : base(message)
        {

        }
    }
}