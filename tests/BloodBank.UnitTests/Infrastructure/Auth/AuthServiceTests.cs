using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BloodBank.Core.Constants;
using BloodBank.Core.Entities;
using BloodBank.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BloodBank.UnitTests.Infrastructure.Auth;

public class AuthServiceTests
{
    private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _passwordHasherMock = new Mock<IPasswordHasher<User>>();
        _configMock = new Mock<IConfiguration>();
        _authService = new AuthService(_configMock.Object, _passwordHasherMock.Object);
    }

    [Fact]
    public void GenerateJwtToken_ShouldReturnValidToken()
    {
        // Arrange
        var userId = "1";
        var email = "user@email.com.br";
        var role = UserRoles.Admin;
        var issuer = "TestIssuer";
        var audience = "TestAudience";        

        _configMock.Setup(c => c["Jwt:Issuer"]).Returns(issuer);
        _configMock.Setup(c => c["Jwt:Audience"]).Returns(audience);
        _configMock.Setup(c => c["Jwt:Key"]).Returns("BloodBankJwtGeneratorSuperSecretKey123");

        // Act
        var token = _authService.GenerateJwtToken(userId, email, role);

        // Assert
        token.Should().NotBeNullOrEmpty();

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        jwt.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value.Should().Be(userId);
        jwt.Claims.First(c => c.Type == ClaimTypes.Email).Value.Should().Be(email);
        jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value.Should().Be(role);
        jwt.Issuer.Should().Be(issuer);
        jwt.Audiences.Should().Contain(audience);
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnToken()
    {
        // Act
        var refreshToken = _authService.GenerateRefreshToken();

        // Assert
        refreshToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void HashPassword_ShouldReturnHashedPassword()
    {
        // Arrange
        var password = "Teste@123";
        var expectedHash = "1791962eadeadcd9001ce88815698370";
        _passwordHasherMock.Setup(h => h.HashPassword(null!, password)).Returns(expectedHash);

        // Act
        var result = _authService.HashPassword(password);

        // Assert
        result.Should().Be(expectedHash);
        _passwordHasherMock.Verify(h => h.HashPassword(null!, password), Times.Once);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        // Arrange
        var password = "Teste@123";
        var hashedPassword = "1791962eadeadcd9001ce88815698370";
        _passwordHasherMock.Setup(h => h.VerifyHashedPassword(null!, hashedPassword, password)).Returns(PasswordVerificationResult.Success);

        // Act
        var result = _authService.VerifyPassword(password, hashedPassword);

        // Assert
        result.Should().BeTrue();
        _passwordHasherMock.Verify(h => h.VerifyHashedPassword(null!, hashedPassword, password), Times.Once);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_WhenPasswordIsIncorrect()
    {
        // Arrange
        var password = "Teste@123";
        var hashedPassword = "45b0c7dbcd4ef4830b7497acc8918290";
        _passwordHasherMock.Setup(h => h.VerifyHashedPassword(null!, hashedPassword, password)).Returns(PasswordVerificationResult.Failed);

        // Act
        var result = _authService.VerifyPassword(password, hashedPassword);

        // Assert
        result.Should().BeFalse();
        _passwordHasherMock.Verify(h => h.VerifyHashedPassword(null!, hashedPassword, password), Times.Once);
    }
}
