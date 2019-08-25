using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using IQCare.AIR.BusinessProcess.Queries;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.Extensions;

namespace IQCare.AIR.BusinessProcess.MapperProfiles
{
    public class ReportingPeriodProfile : Profile
    {
        public ReportingPeriodProfile()
        {
            CreateMap<ReportingPeriod, FormReportingPeriodViewModel>()
                .ForMember(dest => dest.ReportName, src => src.MapFrom(x => x.ReportingForm.Name))
                .ForMember(dest => dest.StrReportDate, src => src.MapFrom(x =>x.ReportDate.GetMonthAndYearFromDate()))
                .ReverseMap();
        }
    }
}
