using System.Collections.Generic;
using System.Linq;
using IQCare.Common.BusinessProcess.Interfaces;
using IQCare.Common.Core.Interfaces.Repositories;
using IQCare.Common.Core.Models;

namespace IQCare.Common.BusinessProcess.Services
{
    public class LookupItemViewService : ILookupItemViewService
    {
        private readonly ILookupItemViewRepository _lookupItemViewRepository;

        public LookupItemViewService(ILookupItemViewRepository lookupItemViewRepository)
        {
            _lookupItemViewRepository = lookupItemViewRepository;
        }
        public List<LookupItemView> GetLookupItemsByGroup(string groupName)
        {
            return _lookupItemViewRepository.FindBy(x => x.MasterName == groupName).ToList();
        }

        public List<KeyValuePair<string, List<LookupItemView>>> GetHtsOptions(string[] options)
        {
            var lookups = new List<KeyValuePair<string, List<LookupItemView>>>();
            for (int i = 0; i < options.Length; i++)
            {
                var items = _lookupItemViewRepository.FindBy(x => x.MasterName == options[i]).OrderBy(y=>y.ItemId).ToList();

                lookups.Add(new KeyValuePair<string, List<LookupItemView>>(options[i], items));
            }

            return lookups;
        }
    }
}