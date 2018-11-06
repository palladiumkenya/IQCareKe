using IQCare.Maternity.BusinessProcess.Queries.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using System;
using AutoMapper;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;


namespace IQCare.Maternity.BusinessProcess.MapperProfiles
{
     public class PatientScreeningViewProfile : Profile
    {
        public PatientScreeningViewProfile()
        {
            CreateMap<PatientScreeningDetailsView, PatientScreeningDetailsQuery>().ReverseMap();
            //CreateMap<PatientScreeningDetailsQuery, PatientScreeningDetailsView>().ReverseMap();
        }
    }
}
