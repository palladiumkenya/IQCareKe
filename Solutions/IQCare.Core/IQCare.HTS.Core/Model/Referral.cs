using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IQCare.HTS.Core.Model
{
    public class Referral
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime ReferralDate { get; set; }
        public int FromFacility { get; set; }
        public int FromServicePoint { get; set; }
        public int ToServicePoint { get; set; }
        public int ToFacility { get; set; }
        public int ReferralReason { get; set; }
        public int ReferredBy { get; set; }
        public DateTime ExpectedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool DeleteFlag { get; set; }
        [XmlIgnore]
        public string AuditData { get; set; }
    }
}
