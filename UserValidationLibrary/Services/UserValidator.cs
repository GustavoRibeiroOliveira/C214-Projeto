using FluentValidation;
using UserValidationLibrary.Models;

namespace UserValidationLibrary.Services;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Nome não pode ser vazio.")
            .MinimumLength(2);

        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches("[A-Z]").WithMessage("Senha deve conter letra maiúscula.")
            .Matches("[a-z]").WithMessage("Senha deve conter letra minúscula.")
            .Matches("[0-9]").WithMessage("Senha deve conter número.");

        RuleFor(user => user.BirthDate)
            .Must(BeOver18).WithMessage("Usuário deve ter mais de 18 anos.");

        RuleFor(user => user.CPF)
            .NotEmpty()
            .Matches(@"^\d{11}$").WithMessage("CPF deve conter 11 dígitos numéricos.");

        RuleFor(user => user.Endereco)
            .NotEmpty().WithMessage("Endereço é obrigatório.");

        RuleFor(user => user.CEP)
            .NotEmpty()
            .Matches(@"^\d{8}$").WithMessage("CEP deve conter 8 dígitos numéricos.");

        RuleFor(user => user.Telefone)
            .NotEmpty()
            .Matches(@"^\d{10,11}$").WithMessage("Telefone deve conter 10 ou 11 dígitos numéricos.");
    }

    private bool BeOver18(DateTime birthDate)
    {
        var age = DateTime.Today.Year - birthDate.Year;
        if (birthDate > DateTime.Today.AddYears(-age)) age--;
        return age >= 18;
    }
}
