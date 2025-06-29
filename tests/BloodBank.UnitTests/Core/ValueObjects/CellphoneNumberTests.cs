using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Core.ValueObjects;

public class CellPhoneNumberTests
{
    [Theory]
    [InlineData("(54) 91234-5678")]
    [InlineData("(11) 98765-4321")]
    public void Constructor_ShouldInitializeProperties_WhenValidNumber(string validNumber)
    {
        // Act
        var cellPhoneNumber = new CellPhoneNumber(validNumber);

        // Assert
        cellPhoneNumber.Value.Should().Be(validNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("123456789")]
    [InlineData("(54)91234-5678")]
    [InlineData("(54) 81234-5678")]
    [InlineData("(54) 912345678")]
    public void Constructor_ShouldThrowArgumentException_WhenInvalidNumber(string invalidNumber)
    {
        // Act
        Action act = () => new CellPhoneNumber(invalidNumber!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForSameValue()
    {
        // Arrange
        var a = new CellPhoneNumber("(54) 91234-5678");
        var b = new CellPhoneNumber("(54) 91234-5678");

        // Act
        var equals = a.Equals(b);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForDifferentValue()
    {
        // Arrange
        var a = new CellPhoneNumber("(54) 91234-5678");
        var b = new CellPhoneNumber("(11) 98765-4321");

        // Act
        var equals = a.Equals(b);

        // Assert
        equals.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldBeEqual_ForSameValue()
    {
        // Arrange
        var a = new CellPhoneNumber("(54) 91234-5678");
        var b = new CellPhoneNumber("(54) 91234-5678");

        // Act
        var hashCode1 = a.GetHashCode();
        var hashCode2 = b.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void GetHashCode_ShouldNotBeEqual_ForDifferentValue()
    {
        // Arrange
        var a = new CellPhoneNumber("(54) 91234-5678");
        var b = new CellPhoneNumber("(11) 98765-4321");

        // Act
        var hashCode1 = a.GetHashCode();
        var hashCode2 = b.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        // Arrange
        var value = "(54) 91234-5678";
        var cellPhoneNumber = new CellPhoneNumber(value);

        // Act
        var result = cellPhoneNumber.ToString();

        // Assert
        result.Should().Be(value);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("123456789", false)]
    [InlineData("(54) 91234-5678", true)]
    [InlineData("(11) 98765-4321", true)]
    [InlineData("(54) 81234-5678", false)]
    public void IsValid_ShouldReturnExpectedResult(string value, bool expected)
    {
        // Act
        var isValid = CellPhoneNumber.IsValid(value!);

        // Assert
        isValid.Should().Be(expected);
    }
}
