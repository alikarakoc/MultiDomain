using Microsoft.AspNetCore.Mvc;
using MultiDomain.Infrastructure.Interfaces;

namespace MultiDomain.UI.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IDomainService _domainService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IDomainService domainService, IHttpContextAccessor httpContextAccessor)
        {
            _domainService = domainService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Hello()
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            return Ok($"{host} Sitesindesin");
        }
    }
}
