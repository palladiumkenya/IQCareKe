using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC
{
    [Serializable]
    [Table("Rpt_FollowUpEducation")]
    public class HIVEducationFollowup
    {
        [Key]
        public int Id { get; set; }
        public int Ptn_pk { get; set; }
        public DateTime VisitDate { get; set; }
        public int? CouncellingTypeId { get; set; }
        public string CouncellingType { get; set; }
        public int? CouncellingTopicId { get; set; }
        public string CouncellingTopic { get; set; }
        public string Comments { get; set; }
        public string CouncellingTopicOther { get; set; }
    }
}
