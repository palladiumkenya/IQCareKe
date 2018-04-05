using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.psmart
{
    [Serializable][Table("mst_Patient")]
    public class MstPatient
    {
        [Key]
        public int Ptn_pk { get; set; }
        public int LocationID { get; set; }
        public string  GODS_NUMBER { get; set; }
        public string CardSerialNumber { get; set; }
        public string  HTSID { get; set; }
        public string FirstName { get; set; }
        public string MIddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public int  DobPrecision { get; set; }
        public int SEX { get; set; }
        public DateTime  DEATH_DATE { get; set; }
        public string Phone { get; set; }
        public int MaritalStatus { get; set; }
        public string Address { get; set; }
        public string VillageName { get; set; }
        public int?  WARD { get; set; }
        public int?  SUB_COUNTY { get; set; }
       public string NEAREST_LANDMARK { get; set; }
    }
}