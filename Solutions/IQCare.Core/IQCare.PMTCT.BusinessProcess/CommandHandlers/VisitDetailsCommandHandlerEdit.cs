using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class VisitDetailsCommandHandlerEdit: IRequestHandler<VisitDetailsCommandEdit, Library.Result<VisitDetailsCommandEditResult>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public VisitDetailsCommandHandlerEdit(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<VisitDetailsCommandEditResult>> Handle(VisitDetailsCommandEdit request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                Core.Models.VisitDetails visitDetailsEdit = await _unitOfWork.Repository<Core.Models.VisitDetails>()
                    .Get(x => x.PatientId == request.PatientId && x.Id == request.Id)
                    .OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                if (visitDetailsEdit != null)
                {

                    visitDetailsEdit.DaysPostPartum = request.DaysPostPartum;
                    visitDetailsEdit.VisitNumber = request.VisitNumber;
                    visitDetailsEdit.VisitType = request.VisitType;

                     _unitOfWork.Repository<Core.Models.VisitDetails>().Update(visitDetailsEdit);
                    await _unitOfWork.SaveAsync();
                    return Result<VisitDetailsCommandEditResult>.Valid(new VisitDetailsCommandEditResult {Id = visitDetailsEdit.Id});
                }
                else
                {
                    return Result<VisitDetailsCommandEditResult>.Valid(new VisitDetailsCommandEditResult { Id = 0 });
                }
            }
        }
    }
}