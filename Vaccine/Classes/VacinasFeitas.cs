using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.Classes
{
    [Table(Name = "Vacinasfeitas")]
    public class VacinasFeitas
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public string NomeVacinaFeita { get; set; }

        [Column(CanBeNull = false)]
        public int idPessoa { get; set; }

        [Column(CanBeNull = false)]
        public DateTime dataVacinaFeita { get; set; }

        [Column(CanBeNull = true)]
        public string loteVacinaFeita { get; set; }

        [Column(CanBeNull = true)]
        public string localVacinaFeita { get; set; }

        [Column(CanBeNull = true)]
        public string reacaoVacinaFeita { get; set; }

        [Column(CanBeNull = false)]
        public DateTime dataAgora { get; set; }

        public string dataVacinaFeitaDisplay
        {
            get { return dataVacinaFeita.ToShortDateString(); }
        }
    }
}