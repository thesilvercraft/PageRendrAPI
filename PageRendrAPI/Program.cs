using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace PageRendrAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static string externalIp = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (Environment.GetEnvironmentVariable("PORT") is not null)
                    {
                        webBuilder.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));
                    }
                    webBuilder.UseStartup<Startup>();
                });
    }
}