namespace CurrencyExchangeRate.Services
{
    using CurrencyExchangeRate.Models;
    using HtmlAgilityPack;

    public class ExchangeRateService
    {
        public async Task<List<ExchangeRate>> GetExchangeRates()
        {
            List<ExchangeRate> currentCurrency = new List<ExchangeRate>();

            string url = "https://bnb.bg/Statistics/StExternalSector/StExchangeRates/StERForeignCurrencies/index.htm";

            HttpClient client = new HttpClient();

            try
            {
                string html = await client.GetStringAsync(url);
                HtmlDocument doc = new HtmlDocument();

                doc.LoadHtml(html);

                HtmlNodeCollection tableNodes = doc
                                                .DocumentNode
                                                .SelectNodes("//div[@class='table_box']");


                if (tableNodes != null)
                {
                    foreach (var node in tableNodes)
                    {
                        var tbodyNodes = node.SelectNodes(".//tbody");

                        if (tbodyNodes != null)
                        {
                            foreach (var tbody in tbodyNodes)
                            {
                                var firstTrNodes = tbody
                                                      .SelectNodes(".//tr[@class='first']");
                                var restTrNodes = tbody
                                                      .SelectNodes(".//tr");

                                if (firstTrNodes != null)
                                {
                                    foreach (var tr in firstTrNodes)
                                    {
                                        HtmlNode firstTd = tr
                                                            .SelectSingleNode(".//td[@class='first']");
                                        HtmlNode centerTd1 = tr
                                                            .SelectSingleNode(".//td[@class='center']");
                                        HtmlNode rightTd = tr
                                                            .SelectSingleNode(".//td[@class='right']");
                                        HtmlNode centerTd2 = tr
                                                            .SelectSingleNode(".//td[@class='center'][2]"); // second center column
                                        HtmlNode lastCenterTd = tr
                                                            .SelectSingleNode(".//td[@class='last center']");

                                        ExchangeRate currency = new ExchangeRate(
                                                                firstTd.InnerText.ToString(),
                                                                centerTd1.InnerText.ToString(),
                                                                rightTd.InnerText.ToString(),
                                                                centerTd2.InnerText.ToString(),
                                                                lastCenterTd.InnerText.ToString()
                                                                );

                                        currentCurrency.Add(currency);
                                    }
                                }

                                if (restTrNodes != null)
                                {
                                    foreach (var tr in restTrNodes)
                                    {
                                        HtmlNode firstTd = tr
                                                        .SelectSingleNode(".//td[@class='first']");
                                        HtmlNode centerTd1 = tr
                                                        .SelectSingleNode(".//td[@class='center']");
                                        HtmlNode rightTd = tr
                                                         .SelectSingleNode(".//td[@class='right']");
                                        HtmlNode centerTd2 = tr
                                                          .SelectSingleNode(".//td[@class='center'][2]"); // second center column
                                        HtmlNode lastCenterTd = tr
                                                          .SelectSingleNode(".//td[@class='last center']");

                                        if (firstTd == null
                                            || centerTd1 == null
                                            || rightTd == null
                                            || centerTd2 == null
                                            || lastCenterTd == null)
                                            continue;

                                        ExchangeRate currency = new ExchangeRate(
                                                                firstTd.InnerText.ToString(),
                                                                centerTd1.InnerText.ToString(),
                                                                rightTd.InnerText.ToString(),
                                                                centerTd2.InnerText.ToString(),
                                                                lastCenterTd.InnerText.ToString()
                                                                );

                                        currentCurrency.Add(currency);

                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }

            return currentCurrency;

        }
    }
}
