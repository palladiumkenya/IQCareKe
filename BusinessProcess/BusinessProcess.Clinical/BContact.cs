using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;

namespace BusinessProcess.Clinical
{
    public class BContact : ProcessBase,IContact 
    {
        #region "Constructor"
        public BContact()
        {
        }
        #endregion

        public DataSet GetTestNorthwindEmployees()
        {
            lock (this)
            {
                string theSQL = string.Format("select * from mst_patient");
                ClsObject theDL = new ClsObject();
                Hashtable theParams = new Hashtable();
                theParams.Clear();
                return (DataSet)Convert.ChangeType(theDL.ReturnObject(theParams, theSQL, ClsUtility.ObjectEnum.DataSet), typeof(DataSet));
            }
 
        }
    }
}
