using System;
using System.Collections.Generic;

using System.Text;
using Mesa_RPG.Models;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;
using Mesa_RPG.Service;
using System.Data;
using System.Threading.Tasks;

namespace Mesa_RPG.Models
{
    public class UserValidations : AbstractValidator<Usuario>
    {
        public UserValidations()
        {
            RuleFor(a => a.NM_USUARIO).NotNull()
                .NotEmpty()
                .Must(NameValidator).WithMessage("Usuário já cadastrado!")
                .Length(3, 30).WithMessage("Informe: USUÁRIO COM 3 A 30 CARACTERES!");
            RuleFor(a => a.DS_EMAIL).NotNull()
                .NotEmpty()
                 .Must(EmailValidator).WithMessage("E-mail já cadastrado!")
                .EmailAddress()
                .WithMessage("E-mail Inválido.");
            RuleFor(a => a.DS_SENHA.ToString()).NotNull()
                .NotEmpty()
                .Length(6, 10).WithMessage("Informe: SENHA COM 6 A 10 CARACTERES!");                
        }


        public static bool numberValidator(string teste)
        {
            bool valido = true;
            long novoTeste = 0;
            if (!long.TryParse(teste, out novoTeste))
            {
                valido = false;
            }
            return valido;
        }

        private static bool NameValidator(string name)
        {
            bool valido = true;
            Usuario _user  = new DataService().GetUsuarioByNameAsync(name);
            if (_user.CD_USUARIO != -1)
            {
                valido = false;
            }
            return valido;
        }

        private static bool EmailValidator(string email)
        {
            bool valido = true; ;
            Usuario _user = new DataService().GetUsuarioByEmailAsync(email);
            if (_user.CD_USUARIO != -1)
            {
                valido = false;
            }
            return valido;
        }

    }
}
