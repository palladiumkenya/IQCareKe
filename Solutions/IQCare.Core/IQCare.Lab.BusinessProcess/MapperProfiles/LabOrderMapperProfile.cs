using AutoMapper;
using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Lab.BusinessProcess.MapperProfiles
{
    public class LabOrderMapperProfile : Profile
    {
        public LabOrderMapperProfile()
        {
            CreateMap<AddLabOrderCommand, LabOrder>()
                .ForMember(src => src.CreatedBy, dest => dest.MapFrom(x => x.UserId))
                .ForMember(src => src.CreateDate, dest => dest.MapFrom(x => DateTime.Now)).ReverseMap();

        }
    }
}
