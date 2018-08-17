using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCareRecords.Common.BusinessProcess.Command
{
    public class GetCountiesCommand : IRequest<Result<AddCountyListResponse>>
    {
        public string CountyId;
        public string SubcountyId;

    }


    public class AddCountyListResponse
    {
        public List<CountyLookup> Counties { get; set; } 
     
    }

   
}


