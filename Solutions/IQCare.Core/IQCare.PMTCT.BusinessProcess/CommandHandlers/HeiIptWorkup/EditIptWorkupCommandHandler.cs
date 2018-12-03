using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIptWorkup;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiIptWorkup
{
    public class EditIptWorkupCommandHandler:IRequestHandler<EditHeiPatientIptWorkupCommand,Result<PatientIptWorkup>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditIptWorkupCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientIptWorkup>> Handle(EditHeiPatientIptWorkupCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientIptWorkup patientIptWorkup = _unitOfWork.Repository<PatientIptWorkup>()
                        .Get(x => x.PatientId == request.PatientIptWorkup.PatientId && !x.DeleteFlag).FirstOrDefault();
                    if (patientIptWorkup != null)
                    {
                        patientIptWorkup.AbdominalTenderness = request.PatientIptWorkup.AbdominalTenderness;
                        patientIptWorkup.IptRegimen = request.PatientIptWorkup.IptRegimen;
                        patientIptWorkup.LiverFunctionTests = request.PatientIptWorkup.LiverFunctionTests;
                        patientIptWorkup.Numbness = request.PatientIptWorkup.Numbness;
                        patientIptWorkup.IptStartDate = request.PatientIptWorkup.IptStartDate;
                        patientIptWorkup.StartIpt = request.PatientIptWorkup.StartIpt;
                        patientIptWorkup.YellowColouredUrine = request.PatientIptWorkup.YellowColouredUrine;
                        patientIptWorkup.YellownessOfEyes = request.PatientIptWorkup.YellownessOfEyes;
                    
                        _unitOfWork.Repository<PatientIptWorkup>().Update(patientIptWorkup);
                        await _unitOfWork.SaveAsync();
                    }

                    return Result<PatientIptWorkup>.Valid(patientIptWorkup);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PatientIptWorkup>.Invalid(e.Message);
                }
            }
        }
    }
}