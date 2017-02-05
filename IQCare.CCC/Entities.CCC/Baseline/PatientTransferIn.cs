using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.PatientCore;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("PatientTransferIn")]

    public class PatientTransferIn:BaseEntity
    {
        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public int ServiceAreaId { get; set; }
        public DateTime TransferInDate { get; set; }
        public DateTime TreatmentStartDate { get; set; }
        public string CurrentTreatment { get; set; }
        public string FacilityFrom { get; set; }
        public int MflCode { get; set; }
        public int CountyFrom { get; set; }
        public string TransferInNotes { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
