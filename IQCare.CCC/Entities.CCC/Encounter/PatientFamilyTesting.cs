using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientFamilyTesting")]
    public class PatientFamilyTesting
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int RelationshipId { get; set; }
        public int BaseLineHivStatusId { get; set; }
        public DateTime BaselineHivStatusDate { get; set; }
        public int HivTestingResultsId { get; set; }
        public DateTime HivTestingResultsDate { get; set; }
        public bool CccReferal { get; set; }
    }
}