using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiTbAssessment
{
    public class DeleteHeiPatientIcfActionCommand: IRequest<Result<HEiPatientIcfAction>>
    {
        public int PatientId { get; set; }
        public int Id { get; set; }

    }
}