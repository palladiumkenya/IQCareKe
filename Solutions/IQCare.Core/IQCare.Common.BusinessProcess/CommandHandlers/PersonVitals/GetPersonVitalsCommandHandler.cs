using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using IQCare.Common.BusinessProcess.Commands.PersonVitals;
using System.Threading.Tasks;
using System.Threading;
using IQCare.SharedKernel.Infrastructure;
using System.Linq;


namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonVitals
{
    public class GetPersonVitalsCommandHandler : IRequestHandler<GetPersonVitalsCommand, Result<List<PersonVitalsView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonVitalsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PersonVitalsView>>> Handle(GetPersonVitalsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var personVitals = _unitOfWork.Repository<PersonVitalsView>().Get(x => x.PersonId == request.PersonId && x.VisitDate.Value.Date == DateTime.Now.Date).OrderByDescending(x => x.CreateDate).AsEnumerable().ToList();


                return await Task.FromResult(Result<List<PersonVitalsView>>.Valid(personVitals));
            }
            catch (Exception ex)
            {
                string message =
                    $"An error occured while getting patient vitals for Person  {request.PersonId} + {ex.Message}";

                return await Task.FromResult(Result<List<PersonVitalsView>>.Invalid(message));
            }

        }
    }
}
