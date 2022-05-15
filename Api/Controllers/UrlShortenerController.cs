using Helper;
using Helper.Factories;
using Aspects;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DataAccess;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers
{
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly ILogger<UrlShortenerController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public UrlShortenerController(ILogger<UrlShortenerController> logger,
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _configuration = configuration;
            _memoryCache = memoryCache;

            string testVal;
            if (!_memoryCache.TryGetValue("test",out testVal))
            {
                using (var unitOfWork = new UnitOfWork(
                    new UrlShortenerContext(_configuration.GetConnectionString("MSSQL"))))
                {
                    unitOfWork.UrlShortenings.GetAll().OrderByDescending(x =>
                    x.DateInfo).Take(100).ToList().ForEach(y =>
                    _memoryCache.Set(y.ShorteningTag, y.InitialUrl));
                }
                _memoryCache.Set("test", "test");
            }
        }

        [HttpGet]
        [Route("/{shorteningTag}")]
        [ExceptionAspect]
        public ActionResult Index(string shorteningTag)
        {
            try
            {
                string initialUrl = "";

                if (!_memoryCache.TryGetValue(shorteningTag, out initialUrl))
                {
                    using (var unitOfWork = new UnitOfWork(
                        new UrlShortenerContext(_configuration.GetConnectionString("MSSQL"))))
                    {
                        initialUrl = unitOfWork.UrlShortenings.Find(x => x.ShorteningTag == shorteningTag).First().InitialUrl;
                    }
                    _memoryCache.Set(shorteningTag, initialUrl);
                }

                return Redirect(initialUrl);
            }
            catch
            {
                return StatusCode(500, "Something went wrong...");
            }
        }

        [HttpGet]
        [ExceptionAspect]
        [Route("/")]
        public ActionResult Index()
        {
            return BadRequest("Please include the shortening tag.");
        }

        [HttpGet]
        [ExceptionAspect]
        [Route("shortenUrl")]
        public ActionResult ShortenUrl(string initialUrl, string? shorteningTag)
        {
            try
            {
                var factory = new ObjFactory();
                var shortenedModel = factory.CreateUrlShorteningModel(initialUrl, shorteningTag);

                using (var unitOfWork = new UnitOfWork(
                    new UrlShortenerContext(_configuration.GetConnectionString("MSSQL"))))
                {
                    unitOfWork.UrlShortenings.Add(shortenedModel);
                    unitOfWork.Complete();
                }
                _memoryCache.Set(shorteningTag, initialUrl);

                return Ok(JsonSerializer.Serialize(shortenedModel));
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Something went wrong...");
            }
        }
    }
}