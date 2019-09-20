using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
   public  class FacilityModule
    {
        public int FacilityID { get; set; }

        public int ModuleID { get; set; }

        public string DisplayName { get; set; }

        public string ModuleName { get; set; }

        public bool? CanEnroll { get; set; }

        public int? ExpPwdFlag { get; set; }

        public string ExpPwdDays { get; set; }

        public bool ModuleFlag { get; set; }

        public int? PharmacyFlag { get; set; }

        public int? StrongPassFlag { get; set; }
    }
}
