using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CCC.Encounter
{

    [Serializable]
    [Table("PatientHighRisk")]
    public class PatientHighRisk : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }

        public int PartnerId { get; set; }
        public int HighRisk { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
