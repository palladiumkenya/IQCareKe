using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Interoperability
{
    [Serializable]
    [Table("Api_PatientMessage")]
    public class PatientMessage
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string IdentifierValue { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string NATIONAL_ID { get; set; }
        public DateTime DATE_OF_BIRTH { get; set; }
        public string SEX { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string CountyName { get; set; }
        public string Subcountyname { get; set; }
        public string WardName { get; set; }
        public string Village { get; set; }
        public string ServiceAreaId { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string TFIRST_NAME { get; set; }
        public string TMIDDLE_NAME { get; set; }
        public string TLAST_NAME { get; set; }
        public string TRELATIONSHIP { get; set; }
        public string TADDRESS { get; set; }
        public string TPHONE_NUMBER { get; set; }
        public string TSEX { get; set; }
        public string TDATE_OF_BIRTH { get; set; }
        public string TCONTACT_ROLE { get; set; }
    }
}
