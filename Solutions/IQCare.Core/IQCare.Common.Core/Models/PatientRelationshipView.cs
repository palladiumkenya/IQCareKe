using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientRelationshipView
    {
        public PatientRelationshipView()
        {

        }
        public int Id { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientMiddleName { get; set; }
        public string RelativeFirstName { get; set; }
        public string RelativeLastName { get; set; }
        public string RelativeMiddleName { get; set; }
        public int PatientPersonId { get; set; }
        public string PatientSex { get; set; }
        public int PatientId { get; set; }
        public string Relationship { get; set; }
        public string RelativeSex { get; set; }
        public int RelativePersonId { get; set; }
        public int ? RelativePatientId { get; set; }
        
    }
}
