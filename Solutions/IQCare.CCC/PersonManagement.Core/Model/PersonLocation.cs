using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using Common.Core.Model;

namespace PersonManagement.Core.Model
{
    [Table("PersonLocation")]

    public class PersonLocation :BaseEntity
    {
        public  int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public int County { get; set; }
        public int? SubCounty { get; set; }
        public int? Ward { get; set; }
        public string  Village { get; set; }
        public string Estate { get; set; }
        public string LandMark { get; set; }
        public string NearestHealthCentre { get; set; }
        public SqlBinary SketchMap {get;set;}
        public int IsActive { get; set; }
        
    }
}
