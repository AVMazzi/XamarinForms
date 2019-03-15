using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1_Vagas.Modelos;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1_Vagas.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalhePage : ContentPage
	{
		public DetalhePage (Vaga vaga)
		{
			InitializeComponent ();

            DisplayAlert("Mensagem", vaga.Cargo, "OK");
            BindingContext = vaga;
        }
	}
}