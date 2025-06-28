using BloodBank.Core.Constants;

namespace BloodBank.UnitTests.Core.Constants;

public class UserRolesTests
{
    [Theory]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.Operator)]
    public void IsValid_ShouldReturnTrue_WhenRoleIsValid(string validRole)
    {
        // Act
        var isValid = UserRoles.IsValid(validRole);

        // Assert
        isValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("InvalidRole")]
    [InlineData("")]
    public void IsValid_ShouldReturnFalse_WhenRoleIsInvalid(string invalidRole)
    {
        // Act
        var isValid = UserRoles.IsValid(invalidRole);

        // Assert
        isValid.Should().BeFalse();
    }
}
