using BloodBank.Core.Constants;
using BloodBank.Core.ValueObjects;
using FluentValidation;

namespace BloodBank.Application.Commands.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
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

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Perfil do usuário é obrigatório.")
            .Must(UserRoles.IsValid).WithMessage($"Perfil do usuário não é válido.");
    }
}
