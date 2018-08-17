using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
    public class GetSubCountiesCommandHandler:IRequestHandler<GetSubCountiesCommand,Result<AddSubCountiesResponse>>
    {

        private readonly ICommonUnitOfWork _unitOfWork;
        List<SubCountyLookup> subcounties = new List<SubCountyLookup>();
        int CountyId;
        int SubCountyId;


        public GetSubCountiesCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<Result<AddSubCountiesResponse>> Handle (GetSubCountiesCommand request,CancellationToken cancellationToken)
        {
            try
            {
                LookupLogic ll = new LookupLogic(_unitOfWork);

                if (String.IsNullOrEmpty(request.CountyId))
                {
                    CountyId = 0;
                }
                else
                {
                    CountyId = Convert.ToInt32(request.CountyId);
                }
                if (String.IsNullOrEmpty(request.SubcountyId))
                {
                    SubCountyId = 0;

                }
                else
                {
                    SubCountyId = Convert.ToInt32(request.SubcountyId);
                }



                if (CountyId > 0 && SubCountyId == 0)
                {
                    subcounties = await ll.GetSubCountyList(CountyId);


                }

                return Result<AddSubCountiesResponse>.Valid(new AddSubCountiesResponse()
                {
                    SubCounties = subcounties


                });

            }
            catch(Exception e)
            {
                return Result<AddSubCountiesResponse>.Invalid(e.Message);
            }
        }

    }
}
