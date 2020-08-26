using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
   public  class Module
    {

        public int ModuleID { get; set; }


        public string ModuleName { get; set; }


        public bool DeleteFlag { get; set; }


        public bool? CanEnroll { get; set; }

        public int? Status { get; set; }


        public string DisplayName { get; set; }




        public bool ModuleFlag { get; set; }

        public int? PharmacyFlag { get; set; }

        public ICollection<lnk_FacilityModule> lnkFacilityModules { get; set; }


    }
}
