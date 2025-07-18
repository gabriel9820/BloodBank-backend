using BloodBank.Core.ValueObjects;

namespace BloodBank.Core.Entities;

public class User : BaseEntity
{
    public string FullName { get; private set; }
    public CellPhoneNumber CellPhoneNumber { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsLowStockNotificationEnabled { get; private set; }

    protected User() { }

    public User(
        string fullName,
        CellPhoneNumber cellPhoneNumber,
        Email email,
        string passwordHash,
        string role,
        bool isLowStockNotificationEnabled)
    {
        FullName = fullName;
        CellPhoneNumber = cellPhoneNumber;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsLowStockNotificationEnabled = isLowStockNotificationEnabled;
        IsActive = true;
    }

    public void Update(
        string fullName,
        CellPhoneNumber cellPhoneNumber,
        string role,
        bool isActive,
        bool isLowStockNotificationEnabled)
    {
        FullName = fullName;
        CellPhoneNumber = cellPhoneNumber;
        Role = role;
        IsActive = isActive;
        IsLowStockNotificationEnabled = isLowStockNotificationEnabled;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
