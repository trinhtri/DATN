using System.IO;
using Abp.Timing;
using Microsoft.AspNetCore.Hosting;

namespace ERP.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Clock.Provider = ClockProviders.Local;
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel(opt => opt.AddServerHeader = false)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();
        }
    }
}
