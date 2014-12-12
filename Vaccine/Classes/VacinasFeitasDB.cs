using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.Classes
{
    class VacinasFeitasDB
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
        public static List<VacinasFeitas> GetVacinasFeitas(int pId)
        {
            DataBase db = getDataBase();
            var query = from vac in db.vacinasFeitas where vac.idPessoa == pId orderby vac.dataVacinaFeita descending select vac;

            List<VacinasFeitas> i = new List<VacinasFeitas>(query.AsEnumerable());
            return i;
        }
        //=================================================== PESQUISA POR NOME DA VACINA ===================================================
        public static List<VacinasFeitas> GetSearchVacinasFeitas(string pNome)
        {
            DataBase db = getDataBase();

            var query = from vac in db.vacinasFeitas where vac.NomeVacinaFeita.Contains(pNome) orderby vac.NomeVacinaFeita select vac;
            List<VacinasFeitas> vacinas = new List<VacinasFeitas>(query.AsEnumerable());
            return vacinas;
        }

        //===================================================== SALVAR/DELETAR/ATUALIZAR ====================================================
        public static void Salvar(VacinasFeitas vacinasfeitas)
        {
            DataBase db = getDataBase();
            db.vacinasFeitas.InsertOnSubmit(vacinasfeitas);
            db.SubmitChanges();
        }

        public static void Deletar(VacinasFeitas vacinasfeitas)
        {
            DataBase db = getDataBase();
            var query = from ind in db.vacinasFeitas where ind.Id == vacinasfeitas.Id select ind;
            db.vacinasFeitas.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }

        public static void Atualizar(VacinasFeitas v)
        {
            DataBase db = getDataBase();
            VacinasFeitas vacinasfeitas = (from ind in db.vacinasFeitas where ind.Id == v.Id select ind).First();

            vacinasfeitas.NomeVacinaFeita = v.NomeVacinaFeita;
            vacinasfeitas.loteVacinaFeita = v.loteVacinaFeita;
            vacinasfeitas.localVacinaFeita = v.localVacinaFeita;
            vacinasfeitas.reacaoVacinaFeita = v.reacaoVacinaFeita;
            vacinasfeitas.dataVacinaFeita = v.dataVacinaFeita;
            db.SubmitChanges();
        }

    }

}
