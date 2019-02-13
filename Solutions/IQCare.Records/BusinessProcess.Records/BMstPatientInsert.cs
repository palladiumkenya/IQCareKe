using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records
{
    public class BMstPatientInsert : ProcessBase, IMst_PatientInsert
    {
        public int AddMstPatient(string firstName, string lastName, string middleName, int locationId, string patientEnrollmentID, int referredFrom, DateTime registrationDate, int sex, DateTime dob, int dobPrecision, int maritalStatus, string address, string phone, int userID, string posId, int moduleId, DateTime startDate, DateTime createDate)
        {
            int ptnPk = 0;
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@FirstName", SqlDbType.VarChar, firstName);
            ClsUtility.AddExtendedParameters("@LastName", SqlDbType.VarChar, lastName);
            ClsUtility.AddExtendedParameters("@MiddleName", SqlDbType.VarChar, middleName);
            ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationId);
            ClsUtility.AddExtendedParameters("@PatientEnrollmentID", SqlDbType.VarChar, patientEnrollmentID);
            ClsUtility.AddExtendedParameters("@ReferredFrom", SqlDbType.Int, referredFrom);
            ClsUtility.AddExtendedParameters("@RegistrationDate", SqlDbType.DateTime, registrationDate);
            ClsUtility.AddExtendedParameters("@Sex", SqlDbType.Int, sex);
            ClsUtility.AddExtendedParameters("@DOB", SqlDbType.DateTime, dob);
            ClsUtility.AddExtendedParameters("@DobPrecision", SqlDbType.Int, dobPrecision);
            ClsUtility.AddExtendedParameters("@MaritalStatus", SqlDbType.Int, maritalStatus);
            ClsUtility.AddExtendedParameters("@Address", SqlDbType.VarChar, address);
            ClsUtility.AddExtendedParameters("@Phone", SqlDbType.VarChar, phone);
            ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, userID);
            ClsUtility.AddExtendedParameters("@PosId", SqlDbType.VarChar, posId);
            ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
            ClsUtility.AddExtendedParameters("@StartDate", SqlDbType.DateTime, startDate);
            ClsUtility.AddExtendedParameters("@CreateDate", SqlDbType.DateTime, createDate);


            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "mstPatient_Insert", ClsUtility.ObjectEnum.DataTable);
            if (dt != null && dt.Rows.Count > 0)
            {
                ptnPk = Convert.ToInt32(dt.Rows[0]["Ptn_Pk"]);
            }

            return ptnPk;
        }

        public void AddOrdVisit(int ptnPk, int locationId, DateTime visitDate, int visitType, int userId, DateTime createDate, int moduleId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@Ptn_Pk", SqlDbType.Int, ptnPk);
            ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationId);
            ClsUtility.AddExtendedParameters("@VisitDate", SqlDbType.DateTime, visitDate);
            ClsUtility.AddExtendedParameters("@VisitType", SqlDbType.Int, visitType);
            ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, userId);
            ClsUtility.AddExtendedParameters("@CreateDate", SqlDbType.DateTime, createDate);
            ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Ord_Visit_Insert", ClsUtility.ObjectEnum.DataTable);
        }
    }
}
