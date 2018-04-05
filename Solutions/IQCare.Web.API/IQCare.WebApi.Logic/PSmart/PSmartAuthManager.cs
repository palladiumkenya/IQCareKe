using System.Security.Policy;
using Application.Common;
using Entity.WebApi.PSmart;
using Interface.WebApi;
using IQCare.DTO.PSmart;

namespace IQCare.WebApi.Logic.PSmart
{
    public class PSmartAuthManager: IPSmartAuthRequest
    {
        private readonly IPSmartAuthManager _pSmartAuthManager = (IPSmartAuthManager)Application.Presentation.ObjectFactory.CreateInstance("BusinessProcess.WebApi.BPSmartAuth, BusinessProcess.WebApi");

        public PSmartAuthManager()
        {

        }

        public DtoUserAuth Authentication(string username, string password)
        {
            DtoUserAuth objAuth=new DtoUserAuth();
            PsmartAuthUser _user = _pSmartAuthManager.LoginValidate(username);
            objAuth.STATUS = "false";
            objAuth.DISPLAYNAME = "";
           // string response = @"{""status"":""false"", ""DisplayName"":""""}";
 
            if (null != _user)
            {
                Utility util = new Utility();
                if (_user.Password == util.Encrypt(password))
                {
                    //response = @"{  ""STATUS"":""true"", ""DISPLAYNAME"":" + _user.DisplayName + "   }";
                    objAuth.STATUS = "true";
                    objAuth.DISPLAYNAME = _user.DisplayName;
                    objAuth.FACILITY = _user.FACILITY;

                }
                //response = _user.DisplayName;
            }
            return objAuth;
        }

        
    }
}
