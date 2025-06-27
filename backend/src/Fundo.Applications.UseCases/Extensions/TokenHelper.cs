using Fundo.Applications.UseCases.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fundo.Applications.UseCases.Extensions
{
	public static class TokenHelper
	{
		public static string GenerateToken(GenerateTokenDto dto)
		{
			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(dto.SecretKey)
			);

			var credentials = new SigningCredentials(
				key,
				SecurityAlgorithms.HmacSha256
			);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Name, "app-name")
			};

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = dto.Expires,
				SigningCredentials = credentials,
				Issuer = dto.Issuer,
				Audience = dto.Audience
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
