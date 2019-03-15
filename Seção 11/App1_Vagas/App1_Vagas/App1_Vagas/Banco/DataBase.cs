using System.Collections.Generic;
using SQLite;
using App1_Vagas.Modelos;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App1_Vagas.Banco
{
    class DataBase
    {
        private SQLiteConnection _conexao;
        public DataBase()
        {
            var dep = DependencyService.Get < ICaminho>();
            string caminho = dep.ObterCaminho("DB_AGENCIAMEPREGO_Sqlite");
            _conexao = new SQLiteConnection(caminho);
            _conexao.CreateTable<Vaga>();
        }

        public List<Vaga> Consultar()
        {
            return _conexao.Table<Vaga>().ToList();
        }

        public List<Vaga> Pesquisar(string cargo)
        {
            return _conexao.Table<Vaga>().Where(a => a.Cargo.Contains(cargo)).ToList();
        }

        public Vaga ObterVagaID(int id)
        {
            return _conexao.Table<Vaga>().Where(a => a.ID == id).FirstOrDefault();
        }

        public void Cadastro (Vaga vaga)
        {
            _conexao.Insert(vaga);
        }

        public void Atualizar(Vaga vaga)
        {
            _conexao.Update(vaga);
        }

        public void Deletar(Vaga vaga)
        {
            _conexao.Delete(vaga);
        }
    }
}
