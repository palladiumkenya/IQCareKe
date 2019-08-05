using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core;
using AutoMapper;
using IQCare.Common.Core.Models;
using IQCare.Common.BusinessProcess.Commands.Allergies;

namespace IQCare.Common.Infrastructure.MapperProfiles
{
    class PatientAllergiesMapperProfile : Profile
    {
        public PatientAllergiesMapperProfile()
        {
            CreateMap<PatientAllergy, AddAllergiesCommand>().ReverseMap();
            CreateMap<PatientAllergy, PatientAllergiesViewModel>().ReverseMap();
        }
    }
}