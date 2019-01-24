
using AutoMapper;
using IQCare.Common.BusinessProcess.Commands.Appointment;
using IQCare.Common.Core.Models;
namespace IQCare.Common.BusinessProcess.MapperProfiles
{
    public class PatientAppoitmentMapperProfile : Profile
    {
        public PatientAppoitmentMapperProfile()
        {
            CreateMap<PatientAppointmentMethodViewModel, Api_PatientAppointmentsView>().ReverseMap();
            //CreateMap<Api_PatientAppointmentsView, PatientAppointmentMethodViewModel>().ReverseMap();

        }
    }
}