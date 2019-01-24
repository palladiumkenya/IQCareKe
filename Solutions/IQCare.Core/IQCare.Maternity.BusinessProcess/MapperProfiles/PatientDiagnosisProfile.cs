using AutoMapper;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.MapperProfiles
{
    public class PatientDiagnosisProfile : Profile
    {
        public PatientDiagnosisProfile()
        {
            CreateMap<AddPatientDiagnosisCommand, PatientDiagnosis>()
                .ForMember(dest => dest.DateCreated, src => src.MapFrom(x => DateTime.Now)).ReverseMap();

            CreateMap<PatientDiagnosis, PatientDiagnosisViewModel>().ReverseMap();
        }
    }
}
