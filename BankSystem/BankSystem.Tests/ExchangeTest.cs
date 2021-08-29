using BankSystem.Services;
using BankSystem.Models;
using BankSystem.Exceptions;
using Xunit;
using System;

namespace BankSystem.Tests
{
    public class ExchangeTest
    {
        //public class Exchange : IExchange
        //{
        //    public double CurrExch<T, U>(double MoneyAmount, T Curr1, U Curr2) where T : Currency where U : Currency
        //    {
        //        return MoneyAmount * Curr1.Rate / Curr2.Rate;
        //    }
        //}

        [Fact]
        public void CurrExch_100_Mult_4_Devide_5_Eq_80()
        {
            //Arrange
            var exch = new Exchange();
            var rup = new Rub() { Rate = 4 };
            var mdl = new Mdl() { Rate = 5 };

            //Act
            var result = exch.CurrExch<Currency, Currency>(100, rup, mdl);

            //Assert
            Assert.Equal(80, result);
        }

        [Fact]
        public void NegCurrExch_min100_Mult_4_Devide_5_ThrowsExc()
        {
            //Arrange
            var exch = new Exchange();
            var rup = new Rub() { Rate = 4 };
            var mdl = new Mdl() { Rate = 5 };

            //Act
            var result = exch.CurrExch<Currency, Currency>(-100, rup, mdl);

            //Assert
            Assert.NotEqual(-80, result);
        }
    }
}
