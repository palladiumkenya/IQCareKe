using Application.Presentation;
using Interface.Administration;
using System;
using System.Data;

namespace IQCare.CCC.UILogic.Interoperability
{
    public sealed class InteropUser
    {
        private static  InteropUser instance = null;

        private static readonly object padlock = new object();
        private int _userId;
        public static int UserId
        {
            get
            {
                return Instance._userId;
            }
        }
        
        private InteropUser()
        {
            Iuser _mgr = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
            DataTable dt = _mgr.GetUserByUserName("interop");
            if(dt != null && dt.Rows.Count == 1)
            {
                _userId = Convert.ToInt32(dt.Rows[0]["UserId"]);
            }
        }
        public static InteropUser Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null )
                    {
                        instance = new InteropUser();
                    }
                    return instance;
                }
            }
        }
    }
}
