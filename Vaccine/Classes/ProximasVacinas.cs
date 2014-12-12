using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.Classes
{
    [Table(Name = "Proximasvacinas")]
    public class ProximasVacinas
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public string NomeProximaVacina { get; set; }

        [Column(CanBeNull = false)]
        public int idPessoa { get; set; }

        [Column(CanBeNull = false)]
        public DateTime dataProximaVacina { get; set; }

        [Column(CanBeNull = false)]
        public DateTime dataAgora { get; set; }

        public string dataProximaVacinaDisplay
        {
            get { return dataProximaVacina.ToShortDateString(); }
        }
    }
}