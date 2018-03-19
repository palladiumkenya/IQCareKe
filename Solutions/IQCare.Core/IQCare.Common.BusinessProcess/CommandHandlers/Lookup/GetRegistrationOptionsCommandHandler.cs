using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetRegistrationOptionsCommandHandler : IRequestHandler<GetRegistrationOptionsCommand, Result<GetRegistrationOptionsResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetRegistrationOptionsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetRegistrationOptionsResponse>> Handle(GetRegistrationOptionsCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var lookups = new List<KeyValuePair<string, List<LookupItemView>>>();
                for (int i = 0; i < request.RegistrationOptions.Length; i++)
                {
                    var items = await _unitOfWork.Repository<LookupItemView>()
                        .Get(c => c.MasterName == request.RegistrationOptions[i]).ToListAsync();

                    lookups.Add(new KeyValuePair<string, List<LookupItemView>>(request.RegistrationOptions[i], items));
                }

                _unitOfWork.Dispose();

                return Result<GetRegistrationOptionsResponse>.Valid(new GetRegistrationOptionsResponse
                {
                    LookupItems = lookups
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}