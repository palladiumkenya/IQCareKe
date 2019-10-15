using IQCare.Library;
using IQCare.Pharm.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.BusinessProcess.Queries
{
    public class GetFacilityModuleQuery  :IRequest<Result<List<FacilityModule>>>
    {
        public int LocationId { get; set; }
    }
}
