using Bank_Sistem.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bank_Sistem.Services
{
    public class CurrencyService
    {
        public async Task<CurrencyResponse> GetActualCurencyRate()
        {
            HttpResponseMessage responseMessage;
            CurrencyResponse currencyResponse;
            using (var client = new HttpClient())
            {
                responseMessage = await client.GetAsync("https://currate.ru/api/?get=rates&pairs=RUBUSD,MDLUSD,EURUSD&key=413d1261d8ff9a7b352b347dd0c29189");

                responseMessage.EnsureSuccessStatusCode();

                string serializeMessage = await responseMessage.Content.ReadAsStringAsync();
                currencyResponse = JsonConvert.DeserializeObject<CurrencyResponse>(serializeMessage);
            }

            return currencyResponse;
        }
    }
}
