using FluentValidation;
using Mesa_RPG.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Mesa_RPG.Service;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Mesa_RPG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroUserPage
    {
        readonly IValidator _validator;
        public CadastroUserPage()
        {
            InitializeComponent();
            _validator = new UserValidations();
        }

        #region Métodos
       
        #endregion
        private void Onclose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void ImageButton_ClickedAsync(object sender, EventArgs e)
        {
            Usuario _user = new Usuario();
            _user.DS_EMAIL = txtEmail.Text;
            _user.NM_USUARIO = txtUser.Text;
            _user.DS_SENHA = txtSenha.Text;
            var resultadoValidacoes = _validator.Validate(_user);

            if (resultadoValidacoes.IsValid)
            {
                _user.DS_SENHA = CryptoSenha.Encrypt(txtSenha.Text);
                await new DataService().AddUserAsync(_user);
                await DisplayAlert("Sucesso", "Cadastro Realizado com Sucesso!", "OK");
                App.Current.MainPage = new NavigationPage(new CadastroUserPage());
            }
            else
            {
                await DisplayAlert("Error", resultadoValidacoes.Errors[0].ErrorMessage, "Ok");
            }
        }
    }
}