namespace BankSystem.Services
{
    public interface IExchange
    {
        public double CurrExch<T, U>(double MoneyAmount, T Curr1, U Curr2) where T : Currency where U : Currency;
    }
}
