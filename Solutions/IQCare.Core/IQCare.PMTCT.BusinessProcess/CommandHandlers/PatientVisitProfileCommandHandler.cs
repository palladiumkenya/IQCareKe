using System;
using System.Collections.Generic;
using System.Text;
using IQCare.PMTCT.BusinessProcess.Commands;using IQCare.PMTCT.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Services;
using IQCare.PMTCT.Services;
using Serilog;
using IQCare.Common.Infrastructure;
using IQCare.PMTCT.Infrastructure;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class PatientVisitProfileCommandHandler : IRequestHandler<PatientVisitProfileCommand, Library.Result<PatientVisitDetailsCommandResult>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        public int visitCount = 0;
        public int VisitNumber = 0;

        public PatientVisitProfileCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Library.Result<PatientVisitDetailsCommandResult>> Handle(PatientVisitProfileCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {              
                try
                {
                    VisitNumber = Convert.ToInt32(request.VisitNumber);

                    PatientProfile patientProfile = new PatientProfile()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        AgeMenarche = request.AgeMenarche,
                        PregnancyId = request.PregnancyId,
                        VisitNumber = Convert.ToInt32(request.VisitNumber),
                        VisitType = Convert.ToInt32(request.VisitType),

                       // CreatedBy = 2,
                        CreatedBy = request.CreatedBy,
                        CreateDate = DateTime.Now,
                    };

                    await _unitOfWork.Repository<PatientProfile>().AddAsync(patientProfile);
                    await _unitOfWork.SaveAsync();

                    return Library.Result<PatientVisitDetailsCommandResult>.Valid(new PatientVisitDetailsCommandResult()
                    {
                        PatientMasterVisitId = request.PatientMasterVisitId,
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Library.Result<PatientVisitDetailsCommandResult>.Invalid(e.Message);
                }
            }
        }
    }
}
