using Fundo.Applications.UseCases.Commands.CreateLoan;
using Fundo.Applications.WebApi.Middlewares;
using Fundo.Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Fundo.Applications.WebApi
{
    public class Startup
    {
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
        {
			services.AddControllers();

			services.ConfigureDependencies(Configuration);
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateLoanCommand>());

			services.AddCors(p => p.AddDefaultPolicy(builder =>
			{
				builder.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowAnyOrigin();
			}));
		}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			app.UseMiddleware<ExceptionMiddleware>();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCors();
			app.UseEndpoints(endpoints => endpoints.MapControllers());

			app.UseSerilogRequestLogging();
		}
	}
}
