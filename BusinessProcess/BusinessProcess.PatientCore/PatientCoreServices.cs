using System.Collections.Generic;
using DataAccess.Context;
using Entities.PatientCore;
using Interface.PatientCore;
using DataAccess.Base;
using System;
using DataAccess.Entity;
using System.Data;
using DataAccess.Common;
using Entities.Common;

namespace BusinessProcess.PatientCore
{
    public class PatientCoreServices : ProcessBase, IPatientService
    {
        public List<PatientVisit> GetAllPatientVisits(int patientId)
        {
            PatientRepository repo = new PatientRepository();
            return repo.GetAllPatientVisits(patientId);
        }

        public PatientVisit GetPatientLastVisit(int patientId)
        {
            PatientRepository repo = new PatientRepository();
            return repo.GetRecentPatientVisit(patientId);
        }

       
        public Patient GetPatient(int Id)
        {
            PatientRepository repo = new PatientRepository();
            return repo.Get(Id);
            //ClsObject obj = new ClsObject();
            //ClsUtility.Init_Hashtable();

            //ClsUtility.AddExtendedParameters("@PatientId", System.Data.SqlDbType.Int, Id);
            //ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

            //DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetPatientRecord_Futures", ClsUtility.ObjectEnum.DataTable);
            //ClsUtility.Init_Hashtable();
            //obj = null;
            //Patient patient = null;
            //DateTime? nullDate = null;
            //if (null != dt && dt.Rows.Count > 0)
            //{
            //    DataRow rowView = dt.Rows[0];

            //    patient = new Patient()
            //    {
            //        Id = Convert.ToInt32(rowView["PatientId"]),
            //        FirstName = rowView["FirstName"].ToString(),
            //        MiddleName = Convert.ToString(rowView["MiddleName"]),
            //        LastName = Convert.ToString(rowView["LastName"]),
            //        DateOfBirth = Convert.ToDateTime(rowView["DOB"]),
            //        DateOfRegistration = Convert.ToDateTime(rowView["RegistrationDate"]),
            //        Sex = Convert.ToString(rowView["PatientSex"]),
            //        UniqueFacilityId = Convert.ToString(rowView["PatientFacilityID"]),
            //        DateOfDeath = rowView["DateOfDeath"] == DBNull.Value ? nullDate : Convert.ToDateTime(rowView["DateOfDeath"]),
            //        DeleteFlag = Convert.ToBoolean(rowView["DeleteFlag"]),
            //        LocationId = Convert.ToInt32(rowView["LocationId"])

            //    };


            //} 
            //return patient;
        }

        public List<PatientAlert> GetPatientAlerts(int moduleId, int patientId)
        {
            PatientRepository repo = new PatientRepository();
           return repo.FetchPatientAlerts(moduleId);
        }

        public ResultKeyValue ExecutePatientAlert(PatientAlert alert, int patientId)
        {
            ClsObject theQB = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@QryString", SqlDbType.VarChar, alert.Query.QueryText);
            ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId);
          
            DataSet ds = (DataSet)theQB.ReturnObject(ClsUtility.theParams, "pr_General_SQL_Parse", ClsUtility.ObjectEnum.DataSet);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                return new ResultKeyValue() {
                    ResultName = ds.Tables[0].Rows[0][0].ToString(),
                    ResultValue = ds.Tables[0].Rows[0][1]
                };
            }
            return null;
                
        }
    }
}
