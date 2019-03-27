using System;
using System.Collections.Generic;
using System.Text;
using Mesa_RPG.Models;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;


namespace Mesa_RPG.Models
{
    public class UserValidations : AbstractValidator<Usuario>
    {
        public UserValidations()
        {
            RuleFor(a => a.NM_USUARIO).NotNull()
                .NotEmpty()
                .Length(8, 30).WithMessage("Informe: USUÁRIO!");
            RuleFor(a => a.DS_EMAIL).NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("E-mail Inválido.");
            RuleFor(a => a.DS_SENHA.ToString()).NotNull()
                .NotEmpty()
                .Length(1, 10).WithMessage("IInforme: SENHA!");                
        }


        public static bool TesteValidator(string teste)
        {
            bool valido = true;
            long novoTeste = 0;
            if (!long.TryParse(teste, out novoTeste))
            {
                valido = false;
            }
            return valido;
        }
    }
}
