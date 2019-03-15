using App1_Vagas.Banco;
using App1_Vagas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1_Vagas.Validacoes;

namespace App1_Vagas.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarPage : ContentPage
    {
        private Vaga vaga { get; set; }
        private List<Municipio> listaCidades { get; set; }
        private List<Municipio> filtroCidades { get; set; }
        public EditarPage(Vaga vaga)
        {
            InitializeComponent();

            this.vaga = vaga;
            cbEstados.ItemsSource = Servicos.Servicos.GetEstados();
            cbCidades.ItemsSource = Servicos.Servicos.GetAllMunicipios();

            NomeVaga.Text = vaga.Cargo;
            Empresa.Text = vaga.Empresa;
            Quantidade.Text = vaga.Quantidade.ToString();
            cbCidades.SelectedItem = vaga.Cidade;
            cbEstados.SelectedItem = vaga.Estado;
            Salario.Text = vaga.Salario.ToString();
            Descricao.Text = vaga.Descricao;
            TipoContratacao.IsToggled = (vaga.TipoContratacao == "CLT") ? false : true;
            Telefone.Text = vaga.Telefone;
            Email.Text = vaga.Email;
        }
        public void SalvarAction(object sender, EventArgs args)
        {
            
            Estado estado = (Estado)cbEstados.SelectedItem;
            Municipio cidade = (Municipio)cbCidades.SelectedItem;
            vaga.Cargo = NomeVaga.Text;
            vaga.Quantidade = short.Parse(Quantidade.Text);
            vaga.Salario = double.Parse(Salario.Text);
            vaga.Empresa = Empresa.Text;
            vaga.Cidade = cidade.Nome;
            vaga.Estado = estado.Nome;
            vaga.Descricao = Descricao.Text;
            vaga.TipoContratacao = (TipoContratacao.IsToggled) ? "PJ" : "CLT";
            vaga.Telefone = Telefone.Text;
            vaga.Email = Email.Text;

            bool validador = VagaValidation.vagaValidation(vaga);
            if (validador)
            {
                DataBase database = new DataBase();
                database.Atualizar(vaga);
                DisplayAlert("Sucesso", "Atualizado com Sucesso!", "OK");
            }
            
            

            App.Current.MainPage = new NavigationPage(new VagasCadastradasPage());
        }

        private void CbEstados_SelectedIndexChanged(object sender, SelectedItemChangedEventArgs e)
        {
            Estado estado = (Estado)cbEstados.SelectedItem;
            listaCidades = Servicos.Servicos.GetMunicipios(estado.Id);
            cbCidades.ItemsSource = listaCidades;
        }

        private void TxtCidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            filtroCidades = listaCidades.Where(a => a.Nome.Contains(e.NewTextValue)).ToList();
            cbCidades.ItemsSource = filtroCidades;
        }
    }
}