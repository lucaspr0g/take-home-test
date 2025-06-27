using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Fundo.Applications.WebApi
{
	[ExcludeFromCodeCoverage]
	public static class Program
    {
        public static void Main(string[] args)
        {
			Log.Logger = new LoggerConfiguration()
	            .Enrich.FromLogContext()
	            .WriteTo.Console()
	            .MinimumLevel.Information()
	            .CreateLogger();

			try
            {
				Log.Information("Starting web application");

				CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
				Log.Fatal(ex, $"Unhandled WebApi exception: {ex.Message}");
            }
            finally
            {
				Log.Information("Application shutting down.");
				Log.CloseAndFlush();
			}
		}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
        }
    }
}
