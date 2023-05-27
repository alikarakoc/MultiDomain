using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MultiDomain.Infrastructure.Interfaces;
using System.Net;

namespace MultiDomain.UI.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IDomainService _domainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static string GetSubDomain(HttpContext httpContext)
        {
            var subDomain = string.Empty;

            var host = httpContext.Request.Host.Host;

            if (!string.IsNullOrWhiteSpace(host))
            {
                subDomain = host.Split('.')[0];
            }

            return subDomain.Trim().ToLower();
        }
        public string ClientIp
        {
            get
            {
                string ip = string.Empty;
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("CF-Connecting-IP"))
                {
                    ip = _httpContextAccessor.HttpContext.Request.Headers["CF-Connecting-IP"];
                }
                else
                {
                    ip = _httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress
                        .ToString();
                }
                return ip;
            }
        }
        public HomeController(IDomainService domainService, IHttpContextAccessor httpContextAccessor)
        {
            _domainService = domainService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Hello()
        {
            return Ok($"Merhaba beni {GetSubDomain(_httpContextAccessor.HttpContext)} bu alana göre inşa et. HostName: {ClientIp} {GetSubDomain(_httpContextAccessor.HttpContext)}");
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
