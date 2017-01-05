using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.PatientCore
{
    [Serializable]
    public class PatientTreatmentSupporter :  IAuditEntity
    {
        public int Id { get; set; }
        public Person Supporter { get; set; }
        public int SupporterId {get;set;}
        public int PatientId {get;set;}
        public Patient Patient { get; set; }
        public string AuditData
        {
            get;set;
        }

        public DateTime CreateDate
        {
            get;set;
        }

        public int CreatedBy
        {
            get;set;
        }

        public bool DeleteFlag
        {
            get;set;
        }
    }
}
