namespace BloodBank.Core.ValueObjects;

public class Address(
    string street,
    string number,
    string neighborhood,
    string city,
    string state,
    string zipCode)
{
    public string Street { get; private set; } = street;
    public string Number { get; private set; } = number;
    public string Neighborhood { get; private set; } = neighborhood;
    public string City { get; private set; } = city;
    public string State { get; private set; } = state;
    public string ZipCode { get; private set; } = zipCode;

    public override string ToString()
    {
        return $"{Street}, {Number} - {Neighborhood}, {City}-{State}, {ZipCode}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Address other)
        {
            return false;
        }

        return Street == other.Street &&
               Number == other.Number &&
               Neighborhood == other.Neighborhood &&
               City == other.City &&
               State == other.State &&
               ZipCode == other.ZipCode;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, Number, Neighborhood, City, State, ZipCode);
    }
}
