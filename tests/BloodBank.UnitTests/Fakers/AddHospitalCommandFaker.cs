using BloodBank.Application.Commands.AddHospital;

namespace BloodBank.UnitTests.Fakers;

public class AddHospitalCommandFaker : Faker<AddHospitalCommand>
{
    public AddHospitalCommandFaker() : base("pt_BR")
    {
        CustomInstantiator(f => new AddHospitalCommand
        {
            Name = f.Company.CompanyName(),
            LandlineNumber = f.Phone.PhoneNumber("(##) ####-####"),
            Address = new AddressInputModelFaker().Generate(),
        });
    }
}
