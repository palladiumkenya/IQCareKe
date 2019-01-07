using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IQCare.Lab.BusinessProcess.Queries;
using IQCare.Lab.Core.Models;

namespace IQCare.Lab.BusinessProcess.MapperProfiles
{
    public class LabOrderTestMapperProfile : Profile
    {
        public LabOrderTestMapperProfile()
        {
            CreateMap<LabOrderTest, LabOrderTestViewModel>().ReverseMap();

        }
    }
}
