using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LMS.Utils
{
    public static class JwtHelper
    {
        public static string GenerateToken(dynamic user)
        {
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "your_secret_key_here";
            var key = Encoding.UTF8.GetBytes(secretKey);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            
            // Extract user properties safely
            string userId = user.Id.ToString();
            string username = user.Username?.ToString();
            string name = user.Name?.ToString();
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim("FullName", name ?? string.Empty),
                new Claim(ClaimTypes.Role, "Librarian")  // Add appropriate role
            };
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),  // Set appropriate expiration
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}