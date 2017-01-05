using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using Common.Core.Model;

namespace PersonManagement.Core.Model
{
    [Table("PersonLocation")]

    public class PersonLocation :BaseEntity
    {
        [Required]
        public  int PersonId { get; set; }
        [ForeignKey("PersonId")]

        [Required]
        public int County { get; set; }

        public int? SubCounty { get; set; }
        public int? Ward { get; set; }

        [Required]
        public string  Village { get; set; }

        [Required]
        public string Estate { get; set; }

        [Required]
        public string LandMark { get; set; }

        [Required]
        public string NearestHealthCentre { get; set; }
        public SqlBinary SketchMap {get;set;}
        public int IsActive { get; set; }
        
    }
}
