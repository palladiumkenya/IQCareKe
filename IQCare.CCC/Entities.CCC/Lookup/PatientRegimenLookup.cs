using System;

namespace Entities.CCC.Lookup
{
    [Serializable]

    [System.ComponentModel.DataAnnotations.Schema.Table("dtlRegimenView")]

    public class PatientRegimenLookup
    {
        public int Id { get; set; }
        public int ptn_pk { get; set; }
        public int PersonId { get; set; }
        public int FacilityId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Drug_pk { get; set; }
        public string RegimenType { get; set; }
        public int RegimenId { get; set; }
        public int OrderID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime createDate { get; set; }

    }
}
