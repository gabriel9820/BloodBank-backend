using BloodBank.Core.Constants;
using BloodBank.Core.Entities;
using BloodBank.Core.ValueObjects;

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

        // Act
        var user = new User(fullName, cellPhoneNumber, email, passwordHash, role);

        // Assert
        user.FullName.Should().Be(fullName);
        user.CellPhoneNumber.Should().Be(cellPhoneNumber);
        user.Email.Should().Be(email);
        user.PasswordHash.Should().Be(passwordHash);
        user.Role.Should().Be(role);
    }

    [Fact]
    public void Update_ShouldModifyProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var user = CreateValidUser();

        var updatedFullName = "Updated User";
        var updatedCellPhoneNumber = new CellPhoneNumber("(54) 98765-4321");
        var updatedRole = UserRoles.Operator; ;
        var updatedIsActive = false;

        // Act
        user.Update(updatedFullName, updatedCellPhoneNumber, updatedRole, updatedIsActive);

        // Assert
        user.FullName.Should().Be(updatedFullName);
        user.CellPhoneNumber.Should().Be(updatedCellPhoneNumber);
        user.Role.Should().Be(updatedRole);
        user.IsActive.Should().Be(updatedIsActive);
    }

    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        // Arrange
        var user = CreateValidUser();

        // Act
        user.Activate();

        // Assert
        user.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var user = CreateValidUser();

        // Act
        user.Deactivate();

        // Assert
        user.IsActive.Should().BeFalse();
    }

    private static User CreateValidUser() => new(
        "Test User",
        new CellPhoneNumber("(54) 91234-5678"),
        new Email("testuser@example.com"),
        "hashedpassword",
        UserRoles.Admin
    );
}
