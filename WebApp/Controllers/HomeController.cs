using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedisConfig;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RedisConfiguration _redis;
        private readonly IDistributedCache _cache;
        private readonly IRedisConnectionFactory _fact;
        public HomeController(ILogger<HomeController> logger, IOptions<RedisConfiguration> redis, IDistributedCache cache, IRedisConnectionFactory factory)
        {
            _logger = logger;
            _redis = redis.Value;
            _cache = cache;
            _fact = factory;
        }

        public IActionResult Index()
        {
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

        private string Get()
        {
            var helloRedis = Encoding.UTF8.GetBytes("Hello Redis");
            HttpContext.Session.Set("hellokey", helloRedis);

            var getHello = default(byte[]);
            HttpContext.Session.TryGetValue("hellokey", out getHello);
            ViewData["Hello"] = Encoding.UTF8.GetString(getHello);
            ViewData["SessionID"] = HttpContext.Session.Id;

            _cache.SetString("CacheTest", "Redis is awesome");

            ViewData["Host"] = _redis.Host;
            ViewData["Port"] = _redis.Port;
            ViewData["Name"] = _redis.Name;


            ViewData["DistCache"] = _cache.GetString("CacheTest");

            var db = _fact.Connection().GetDatabase();
            db.StringSet("StackExchange.Redis.Key", "Stack Exchange Redis is Awesome");
            ViewData["StackExchange"] = db.StringGet("StackExchange.Redis.Key");

            return "";
        }
        private string Set()
        {
            //var redis = new RedisVoteService<Vote>(this._fact);
            //var theVote = new Vote();
            //switch (value)
            //{
            //    case "Y":
            //        theVote.Yes = 1;
            //        break;
            //    case "N":
            //        theVote.No = 1;
            //        break;
            //    case "U":
            //        theVote.Undecided = 1;
            //        break;
            //    default: break;.
            //}

            //redis.Save("RedisVote", theVote);

            //var model = redis.Get("RedisVote");

            var db = _fact.Connection().GetDatabase();
            return "";
        }
    }
}
