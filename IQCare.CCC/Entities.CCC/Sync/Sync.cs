using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CCC.Sync
{
    [Serializable]
    [Table("Vaccination")]
    public class Sync : BaseObject
    {
    }
    [Serializable]
    [Table("Vaccination")]
    public class SyncRecord : BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int Vaccine { get; set; }
        public string VaccineStage { get; set; }
    }
    }
