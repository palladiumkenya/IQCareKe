using System;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name ="Patient")]

    public class Patient : BaseEntity
    {
        public int Ptn_Pk { get; set;}
        public int PersonId {get; set; }
        public int FacilityId { get; set; }
        public string PatientIndex { get; set; }
        public int IdentificationType { get;set;}
        public string IdentificationNo { get; set; }
        public bool Active { get; set; }
    }
   
}
