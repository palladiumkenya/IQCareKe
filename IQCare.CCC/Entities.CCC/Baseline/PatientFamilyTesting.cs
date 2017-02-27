using System;

namespace Entities.CCC.Baseline
{
    public class PatientFamilyTesting
    {
        public int PersonId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RelationshipId { get; set; }
        public int BaseLineHivStatusId { get; set; }
        public DateTime BaselineHivStatusDate { get; set; }
        public int HivTestingResultsId { get; set; }
        public DateTime HivTestingResultsDate { get; set; }
        public bool CccReferal { get; set; }
        public string CccReferaalNumber { get; set; }
    }
}