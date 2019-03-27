using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mesa_RPG.Service;
using Mesa_RPG.Models;

namespace Mesa_RPG
{
    public partial class MainPage : ContentPage
    {
        List<Usuario> _usuarios;
        DataService dataService;
        public MainPage()
        {
            InitializeComponent();
            dataService = new DataService();
            AtualizaDados();
        }

        private async void AtualizaDados()
        {
            _usuarios = await dataService.GetUsuarioAsync();
        }

        private async void BtnSearch_Clicked(object sender, EventArgs e)
        {
            if (txtNome.Text == null && txtEmail.Text == null && txtCd.Text == null)
            {
                listUsuario.ItemsSource = _usuarios.OrderBy(item => item.CD_USUARIO).ToList();
                LimpaUsuario();
            }
            else if (txtNome.Text != null && txtEmail.Text == null && txtCd.Text == null)
            {

                Usuario user = await new DataService().GetUsuarioByNameAsync(txtNome.Text);
                List<Usuario> usuarios = new List<Usuario>();
                usuarios.Add(user);
                listUsuario.ItemsSource = usuarios.OrderBy(item => item.CD_USUARIO).ToList();
                LimpaUsuario();
            }
            else if (txtNome.Text == null && txtEmail.Text != null && txtCd.Text == null)
            {
                Usuario user = await new DataService().GetUsuarioByEmailAsync(txtEmail.Text);
                List<Usuario> usuarios = new List<Usuario>();
                usuarios.Add(user);
                listUsuario.ItemsSource = usuarios.OrderBy(item => item.CD_USUARIO).ToList();
            }
            else if (txtNome.Text == null && txtEmail.Text == null && txtCd.Text != null)
            {
                Usuario user = await new DataService().GetUsuarioByCdAsync(int.Parse(txtCd.Text));
                List<Usuario> usuarios = new List<Usuario>();
                usuarios.Add(user);
                listUsuario.ItemsSource = usuarios.OrderBy(item => item.CD_USUARIO).ToList();
            }
            else
            {
                await DisplayAlert("Alerta", "Preencha Somente uma opção de Busca.", "OK");
            }
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (Valida())
            {
                Usuario user = new Usuario
                {
                    NM_USUARIO = txtNome.Text,
                    DS_EMAIL = txtEmail.Text
                };
                try
                {
                    await dataService.AddUserAsync(user);
                    LimpaUsuario();
                    AtualizaDados();
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Alerta", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Alerta", "Dados Inválidos", "OK");
            }
        }
        private void ListUsuario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var usuario = e.SelectedItem as Usuario;
            txtCd.Text = usuario.CD_USUARIO.ToString();
            txtCd.IsReadOnly = true;
            txtEmail.Text = usuario.DS_EMAIL;
            txtNome.Text = usuario.NM_USUARIO;
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (Valida())
            {
                try
                {
                    var mi = ((MenuItem)sender);
                    Usuario usuario = (Usuario)mi.CommandParameter;
                    usuario.NM_USUARIO = txtNome.Text;
                    usuario.DS_EMAIL = txtEmail.Text;
                    await dataService.UpdateUserAsync(usuario);

                    LimpaUsuario();
                    AtualizaDados();
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Alerta", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Alerta", "Dados Inválidos", "OK");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);
                Usuario usuario = (Usuario)mi.CommandParameter;

                await dataService.DeleteUserAsync(usuario);

                LimpaUsuario();
                AtualizaDados();
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "OK");
            }

        }

        private void LimpaUsuario()
        {
            txtCd.IsReadOnly = false;
            txtCd.Text = null;
            txtEmail.Text = null;
            txtNome.Text = null;
        }

        private bool Valida()
        {
            if (string.IsNullOrEmpty(txtEmail.Text) && string.IsNullOrEmpty(txtNome.Text))
                return false;
            else
                return true;
        }
    }
}
