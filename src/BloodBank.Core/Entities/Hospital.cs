using BloodBank.Core.ValueObjects;

namespace BloodBank.Core.Entities;

public class Hospital(
    string name,
    LandlineNumber landlineNumber,
    Address address) : BaseEntity
{
    public string Name { get; private set; } = name;
    public LandlineNumber LandlineNumber { get; private set; } = landlineNumber;
    public Address Address { get; private set; } = address;

    /* Navigation Properties */
    public IEnumerable<BloodTransfer> BloodTransfers { get; private set; } = [];

    public void Update(
        string name,
        LandlineNumber landlineNumber,
        Address address)
    {
        Name = name;
        LandlineNumber = landlineNumber;
        Address = address;
    }
}
