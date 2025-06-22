using BloodBank.Core.ValueObjects;

namespace BloodBank.Core.Entities;

public class User(
    string fullName,
    CellPhoneNumber cellPhoneNumber,
    Email email,
    string passwordHash,
    string role) : BaseEntity
{
    public string FullName { get; private set; } = fullName;
    public CellPhoneNumber CellPhoneNumber { get; private set; } = cellPhoneNumber;
    public Email Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
    public string Role { get; private set; } = role;
    public bool IsActive { get; private set; } = true;

    protected User() : this(default!, default!, default!, default!, default!) { }

    public void Update(
        string fullName,
        CellPhoneNumber cellPhoneNumber,
        string role,
        bool isActive)
    {
        FullName = fullName;
        CellPhoneNumber = cellPhoneNumber;
        Role = role;
        IsActive = isActive;
    }

    public bool CheckPassword(string passwordHash)
    {
        return PasswordHash == passwordHash;
    }
}
