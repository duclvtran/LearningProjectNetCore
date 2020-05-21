using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using RedisConfig;
using StackExchange.Redis;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly RedisConfiguration _redis;
        private readonly IDistributedCache _cache;
        private readonly IRedisConnectionFactory _fact;
        public RedisController(IOptions<RedisConfiguration> redis, IDistributedCache cache, IRedisConnectionFactory factory)
        {
            _redis = redis.Value;
            _cache = cache;
            _fact = factory;
        }

        [Route("Set")]
        public ReturnModel<string>Set()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379,password=123123");
            IDatabase db = redis.GetDatabase();
            string value = "abcdefg";
            db.StringSet("mykey", value);
            return new ReturnModel<string>(value);
        }

        [Route("Get")]
        public ReturnModel<string> Get()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379,password=123123");
            IDatabase db = redis.GetDatabase();
            string value = db.StringGet("mykey");
            return new ReturnModel<string>(value);
        }

        [Route("Set1")]
        public ReturnModel<string> Set1()
        {
           
            return new ReturnModel<string>("");
        }

        [Route("Get1")]
        public ReturnModel<string> Get1()
        {
           

            return new ReturnModel<string>("");
        }
    }
}