using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisExample.Models;

namespace RedisExample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDistributedCache _cache;

    public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public IActionResult Index()
    {
        var cachedTime = _cache.Get("lasttime");

        if (cachedTime == null)
        {
            var timeNow = DateTime.Now.TimeOfDay;
            ViewBag.LastTime = timeNow.ToString();

            var bytes = Encoding.UTF8.GetBytes(timeNow.ToString());
            
            _cache.Set("lasttime", bytes, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
            });
        }
        else
        {
            ViewBag.LastTime = Encoding.UTF8.GetString(_cache.Get("lasttime"));
        }
        
        return View();
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
