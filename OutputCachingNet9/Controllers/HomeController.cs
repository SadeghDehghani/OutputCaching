using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using OutputCachingNet9.Models;

namespace OutputCachingNet9.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOutputCacheStore _cacheStore;

        public HomeController(ILogger<HomeController> logger, 
            IOutputCacheStore outputCacheStore)
        {
            _logger = logger;
            _cacheStore = outputCacheStore;   
        }


         [OutputCache(Tags =["TestData"])]
        public IActionResult Index()
        {
            ViewBag.Data = GenarateTimeDate();   
            return View();
        }
        

        public  string GenarateTimeDate()
        {
            return $"{DateTime.UtcNow:yyyy-MM-dd-HH:mm:ss}";
        }


        [HttpGet]
        public async Task<IActionResult> ClearAllCache()
        {
            await _cacheStore.EvictByTagAsync("TestData", HttpContext.RequestAborted);
           // await _cacheStore.EvictByTagAsync(string.Empty, cancellation);
            return LocalRedirect("/");
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
