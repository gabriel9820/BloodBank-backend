using BloodBank.Application.Validators;
using BloodBank.Core.ValueObjects;
using FluentValidation;

namespace BloodBank.Application.Commands.UpdateDonor;

public class UpdateDonorValidator : AbstractValidator<UpdateDonorCommand>
{
    public UpdateDonorValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(250).WithMessage("Nome deve ter no máximo 250 caracteres.");

        RuleFor(x => x.CellPhoneNumber)
            .NotEmpty().WithMessage("Número de celular é obrigatório.")
            .Must(CellPhoneNumber.IsValid).WithMessage("Número de celular deve estar no formato (XX) 9XXXX-XXXX.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail informado não é válido.")
            .MaximumLength(250).WithMessage("E-mail deve ter no máximo 250 caracteres.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Data de nascimento é obrigatória.");

        RuleFor(x => x.Gender)
            .NotNull().WithMessage("Gênero é obrigatório.")
            .IsInEnum().WithMessage("Gênero deve ser um valor válido.");

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Peso deve ser maior que zero.");

        RuleFor(x => x.BloodType)
            .NotNull().WithMessage("Tipo sanguíneo é obrigatório.")
            .IsInEnum().WithMessage("Tipo sanguíneo deve ser um valor válido.");

        RuleFor(x => x.RhFactor)
            .NotNull().WithMessage("Fator Rh é obrigatório.")
            .IsInEnum().WithMessage("Fator Rh deve ser um valor válido.");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Endereço é obrigatório.")
            .SetValidator(new AddressInputModelValidator());
    }
}
