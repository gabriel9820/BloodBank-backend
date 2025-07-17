using BloodBank.Core.ValueObjects;

namespace BloodBank.Core.Entities;

public class Hospital : BaseEntity
{
    public string Name { get; private set; }
    public LandlineNumber LandlineNumber { get; private set; }
    public Address Address { get; private set; }

    /* Navigation Properties */
    public IEnumerable<BloodTransfer> BloodTransfers { get; private set; }

    protected Hospital() { }

    public Hospital(
        string name,
        LandlineNumber landlineNumber,
        Address address)
    {
        Name = name;
        LandlineNumber = landlineNumber;
        Address = address;
    }

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
