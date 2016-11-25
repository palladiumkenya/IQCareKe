using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.Common;
using Entities.Administration;
using Interface.FormBuilder;
namespace BusinessProcess.FormBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DataAccess.Base.ProcessBase" />
    /// <seealso cref="Interface.FormBuilder.IModule" />
    public class BModule : ProcessBase, IModule
    {
        /// <summary>
        /// Gets the module detail.
        /// </summary>
        /// <returns></returns>
        public DataSet GetModuleDetail()
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetModuleIdentifier_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        /// <summary>
        /// Gets the module identifier.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        public DataSet GetModuleIdentifier(Int32 ModuleId)
        {
            lock (this)
            {
                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                return (DataSet)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_GetModuleIdentificationDetails_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }
        /// <summary>
        /// Statuses the update.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
         public int StatusUpdate(Hashtable ht)
        {
            int RowsEffected = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ht["ModuleID"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, ht["Status"].ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, ht["DeleteFlag"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                RowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_StatusUpdate_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }
            
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return RowsEffected;

        }
         /// <summary>
         /// Saves the update module detail.
         /// </summary>
         /// <param name="ht">The ht.</param>
         /// <param name="moduleDetail">The module detail.</param>
         /// <param name="businessRule">The business rule.</param>
         /// <returns></returns>
         public int SaveUpdateModuleDetail(Hashtable ht, DataTable moduleDetail, DataTable businessRule)
         {
             int moduleId;
             bool canEnroll = true;
             try
             {
                 this.Connection = DataMgr.GetConnection();
                 this.Transaction = DataMgr.BeginTransaction(this.Connection);

                 ClsObject ModuleMgr = new ClsObject();
                 ModuleMgr.Connection = this.Connection;
                 ModuleMgr.Transaction = this.Transaction;

                 canEnroll = Convert.ToBoolean(ht["CanEnroll"].ToString());
                 ClsUtility.Init_Hashtable();
                 ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, ht["ModuleId"].ToString());
                 ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, ht["ModuleName"].ToString());
                 ClsUtility.AddParameters("@DisplayName", SqlDbType.VarChar, ht["DisplayName"].ToString());
                 ClsUtility.AddExtendedParameters("@CanEnroll", SqlDbType.Bit, Convert.ToBoolean(ht["CanEnroll"].ToString()));
                 ClsUtility.AddParameters("@Status", SqlDbType.Int, ht["Status"].ToString());
                 ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, ht["DeleteFlag"].ToString());
                 ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());

                 ClsUtility.AddParameters("@PharmacyFlag", SqlDbType.Int, ht["PharmacyFlag"].ToString());

                 DataTable theDT = (DataTable)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_SaveUpdateModule_Constella", ClsUtility.ObjectEnum.DataTable);

                 moduleId = Convert.ToInt32(theDT.Rows[0][0]);
                 if (moduleId != 0)
                 {
                     for (int i = 0; i < moduleDetail.Rows.Count; i++)
                     {
                         //if (dt.Rows[i]["Selected"].ToString() == "True")
                         //{
                         ClsUtility.Init_Hashtable();
                         ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, moduleId.ToString());
                         ClsUtility.AddParameters("@FieldID", SqlDbType.Int, moduleDetail.Rows[i]["Id"].ToString());
                         ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, moduleDetail.Rows[i]["IdentifierName"].ToString());
                         ClsUtility.AddParameters("@FieldType", SqlDbType.Int, moduleDetail.Rows[i]["FieldType"].ToString());
                         if (canEnroll)
                         {
                             ClsUtility.AddParameters("@Identifierchecked", SqlDbType.VarChar, moduleDetail.Rows[i]["Selected"].ToString());
                         }
                         else
                         {
                             ClsUtility.AddParameters("@Identifierchecked", SqlDbType.VarChar, "False");
                         }
                         ClsUtility.AddExtendedParameters("@RequiredFlag", SqlDbType.Bit, moduleDetail.Rows[i]["Selected"].ToString().ToLower()=="true");
                         ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                         ClsUtility.AddParameters("@Label", SqlDbType.VarChar, moduleDetail.Rows[i]["Label"].ToString());
                         ClsUtility.AddParameters("@autopopulatenumber", SqlDbType.Int, moduleDetail.Rows[i]["autopopulatenumber"].ToString());

                         Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_SaveUpdateModuleIdentification_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                         //}
                     }
                     //if (businessRule.Rows.Count == 0)
                     //{
                         ClsUtility.Init_Hashtable();
                         ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
                         Int32 num = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "FormBuilder_DeleteModuleBusinessRules", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    // }
                     for (int i = 0; i < businessRule.Rows.Count; i++)
                     {
                         //if (dt.Rows[i]["Selected"].ToString() == "True")
                         //{
                         ClsUtility.Init_Hashtable();
                         ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId);
                         ClsUtility.AddExtendedParameters("@BusRuleId", SqlDbType.Int, Convert.ToInt32(businessRule.Rows[i]["BusRuleId"]));
                         ClsUtility.AddParameters("@value", SqlDbType.Int, businessRule.Rows[i]["Value"].ToString());
                         ClsUtility.AddParameters("@value1", SqlDbType.Int, businessRule.Rows[i]["Value1"].ToString());
                         ClsUtility.AddParameters("@UserID", SqlDbType.Int, ht["UserID"].ToString());
                         ClsUtility.AddParameters("@setType", SqlDbType.Int, businessRule.Rows[i]["SetType"].ToString());

                         Int32 NoRowsEffected = (Int32)ModuleMgr.ReturnObject(ClsUtility.theParams, "FormBuilder_SaveModuleBusinessRules", ClsUtility.ObjectEnum.ExecuteNonQuery);
                         //}
                     }
                 }
                 DataMgr.CommitTransaction(this.Transaction);
                 DataMgr.ReleaseConnection(this.Connection);
             }
             catch
             {
                 DataMgr.RollBackTransation(this.Transaction);
                 throw;
             }
             finally
             {
                 if (this.Connection != null)
                     DataMgr.ReleaseConnection(this.Connection);
             }
             return moduleId;
         }


         /// <summary>
         /// Deletes the module.
         /// </summary>
         /// <param name="ModuleId">The module identifier.</param>
        public void DeleteModule(Int32 ModuleId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ModuleMgr = new ClsObject();
                ModuleMgr.Connection = this.Connection;
                ModuleMgr.Transaction = this.Transaction;

                ClsObject BusinessRule = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ModuleId", SqlDbType.VarChar, ModuleId.ToString());
                Int32 NoRowsEffected = (Int32)BusinessRule.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteModule_Future", ClsUtility.ObjectEnum.ExecuteNonQuery);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
            }

            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
       
        /// <summary>
        /// Gets the business rule.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns></returns>
        public List<ServiceRule> GetBusinessRule(int? moduleId = null)
        {
            ClsObject BusinessRule = new ClsObject();
            ClsUtility.Init_Hashtable();
           if(moduleId.HasValue) 
               ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, moduleId.Value);
            DataTable dt = (DataTable)BusinessRule.ReturnObject(ClsUtility.theParams, "ServiceArea_GetBusinessRule", ClsUtility.ObjectEnum.DataTable);

            var result = (from row in dt.AsEnumerable()
                          select new ServiceRule()
                          {
                              Id = Convert.ToInt32(row["Id"]),
                              MinValue = Convert.ToString(row["MinValue"]),
                              MaxValue = Convert.ToString(row["MaxValue"]),
                              RuleSet = Convert.ToInt32(row["RuleSet"]),
                              ServiceAreaId = Convert.ToInt32(row["ModuleId"]),
                              BusinessRule = new BusinessRule() { 
                                  RuleId = Convert.ToInt32(row["BusRuleId"]),
                                  RuleName = Convert.ToString(row["BusRuleName"]),
                                  ReferenceId= Convert.ToString(row["BusRuleReferenceId"]),
                                  DeleteFlag = Convert.ToBoolean(row["BusRuleDeleteFlag"])
                              }
                          }
                          ).ToList<ServiceRule>();
            return result;
        }


       
    }
}