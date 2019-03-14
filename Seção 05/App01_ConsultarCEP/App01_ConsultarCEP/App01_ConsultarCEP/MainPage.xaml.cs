using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnCEP_Clicked(object sender, EventArgs e)
        {
            
            string cep = txtCEP.Text.Trim();
           

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                    {
                        lblResultado.Text = string.Format("Endereço: \n {0},\n {1}, {2} - {3} ",
                                                       end.Logradouro, end.Bairro, end.Localidade, end.Uf);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "Endereço não encontrado para o CEP " + cep , "OK");
                    }
                }
                catch (Exception E)
                {

                    DisplayAlert("ERRO CRÍTICO", E.Message, "OK");
                } 
            }
        }
        private bool isValidCEP(string cep)
        {
            bool valido = true;
            int novoCep = 0;

            if (!int.TryParse(cep, out novoCep))
            {
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter apenas números", "OK");
                valido = false;
            }
            else if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter 8 números", "OK");
                valido = false;
            }       

            return valido;
        }
    }
}
