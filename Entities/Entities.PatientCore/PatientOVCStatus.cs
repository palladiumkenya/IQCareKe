using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.PatientCore
{
    [Serializable]
    public class PatientOVCStatus : IAuditEntity
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public virtual int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
       
        public bool Orphan { get; set; }
        public bool InSchool { get; set; }
        public string AuditData { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Person Guardian { get; set; }
        public virtual int GuardianId { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
