using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using Newtonsoft;
using System.Threading.Tasks;
using Mesa_RPG.Models;
using Newtonsoft.Json;
using System.Data;

namespace Mesa_RPG.Service
{
    public class DataService
    {
        HttpClient client = new HttpClient();
        WebClient wc = new WebClient();

        public async Task<List<Usuario>> GetUsuarioAsync()
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/usuarios/";

                var response = await client.GetStringAsync(url);
                var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(response);
                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Usuario> GetUsuarioByCdAsync(int cdUser)
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/Usuarios?CD={0}";

                var uri = new Uri(String.Format(url, cdUser));
                var response = await client.GetStringAsync(uri);
                var usuarios = JsonConvert.DeserializeObject<Usuario>(response);
                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Usuario GetUsuarioByNameAsync(string nameUser)
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/Usuarios?Nome={0}";

                var uri = new Uri(String.Format(url, nameUser));
                string response = wc.DownloadString(uri);
                Usuario usuarios = JsonConvert.DeserializeObject<Usuario>(response);
                //DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(response);

                //DataTable dt = dataSet.Tables["TB_USUARIO"];
                //Usuario usuarios = new Usuario();
                //if (dt.Rows.Count > 0)
                //{
                //    usuarios.CD_USUARIO = Convert.ToInt32(dt.Rows[0]["CD_USUARIO"]);
                //    usuarios.NM_USUARIO = dt.Rows[0]["NM_USUARIO"].ToString();
                //    usuarios.DS_EMAIL = dt.Rows[0]["DS_EMAIL"].ToString();
                //}

                return usuarios;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public  Usuario GetUsuarioByEmailAsync(string emailUser)
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/Usuarios?Email={0}";

                var uri = new Uri(String.Format(url, emailUser));
                string response = wc.DownloadString(uri);
                Usuario usuarios = JsonConvert.DeserializeObject<Usuario>(response);

                return usuarios;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> GetLogin(string usuario, string senha)
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/Usuarios?usuario={0}&senha={1}";

                var uri = new Uri(String.Format(url, usuario, senha));
                var response = await client.GetStringAsync(uri);
                var usuarios = JsonConvert.DeserializeObject<int>(response);

                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task AddUserAsync(Usuario usuario)
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/usuarios/{0}";

                var uri = new Uri(String.Format(url, usuario.CD_USUARIO));

                var data = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir Usuário!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateUserAsync(Usuario usuario)
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/Usuarios/{0}";

                var uri = new Uri(String.Format(url, usuario.CD_USUARIO));

                var data = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PutAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar Usuário!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteUserAsync(Usuario usuario)
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/Usuarios?Cd={0}";
                var uri = new Uri(String.Format(url, usuario.CD_USUARIO));
                HttpResponseMessage response = null;
                response = await client.DeleteAsync(uri);
                //if (!response.IsSuccessStatusCode)
                //{
                //    throw new Exception("Erro ao apagar Usuário!");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
