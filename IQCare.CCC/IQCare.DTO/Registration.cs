using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.DTO
{
    public class Registration
    {
        public Registration()
        {
            InternalPatientIdentifiers = new List<DTOIdentifier>();
            Patient = new DTOPerson();
            TreatmentSupporter = new DTOPerson();
        }
        public List<DTOIdentifier> InternalPatientIdentifiers { get; set; }
        public DTOPerson Patient { get; set; }
        public string DateOfEnrollment { get; set; }
        public string EntryPoint { get; set; }
        public string MotherMaidenName { get; set; }
        public string Village { get; set; }
        public string Ward { get; set; }
        public string SubCounty { get; set; }
        public string County { get; set; }
        public string MaritalStatus { get; set; }
        public string DateOfDeath { get; set; }
        public string DeathIndicator { get; set; }
        public DTOPerson TreatmentSupporter { get; set; }
        public string TSRelationshipType { get; set; }
    }
}
