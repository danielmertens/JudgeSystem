using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(JudgeSystem.Web.Areas.Identity.IdentityHostingStartup))]
namespace JudgeSystem.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}