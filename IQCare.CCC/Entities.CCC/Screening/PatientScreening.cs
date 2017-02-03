using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Screening
{
    [Serializable]
    [Table("PatientScreening")]
    public class PatientScreening : BaseObject
    {
        [Column]

        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int ScreeningTypeId { get; set; }
        public int ScreeningDone { get; set; }
        public DateTime ScreeningDate { get; set; }
        public int ScreeningCategoryId { get; set; }
        public int   ScreeningValueId { get; set; }
        public string Comment { get; set; }
    }
}
