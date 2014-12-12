using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.Classes
{
    class ProximasVacinasDB
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
        //=============================================== RELAÇÃO VACINA X PESSOA CADASTRADA ===============================================
        public static List<ProximasVacinas> GetProximasVacinas(int pId)
        {
            DataBase db = getDataBase();
            var query = from vac in db.proximasVacinas where vac.idPessoa == pId orderby vac.dataProximaVacina ascending select vac;

            List<ProximasVacinas> i = new List<ProximasVacinas>(query.AsEnumerable());
            return i;
        }

        //=================================================== PESQUISA POR NOME DA VACINA ===================================================
        public static List<ProximasVacinas> GetSearchProximasVacinas(string pNome)
        {
            DataBase db = getDataBase();

            var query = from vac in db.proximasVacinas where vac.NomeProximaVacina.Contains(pNome) orderby vac.NomeProximaVacina select vac;
            List<ProximasVacinas> vacinas = new List<ProximasVacinas>(query.AsEnumerable());
            return vacinas;
        }
        //================================================ PESQUISA QUE RETORNA PRÓXIMA DATA ================================================
        public static ProximasVacinas GetProximaDataLiveTile()
        {
            DataBase db = getDataBase();

            var query = (from vac in db.proximasVacinas where vac.dataProximaVacina >= DateTime.Now.Date orderby vac.dataProximaVacina ascending select vac).FirstOrDefault();
            return query;
        }

        //===================================================== SALVAR/DELETAR/ATUALIZAR ====================================================
        public static void Salvar(ProximasVacinas proximasvacinas)
        {
            DataBase db = getDataBase();
            db.proximasVacinas.InsertOnSubmit(proximasvacinas);
            db.SubmitChanges();
        }

        public static void Deletar(ProximasVacinas proximasvacinas)
        {
            DataBase db = getDataBase();
            var query = from ind in db.proximasVacinas where ind.Id == proximasvacinas.Id select ind;
            db.proximasVacinas.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }

        public static void Atualizar(ProximasVacinas v)
        {
            DataBase db = getDataBase();
            ProximasVacinas proximasvacinas = (from ind in db.proximasVacinas where ind.Id == v.Id select ind).First();

            proximasvacinas.NomeProximaVacina = v.NomeProximaVacina;
            proximasvacinas.dataProximaVacina = v.dataProximaVacina;
            db.SubmitChanges();
        }
    }
}
