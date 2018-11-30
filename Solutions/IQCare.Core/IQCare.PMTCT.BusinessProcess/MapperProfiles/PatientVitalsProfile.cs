using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.BusinessProcess.Queries;
using IQCare.PMTCT.Core.Models;

namespace IQCare.PMTCT.BusinessProcess.MapperProfiles
{
    public class PatientVitalsProfile : Profile
    {
        public PatientVitalsProfile()
        {
            CreateMap<AddPatientVitalCommand, PatientVital>()
                .ForMember(dest => dest.DateCreated, src => src.MapFrom(x => DateTime.Now));

            CreateMap<PatientVital, PatientVitalViewModel>().ReverseMap();
        }
    }
}
