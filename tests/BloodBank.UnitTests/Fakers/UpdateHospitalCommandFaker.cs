using BloodBank.Application.Commands.UpdateHospital;

namespace BloodBank.UnitTests.Fakers;

public class UpdateHospitalCommandFaker : Faker<UpdateHospitalCommand>
{
    public UpdateHospitalCommandFaker() : base("pt_BR")
    {
        RuleFor(c => c.Id, f => f.Random.Int(1, 1000));
        RuleFor(c => c.Name, f => f.Company.CompanyName());
        RuleFor(c => c.LandlineNumber, f => f.Phone.PhoneNumber("(##) ####-####"));
        RuleFor(c => c.Address, new AddressInputModelFaker().Generate());
    }
}
