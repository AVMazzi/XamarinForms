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
using FluentValidation;
using System.Text.RegularExpressions;

namespace App1_Vagas.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPage : ContentPage
    {
        private Vaga vaga { get; set; }
        private List<Municipio> listaCidades { get; set; }
        private List<Municipio> filtroCidades { get; set; }
        private static List<Municipio> Cidades { get; set; }
        private static List<Estado> Estados { get; set; }

        readonly IValidator _validator;

        public CadastroPage()
        {
            InitializeComponent();

            _validator = new VagaValidation();
            Cidades = Servicos.Servicos.GetAllMunicipios();
            Estados = Servicos.Servicos.GetEstados();

            cbEstados.ItemsSource = Estados.OrderBy(a=>a.Nome).ToList();
            cbCidades.ItemsSource = Cidades.OrderBy(a => a.Nome).ToList(); ;
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

            string telefone = objVaga.Telefone;
            string telefoneNum = Regex.Replace(telefone, "[^0-9]", "").Trim();
            objVaga.Telefone = telefoneNum;
            var resultadoValidacoes = _validator.Validate(objVaga);

            if (resultadoValidacoes.IsValid)
            {
                DataBase database = new DataBase();
                database.Cadastro(objVaga);
                DisplayAlert("Sucesso", "Cadastro Realizado com Sucesso!", "OK");
                App.Current.MainPage = new NavigationPage(new VagasCadastradasPage());
            }
            else
            {
                DisplayAlert("Error", resultadoValidacoes.Errors[0].ErrorMessage, "Ok");
            }
            
        }

        private void CbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            Estado estado = (Estado)cbEstados.SelectedItem;
            listaCidades = Servicos.Servicos.GetMunicipios(estado.Id);
            cbCidades.ItemsSource = listaCidades.OrderBy(a => a.Nome).ToList();
        }

        private void TxtCidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (listaCidades != null)
            {
                var selecao1 = cbCidades.SelectedIndex;
                var selectItem = cbCidades.SelectedItem;
                filtroCidades = listaCidades.Where(a => a.Nome.Contains(e.NewTextValue)).ToList();
                cbCidades.ItemsSource = filtroCidades.OrderBy(x => x.Nome).ToList();
            }
            else
            {
                filtroCidades = Cidades.Where(a => a.Nome.Contains(e.NewTextValue)).ToList();
                cbCidades.ItemsSource = filtroCidades.OrderBy(x => x.Nome).ToList();
            }
        }
    }
}