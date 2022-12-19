using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MultiDomain.Infrastructure.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

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
 
            string host = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            return Ok($"{host} Sitesindesin");
        }
    }
}
