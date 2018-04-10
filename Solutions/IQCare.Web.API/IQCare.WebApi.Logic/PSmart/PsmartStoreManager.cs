using Entity.WebApi.PSmart;
using Interface.WebApi;
using System;

namespace IQCare.WebApi.Logic.PSmart
{
    public class PsmartStoreManager
    {
        private readonly IPsmartStoreManager _psmartStoreManager = (IPsmartStoreManager)Application.Presentation.ObjectFactory.CreateInstance("BusinessProcess.WebApi.BPsmartStore, BusinessProcess.WebApi");
        private int _result; 

        public PsmartStoreManager()
        {
            
        }

        public int SaveEncryptedPsmartShr(string encryptedShr)
        {

            Psmart_Store psmartStore = new Psmart_Store()
            {
                Status = "PENDING",
                uuid = Guid.NewGuid().ToString(),
                SHR = encryptedShr.ToString()

            };

            _result = _psmartStoreManager.SaveShr(psmartStore);
            return _result;
        }
    }
}