using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PersonEmergencyView
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        public int EmergencyContactPersonId { get; set; }


        public bool RegisteredToClinic { get; set; }


        public string MobileContact { get; set; }

        public int? RelationshipTypeId { get; set; }
        
        public bool DeleteFlag { get; set; }

        public int? ConsentType { get; set; }

        public int? ConsentValue { get; set; }

        public string ConsentReason { get; set; }
        public int CreatedBy { get; set; }
       
        


        public string EmergencyFirstName { get; set; }

        public string EmergencyLastName { get; set; }

        public string EmergencyMidName { get; set; }

        public string Gender { get; set; }

       
          public int Sex { get; set; }



        public string EmergencyItemName { get; set; }

        public int? EmergencyItemId { get; set; }

        public string NextofkinItemName { get; set; }

        public int? NextofKinItemId { get; set; }

        public DateTime? CreateDate { get; set; }


    }
}
