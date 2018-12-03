using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Core.Domain.PNC;

namespace IQCare.Maternity.BusinessProcess.MapperProfiles
{
    public class PatientExaminationProfile : Profile
    {
        public PatientExaminationProfile()
        {
            CreateMap<AddPostNatalExaminationCommand, PhysicalExamination>();
              
        }
    }
}
