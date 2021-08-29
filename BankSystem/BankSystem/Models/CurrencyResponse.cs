using System.Collections.Generic;

namespace Bank_Sistem.Models
{
    public class CurrencyResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Dictionary<string, double> Data { get; set; }
    }
}
