using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name ="Patient")]

    public class Patient : BaseEntity
    {
        public int ptn_pk { get; set;}
        public System.Guid UGuid { get; set;}
        public string FName {get;set;}
        public string MName { get; set; }
        public string LName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public int IdentificationType { get;set;}
        public string IdentificationNo { get; set; }
        public bool Status { get; set; }
    }
   
}
