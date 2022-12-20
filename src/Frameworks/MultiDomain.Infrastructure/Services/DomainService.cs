using MultiDomain.Infrastructure.Interfaces;

namespace MultiDomain.Infrastructure.Services
{
    public class DomainService : IDomainService
    {
        public string GetDomainName()
        {
            return "alikarakoc.com";
        }
    }
}
