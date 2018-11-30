using AutoMapper;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.BusinessProcess.Queries.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.MapperProfiles
{
    public class PatientScreeningProfiler: Profile
    {
        public PatientScreeningProfiler()

        {
            CreateMap<PatientScreeningCommand, PatientScreening>().ReverseMap();

        }
    }
}
