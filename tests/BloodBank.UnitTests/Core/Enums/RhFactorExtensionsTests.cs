using BloodBank.Core.Enums;

namespace BloodBank.UnitTests.Core.Enums;

public class RhFactorExtensionsTests
{
    [Theory]
    [InlineData(RhFactor.Positive, "+")]
    [InlineData(RhFactor.Negative, "-")]
    public void ToDisplayString_ShouldReturnCorrectSymbol(RhFactor rh, string expected)
    {
        // Act
        var result = rh.ToDisplayString();

        // Assert
        result.Should().Be(expected);
    }
}
