using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.Classes
{
    class DataBase : DataContext
    {
        public static string DBConnectionString = "Data Source ='isostore:pessoas.sdf'";

        public DataBase()
            : base(DBConnectionString)
        { }

        public Table<Pessoas> Pessoas
        {
            get
            {
                return this.GetTable<Pessoas>();
            }
        }

        public Table<VacinasFeitas> vacinasFeitas
        {
            get
            {
                return this.GetTable<VacinasFeitas>();
            }
        }

        public Table<ProximasVacinas> proximasVacinas
        {
            get
            {
                return this.GetTable<ProximasVacinas>();
            }
        }
    }
}