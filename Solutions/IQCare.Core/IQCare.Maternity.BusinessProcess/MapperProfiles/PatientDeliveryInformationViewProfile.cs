using AutoMapper;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.MapperProfiles
{
    public class PatientDeliveryInformationViewProfile : Profile
    {
        public PatientDeliveryInformationViewProfile()
        {
            CreateMap<PatientDeliveryInformationView, PatientDeliveryInfomationViewModel>().ReverseMap();
        }
    }
}
