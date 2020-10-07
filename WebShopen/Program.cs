using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebShopen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        //public static async Task Main(string[] args)
        //{
        //    var builder = WebAssemblyHostBuilder.CreateDefault(args);
        //    builder.RootComponents.Add<App>("app"); builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); builder.Services.AddMsalAuthentication(options =>
        //    {
        //        builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
        //    }); await builder.Build().RunAsync();
        //}
    }
}




