using BloodBank.Application.Commands.AddHospital;

namespace BloodBank.UnitTests.Fakers;

public class AddHospitalCommandFaker : Faker<AddHospitalCommand>
{
    public AddHospitalCommandFaker() : base("pt_BR")
    {
        RuleFor(c => c.Name, f => f.Company.CompanyName());
        RuleFor(c => c.LandlineNumber, f => f.Phone.PhoneNumber("(##) ####-####"));
        RuleFor(c => c.Address, new AddressInputModelFaker().Generate());
    }
}
