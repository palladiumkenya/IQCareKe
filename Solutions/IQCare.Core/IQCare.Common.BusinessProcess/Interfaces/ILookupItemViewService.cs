using IQCare.Common.Core.Models;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Interfaces
{
    public interface ILookupItemViewService
    {
        List<LookupItemView> GetLookupItemsByGroup(string groupName);

        List<KeyValuePair<string, List<LookupItemView>>> GetHtsOptions(string[] options);
    }
}