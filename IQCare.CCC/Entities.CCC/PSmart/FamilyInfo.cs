using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.psmart
{
    [Serializable][Table("dtl_FamilyInfo")]
    public class FamilyInfo
    {
        [Key]
        public int Id { get; set; }
        public string Rfirstname { get; set; }
        public string RMiddleName { get; set; }
        public string RLastName { get; set; }
        public int  Sex { get; set; }
        public int Relationship { get; set; }
        public int HivStatus { get; set; }
        public int HIvCarestatus { get; set; }
        public string RegistrationNo { get; set; }
    }
}