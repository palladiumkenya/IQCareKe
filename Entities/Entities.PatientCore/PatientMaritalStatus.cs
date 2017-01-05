using Entities.Common;
using System;

namespace Entities.PatientCore
{
    [Serializable]
   public class PatientMaritalStatus:IAuditEntity
    {
        public virtual int PatientId { get; set; }

        public virtual int PatientMasterVisitId { get; set; }
        public virtual Patient Patient { get; set; }

        public int MaritalStatusId { get; set; }

        public bool Active { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public bool DeleteFlag { get; set; }


        public string AuditData { get; set; }
    }
}
