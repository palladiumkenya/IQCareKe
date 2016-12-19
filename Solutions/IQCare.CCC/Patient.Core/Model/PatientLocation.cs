using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientLocation")]

    public class PatientLocation :BaseEntity
    {
        public  int patientId { get; set; }
        [ForeignKey("PatientId")]
        public int County { get; set; }
        public int? SubCounty { get; set; }
        public int? Ward { get; set; }
        public string  Village { get; set; }
        public string Estate { get; set; }
        public string LandMark { get; set; }
        public string NearestHealthCentre { get; set; }
        public int IsActive { get; set; }
        
    }
}
