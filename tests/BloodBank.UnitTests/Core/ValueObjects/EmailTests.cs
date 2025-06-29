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
        var a = new Email("user@email.com");
        var b = new Email("user@email.com");

        a.Equals(b).Should().BeTrue();
        b.Equals(a).Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForDifferentValue()
    {
        var a = new Email("user@email.com");
        var b = new Email("other@email.com");

        a.Equals(b).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldBeEqual_ForSameValue()
    {
        var a = new Email("user@email.com");
        var b = new Email("user@email.com");

        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldNotBeEqual_ForDifferentValue()
    {
        var a = new Email("user@email.com");
        var b = new Email("other@email.com");

        a.GetHashCode().Should().NotBe(b.GetHashCode());
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        var value = "user@email.com";
        var email = new Email(value);

        email.ToString().Should().Be(value);
    }
}
