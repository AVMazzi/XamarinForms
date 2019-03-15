using App1_Vagas.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App1_Vagas.Servicos
{
    public class Servicos
    {
        private static string URLEstados = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
        private static string URLMunicipios = "https://servicodados.ibge.gov.br/api/v1/localidades/estados/{0}/municipios";
        private static string URLMunicipiosAll = "https://servicodados.ibge.gov.br/api/v1/localidades/municipios";

        public static List<Estado> GetEstados()
        {
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(URLEstados);

            return JsonConvert.DeserializeObject<List<Estado>>(conteudo);
        }

        public static List<Municipio> GetMunicipios(int estado)
        {
            string NewURL = string.Format(URLMunicipios, estado);
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(NewURL);

            return JsonConvert.DeserializeObject<List<Municipio>>(conteudo);
        }

        public static List<Municipio> GetAllMunicipios()
        {
            string NewURL = string.Format(URLMunicipiosAll);
            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(NewURL);

            return JsonConvert.DeserializeObject<List<Municipio>>(conteudo);
        }
    }
}
