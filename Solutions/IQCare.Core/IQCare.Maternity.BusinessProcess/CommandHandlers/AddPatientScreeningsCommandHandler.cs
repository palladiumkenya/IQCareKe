using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class AddPatientScreeningsCommandHandler : IRequestHandler<AddPatientScreeningsCommand, Result<PatientScreeningsResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        public string comment;
        public AddPatientScreeningsCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<PatientScreeningsResponse>> Handle(AddPatientScreeningsCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    if (request.Screenings.Any())
                    {
                        List<PatientScreening> clientScreenings = new List<PatientScreening>();
                        request.Screenings.ForEach(x => clientScreenings.Add(new PatientScreening
                        {
                            
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            DeleteFlag = false,
                            CreateDate = DateTime.Now,
                            CreatedBy = request.CreatedBy,
                            Comment = (x.Comment == null) ? "" : x.Comment ,
                            Active = false,
                            ScreeningCategoryId = x.ScreeningCategoryId,
                            ScreeningDate = request.VisitDate,
                            ScreeningDone = true,
                            ScreeningTypeId = x.ScreeningTypeId,
                            ScreeningValueId = x.ScreeningValueId,
                            VisitDate = request.VisitDate
                        }));

                        await _maternityUnitOfWork.Repository<PatientScreening>().AddRangeAsync(clientScreenings);
                        await _maternityUnitOfWork.SaveAsync();
                    }

                    return Result<PatientScreeningsResponse>.Valid(new PatientScreeningsResponse()
                    {
                        Message = "Successfully added patient screening"
                    });
                }
                catch (Exception e)
                {
                    Log.Error($"");
                    return Result<PatientScreeningsResponse>.Invalid($"");
                }
            }
        }
    }
}