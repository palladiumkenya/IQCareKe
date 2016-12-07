using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.Lab;
using Interface.Laboratory;

namespace BusinessProcess.Laboratory
{
    public class BLabManager : ProcessBase, ILabManager
    {
        public int ActivateLabTest(int labTestId, bool active, int userId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@LabTestId", SqlDbType.Int, labTestId);
            ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, active);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
            int rowcount = (int)obj.ReturnObject(ClsUtility.theParams, "Laboratory_ActivateLabTest", ClsUtility.ObjectEnum.ExecuteNonQuery);
            return rowcount;
        }
        public int DeleteLabTest(int labTestId, int userId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@LabTestId", SqlDbType.Int, labTestId);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);

            int rowcount = (int)obj.ReturnObject(ClsUtility.theParams, "Laboratory_DeleteLabTest", ClsUtility.ObjectEnum.ExecuteNonQuery);
            return rowcount;
        }
        public int DeleteTestParameter(int parameterId, int labTestId, int userId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@ParameterId", SqlDbType.Int, parameterId);
            ClsUtility.AddExtendedParameters("@LabTestId", SqlDbType.Int, labTestId);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);

            int rowcount = (int)obj.ReturnObject(ClsUtility.theParams, "Laboratory_DeleteTestParameter", ClsUtility.ObjectEnum.ExecuteNonQuery);
            return rowcount;
        }

        public LabTest GetLabTestById(int LabTestId)
        {
            DataTable dt = this.GetLabTests(null);
            LabTest result = null;
            TestDepartment department = null;
            if (null != dt && dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                result = new LabTest()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    IsGroup = Convert.ToBoolean(row["IsGroup"]),
                    ReferenceId = row["ReferenceId"].ToString(),
                    ParameterCount = Convert.ToInt32(row["ParameterCount"]),
                    Department = row["DepartmentId"] == DBNull.Value ? department : new TestDepartment() { Id = Convert.ToInt32(row["DepartmentId"]), Name = row["Department"].ToString() },  
                    Active = Convert.ToBoolean(row["Active"]),                 
                    DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                    TestParameter = this.GetLabTestParameters(LabTestId)
                };
            }

            return result;
        }

        public TestParameter GetLabTestParameterById(int LabTestId, int ParameterId)
        {
            DataTable dt = this.GetTestParameters(LabTestId, ParameterId);
            TestParameter param = null;
            if (null != dt && dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                param = new TestParameter()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["ParameterName"].ToString(),
                    ReferenceId = row["ReferenceId"].ToString(),
                    LabTestId = Convert.ToInt32(row["LabTestId"]),
                    DataType = row["DataType"].ToString(),
                    Rank = Convert.ToDecimal(row["OrdRank"]),
                    LoincCode = row["LoincCode"].ToString(),
                    ResultConfig = null,
                    DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                    ResultOption = null
                };
            }

            return param;
        }

        public List<TestParameter> GetLabTestParameters(int LabTestId)
        {
            DataTable dt = this.GetTestParameters(LabTestId, null);
            var result = (from row in dt.AsEnumerable()
                          select new TestParameter()
                          {
                              Id = Convert.ToInt32(row["Id"]),
                              Name = row["ParameterName"].ToString(),
                              ReferenceId = row["ReferenceId"].ToString(),
                              LabTestId = Convert.ToInt32(row["LabTestId"]),
                              DataType = row["DataType"].ToString(),
                              Rank = Convert.ToDecimal(row["OrdRank"]),
                              LoincCode = row["LoincCode"].ToString(),
                              ResultConfig = null,
                              DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                              ResultOption = null
                          }
                            );
            return result.ToList();
        }
        
        public List<LabTest> GetLabTests()
        {
            DataTable dt = this.GetLabTests(null);
            TestDepartment department = null;
            var result = (from row in dt.AsEnumerable()
                          select new LabTest()
                          {
                              Id = Convert.ToInt32(row["Id"]),
                              Name = row["Name"].ToString(),
                              IsGroup = Convert.ToBoolean(row["IsGroup"]),
                              ReferenceId = row["ReferenceId"].ToString(),
                              ParameterCount = Convert.ToInt32(row["ParameterCount"]),
                              Department = row["DepartmentId"] == DBNull.Value ? department : new TestDepartment() { Id = Convert.ToInt32(row["DepartmentId"]), Name = row["Department"].ToString() },  
                               Active = Convert.ToBoolean(row["Active"]),
                              DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                              TestParameter = null
                          }
                            );
            return result.ToList(); ;
        }
        decimal? nullDecimal = null;
        public List<ParameterResultConfig> GetParameterConfig(int parameterId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();

            ClsUtility.AddExtendedParameters("@ParameterId", SqlDbType.Int, parameterId);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetParameterResultConfig", ClsUtility.ObjectEnum.DataTable);

          
            var result = (from row in dt.AsEnumerable()
                          select new ParameterResultConfig()
                          {
                              Id = Convert.ToInt32(row["Id"]),
                              ParameterId = Convert.ToInt32(row["ParameterId"]),
                              MinBoundary = row["MinBoundary"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["MinBoundary"]),
                              MaxBoundary = row["MaxBoundary"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["MaxBoundary"]),
                              MinNormalRange = row["MinNormalRange"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["MinNormalRange"]),
                              MaxNormalRange = row["MaxNormalRange"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["MaxNormalRange"]),
                              DetectionLimit = row["DetectionLimit"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["DetectionLimit"]),
                              IsDefault = Convert.ToBoolean(row["DefaultUnit"]),
                              ResultUnit = new ResultUnit() { Id = Convert.ToInt32(row["UnitId"]), Text = row["UnitName"].ToString() },
                              DeleteFlag = false
                          }
                            );
            return result.ToList();
        }

        public List<ParameterResultOption> GetParameterResultOption(int ParameterId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@ParameterId", SqlDbType.Int, ParameterId);
            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetParameterResultOption", ClsUtility.ObjectEnum.DataTable);
            var result = (from row in dt.AsEnumerable()
                          select new ParameterResultOption()
                          {
                              Id = Convert.ToInt32(row["Id"]),
                              ParameterId = Convert.ToInt32(row["ParameterId"]),
                              Text = row["Value"].ToString()
                          }
                            );
            return result.ToList();
        }

        public List<ResultUnit> GetResultUnits()
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetResultUnit", ClsUtility.ObjectEnum.DataTable);
            var result = (from row in dt.AsEnumerable()
                          select new ResultUnit()
                          {
                              Id = Convert.ToInt32(row["UnitId"]),
                              Text = row["Name"].ToString()
                          }
             );
            return result.ToList();
        }

        public LabTest SaveLabTest(LabTest labTest, int userId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();

            ClsUtility.AddExtendedParameters("@ReferenceId", SqlDbType.VarChar, labTest.ReferenceId);
            ClsUtility.AddExtendedParameters("@TestName", SqlDbType.VarChar, labTest.Name);
            ClsUtility.AddExtendedParameters("@IsGroup", SqlDbType.Bit, labTest.IsGroup);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
            if (labTest.Department != null)
            {
                ClsUtility.AddExtendedParameters("@DepartmentId", SqlDbType.Int, labTest.DepartmentId);
                ClsUtility.AddExtendedParameters("@LoincCode" ,SqlDbType.VarChar, labTest.LoincCode);
            }
            try
            {
                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_SaveLabTest", ClsUtility.ObjectEnum.DataTable);
                TestDepartment department = null;
                labTest.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                labTest.ParameterCount = Convert.ToInt32(dt.Rows[0]["ParameterCount"]);
                labTest.Department = dt.Rows[0]["DepartmentId"] == DBNull.Value ? department : new TestDepartment() {Id= Convert.ToInt32(dt.Rows[0]["DepartmentId"]), Name=dt.Rows[0]["Department"].ToString() }; 
                labTest.ReferenceId = dt.Rows[0]["ReferenceId"].ToString();
                labTest.DeleteFlag = Convert.ToBoolean(dt.Rows[0]["DeleteFlag"]);
                labTest.Active = Convert.ToBoolean(dt.Rows[0]["Active"]);
                labTest.Name = dt.Rows[0]["Name"].ToString();
                labTest.TestParameter = null;
                return labTest;
            }
            catch
            {
                throw;
            }
        }

        public TestParameter SaveLabTestParameter(TestParameter testParameter, int userId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            if (testParameter.Id > 0)
            {
                ClsUtility.AddExtendedParameters("@ParameterId", SqlDbType.Int, testParameter.Id);
            }
            ClsUtility.AddExtendedParameters("@LabTestId", SqlDbType.Int, testParameter.LabTestId);
            ClsUtility.AddExtendedParameters("@ReferenceId", SqlDbType.VarChar, testParameter.ReferenceId);
            ClsUtility.AddExtendedParameters("@Name", SqlDbType.VarChar, testParameter.Name);
            ClsUtility.AddExtendedParameters("@DataType", SqlDbType.VarChar, testParameter.DataType);
            ClsUtility.AddExtendedParameters("@Rank", SqlDbType.Decimal, testParameter.Rank);
            ClsUtility.AddExtendedParameters("@LoincCode", SqlDbType.VarChar, testParameter.LoincCode);
            ClsUtility.AddExtendedParameters("@DeleteFlag", SqlDbType.Bit, testParameter.DeleteFlag);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
            if (null != testParameter.ResultConfig && testParameter.ResultConfig.Count > 0)
            {
                XDocument docX = new XDocument(
                new XElement("root", (from config in testParameter.ResultConfig
                                      select new XElement("config",
                                           new XElement("configid", config.Id),
                                           new XElement("parameterid", testParameter.Id),
                                           new XElement("minboundary", config.MinBoundary),
                                           new XElement("maxboundary", config.MaxBoundary),
                                           new XElement("minnormal", config.MinNormalRange),
                                           new XElement("maxnormal", config.MaxNormalRange),
                                           new XElement("unit", config.UnitId),
                                           new XElement("defaultunit", config.IsDefault),
                                           new XElement("limit", config.DetectionLimit),
                                            new XElement("deleteflag", config.DeleteFlag)
                                           )
                )));
                ClsUtility.AddParameters("@ConfigList", SqlDbType.VarChar, docX.ToString());
            }
            if (null != testParameter.ResultOption && testParameter.ResultOption.Count > 0)
            {
                XDocument docY = new XDocument(
                new XElement("root", (from option in testParameter.ResultOption
                                      select new XElement("options",
                                           new XElement("optionid", option.Id),
                                           new XElement("parameterid", testParameter.Id),
                                           new XElement("value", option.Text),
                                           new XElement("deleteflag", option.DeleteFlag)
                                           )
                )));
                ClsUtility.AddParameters("@OptionList", SqlDbType.VarChar, docY.ToString());
            }

            try
            {
                DataSet ds = (DataSet)obj.ReturnObject(ClsUtility.theParams, "Laboratory_SaveTestParameter", ClsUtility.ObjectEnum.DataSet);
                DataTable dt = ds.Tables[0];
                testParameter = new TestParameter()
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    DeleteFlag = Convert.ToBoolean(dt.Rows[0]["DeleteFlag"]),
                    DataType = dt.Rows[0]["DataType"].ToString(),
                    LabTestId = Convert.ToInt32(dt.Rows[0]["LabTestId"]),
                    Rank = Convert.ToDecimal(dt.Rows[0]["OrdRank"]),
                    LoincCode = dt.Rows[0]["LoincCode"].ToString(),
                    Name = dt.Rows[0]["ParameterName"].ToString(),
                    ReferenceId = dt.Rows[0]["ReferenceId"].ToString(),
                    ResultOption = null,
                    ResultConfig = null
                };
                if (ds.Tables[1].Rows.Count > 0)
                {
                    var config = (from row in ds.Tables[1].AsEnumerable()
                                  select new ParameterResultConfig()
                                  {
                                      Id = Convert.ToInt32(row["Id"]),
                                      ParameterId = Convert.ToInt32(row["ParameterId"]),
                                      MinBoundary = row["MinBoundary"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["MinBoundary"]),
                                      MaxBoundary = row["MaxBoundary"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["MaxBoundary"]),
                                      MinNormalRange = row["MinNormalRange"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["MinNormalRange"]),
                                      MaxNormalRange = row["MaxNormalRange"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["MaxNormalRange"]),
                                      DetectionLimit = row["DetectionLimit"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["DetectionLimit"]),
                                      DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                                      IsDefault = Convert.ToBoolean(row["DefaultUnit"]),
                                      ResultUnit = new ResultUnit() { Id = Convert.ToInt32(row["UnitId"]), Text = row["UnitName"].ToString() }
                                  });
                    testParameter.ResultConfig = config.ToList();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    var options = (from row in ds.Tables[2].AsEnumerable()
                                   select new ParameterResultOption()
                                   {
                                       Id = Convert.ToInt32(row["Id"]),
                                       ParameterId = Convert.ToInt32(row["ParameterId"]),
                                       Text = row["value"].ToString(),
                                       DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                                   }
                                      );
                }

                return testParameter;
            }
            catch
            {
                throw;
            }
        }
        public List<TestDepartment> GetTestDepartments()
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetTestDepartment", ClsUtility.ObjectEnum.DataTable);
            var result = (from row in dt.AsEnumerable()
                          select new TestDepartment()
                          {
                              Id = Convert.ToInt32(row["Id"]),
                              Name = row["Name"].ToString(),                              
                              DeleteFlag = Convert.ToBoolean(row["DeleteFlag"])
                          }
                            );
            return result.ToList();
        }
        public List<ParameterResultConfig> SaveParameterConfig(List<ParameterResultConfig> paramConfig)
        {
            throw new NotImplementedException();
        }

        public List<ParameterResultOption> SaveParameterResultOption(List<ParameterResultOption> options)
        {
            throw new NotImplementedException();
        }

        private DataTable GetLabTests(int? LabTestId = null)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            if (LabTestId.HasValue)
                ClsUtility.AddExtendedParameters("@LabTestId", SqlDbType.Int, LabTestId.Value);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetLabTest", ClsUtility.ObjectEnum.DataTable);
            return dt;
        }

        private DataTable GetTestParameters(int LabTestId, int? ParameterId = null)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@LabTestId", SqlDbType.Int, LabTestId);
            if (ParameterId.HasValue)
                ClsUtility.AddExtendedParameters("@ParameterId", SqlDbType.Int, ParameterId.Value);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetLabTestParameters", ClsUtility.ObjectEnum.DataTable);

            return dt;
        }


        public LabTestGroup GetGroupLabTest(int mainTestId)
        {
            LabTest mainLabTest = this.GetLabTestById(mainTestId);
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@MainTestId", SqlDbType.Int, mainTestId);
            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GroupGetLabTest", ClsUtility.ObjectEnum.DataTable);
            LabTestGroup groupLab = new LabTestGroup() { GroupTest = mainLabTest };
            TestDepartment department = null;
            var component = (from row in dt.AsEnumerable()
                          select new LabTest()
                          {
                              Id = Convert.ToInt32(row["Id"]),
                              Name = row["Name"].ToString(),
                              IsGroup = Convert.ToBoolean(row["IsGroup"]),
                              ReferenceId = row["ReferenceId"].ToString(),
                              ParameterCount = Convert.ToInt32(row["ParameterCount"]),
                              Department = row["DepartmentId"] == DBNull.Value ? department : new TestDepartment() { Id = Convert.ToInt32(row["DepartmentId"]), Name = row["Department"].ToString() },
                              DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                              Active = Convert.ToBoolean(row["Active"]),
                              TestParameter = null
                          }
                             ).ToList<LabTest>();
            groupLab.ComponentTest = component;
            obj = null;
            return groupLab;
        }

        public void RemoveTestFromGroup(int labTestId, int mainTestId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@MainTestId", SqlDbType.Int, mainTestId);
            ClsUtility.AddExtendedParameters("@LabTestId", SqlDbType.Int, labTestId);
            obj.ReturnObject(ClsUtility.theParams, "Laboratory_GroupRemoveTest", ClsUtility.ObjectEnum.ExecuteNonQuery);
            obj = null;
        }

        public void SaveGroupLabTest(int labTestId, int mainTestId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@MainTestId", SqlDbType.Int, mainTestId);
            ClsUtility.AddExtendedParameters("@LabTestId", SqlDbType.Int, labTestId);
            obj.ReturnObject(ClsUtility.theParams, "Laboratory_GroupAddTest", ClsUtility.ObjectEnum.ExecuteNonQuery);
            obj = null;
        }
    }
}