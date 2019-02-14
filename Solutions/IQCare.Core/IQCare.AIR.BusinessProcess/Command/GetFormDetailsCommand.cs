using IQCare.AIR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;

namespace IQCare.AIR.BusinessProcess.Command
{
   public  class GetFormDetailsCommand:IRequest<Result<GetFormDetailsResultResponse>>
    {
        public int FormId { get; set; }
    }

    public class GetFormDetailsResultResponse    {
        public ReportingForm reportingForm { get; set; }

        public List<ReportSection> ReportSections { get; set; }

        public List <ReportSubSection> ReportSubSections { get; set; }

        public List<IndicatorDetails> IndicatorDetails { get; set; }
    }

    public class IndicatorDetails
    {
        public int Id { get;  set; }
        public string Code { get; set; }
        public string Name { get;  set; }

        public string IndicatorDataTypeName { get; set; }

        public string SectionName { get; set; }

        public int SectionId { get; set; }

        public string ReportSubSectionName { get; set; }

        public int ReportSubSectionId { get; set; }

        public int FormId { get; set; }

        
    }
}
