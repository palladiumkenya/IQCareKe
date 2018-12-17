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
                .ForMember(dest => dest.OrderReason, src => src.MapFrom(x => x.Reasons))
                .ForMember(dest=>dest.ResultStatus,src=>src.MapFrom(x=>x.Results))
                .ReverseMap();
        }
    }
}
