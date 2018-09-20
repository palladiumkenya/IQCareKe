using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entity.WebApi.PSmart;
using Interface.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.WebApi
{
    public class BPSmartAuth : ProcessBase, IPSmartAuthManager
    {
        public PsmartAuthUser LoginValidate(string username)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
            {
                var _user = unitOfWork.PSmartAuthRepository.FindBy(x => x.UserName == username.Trim()).FirstOrDefault();
                if (null != _user)
                {
                    return _user;
                }
                else { return null; }
            }
        }
    }
}
