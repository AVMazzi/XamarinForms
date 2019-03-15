using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using App1_Vagas.Validacoes;

namespace App1_Vagas.Modelos
{
    [Table("Vaga")]
    public class Vaga
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Cargo{ get; set; }
        public string Empresa { get; set; }
        public int Quantidade { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public double Salario { get; set; }
        public string Descricao { get; set; }
        public string TipoContratacao { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
