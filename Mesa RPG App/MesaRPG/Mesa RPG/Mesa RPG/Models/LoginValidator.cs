using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mesa_RPG.Models
{
    class LoginValidator:AbstractValidator<Usuario>
    {
        public LoginValidator()
        {
            RuleFor(a => a.NM_USUARIO).NotNull()
                .NotEmpty()
                .Length(3, 30).WithMessage("Informe: USUÁRIO ACIMA DE 3 CARACTERES!");
            RuleFor(a => a.DS_SENHA.ToString()).NotNull()
                .NotEmpty()
                .Length(6, 10).WithMessage("Informe: SENHA COM 6 A 10 CARACTERES!");
        }
    }
}
