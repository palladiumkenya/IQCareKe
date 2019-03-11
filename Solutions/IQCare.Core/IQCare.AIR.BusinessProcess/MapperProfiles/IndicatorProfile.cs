using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IQCare.AIR.BusinessProcess.Queries;
using IQCare.AIR.Core.Domain;

namespace IQCare.AIR.BusinessProcess.MapperProfiles
{
    public class IndicatorProfile : Profile
    {
        public IndicatorProfile()
        {
            CreateMap<Indicator, IndicatorViewModel>()
                .ForMember(src => src.DataType, dest => dest.MapFrom(i => i.DataType.Type)).ReverseMap();
        }
    }
}
