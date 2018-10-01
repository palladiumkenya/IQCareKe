using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records
{
    [Serializable]
    [Table("mst_Facility")]
    public class LookupFacility
    {
        [Key]
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string SatelliteID { get; set; }
        public int DeleteFlag { get; set; }
        public int UserID { get; set; }
        [Column("NationalId")]
        public string MFLCode { get; set; }
        //public int AppGracePeriod { get; set; }

    }
}
