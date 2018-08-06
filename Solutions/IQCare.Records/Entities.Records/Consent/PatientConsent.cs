using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records.Consent
{
    public class PatientConsent:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int PatientMasterVisitId { get; set; }

        public int PatientId { get; set; }

        public int ServiceAreaId { get; set; }

        public int ConsentType { get; set; }

        public DateTime ConsentDate { get; set; }

        public string DeclineReason { get; set; }

        public string ConsentReason { get; set; }

        public int ConsentValue { get; set; }
    }
}
