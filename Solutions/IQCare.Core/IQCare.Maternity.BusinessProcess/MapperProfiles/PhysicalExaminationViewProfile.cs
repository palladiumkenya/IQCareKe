using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
using IQCare.Maternity.BusinessProcess.Queries.PNC;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Core.Domain.PNC;

namespace IQCare.Maternity.BusinessProcess.MapperProfiles
{
    class PhysicalExaminationViewProfile : Profile
    {
        public PhysicalExaminationViewProfile()
            
        {
            CreateMap<GetPhysicalExaminationQuery, PhysicalExaminationView>().ReverseMap();
           
        }
    }
}

