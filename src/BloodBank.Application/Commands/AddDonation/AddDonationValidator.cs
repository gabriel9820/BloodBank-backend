using BloodBank.Core.Constants;
using FluentValidation;

namespace BloodBank.Application.Commands.AddDonation;

public class AddDonationValidator : AbstractValidator<AddDonationCommand>
{
    public AddDonationValidator()
    {
        RuleFor(command => command.DonationDate)
            .NotEmpty().WithMessage("Data é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data não pode ser futura.");

        RuleFor(command => command.QuantityML)
            .NotEmpty().WithMessage("Quantidade é obrigatória.")
            .InclusiveBetween(DonationRules.MIN_DONATION_QUANTITY_ML, DonationRules.MAX_DONATION_QUANTITY_ML)
            .WithMessage($"Quantidade deve estar entre {DonationRules.MIN_DONATION_QUANTITY_ML} e {DonationRules.MAX_DONATION_QUANTITY_ML} ml.");

        RuleFor(command => command.DonorId)
            .NotEmpty().WithMessage("ID do Doador é obrigatório.");
    }
}
