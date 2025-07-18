using BloodBank.Core.Constants;
using BloodBank.Core.Entities;
using BloodBank.Core.ValueObjects;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Core.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var fullName = "Test User";
        var cellPhoneNumber = new CellPhoneNumber("(54) 91234-5678");
        var email = new Email("testuser@example.com");
        var passwordHash = "hashedpassword";
        var role = UserRoles.Admin;
        var isLowStockNotificationEnabled = true;

        // Act
        var user = new User(fullName, cellPhoneNumber, email, passwordHash, role, isLowStockNotificationEnabled);

        // Assert
        user.FullName.Should().Be(fullName);
        user.CellPhoneNumber.Should().Be(cellPhoneNumber);
        user.Email.Should().Be(email);
        user.PasswordHash.Should().Be(passwordHash);
        user.Role.Should().Be(role);
        user.IsLowStockNotificationEnabled.Should().Be(isLowStockNotificationEnabled);
    }

    [Fact]
    public void Update_ShouldModifyProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var user = new UserFaker().Generate();

        var updatedFullName = "Updated User";
        var updatedCellPhoneNumber = new CellPhoneNumber("(54) 98765-4321");
        var updatedRole = UserRoles.Operator; ;
        var updatedIsActive = false;
        var updatedIsLowStockNotificationEnabled = false;

        // Act
        user.Update(updatedFullName, updatedCellPhoneNumber, updatedRole, updatedIsActive, updatedIsLowStockNotificationEnabled);

        // Assert
        user.FullName.Should().Be(updatedFullName);
        user.CellPhoneNumber.Should().Be(updatedCellPhoneNumber);
        user.Role.Should().Be(updatedRole);
        user.IsActive.Should().Be(updatedIsActive);
        user.IsLowStockNotificationEnabled.Should().Be(updatedIsLowStockNotificationEnabled);
    }

    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        // Arrange
        var user = new UserFaker().Generate();

        // Act
        user.Activate();

        // Assert
        user.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var user = new UserFaker().Generate();

        // Act
        user.Deactivate();

        // Assert
        user.IsActive.Should().BeFalse();
    }
}
