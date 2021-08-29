using Bank_Sistem.Models;
using Bank_Sistem.Services;
using System;
using System.Threading.Tasks;

namespace BankSystem.Services
{
    [Serializable]
    public class Currency
    {
        public double Rate { get; set; }
        public Currency(double rate)
        {
            Rate = rate;
        }

        public static async Task<Double> GetCurencyRate(string currencyPair)
        {
            CurrencyService currencyService = new CurrencyService();
            CurrencyResponse currate = await currencyService.GetActualCurencyRate();

            return currate.Data[currencyPair];
        }
    }
}
