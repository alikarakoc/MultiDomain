using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MultiDomain.Infrastructure.Interfaces;
using System.Net;
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
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            return Ok($"GetHostEntry() returns HostName: {GetHeader(_httpContextAccessor.HttpContext.Request,"Host")}");
        }
        public string DoGetHostEntry(IPAddress address)
        {
            IPHostEntry host = Dns.GetHostEntry(address);

            return $"GetHostEntry({address}) returns HostName: {host.HostName}";
        }
        public string GetHeader(HttpRequest request, string key)
        {
            return request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
        }
        public string GetHeaderData(string headerKey)
        {
            Request.Headers.TryGetValue(headerKey, out var headerValue);
            return headerValue;
        }
    }
}
