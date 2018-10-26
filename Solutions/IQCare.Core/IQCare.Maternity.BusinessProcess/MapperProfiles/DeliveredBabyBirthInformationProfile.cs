using AutoMapper;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.MapperProfiles
{
    public class DeliveredBabyBirthInformationProfile : Profile
    {
        public DeliveredBabyBirthInformationProfile()
        {
            CreateMap<AddDeliveredBabyBirthInformationCommand, DeliveredBabyBirthInformation>()
                .ForMember(dest => dest.CreateDate, src => src.MapFrom(x => DateTime.Now)).ReverseMap();

            CreateMap<DeliveredBabyBirthInfoView, DeliveredBabyBirthInfoViewModel>().ReverseMap();
        }
    }
}
