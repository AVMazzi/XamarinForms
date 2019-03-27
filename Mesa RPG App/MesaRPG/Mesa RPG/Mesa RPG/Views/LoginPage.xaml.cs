using FluentValidation;
using Mesa_RPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mesa_RPG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        readonly IValidator _validator;
        public LoginPage()
        {
            InitializeComponent();
            _validator = new UserValidations();
        }

        private void BtnCadastrar_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {

        }
    }
}