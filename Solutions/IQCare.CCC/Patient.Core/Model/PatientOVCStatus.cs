using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [Table( "PatientOVCStatus")]

    public class PatientOVCStatus :BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int GuardianId { get; set; }
        [ForeignKey("GuardianId")]
        public string Orphan { get; set; }
        public string InSchool { get; set; }
        public int Active { get; set; }
    }
}
