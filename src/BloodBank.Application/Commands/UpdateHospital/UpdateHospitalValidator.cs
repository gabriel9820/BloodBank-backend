using BloodBank.Application.Validators;
using BloodBank.Core.ValueObjects;
using FluentValidation;

namespace BloodBank.Application.Commands.UpdateHospital;

public class UpdateHospitalValidator : AbstractValidator<UpdateHospitalCommand>
{
    public UpdateHospitalValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(250).WithMessage("Nome deve ter no máximo 250 caracteres.");

        RuleFor(x => x.LandlineNumber)
            .NotEmpty().WithMessage("Número de telefone é obrigatório.")
            .Must(LandlineNumber.IsValid).WithMessage("Número de telefone deve estar no formato (XX) XXXX-XXXX.");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Endereço é obrigatório.")
            .SetValidator(new AddressInputModelValidator());
    }
}
