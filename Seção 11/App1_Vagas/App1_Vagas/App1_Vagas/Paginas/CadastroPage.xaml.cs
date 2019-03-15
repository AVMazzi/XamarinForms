using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1_Vagas.Modelos;
using App1_Vagas.Banco;
using App1_Vagas.Servicos;
using App1_Vagas.Validacoes;

namespace App1_Vagas.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPage : ContentPage
    {
        private List<Municipio> listaCidades { get; set; }
        private List<Municipio> filtroCidades { get; set; }

        public CadastroPage()
        {
            InitializeComponent();
            cbEstados.ItemsSource = Servicos.Servicos.GetEstados();
            cbCidades.ItemsSource = Servicos.Servicos.GetAllMunicipios();
        }

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            //TODO - VALIDAR DADOS CADASTRAIS

            Vaga objVaga = new Vaga();
            Estado estado = (Estado)cbEstados.SelectedItem;
            Municipio cidade = (Municipio)cbCidades.SelectedItem;
            objVaga.Cargo = txtVaga.Text;
            objVaga.Quantidade = int.Parse(txtQuantidade.Text);
            objVaga.Salario = double.Parse(txtSalario.Text);
            objVaga.Empresa = txtEmpresa.Text;
            objVaga.Cidade = cidade.Nome;
            objVaga.Estado = estado.Nome;
            objVaga.Descricao = edtDescricao.Text;
            objVaga.TipoContratacao = (swtTipoContratacao.IsToggled) ? "PJ" : "CLT";
            objVaga.Telefone = txtTelefone.Text;
            objVaga.Email = txtEmail.Text;

            bool validador = VagaValidation.vagaValidation(objVaga);
            DataBase db = new DataBase();
            db.Cadastro(objVaga);
            App.Current.MainPage = new NavigationPage(new ConsultaPage());
        }

        private void CbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            Estado estado = (Estado)cbEstados.SelectedItem;
            listaCidades = Servicos.Servicos.GetMunicipios(estado.Id);
            cbCidades.ItemsSource = listaCidades;
            cbCidades.IsEnabled = true;
            txtCidade.IsEnabled = true;
        }

        private void TxtCidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            filtroCidades =listaCidades.Where(a => a.Nome.Contains(e.NewTextValue)).ToList();
            cbCidades.ItemsSource = filtroCidades;
        }
    }
}