using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
   public  class Facility
    {
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string PosID { get; set; }
        public int? Preferred { get; set; }
        public int? DeleteFlag { get; set; }


        public int Frequency { get; set; }


        public int? StrongPassFlag { get; set; }


        public int? ExpPwdFlag { get; set; }




        public string ExpPwdDays { get; set; }


        public ICollection<lnk_FacilityModule> lnkFacilityModules { get; set; }
 

    }
}
