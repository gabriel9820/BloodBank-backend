namespace BloodBank.Core.Entities;

public class User(
    string fullName,
    string email,
    string passwordHash,
    string role) : BaseEntity
{
    public string FullName { get; private set; } = fullName;
    public string Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
    public string Role { get; private set; } = role;
    public bool IsActive { get; private set; } = true;

    public void Update(
        string fullName,
        string role,
        bool isActive)
    {
        FullName = fullName;
        Role = role;
        IsActive = isActive;
    }

    public bool CheckPassword(string passwordHash)
    {
        return PasswordHash == passwordHash;
    }
}
