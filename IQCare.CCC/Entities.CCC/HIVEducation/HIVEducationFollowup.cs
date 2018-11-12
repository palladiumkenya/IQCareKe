using Entities.CCC.Enrollment;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CCC.HIVEducation
{
    [Serializable]
    [Table("Rpt_FollowUpEducation")]
    public class HIVEducationFollowup : BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("Ptn_pk")]
        public virtual PatientEntity Patient { get; set; }
        public DateTime VisitDate { get; set; }
        public int? CouncellingTypeId { get; set; }
        public string CouncellingType { get; set; }
        public int? CouncellingTopicId { get; set; }
        public string CouncellingTopic { get; set; }
        public string Comments { get; set; }
        public string CouncellingTopicOther { get; set; }
    }
}
