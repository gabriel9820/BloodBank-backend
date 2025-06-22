using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BloodBank.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BloodBank.Infrastructure.Auth;

public class AuthService(
    IConfiguration configuration,
    IPasswordHasher<User> passwordHasher) : IAuthService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public string GenerateJwtToken(string userId, string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null!, password); ;
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(null!, hashedPassword, password) == PasswordVerificationResult.Success;
    }
}
