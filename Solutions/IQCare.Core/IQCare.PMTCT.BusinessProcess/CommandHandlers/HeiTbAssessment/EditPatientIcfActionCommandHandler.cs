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
    public class EditPatientIcfActionCommandHandler:IRequestHandler<EditHeiPatientIcfActionCommand, Result<HEiPatientIcfAction>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditPatientIcfActionCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<HEiPatientIcfAction>> Handle(EditHeiPatientIcfActionCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    HEiPatientIcfAction hEiPatientIcfAction = _unitOfWork.Repository<HEiPatientIcfAction>()
                        .Get(x => x.PatientId == request.HEiPatientIcfAction.PatientId &&
                                  x.Id == request.HEiPatientIcfAction.Id).FirstOrDefault();
                    if (hEiPatientIcfAction != null)
                    {
                        hEiPatientIcfAction.ChestXray = request.HEiPatientIcfAction.ChestXray;
                        hEiPatientIcfAction.EvaluatedForIpt = request.HEiPatientIcfAction.EvaluatedForIpt;
                        hEiPatientIcfAction.GeneXpert = request.HEiPatientIcfAction.GeneXpert;
                        hEiPatientIcfAction.SputumSmear = request.HEiPatientIcfAction.SputumSmear;
                        hEiPatientIcfAction.StartAntiTb = request.HEiPatientIcfAction.StartAntiTb;

                        _unitOfWork.Repository<HEiPatientIcfAction>().Update(hEiPatientIcfAction);
                        await _unitOfWork.SaveAsync();
                    }
                    return Result<HEiPatientIcfAction>.Valid(hEiPatientIcfAction);
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