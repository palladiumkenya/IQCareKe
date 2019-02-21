using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.AIR.Core.Domain
{
    public class IndicatorResult
    {
        public IndicatorResult()
        {
            
        }

        public IndicatorResult(int reportingPeriodId, int indicatorId, string resultText, decimal ? resultNumeric,int createdBy)
        {
            ReportingPeriodId = reportingPeriodId;
            IndicatorId = indicatorId;
            ResultText = resultText;
            ResultNumeric = resultNumeric;
            CreatedBy = createdBy;
            DateCreated = DateTime.Now;
        }


        public int Id { get; private set; }
        public int ReportingPeriodId { get; private set; }
        public int IndicatorId { get; private set; }
        public string ResultText { get; private set; }
        public decimal ? ResultNumeric { get; private set; }
        public DateTime DateCreated { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTime? DateUpdated { get; private set; }
        public bool DeleteFlag { get; private set; }
        public virtual  ReportingPeriod ReportingPeriod { get; set; }
        public virtual Indicator Indicator { get; set; }

    }
}
