using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(StreetWorkout.Areas.Identity.IdentityHostingStartup))]

namespace StreetWorkout.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
            => builder.ConfigureServices((context, services) => { });
    }
}