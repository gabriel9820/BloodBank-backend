using BloodBank.Core.ValueObjects;

namespace BloodBank.Core.Entities;

public class Hospital(
    string name,
    Address address) : BaseEntity
{
    public string Name { get; private set; } = name;
    public Address Address { get; private set; } = address;

    /* Navigation Properties */
    public IEnumerable<BloodTransfer> BloodTransfers { get; private set; } = [];

    public void Update(string name, Address address)
    {
        Name = name;
        Address = address;
    }
}
