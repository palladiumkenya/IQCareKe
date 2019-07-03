using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.CCC;
using Interface.CCC.HIVEducation;
using System;
using System.Data;

namespace BusinessProcess.CCC.HIVEducation

{
    public class BHIVEducation : ProcessBase, IHIVEducation
    {
       // private int result;

        public int AddPatientHIVEducation(int Id, int Ptn_Pk, int visitPk, int locationID, int? councellingTypeId = null, int? councellingTopicId = null, DateTime? visitDate = null, string comments = null, string otherDetail = null, int? userId = null, bool? deleteFlag = null, int? moduleId = null)
        {
            try
            {
                ClsObject obj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@Id", SqlDbType.Int, -1);
                ClsUtility.AddExtendedParameters("@Ptn_Pk", SqlDbType.Int, Ptn_Pk);
                ClsUtility.AddExtendedParameters("@VisitPk", SqlDbType.Int, visitPk);
                ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, locationID);
                ClsUtility.AddExtendedParameters("@CouncellingTypeId", SqlDbType.Int, councellingTypeId);
                ClsUtility.AddExtendedParameters("@CouncellingTopicId", SqlDbType.Int, councellingTopicId);
                ClsUtility.AddExtendedParameters("@VisitDate", SqlDbType.DateTime, visitDate);
                ClsUtility.AddExtendedParameters("@Comments", SqlDbType.VarChar, comments);
                ClsUtility.AddExtendedParameters("@OtherDetail", SqlDbType.VarChar, null);
                ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
                ClsUtility.AddExtendedParameters("@DeleteFlag", SqlDbType.Bit, deleteFlag);
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);


                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Pr_Clinical_SaveFollowupEducation_Constella", ClsUtility.ObjectEnum.DataTable);

                return 0;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
            
        }

        public DataTable getCounsellingTopics(string counsellingtopics)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject(); // Entity
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@regimenLine", SqlDbType.Int, counsellingtopics);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPharmacyRegimens", ClsUtility.ObjectEnum.DataTable);

            }
        }

        public DataTable GetPatientFollowupEducationData(int ptnPk)
        {
            try
            {
                ClsObject obj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@Ptn_Pk", SqlDbType.Int, ptnPk);

                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetFollowupEducation_Constella", ClsUtility.ObjectEnum.DataTable);
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int UpdatePatientHIVEducation(HIVEducationFollowup HEF)
        {
            throw new NotImplementedException();
        }
    }
}
