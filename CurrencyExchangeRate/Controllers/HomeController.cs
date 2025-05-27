using System.Diagnostics;
using CurrencyExchangeRate.Models;
using CurrencyExchangeRate.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeRate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExchangeRateService exchRateService;

        public HomeController(ExchangeRateService exchangeRateService)
        {
            this.exchRateService = exchangeRateService;
        }

        public async Task<IActionResult> Index()
        {
            var rates = await exchRateService.GetExchangeRates();
            return View(rates);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
