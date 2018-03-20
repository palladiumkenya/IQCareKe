using System;

namespace IQCare.Common.Core.Models
{
    public class PartnersView
    {
        public Int64 RowID { get; set; }
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Sex { get; set; }
        public string Gender { get; set; }
        public int RelationshipTypeId { get; set; }
        public string RelationshipType { get; set; }
    }
}