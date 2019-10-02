using IQCare.Lab.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQCare.Lab.BusinessProcess.Queries
{
    public class GetVlStatusCountQuery : IRequest<Result<IEnumerable<VLStatuses<string, int>>>>
    {
        // IEnumerable<IGrouping<TKey, TSource>>
    }

    public class VLStatuses<T1, T2>
    {
        public string Metric { get; set; }
        public int Count { get; set; }
    }
}
