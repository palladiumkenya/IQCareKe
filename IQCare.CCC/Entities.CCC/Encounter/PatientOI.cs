using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Encounter
{

    [Serializable]
    [Table("PatientOI")]
    public  class PatientOI:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }

        public int OIId { get; set; }
        
        public DateTime? Current { get; set; }
        public DateTime? UpdateDate { get; set; }


    }
}
