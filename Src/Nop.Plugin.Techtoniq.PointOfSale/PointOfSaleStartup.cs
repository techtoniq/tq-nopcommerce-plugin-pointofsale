using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Techtoniq.PointOfSale
{
    public class PointOfSaleStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new PointOfSaleViewEngine());
            });
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order
        {
            get { return 1001; }
        }
    }
}
