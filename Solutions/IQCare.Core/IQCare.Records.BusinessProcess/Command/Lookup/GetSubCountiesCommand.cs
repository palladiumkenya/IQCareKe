using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using County = IQCare.Common.Core.Models.County;
using SubCountyLookup = IQCare.Common.Core.Models.SubCountyLookup;

namespace IQCare.Records.BusinessProcess.Command.Lookup
{
   public  class GetSubCountiesCommand: IRequest<Result<List<SubCountyLookup>>>
   {
        public int CountyId;
   }
}
