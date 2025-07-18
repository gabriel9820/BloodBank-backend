using FluentValidation;

namespace BloodBank.Application.Commands.AddBloodTransfer;

public class AddBloodTransferValidator : AbstractValidator<AddBloodTransferCommand>
{
    public AddBloodTransferValidator()
    {
        RuleFor(x => x.TransferDate)
            .NotEmpty().WithMessage("Data é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data não pode ser futura.");

        RuleFor(x => x.BloodType)
            .NotNull().WithMessage("Tipo sanguíneo é obrigatório.")
            .IsInEnum().WithMessage("Tipo sanguíneo deve ser um valor válido.");

        RuleFor(x => x.RhFactor)
            .NotNull().WithMessage("Fator Rh é obrigatório.")
            .IsInEnum().WithMessage("Fator Rh deve ser um valor válido.");

        RuleFor(x => x.QuantityML)
            .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.");

        RuleFor(x => x.HospitalId)
            .NotEmpty().WithMessage("ID do Hospital é obrigatório.");
    }
}
