using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.PatientCore
{
    [Serializable]
    public class PatientContact : IAuditEntity
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public virtual int PatientId { get; set; }
   
        public virtual Patient Patient { get; set; }
        public string PostalAddress { get; set; }
        public string MobileNo { get; set; }

        public int CreatedBy
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public bool DeleteFlag
        {
            get; set;
        }

        public string AuditData
        {
            get; set;
        }
    }
}
