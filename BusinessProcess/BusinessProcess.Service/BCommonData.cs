using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Service;
using System.Data;
using DataAccess.Entity;
using DataAccess.Common;

namespace BusinessProcess.Service
{
    public class BCommonData : ProcessBase, ICommonData
    {
        public BCommonData() { }

        public DataTable getUserList()
        { 
            ClsUtility.Init_Hashtable();
            ClsObject oUserList = new ClsObject();
            //return (DataTable)oUserList.ReturnObject(ClsUtility.theParams, "VW_UserDesignationTransaction", ClsDBUtility.ObjectEnum.DataTable);
            return (DataTable)oUserList.ReturnObject(ClsUtility.theParams, "select * from VW_UserDesignationTransaction", ClsUtility.ObjectEnum.DataTable);// Change by Rahmat(14.02.17)
        }

    }
    
}
