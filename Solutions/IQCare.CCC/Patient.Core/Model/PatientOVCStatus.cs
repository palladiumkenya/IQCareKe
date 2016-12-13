using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientOVCStatus")]

    class PatientOVCStatus :BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public string GuardianNames { get; set; }
        public int GuardianIdentificationNo { get; set; }
        public string Orphan { get; set; }
        public string InSchool { get; set; }
        public int Active { get; set; }
    }
}
