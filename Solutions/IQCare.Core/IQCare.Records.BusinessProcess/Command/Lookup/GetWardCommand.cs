using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCareRecords.Common.BusinessProcess.Command
{
    public class GetWardCommand:IRequest<Result<AddWardListReponse>>
    {
        public string CountyId;
        public string SubcountyId;
    }

    public class AddWardListReponse
    {
        public List<WardLookup> Wards { get; set; }
    }
}
