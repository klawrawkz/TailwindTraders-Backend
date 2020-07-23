using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Tailwind.Traders.WebBff.Infrastructure
{
    public class DevspacesMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DevspacesMessageHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var req = _httpContextAccessor.HttpContext.Request;

            if (req.Headers.ContainsKey("kubernetes-route-as"))
            {
                request.Headers.Add("kubernetes-route-as", req.Headers["kubernetes-route-as"] as IEnumerable<string>);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
