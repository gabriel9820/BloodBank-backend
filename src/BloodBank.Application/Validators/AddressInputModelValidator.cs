using BloodBank.Application.DTOs.InputModels;
using FluentValidation;

namespace BloodBank.Application.Validators;

public class AddressInputModelValidator : AbstractValidator<AddressInputModel>
{
    public AddressInputModelValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Rua é obrigatória.")
            .MaximumLength(100).WithMessage("Rua deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Número é obrigatório.")
            .MaximumLength(10).WithMessage("Número deve ter no máximo 10 caracteres.");

        RuleFor(x => x.Neighborhood)
            .NotEmpty().WithMessage("Bairro é obrigatório.")
            .MaximumLength(50).WithMessage("Bairro deve ter no máximo 50 caracteres.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Cidade é obrigatória.")
            .MaximumLength(50).WithMessage("Cidade deve ter no máximo 50 caracteres.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("Estado é obrigatório.")
            .MaximumLength(50).WithMessage("Estado deve ter no máximo 50 caracteres.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("CEP é obrigatório.")
            .Matches(@"^\d{5}-\d{3}$").WithMessage("CEP deve estar no formato XXXXX-XXX.");
    }
}
