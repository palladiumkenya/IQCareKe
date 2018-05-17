using System;
using System.Collections.Generic;

namespace IQCare.Common.Core.Models
{
    public class AppStateStore
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public int? PatientId { get; set; }
        public int? PatientMasterVisitId { get; set; }
        public int? EncounterId { get; set; }
        public int AppStateId { get; set; }
        public DateTime StatusDate { get; set; }
        public bool DeleteFlag { get; set; }

        public virtual ICollection<AppStateStoreObjects> AppStateStoreObjects { get; set; }
    }
}