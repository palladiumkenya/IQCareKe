using AutoMapper;
using IQCare.Lab.BusinessProcess.Queries;
using IQCare.Lab.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Lab.BusinessProcess.MapperProfiles
{
    public class PatientLabTrackerMapperProfile : Profile
    {
        public PatientLabTrackerMapperProfile()
        {
            CreateMap<PatientLabTracker, LabTestResultViewModel>()
                .ForMember(dest => dest.LabTestName, src => src.MapFrom(x => x.LabName))
                .ForMember(dest => dest.Result, src => src.MapFrom(x=> GetValidResult(x.ResultValues,x.ResultOptions,x.ResultTexts)))
                .ForMember(dest=>dest.ResultUnits,src=>src.MapFrom(x=>string.IsNullOrEmpty(x.ResultUnits) ? "No Units" : x.ResultUnits))
                .ForMember(dest => dest.OrderReason, src => src.MapFrom(x => x.Reasons))
                .ForMember(dest => dest.OrderDate, src => src.MapFrom(x => x.SampleDate.HasValue ? x.SampleDate.Value.ToShortDateString() : null))
                .ForMember(dest => dest.ResultStatus, src => src.MapFrom(x => x.Results))
                .ReverseMap();

            CreateMap<LabOrderTestResult, LabTestResultViewModel>()
                .ForMember(dest=>dest.Parameter,src=>src.MapFrom(x=>x.Parameter.ParameterName))
                .ForMember(dest => dest.Result, src => src.MapFrom(x => GetValidResult(x.ResultValue,x.ResultOption,x.ResultText)))
                .ForMember(dest => dest.ResultUnits, src => src.MapFrom(x => string.IsNullOrEmpty(x.ResultUnit) ? "No Units" : x.ResultUnit))
                .ReverseMap();

        }


        public static string GetValidResult(decimal? resultValue, string resultOption, string resultText)
        {
            if (resultValue.HasValue)
                return resultValue.Value.ToString();
            else if (!string.IsNullOrEmpty(resultOption))
                return resultOption;
            else
                return resultText;
        }
    }
}
