namespace BankSystem.Services
{
    public class Exchange : IExchange
    {
        public double CurrExch<T,U>(double MoneyAmount, T Curr1, U Curr2) where T : Currency where U:Currency
        {
            return MoneyAmount * Curr1.Rate / Curr2.Rate;
        }
    }
}
