using AutoMapper;
using IQCare.PMTCT.BusinessProcess.Queries;
using IQCare.PMTCT.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.MapperProfiles
{
    public class PatientChronicIllnessProfile : Profile
    {
        public PatientChronicIllnessProfile()
        {
            CreateMap<PatientChronicIllnessView, PatientChronicIllnessViewModel>().ReverseMap();
        }
    }
}
