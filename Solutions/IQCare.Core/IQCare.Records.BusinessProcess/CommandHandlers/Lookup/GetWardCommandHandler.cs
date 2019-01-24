using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Records.BusinessProcess.Command.Lookup;
using MediatR;
using Serilog;
using WardLookup = IQCare.Common.Core.Models.WardLookup;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Lookup
{
   public class GetWardCommandHandler:IRequestHandler<GetWardCommand,Result<List<WardLookup>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetWardCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }



        public async Task<Result<List<WardLookup>>> Handle(GetWardCommand request,CancellationToken cancellationToken)
        {
            try
            {
                LookupLogic ll = new LookupLogic(_unitOfWork);
                var wards = await ll.GetWardList(request.SubcountyId);
                return Result<List<WardLookup>>.Valid(wards);
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                return Result<List<WardLookup>>.Invalid(e.Message);
            }
        }
    }



}
