using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiTbAssessment;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiTbAssessment
{
    public class EditHeiPatientIcfCommandHandler: IRequestHandler<EditPatientIcfCommand,Result<HeiPatientIcf>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditHeiPatientIcfCommandHandler(IPmtctUnitOfWork unitiOfWork)
        {
            _unitOfWork = unitiOfWork;
        }

        public async Task<Result<HeiPatientIcf>> Handle(EditPatientIcfCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    HeiPatientIcf empty= new HeiPatientIcf();
                    HeiPatientIcf heiPatientIcf = _unitOfWork.Repository<HeiPatientIcf>().Get(x =>
                        x.PatientId == request.HeiPatientIcf.PatientId && x.Id == request.HeiPatientIcf.Id &&
                        !x.DeleteFlag).FirstOrDefault();
                    if (heiPatientIcf != null)
                    {
                        heiPatientIcf.ContactWithTb = request.HeiPatientIcf.ContactWithTb;
                        heiPatientIcf.Cough = request.HeiPatientIcf.Cough;
                        heiPatientIcf.EverBeenOnIpt = request.HeiPatientIcf.EverBeenOnIpt;
                        heiPatientIcf.Fever = request.HeiPatientIcf.Fever;
                        heiPatientIcf.OnAntiTbDrugs = request.HeiPatientIcf.OnAntiTbDrugs;
                        heiPatientIcf.ContactWithTb = request.HeiPatientIcf.ContactWithTb;

                        _unitOfWork.Repository<HeiPatientIcf>().Update(heiPatientIcf);
                        await _unitOfWork.SaveAsync();
                    }
                    return (heiPatientIcf.Id > 0)? Result<HeiPatientIcf>.Valid(heiPatientIcf): Result<HeiPatientIcf>
                        .Valid(empty);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<HeiPatientIcf>.Invalid(e.Message);
                }
            }
        }
    }
}