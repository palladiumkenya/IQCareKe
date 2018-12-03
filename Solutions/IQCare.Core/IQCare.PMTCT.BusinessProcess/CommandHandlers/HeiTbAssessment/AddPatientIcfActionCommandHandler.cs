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
    public class AddPatientIcfActionCommandHandler: IRequestHandler<AddPatientIcfActionCommand, Result<HEiPatientIcfAction>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddPatientIcfActionCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<HEiPatientIcfAction>> Handle(AddPatientIcfActionCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    HEiPatientIcfAction heiPatientIcfAction = _unitOfWork.Repository<HEiPatientIcfAction>()
                        .Get(x => x.PatientId == request.HEiPatientIcfAction.PatientId &&
                                  x.PatientMasterVisitId == request.HEiPatientIcfAction.PatientMasterVisitId &&
                                  !x.DeleteFlag).FirstOrDefault();
                    if (heiPatientIcfAction == null)
                    {
                        await _unitOfWork.Repository<HEiPatientIcfAction>().AddAsync(request.HEiPatientIcfAction);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        heiPatientIcfAction.ChestXray = request.HEiPatientIcfAction.ChestXray;
                        heiPatientIcfAction.EvaluatedForIpt = request.HEiPatientIcfAction.EvaluatedForIpt;
                        heiPatientIcfAction.GeneXpert = request.HEiPatientIcfAction.GeneXpert;
                        heiPatientIcfAction.SputumSmear = request.HEiPatientIcfAction.SputumSmear;
                        heiPatientIcfAction.StartAntiTb = request.HEiPatientIcfAction.StartAntiTb;

                        _unitOfWork.Repository<HEiPatientIcfAction>().Update(heiPatientIcfAction);
                        await _unitOfWork.SaveAsync();
                    }

                    return Result<HEiPatientIcfAction>.Valid(request.HEiPatientIcfAction);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<HEiPatientIcfAction>.Invalid(e.Message);
                }
            }
        }
    }
}