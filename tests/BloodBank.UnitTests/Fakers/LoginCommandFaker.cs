using BloodBank.Application.Commands.Login;

namespace BloodBank.UnitTests.Fakers;

public class LoginCommandFaker : Faker<LoginCommand>
{
    public LoginCommandFaker() : base("pt_BR")
    {
        RuleFor(c => c.Email, f => f.Internet.Email());
        RuleFor(c => c.Password, f => f.Internet.Password());
    }
}
