using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("Pregnancy")]

   public class Pregnancy :BaseObject
    {
        class pregnancy { }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public DateTime Lmp { get; set; }
        public DateTime Edd { get; set; }
        public string Gravidae { get; set; }
        public string Parity { get; set; }
        public int Outcome { get; set; }
        public DateTime DateOfOutcome { get; set; }
    }
}
