using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.BusinessProcess.Queries.PNC;
using IQCare.Maternity.Core.Domain.PNC;

namespace IQCare.Maternity.BusinessProcess.MapperProfiles
{
    public class PatientFamilyPlanningMethodViewProfile : Profile
    {
        public PatientFamilyPlanningMethodViewProfile()
        {
            CreateMap<GetPatientFamilyPlanningMethodQuery, PatientFamilyPlanningMethodView>();

        }
    }
}
