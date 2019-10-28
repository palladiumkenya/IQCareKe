using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
   public  class lnk_FacilityModule
    {

       
        public int FacilityID { get; set; }


        public int  ModuleID { get; set; }


        public int UserId { get; set; }

        public virtual Facility Facility { get; set; }

        public virtual Module Module { get; set; }

        
    }

}
