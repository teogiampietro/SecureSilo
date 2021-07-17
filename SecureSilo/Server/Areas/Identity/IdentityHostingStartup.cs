using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SecureSilo.Server.Areas.Identity.IdentityHostingStartup))]
namespace SecureSilo.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}