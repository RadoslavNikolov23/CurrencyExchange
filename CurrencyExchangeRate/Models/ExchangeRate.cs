namespace CurrencyExchangeRate.Models
{
    public class ExchangeRate
    {
        public ExchangeRate()
        {

        }
        public ExchangeRate(string currency, string code, string forOne, string value, string curs)
        {
            Currency = currency;
            Code = code;
            ForOne = forOne;
            Value = value;
            Curs = curs;
        }

        public string Currency { get; set; }
        public string Code { get; set; }
        public string ForOne { get; set; }
        public string Value { get; set; }
        public string Curs { get; set; }

        public override string ToString()
        {
            return $"{Currency} - {Code} - {ForOne} - {Value} - {Curs}";
        }
    }
}
