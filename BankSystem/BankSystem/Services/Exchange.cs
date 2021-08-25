using System;
using BankSystem.Exceptions;

namespace BankSystem.Services
{
    public class Exchange : IExchange
    {
        public double CurrExch<T, U>(double MoneyAmount, T Curr1, U Curr2) where T : Currency where U : Currency
        {
            //if (MoneyAmount < 0)
            //{
            //    throw new NegativeAmountException("NegativeAmountException");
            //}
            return MoneyAmount * Curr1.Rate / Curr2.Rate;
        }
    }
}
