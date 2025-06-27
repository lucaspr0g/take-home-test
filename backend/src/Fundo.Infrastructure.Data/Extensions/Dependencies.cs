using Fundo.Core.Interfaces;
using Fundo.Core.Services;
using Fundo.Core.Validators;
using Fundo.Infrastructure.Data.Configuration;
using Fundo.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Fundo.Infrastructure.Data.Extensions
{
	public static class Dependencies
	{
		public static IServiceCollection ConfigureDependencies(this IServiceCollection services, 
			IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt =>
				{
					opt.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateIssuerSigningKey = true,
						ClockSkew = TimeSpan.Zero,
						RequireExpirationTime = true,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
						ValidAudience = configuration["Jwt:Audience"],
						ValidIssuer = configuration["Jwt:Issuer"]
					};
				});

			services.AddAuthorization();

			services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
					opt => opt.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName))
			);

			// Repositories
			services.AddScoped<ILoanRepository, LoanRepository>();

			// Services
			services.AddScoped<ILoanService, LoanService>();

			// Validators
			services.AddScoped<ILoanValidator, LoanValidator>();
			services.AddScoped<IDeductValidator, DeductValidator>();

			return services;
		}
	}
}