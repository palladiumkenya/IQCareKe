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

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Lookup
{
   public class GetWardCommandHandler:IRequestHandler<GetWardCommand,Result<AddWardListReponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        List<WardLookup> wards = new List<WardLookup>();
        int CountyId;
        int SubCountyId;


        public GetWardCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }



        public async Task<Result<AddWardListReponse>> Handle(GetWardCommand request,CancellationToken cancellationToken)
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

                if (CountyId == 0 && SubCountyId > 0)
                {


                    wards = await ll.GetWardList(SubCountyId);




                }

                return Result<AddWardListReponse>.Valid(new AddWardListReponse()


                {

                    Wards = wards


                });


            }
            catch (Exception e)
            {
                return Result<AddWardListReponse>.Invalid(e.Message);
            }

        }
            
            
    }



}
