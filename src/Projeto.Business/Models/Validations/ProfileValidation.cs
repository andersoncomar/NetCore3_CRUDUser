using FluentValidation;

namespace Projeto.Business.Models.Validations
{
    public class ProfileValidation : AbstractValidator<Profile>
    {
        public ProfileValidation()
        {
            RuleFor(p => p.Avatar)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.Endereco)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.Documento)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 5)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
