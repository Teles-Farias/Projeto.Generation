using blogpessoal.Model;
using FluentValidation;

namespace blogpessoal.Validator
{
    public class AlunoValidator : AbstractValidator<Aluno>
    {
         public AlunoValidator() 
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(u => u.Idade)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(u => u.NotaPrimeiroSemestre)
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(255);

            RuleFor(u => u.NotaSegundoSemestre)
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(255);

            RuleFor(u => u.Professor)
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(255);

            RuleFor(u => u.Sala)
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(255);
        }  
    }
}