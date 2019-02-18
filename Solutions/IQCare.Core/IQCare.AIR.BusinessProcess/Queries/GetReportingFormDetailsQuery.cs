using IQCare.AIR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;

namespace IQCare.AIR.BusinessProcess.Queries
{
    public class GetReportingFormDetailsQuery : IRequest<Result<ReportingFormViewModel>>
    {
        public int Id { get; set; }

    }



    public class ReportingFormViewModel
    {
        public int Id { get;  set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ReportSectionViewModel> ReportSections { get; set; }     
    }

    public class ReportSectionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReportingFormId { get; set; }
        public DateTime DateCreated  { get; set; }
        public List<ReportSubSectionViewModel> ReportSubSections { get; set; }
    }

    public class ReportSubSectionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReportSectionId { get; set; }
        public DateTime DateCreated { get; set; }
        public List<IndicatorViewModel> Indicators { get; set; }      
    }


    public class IndicatorViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public string Result { get; set; }
        
    }
}
