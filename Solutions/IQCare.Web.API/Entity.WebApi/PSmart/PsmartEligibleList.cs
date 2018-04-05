using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entity.WebApi.PSmart
{
    [Serializable]
    [Table("psmart_ClientEligibleList")]
    public class PsmartEligibleList
    {
        [Key]
        public int PATIENTID { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string GENDER { get; set; }
        public int? AGE { get; set; }

        public static implicit operator List<object>(PsmartEligibleList v)
        {
            throw new NotImplementedException();
        }
    }
}