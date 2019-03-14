using System;
using System.Collections.Generic;
using System.Text;

namespace App01_ConsultarCEP.Servico.Modelo
{
    public class Endereco
    {
        private string cep;
        private string logradouro;
        private string bairro;
        private string localidade;
        private string uf;
        private string unidade;
        private string ibge;
        private string gia;

        public string Cep { get => cep; set => cep = value; }
        public string Logradouro { get => logradouro; set => logradouro = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Localidade { get => localidade; set => localidade = value; }
        public string Uf { get => uf; set => uf = value; }
        public string Unidade { get => unidade; set => unidade = value; }
        public string Ibge { get => ibge; set => ibge = value; }
        public string Gia { get => gia; set => gia = value; }
    }
}
