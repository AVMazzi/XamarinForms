using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1_Vagas.Modelos;
using App1_Vagas.Banco;

namespace App1_Vagas.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConsultaPage : ContentPage
	{
        List<Vaga> Lista { get; set; }
        public ConsultaPage ()
		{
			InitializeComponent ();

            DataBase bd = new DataBase();
            Lista= bd.Consultar();
            listVagas.ItemsSource = Lista;
            string vagas = "Vagas Disponíveis.";
            lblQtdVagas.Text = Lista.Count.ToString() + vagas;
		}

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CadastroPage());
        }

        private void BtnVagas_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VagasCadastradasPage());
        }

        public void MaisDetalheAction(object sender, EventArgs args)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalhe.GestureRecognizers[0];
            Vaga vaga = tapGest.CommandParameter as Vaga;
            Navigation.PushAsync(new DetalhePage(vaga));
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            listVagas.ItemsSource = Lista.Where(a => a.Cargo.Contains(e.NewTextValue)).ToList();
        }
    }
}