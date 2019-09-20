using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
   public  class PatientPartnerProfile
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

       



         public DateTime?   HivPositiveStatusDate { get; set; }

        public int CCCEnrollment { get; set; }
        public DateTime? PartnerARTStartDate { get; set; }
        public string HIVSeroDiscordantDuration { get; set; }

        public int SexWithoutCondoms { get; set; }
        public string NumberofChildren { get; set; }

        public string CCCNumber { get; set; }


        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public Boolean DeleteFlag { get; set; }

        public string AuditData { get; set; }


        public bool Active { get; set; }


    }
}
