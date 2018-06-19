using DataAccess.Base;
using Interface.WebApi;

namespace BusinessProcess.WebApi
{
    public class BIQConfig : ProcessBase, IIQConfig
    {
        public string EMRConnectionString => DataMgr.ConnectionString;
    }
}
