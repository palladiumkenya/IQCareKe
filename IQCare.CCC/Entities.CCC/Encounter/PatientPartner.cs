using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Encounter
{

    [Serializable]
    [Table("PatientPartner")]
    public class PatientPartner:BaseEntity
    {
        [Key]
        public int Id { get; set; }


        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        
        public int NoofPartners { get; set; }

        public DateTime? UpdateDate  { get; set; }

    }
}
