using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Entities.Common;
using Entities.CCC.Enrollment;

namespace Entities.CCC.Lookup
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
        //public int AppGracePeriod { get; set; }

    }
}
