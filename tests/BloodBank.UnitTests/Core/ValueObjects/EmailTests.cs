using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Core.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("user@email.com.br")]
    [InlineData("donor@domain.org")]
    [InlineData("a@b.co")]
    public void Constructor_ShouldSetValue_WhenValidEmail(string validEmail)
    {
        // Act
        var email = new Email(validEmail);

        // Assert
        email.Value.Should().Be(validEmail);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("invalidemail")]
    [InlineData("user@")]
    [InlineData("@domain.com")]
    [InlineData("user@domain")]
    [InlineData("user@domain,com")]
    public void Constructor_ShouldThrowArgumentException_WhenInvalidEmail(string invalidEmail)
    {
        // Act
        Action act = () => new Email(invalidEmail!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForSameValue()
    {
        // Arrange
        var a = new Email("user@email.com");
        var b = new Email("user@email.com");

        // Act
        var equals = a.Equals(b);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForDifferentValue()
    {
        // Arrange
        var a = new Email("user@email.com");
        var b = new Email("other@email.com");

        // Act
        var equals = a.Equals(b);

        // Assert
        equals.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldBeEqual_ForSameValue()
    {
        // Arrange
        var a = new Email("user@email.com");
        var b = new Email("user@email.com");

        // Act
        var hashCodeA = a.GetHashCode();
        var hashCodeB = b.GetHashCode();

        // Assert
        hashCodeA.Should().Be(hashCodeB);
    }

    [Fact]
    public void GetHashCode_ShouldNotBeEqual_ForDifferentValue()
    {
        // Arrange
        var a = new Email("user@email.com");
        var b = new Email("other@email.com");

        // Act
        var hashCodeA = a.GetHashCode();
        var hashCodeB = b.GetHashCode();

        // Assert
        hashCodeA.Should().NotBe(hashCodeB);
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        // Arrange
        var value = "user@email.com";
        var email = new Email(value);

        // Act
        var result = email.ToString();

        // Assert
        result.Should().Be(value);
    }
}
