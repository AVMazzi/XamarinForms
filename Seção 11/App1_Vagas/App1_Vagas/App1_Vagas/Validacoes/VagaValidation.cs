using System;
using System.Collections.Generic;
using System.Text;
using App1_Vagas.Modelos;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;

namespace App1_Vagas.Validacoes
{
    public class VagaValidation : AbstractValidator<Vaga>
    {
        public VagaValidation()
        {
            RuleFor(a => a.Cargo).NotNull()
                .NotEmpty()
                .Length(8, 30).WithMessage("Informe o cargo disponível.");
            RuleFor(a => a.Empresa).NotNull()
                .NotEmpty()
                .Length(3, 30).WithMessage("Informe o nome da Empresa.");
            RuleFor(a => a.Quantidade.ToString()).NotNull()
                .NotEmpty()
                .Length(1, 3).WithMessage("Informe a quantidade de vagas.");
            RuleFor(a => a.Cidade).NotNull()
                .NotEmpty()
                .Length(8, 30).WithMessage("Informe a Cidade.");
            RuleFor(a => a.Estado).NotNull()
                .NotEmpty()
                .Length(8, 30).WithMessage("Informe o Estado.");
            RuleFor(a => a.Salario.ToString()).NotNull()
                .NotEmpty()
                .Length(6, 30).WithMessage("Informe o Salário.");
            RuleFor(a => a.Descricao).NotNull()
                .NotEmpty()
                .Length(8, 100).WithMessage("Informe o Requisito do para vaga.");
            RuleFor(a => a.TipoContratacao).NotNull()
                .NotEmpty()
                .Length(8, 30).WithMessage("Informe o tipo de Contratação.");
            RuleFor(a => a.Telefone).NotNull()
                .NotEmpty()
                .Length(13, 14).WithMessage("Informe o Estado.");
            RuleFor(a => a.Email).NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("E-mail Inválido.");
        }

        public static bool vagaValidation(Vaga vaga)
        {
            var validator = new VagaValidation();
            var validRes = validator.Validate(vaga);
            bool validador = false;
            if (validRes.IsValid)
            {
                string telefone = vaga.Telefone;
                string telefoneNum = Regex.Replace(telefone, "[^0-9]", "");
                int teste = int.Parse(telefoneNum);
                validador = TesteValidator(telefoneNum);
                return validador;
            }

            return validador;
        }

        public static bool TesteValidator(string teste)
        {
            bool valido = true;
            int novoTeste = 0;

            if (!int.TryParse(teste, out novoTeste))
            {
                valido = false;
            }
            else if (teste.Length != 8)
            {
                valido = false;
            }
            return valido;
        }
    }
}