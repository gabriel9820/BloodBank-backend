using BloodBank.Core.Constants;
using BloodBank.Core.Entities;
using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Fakers;

public class UserFaker : Faker<User>
{
    public UserFaker() : base("pt_BR")
    {
        CustomInstantiator(f => new User(
            f.Person.FullName,
            new CellPhoneNumber(f.Phone.PhoneNumber("(##) 9####-####")),
            new Email(f.Internet.Email()),
            f.Internet.Password(),
            f.PickRandom(UserRoles.Admin, UserRoles.Operator),
            f.Random.Bool()
        ));

        RuleFor(u => u.Id, f => f.Random.Int(1, 1000));
    }
}
