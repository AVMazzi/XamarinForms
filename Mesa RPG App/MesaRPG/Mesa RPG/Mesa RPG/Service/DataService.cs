using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using Newtonsoft;
using System.Threading.Tasks;
using Mesa_RPG.Models;
using Newtonsoft.Json;

namespace Mesa_RPG.Service
{
    public class DataService
    {
        HttpClient client = new HttpClient();

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

        public async Task<Usuario> GetUsuarioByNameAsync(string nameUser)
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/Usuarios?Nome={0}";

                var uri = new Uri(String.Format(url, nameUser));
                var response = await client.GetStringAsync(uri);
                var usuarios = JsonConvert.DeserializeObject<Usuario>(response);
                return usuarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Usuario> GetUsuarioByEmailAsync(string emailUser)
        {
            try
            {
                string url = "http://mesarpg.somee.com/api/Usuarios?Email={0}";                
                
                var uri = new Uri(String.Format(url, emailUser));
                var response = await client.GetStringAsync(uri);
                var usuarios = JsonConvert.DeserializeObject<Usuario>(response);
 
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

                var uri = new Uri(String.Format(url, usuario.CD_USER));

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

                var uri = new Uri(String.Format(url, usuario.CD_USER));

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
                var uri = new Uri(String.Format(url, usuario.CD_USER));
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
