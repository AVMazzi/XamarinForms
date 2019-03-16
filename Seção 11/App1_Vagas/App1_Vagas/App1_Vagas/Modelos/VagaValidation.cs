using System;
using System.Collections.Generic;
using System.Text;
using App1_Vagas.Modelos;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;

using App1_Vagas.Paginas;

namespace App1_Vagas.Modelos
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
                .Length(1, 30).WithMessage("Informe o Estado.");
            RuleFor(a => a.Salario.ToString()).NotNull()
                .NotEmpty()
                .Length(6, 30).WithMessage("Informe o Salário.");
            RuleFor(a => a.Descricao).NotNull()
                .NotEmpty()
                .Length(8, 100).WithMessage("Informe o Requisito do para vaga.");
            RuleFor(a => a.TipoContratacao).NotNull()
                .NotEmpty()
                .Length(2, 3).WithMessage("Informe o tipo de Contratação.");
            RuleFor(a => a.Telefone).NotNull()
                .NotEmpty()
                .Must(TesteValidator).WithMessage("Telefone deve conter apenas números.")
                .Length(10, 11).WithMessage("Telefone Inválido.");
            RuleFor(a => a.Email).NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("E-mail Inválido.");
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