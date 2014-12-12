using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.Classes
{
    class PessoasDB
    {
        private static DataBase getDataBase()
        {
            DataBase db = new DataBase();
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
            }
            return db;
        }

        public static Pessoas GetPessoa(int pId)
        {
            DataBase db = getDataBase();
            Pessoas pessoas = (from pes in db.Pessoas where pes.Id == pId select pes).First();
            return pessoas;
        }
        //=================================================== PESQUISA POR NOME DA PESSOA ==================================================
        public static List<Pessoas> GetPessoas(string pNome)
        {
            DataBase db = getDataBase();

            if (pNome != null)
            {
                var query = from pes in db.Pessoas where pes.Nome.Contains(pNome) orderby pes.Nome select pes;
                List<Pessoas> pessoas = new List<Pessoas>(query.AsEnumerable());
                return pessoas;
            }
            else
            {
                var query = from pes in db.Pessoas orderby pes.Nome select pes;
                List<Pessoas> pessoas = new List<Pessoas>(query.AsEnumerable());
                return pessoas;
            }

        }
        //===================================================== SALVAR/DELETAR/ATUALIZAR ====================================================
        public static void Salvar(Pessoas pessoa)
        {
            DataBase db = getDataBase();
            db.Pessoas.InsertOnSubmit(pessoa);
            db.SubmitChanges();
        }

        public static void Deletar(Pessoas pessoa)
        {
            DataBase db = getDataBase();
            var query = from pes in db.Pessoas where pes.Id == pessoa.Id select pes;
            db.Pessoas.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }

        public static void Atualizar(Pessoas pe)
        {
            DataBase db = getDataBase();
            Pessoas pessoa = (from pes in db.Pessoas where pes.Id == pe.Id select pes).First();

            pessoa.Nome = pe.Nome;
            db.SubmitChanges();
        }
    }
}
