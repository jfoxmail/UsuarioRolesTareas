using DoubleV.UsuarioRolesTareas.Application.Models;
using FluentValidation;

namespace DoubleV.UsuarioRolesTareas.Application.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioModel>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull().NotEmpty().MaximumLength(200);

            RuleFor(x => x.Contraseña)
                .NotNull().NotEmpty().MaximumLength(50);

            RuleFor(x => x.Email)
                .NotNull().NotEmpty().MaximumLength(200)
                .EmailAddress().WithMessage("El correo electrónico no es válido");
        }
    }
}

