using BloodBank.Application.Commands.Register;
using BloodBank.Core.Constants;

namespace BloodBank.UnitTests.Fakers;

public class RegisterCommandFaker : Faker<RegisterCommand>
{
    public RegisterCommandFaker() : base("pt_BR")
    {
        RuleFor(c => c.FullName, f => f.Person.FullName);
        RuleFor(c => c.CellPhoneNumber, f => f.Phone.PhoneNumber("(##) 9####-####"));
        RuleFor(c => c.Email, f => f.Internet.Email());
        RuleFor(c => c.Password, f => f.Internet.Password());
        RuleFor(c => c.Role, f => f.PickRandom(UserRoles.Admin, UserRoles.Operator));
    }
}
