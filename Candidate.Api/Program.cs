using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Candidate.Api
{
    /// <summary>
    /// Start Point
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {


        /// <summary>
        /// Kick Off
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        /// <summary>
        /// Web Host Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>();
                });

    }
}
