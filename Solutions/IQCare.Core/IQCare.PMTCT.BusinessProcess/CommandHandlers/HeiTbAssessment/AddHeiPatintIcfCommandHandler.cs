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
    public class AddHeiPatintIcfCommandHandler: IRequestHandler<AddPatientIcfCommand, Result<HeiPatientIcf>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddHeiPatintIcfCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<HeiPatientIcf>> Handle(AddPatientIcfCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    HeiPatientIcf heiPatient = _unitOfWork.Repository<HeiPatientIcf>().Get(x =>
                            x.PatientId == request.HeiPatientIcf.PatientId &&
                            x.PatientMasterVisitId == request.HeiPatientIcf.PatientMasterVisitId && !x.DeleteFlag)
                        .FirstOrDefault();
                    if (heiPatient == null)
                    {
                        await _unitOfWork.Repository<HeiPatientIcf>().AddAsync(request.HeiPatientIcf);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        heiPatient.ContactWithTb = request.HeiPatientIcf.ContactWithTb;
                        heiPatient.Cough = request.HeiPatientIcf.Cough;
                        heiPatient.EverBeenOnIpt = request.HeiPatientIcf.EverBeenOnIpt;
                        heiPatient.Fever = request.HeiPatientIcf.Fever;
                        heiPatient.OnAntiTbDrugs = request.HeiPatientIcf.OnAntiTbDrugs;
                        heiPatient.ContactWithTb = request.HeiPatientIcf.ContactWithTb;
                         _unitOfWork.Repository<HeiPatientIcf>().Update(heiPatient);
                        await _unitOfWork.SaveAsync();
                    }                   
                    return Result<HeiPatientIcf>.Valid(request.HeiPatientIcf);
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