﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Core.Model
{
    public class Referral
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime ReferralDate { get; set; }
        public int FromFacility { get; set; }
        public string FromServicePoint { get; set; }
        public string ToServicePoint { get; set; }
        public int ToFacility { get; set; }
        public int Reason { get; set; }
        public string ReferredBy { get; set; }
        public DateTime ExpectedDate { get; set; }
    }
}
