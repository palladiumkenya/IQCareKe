using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IQCare.Pharm.Infrastructure;
using IQCare.Pharm.Core.Models;
using IQCare.Pharm.BusinessProcess.Queries;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace IQCare.Pharm.BusinessProcess.QueryHandler
{
    public class GetFacilityModuleQueryHandler : IRequestHandler<GetFacilityModuleQuery, Result<List<FacilityModule>>>
    {
        private readonly IPharmUnitOfWork _pharmUnitOfWork;
        //  private readonly IMapper _mapper;

        public List<FacilityModule> FacilityModuleList = new List<FacilityModule>();
        public GetFacilityModuleQueryHandler(IPharmUnitOfWork pharmUnitOfWork
          )
        {
            _pharmUnitOfWork = pharmUnitOfWork;
     //       _mapper = mapper;
        }

        public  Task<Result<List<FacilityModule>>> Handle(GetFacilityModuleQuery request, CancellationToken cancellationToken)
        {

            try
            {

                var FacilityModuleResults = _pharmUnitOfWork.Repository<lnk_FacilityModule>().Get(x => x.FacilityID == request.LocationId).Include(x => x.Facility)
                .Include(x => x.Module).Where(x => x.Module.Status == 2);
                List<FacilityModule> facilityList = FacilityModuleResults.Where(x => x.Module.ModuleID == 203 && x.ModuleID == 203).Select(t => new FacilityModule()
                {
                    FacilityID = t.FacilityID,
                    ModuleID = t.ModuleID,
                    DisplayName = t.Module.DisplayName,
                    ModuleName = t.Module.ModuleName,
                    CanEnroll = t.Module.CanEnroll,
                    ExpPwdFlag = t.Facility.ExpPwdFlag,
                    ExpPwdDays = t.Facility.ExpPwdDays,
                    ModuleFlag = t.Module.ModuleFlag,
                    PharmacyFlag = t.Module.PharmacyFlag,
                    StrongPassFlag = t.Facility.StrongPassFlag
                }).ToList();

                List<FacilityModule> facilitygreencard = FacilityModuleResults.Where(x => x.Module.ModuleID == 203 && x.ModuleID == 203).Select(t => new FacilityModule()
                {
                    FacilityID = t.FacilityID,
                    ModuleID = t.ModuleID,
                    DisplayName = "Green Card (2016)",
                    ModuleName = "CCC",
                    CanEnroll = t.Module.CanEnroll,
                    ExpPwdFlag = t.Facility.ExpPwdFlag,
                    ExpPwdDays = t.Facility.ExpPwdDays,
                    ModuleFlag = t.Module.ModuleFlag,
                    PharmacyFlag = t.Module.PharmacyFlag,
                    StrongPassFlag = t.Facility.StrongPassFlag
                }).ToList();

                List<FacilityModule> facilityUniversalPortal = FacilityModuleResults.Where(x => x.Module.ModuleID == 203 && x.ModuleID == 203).Select(t => new FacilityModule()
                {
                    FacilityID = t.FacilityID,
                    ModuleID = t.ModuleID,
                    DisplayName = "'Universal Service Portal'",
                    ModuleName = "HTS",
                    CanEnroll = true,
                    ExpPwdFlag = t.Facility.ExpPwdFlag,
                    ExpPwdDays = t.Facility.ExpPwdDays,
                    ModuleFlag = true,
                    PharmacyFlag = t.Module.PharmacyFlag,
                    StrongPassFlag = t.Facility.StrongPassFlag
                }).ToList();

                if (facilityList != null && facilityList.Count > 0)
                {

                    FacilityModuleList.AddRange(facilityList);
                }

                if (facilitygreencard != null  && facilitygreencard.Count > 0)
                {
                    FacilityModuleList.AddRange(facilitygreencard);
                }
                if (facilityUniversalPortal != null  && facilityUniversalPortal.Count > 0)
                {
                    FacilityModuleList.AddRange(facilityUniversalPortal);
                }



                return Task.FromResult(Result<List<FacilityModule>>.Valid(FacilityModuleList));

            }
            catch(Exception ex)
            {
                
                return Task.FromResult(Result<List<FacilityModule>>.Invalid(ex.Message.ToString()));

            }
        }


    }
}
