using Entities.CCC.Enrollment;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("PatientTransferIn")]

    public class PatientTransferIn:BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int ServiceAreaId { get; set; }
        public DateTime TransferInDate { get; set; }
        public DateTime TreatmentStartDate { get; set; }
        public string CurrentTreatment { get; set; }
        public string FacilityFrom { get; set; }
        public int MflCode { get; set; }
        public string CountyFrom { get; set; }
        public string TransferInNotes { get; set; }
    }
}
