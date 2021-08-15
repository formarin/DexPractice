using System;

namespace BankSystem.Exceptions
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(string message) : base(message)
        {

        }
    }
}