using AutoMapper;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.MapperProfiles
{
    public class PatientPatnertTestingMapperProfile : Profile
    {
        public PatientPatnertTestingMapperProfile()
        {
            CreateMap<PatientPartnerTestingCommand, PatientPartnerTesting>();

        }
    }
}
