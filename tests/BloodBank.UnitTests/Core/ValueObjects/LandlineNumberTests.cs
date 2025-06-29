using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Core.ValueObjects;

public class LandlineNumberTests
{
    [Theory]
    [InlineData("(54) 1234-5678")]
    [InlineData("(11) 3232-3232")]
    public void Constructor_ShouldSetValue_WhenValidNumber(string validNumber)
    {
        // Act
        var landline = new LandlineNumber(validNumber);

        // Assert
        landline.Value.Should().Be(validNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("12345678")]
    [InlineData("(54)1234-5678")]
    [InlineData("(54) 91234-5678")]
    public void Constructor_ShouldThrowArgumentException_WhenInvalidNumber(string invalidNumber)
    {
        // Act
        Action act = () => new LandlineNumber(invalidNumber!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForSameValue()
    {
        // Arrange
        var a = new LandlineNumber("(54) 1234-5678");
        var b = new LandlineNumber("(54) 1234-5678");

        // Act
        var equals = a.Equals(b);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForDifferentValue()
    {
        // Arrange
        var a = new LandlineNumber("(54) 1234-5678");
        var b = new LandlineNumber("(11) 3232-3232");

        // Act
        var equals = a.Equals(b);

        // Assert
        equals.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldBeEqual_ForSameValue()
    {
        // Arrange
        var a = new LandlineNumber("(54) 1234-5678");
        var b = new LandlineNumber("(54) 1234-5678");

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
        var a = new LandlineNumber("(54) 1234-5678");
        var b = new LandlineNumber("(11) 3232-3232");

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
        var value = "(54) 1234-5678";
        var landline = new LandlineNumber(value);

        // Act
        var result = landline.ToString();

        // Assert
        result.Should().Be(value);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("(54) 1234-5678", true)]
    [InlineData("(11) 4321-8765", true)]
    [InlineData("12345678", false)]
    [InlineData("(54) 91234-5678", false)]
    public void IsValid_ShouldReturnExpectedResult(string value, bool expected)
    {
        // Act
        var isValid = LandlineNumber.IsValid(value!);

        // Assert
        isValid.Should().Be(expected);
    }
}
