using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IQCare.AIR.Core.Domain
{
    public class ReportingPeriod
    {

        public ReportingPeriod()
        {
            
        }

        public ReportingPeriod(int reportingFormId, DateTime reportDate,int createdBy)
        {
            ReportingFormId = reportingFormId;
            ReportDate = reportDate;
            DateCreated = DateTime.Now;
            CreatedBy = createdBy;
        }

        [Key]
        public int Id { get; private set; }
        public int ReportingFormId { get; private set; }
        public DateTime ReportDate { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateUpdated { get; private set; }
        public bool DeleteFlag { get; private set; }
        public int CreatedBy { get; private set; }
        public virtual ReportingForm ReportingForm { get; set; }
        public virtual ICollection<IndicatorResult> IndicatorResults { get; set; }
    }
}
