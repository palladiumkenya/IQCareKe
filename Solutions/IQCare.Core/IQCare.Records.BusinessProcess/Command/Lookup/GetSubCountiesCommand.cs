using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCareRecords.Common.BusinessProcess.Command
{
   public  class GetSubCountiesCommand: IRequest<Result<AddSubCountiesResponse>>
    {
        public string CountyId;
        public string SubcountyId;
    }


    public class AddSubCountiesResponse
    {
        public List<SubCountyLookup> SubCounties { get; set; }
    }
}
