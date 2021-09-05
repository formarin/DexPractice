using BankSystem.Services;
using BankSystem.Models;
using Xunit;

namespace BankSystem.Tests
{
    public class ExchangeTest
    {
        [Fact]
        public void CurrencyExchange_100Rup_To_Mdl()
        {
            //Arrange
            var exch = new Exchange();
            var rup = new Rub() { Rate = 4 };
            var mdl = new Mdl() { Rate = 5 };

            //Act
            var result = exch.CurrencyExchange<Currency, Currency>(100, rup, mdl);

            //Assert
            Assert.Equal(80, result);
        }
    }
}
