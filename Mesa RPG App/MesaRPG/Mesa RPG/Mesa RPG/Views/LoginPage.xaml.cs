using FluentValidation;
using Mesa_RPG.Models;
using Mesa_RPG.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Contracts;

namespace Mesa_RPG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        readonly IValidator _validator;
        public LoginPage()
        {
            InitializeComponent();
            _validator = new LoginValidator();
        }

        private async void BtnCadastrar_Clicked(object sender, EventArgs e)
        {
            //await PopupNavigation.PushAsync(new CadastroUserPage(), true);
            await Navigation.PushPopupAsync(new CadastroUserPage());
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {

            Usuario _user = new Usuario();
            _user.NM_USUARIO = txtUser.Text;
            _user.DS_SENHA = txtPassword.Text;
            var resultadoValidacoes = _validator.Validate(_user);

            if (resultadoValidacoes.IsValid)
            {
                _user.DS_SENHA = CryptoSenha.Encrypt(_user.DS_SENHA);
                int login = await new DataService().GetLogin(_user.NM_USUARIO, _user.DS_SENHA);

                if (login == 0)
                {
                    await DisplayAlert("Login Incorreto!", "Falha ao fazer o Login!", "OK");
                }
                else
                {
                    await DisplayAlert("Login Sucesso!", "Login Realizado com Sucesso!", "OK");
                }
            }




                

        }
    }
}