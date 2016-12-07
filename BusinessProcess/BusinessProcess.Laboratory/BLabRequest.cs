using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Context;
using DataAccess.Entity;
using Entities.Lab;
using Interface.Laboratory;
using Interface.PatientCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace BusinessProcess.Laboratory
{
    public class BLabRequest : ProcessBase, ILabRequest, ILabRepo
    {
        public DataTable FindLabByName(string SearchText, bool excludeGroup = false)
        {
            lock (this)
            {
                ClsObject theON = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SearchText", SqlDbType.VarChar, SearchText);
                ClsUtility.AddExtendedParameters("@ExcludeGroup", SqlDbType.Bit, excludeGroup);
                return (DataTable)theON.ReturnObject(ClsUtility.theParams, "Laboratory_FindTestByName", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public List<LabOrder> GetLabOrders(int locationId, int? patientId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            if (patientId.HasValue)
            {
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, patientId.Value);
            }
            ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
            DateTime? nullDate = null;
            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetLabOrder", ClsUtility.ObjectEnum.DataTable);
            ClsUtility.Init_Hashtable();
            obj = null;
            IPatientService service = new BusinessProcess.PatientCore.PatientCoreServices();
            // (IPatientService)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
            var result = (from rowView in dt.AsEnumerable()
                          select new LabOrder()
                          {

                              Id = Convert.ToInt32(rowView["LabOrderId"]),
                              ClinicalNotes = rowView["ClinicalNotes"].ToString(),
                              CreateDate = Convert.ToDateTime(rowView["CreateDate"]),
                              DeleteFlag = Convert.ToBoolean(rowView["DeleteFlag"]),
                              LocationId = Convert.ToInt32(rowView["LocationId"]),
                              ModuleId = Convert.ToInt32(rowView["ModuleId"]),
                              OrderDate = Convert.ToDateTime(rowView["OrderDate"]),
                              OrderedBy = Convert.ToInt32(rowView["OrderedBy"]),
                              OrderNumber = rowView["OrderNumber"].ToString(),
                              PreClinicDate = rowView["PreClinicLabDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(rowView["PreClinicLabDate"]),
                              Client = service.GetPatient(Convert.ToInt32(rowView["PatientId"])),
                              PatientId = Convert.ToInt32(rowView["PatientId"]),
                              UserId = Convert.ToInt32(rowView["UserId"]),
                              VisitId = Convert.ToInt32(rowView["VisitId"]),
                              OrderStatus = rowView["OrderStatus"].ToString(),
                              OrderedTest = this.GetOrderedTests(Convert.ToInt32(rowView["LabOrderId"]))

                          }).ToList<LabOrder>();
            return result;
        }
        public List<LabOrder> GetLabOrders(int locationId, string orderStatus, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            if (dateFrom.HasValue)
            {
                ClsUtility.AddExtendedParameters("@DateFrom", System.Data.SqlDbType.DateTime, dateFrom.Value);
            }
            if (dateTo.HasValue)
            {
                ClsUtility.AddExtendedParameters("@DateTo", SqlDbType.DateTime, dateTo.Value);
            }
            ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
            if (!string.IsNullOrEmpty(orderStatus))
            {
                ClsUtility.AddParameters("@OrderStatus", SqlDbType.VarChar, orderStatus);
            }
            DateTime? nullDate = null;
            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetLabOrder", ClsUtility.ObjectEnum.DataTable);
            ClsUtility.Init_Hashtable();
            IPatientService service = new BusinessProcess.PatientCore.PatientCoreServices();
            obj = null;
            var result = (from rowView in dt.AsEnumerable()
                          select new LabOrder()
                          {

                              Id = Convert.ToInt32(rowView["LabOrderId"]),
                              ClinicalNotes = rowView["ClinicalNotes"].ToString(),
                              CreateDate = Convert.ToDateTime(rowView["CreateDate"]),
                              DeleteFlag = Convert.ToBoolean(rowView["DeleteFlag"]),
                              LocationId = Convert.ToInt32(rowView["LocationId"]),
                              ModuleId = Convert.ToInt32(rowView["ModuleId"]),
                              OrderDate = Convert.ToDateTime(rowView["OrderDate"]),
                              OrderedBy = Convert.ToInt32(rowView["OrderedBy"]),
                              OrderNumber = rowView["OrderNumber"].ToString(),
                              PreClinicDate = rowView["PreClinicLabDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(rowView["PreClinicLabDate"]),
                              PatientId = Convert.ToInt32(rowView["PatientId"]),
                              Client = service.GetPatient(Convert.ToInt32(rowView["PatientId"])),
                              UserId = Convert.ToInt32(rowView["UserId"]),
                              VisitId = Convert.ToInt32(rowView["VisitId"]),
                              OrderStatus = rowView["OrderStatus"].ToString(),
                              OrderedTest = this.GetOrderedTests(Convert.ToInt32(rowView["LabOrderId"]))

                          }).ToList();
            return result;
        }
        public List<LabOrderTest> GetOrderedTests(int orderId, int? labOrderTestId = null)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@LabOrderId", SqlDbType.Int, orderId);
            if (labOrderTestId.HasValue)
            {
                ClsUtility.AddExtendedParameters("@LabOrderTestId", SqlDbType.Int, labOrderTestId.Value);
            }

            int? nullint = null;
            DateTime? nullDate = null;
            TestDepartment nullDepartment = null;
            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetLabOrderTests", ClsUtility.ObjectEnum.DataTable);
            obj = null;
            ClsUtility.Init_Hashtable();
            var result = (from row in dt.AsEnumerable()
                          select new LabOrderTest
                          {
                              Id = row.Field<int>("TestOrderId"),
                              OrderDate = Convert.ToDateTime(row["OrderDate"]),
                              OrderNumber = row["OrderNumber"].ToString(),
                              OrderedBy = row.Field<int>("OrderedBy"),
                              ServiceAreaId = row.Field<int>("ModuleId"),
                              DeleteFlag = row.Field<bool>("DeleteFlag"),
                              LabOrderId = row.Field<int>("LabOrderId"),
                              ParentLabTestId = row["ParentTestId"] == DBNull.Value ? nullint : row.Field<int>("ParentTestId"),
                              Test = new LabTest()
                              {
                                  Id = row.Field<int>("TestId"),
                                  DeleteFlag = row.Field<bool>("TestDeleteFlag"),
                                  Name = row["TestName"].ToString(),
                                  ReferenceId = row["ReferenceId"].ToString(),
                                  Department = row["DepartmentId"] != DBNull.Value ? new TestDepartment() { Id = Convert.ToInt32(row["DepartmentId"]), Name = row["Department"].ToString() } : nullDepartment,
                                  IsGroup = row.Field<bool>("IsGroup")
                              },
                              TestNotes = row["TestNotes"].ToString(),
                              ResultBy = row["ResultBy"] == DBNull.Value ? nullint : Convert.ToInt32(row["ResultBy"]),
                              ResultDate = row["ResultDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(row["ResultDate"]),
                              ResultNotes = row["ResultNotes"].ToString(),
                              ParameterResults = GetLabTestParameterResult(row.Field<int>("TestOrderId"))
                          }
                      ).ToList();

            return result;
        }

        public LabOrder GetLabOrder(int locationId, int LabOrderId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            PatientCore.PatientCoreServices pt = new PatientCore.PatientCoreServices();
            ClsUtility.AddExtendedParameters("@LabOrderId", SqlDbType.Int, LabOrderId);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetLabOrder", ClsUtility.ObjectEnum.DataTable);

            DateTime? nullDate = null;
            LabOrder order = null;
            if (null != dt && dt.Rows.Count > 0)
            {
                DataRow rowView = dt.Rows[0];


                order = new LabOrder()
                {
                    Id = Convert.ToInt32(rowView["LabOrderId"]),
                    ClinicalNotes = rowView["ClinicalNotes"].ToString(),
                    CreateDate = Convert.ToDateTime(rowView["CreateDate"]),
                    DeleteFlag = Convert.ToBoolean(rowView["DeleteFlag"]),
                    LocationId = Convert.ToInt32(rowView["LocationId"]),
                    ModuleId = Convert.ToInt32(rowView["ModuleId"]),
                    OrderDate = Convert.ToDateTime(rowView["OrderDate"]),
                    OrderedBy = Convert.ToInt32(rowView["OrderedBy"]),
                    OrderNumber = rowView["OrderNumber"].ToString(),
                    PreClinicDate = rowView["PreClinicLabDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(rowView["PreClinicLabDate"]),

                    PatientId = Convert.ToInt32(rowView["PatientId"]),
                    Client = pt.GetPatient(Convert.ToInt32(rowView["PatientId"])),
                    UserId = Convert.ToInt32(rowView["UserId"]),
                    VisitId = Convert.ToInt32(rowView["VisitId"]),
                    OrderStatus = rowView["OrderStatus"].ToString(),
                    OrderedTest = this.GetOrderedTests(Convert.ToInt32(rowView["LabOrderId"]))
                };

            }
            return order;
        }
        decimal? nullDecimal = null;
        public List<LabTestParameterResult> GetLabTestParameterResult(int LabTestOrderId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();

            ClsUtility.AddExtendedParameters("@LabTestOrderId", SqlDbType.Int, LabTestOrderId);
            try
            {
                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_GetTestParameterResult", ClsUtility.ObjectEnum.DataTable);
                
                bool? nullBool = null;
                int? nullInt = null;
                // DateTime? nullDate = null;
                var result = (from row in dt.AsEnumerable()
                              select new LabTestParameterResult()
                              {
                                  Id = row.Field<int>("Id"),
                                  Parameter = new TestParameter()
                                  {
                                      Id = row.Field<int>("ParameterId"),
                                      Name = row["ParameterName"].ToString(),
                                      ReferenceId = row["ReferenceId"].ToString(),
                                      DataType = row["DataType"].ToString(),
                                      Rank = Convert.ToDecimal(row["OrdRank"]),
                                      LabTestId = row.Field<int>("LabTestId"),
                                      DeleteFlag = Convert.ToBoolean(row["ParamDeleteFlag"]),
                                      ResultConfig = null,
                                      ResultOption = null

                                  },
                                  LabOrderTestId = row.Field<int>("LabOrderTestId"),
                                  LabOrderId = row.Field<int>("LabOrderId"),
                                  UserId = row.Field<int>("UserId"),
                                  DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                                  CreateDate = Convert.ToDateTime(row["CreateDate"]),
                                  Undetectable = row["Undetectable"] == DBNull.Value ? nullBool : Convert.ToBoolean(row["Undetectable"]),
                                  DetectionLimit = row["DetectionLimit"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["DetectionLimit"]),
                                  ResultUnit = row["ResultUnitId"] == DBNull.Value ? null : (new ResultUnit()
                                  {
                                      Id = row.Field<int>("ResultUnitId"),
                                      Text = row["ResultUnit"].ToString()
                                  }),
                                  ResultValue = row["ResultValue"] == DBNull.Value ? nullDecimal : Convert.ToDecimal(row["ResultValue"]),
                                  ResultText = row["ResultText"].ToString(),
                                  ResultOption = row["ResultOption"].ToString(),
                                  ResultOptionId = row["ResultOptionId"] == DBNull.Value ? nullInt : Convert.ToInt32(row["ResultOptionId"])

                              }
                 ).ToList();

                return result;
            }
            catch
            {
                throw;
            }
        }



        public LabOrder SaveLabOrder(LabOrder labOrder, int UserId, int LocationId)
        {
            // this.Connection = DataMgr.GetConnection();
            // this.Transaction = DataMgr.BeginTransaction(this.Connection);

            ClsObject obj = new ClsObject();
            //  obj.Connection = this.Connection;
            // obj.Transaction = this.Transaction;

            ClsUtility.Init_Hashtable();
            //  Decimal? nullDecimal = null;
            if (labOrder.Id > -1)
                ClsUtility.AddExtendedParameters("@LabOrderId", SqlDbType.Int, labOrder.Id);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, UserId);
            ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, labOrder.PatientId);
            ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, LocationId);
            ClsUtility.AddExtendedParameters("@OrderedBy", SqlDbType.Int, labOrder.OrderedBy);
            ClsUtility.AddExtendedParameters("@OrderDate", SqlDbType.DateTime, labOrder.OrderDate);
            ClsUtility.AddParameters("@ClinicalNotes", SqlDbType.VarChar, labOrder.ClinicalNotes);
            ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, labOrder.ModuleId);
            if (labOrder.PreClinicDate.HasValue)
            {
                ClsUtility.AddExtendedParameters("@PreClinicDate", SqlDbType.DateTime, labOrder.PreClinicDate);
            }
            XDocument docX = new XDocument(
                new XElement("root", (from test in labOrder.OrderedTest
                                      select new XElement("request",
                                           new XElement("testid", test.TestId),
                                           new XElement("testnotes", test.TestNotes),
                                           new XElement("isgroup", test.Test.IsGroup)

                ))));
            ClsUtility.AddParameters("@itemList", SqlDbType.VarChar, docX.ToString());

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Laboratory_SaveLabOrder", ClsUtility.ObjectEnum.DataTable);
            ClsUtility.Init_Hashtable();
            obj = null;

            labOrder.Id = Convert.ToInt32(dt.Rows[0]["LabOrderId"]);
            labOrder.OrderNumber = dt.Rows[0]["OrderNumber"].ToString();
            labOrder.VisitId = Convert.ToInt32(dt.Rows[0]["VisitId"]);
            labOrder.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
            labOrder.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]); ;
            labOrder.LocationId = Convert.ToInt32(dt.Rows[0]["LocationId"]);

            List<LabOrderTest> orderedTests = GetOrderedTests(labOrder.Id);

            foreach (LabOrderTest testSaved in orderedTests)
            {

                LabOrderTest submittedTest = labOrder.OrderedTest.Where(t => t.TestId == testSaved.TestId).FirstOrDefault();

                if (null != submittedTest && null != submittedTest.ParameterResults && submittedTest.ResultBy.HasValue && submittedTest.ResultDate.HasValue
                    && submittedTest.ParameterResults.Count > 0 && submittedTest.ParameterResults.Count(p => p.HasResult) > 0)
                {
                    List<LabTestParameterResult> results = testSaved.ParameterResults;

                    foreach (LabTestParameterResult result in results)
                    {
                        LabTestParameterResult submittedParam = submittedTest.ParameterResults.Where(p => p.ParameterId == result.ParameterId && p.HasResult).DefaultIfEmpty(null).FirstOrDefault();
                        if (null != submittedParam)
                        {
                            result.ResultOption = submittedParam.ResultOption;
                            result.ResultOptionId = submittedParam.ResultOptionId;
                            result.ResultText = submittedParam.ResultText;
                            result.ResultValue = submittedParam.ResultValue;
                            result.ResultUnit = submittedParam.ResultUnit;
                            result.Config = submittedParam.Config;
                            result.DetectionLimit = submittedParam.DetectionLimit;
                            result.Undetectable = submittedParam.Undetectable;

                        }

                    }

                    this.SaveLabResults(results, testSaved.Id, submittedTest.ResultNotes, UserId, submittedTest.ResultBy.Value, submittedTest.ResultDate.Value);
                }
            }


            return labOrder;
        }

        public int SaveLabResults(
            List<LabTestParameterResult> results,
            int LabTestOrderId,
            string ResultNotes,
            int userId,
            int ResultBy,
            DateTime ResultDate)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);
            ClsUtility.AddExtendedParameters("@LabTestOrderId", SqlDbType.Int, LabTestOrderId);
            ClsUtility.AddParameters("@ResultNotes", SqlDbType.VarChar, ResultNotes);
            ClsUtility.AddExtendedParameters("@ResultBy", SqlDbType.Int, ResultBy);
            ClsUtility.AddExtendedParameters("@ResultDate", SqlDbType.DateTime, ResultDate);
         
            XDocument docX = new XDocument(
                new XElement("root", (from result in results
                                      select new XElement("result",
                                           new XElement("id", result.Id),
                                           new XElement("parameterid", result.Parameter.Id),
                                           new XElement("resultvalue", result.ResultValue == null ? nullDecimal : result.ResultValue),
                                           new XElement("resulttext", result.ResultText),
                                           new XElement("resultoption", result.ResultOption),
                                           new XElement("resultoptionvalue", result.ResultOptionId),
                                           new XElement("resultdate", ResultDate),
                                           new XElement("resultby", ResultBy),
                                           new XElement("resultunit", result.ResultUnit == null ? null : result.ResultUnitName),
                                           new XElement("resultunitid", result.ResultUnit == null ? null : result.ResultUnitId.Value.ToString()),
                                           new XElement("undetectable", result.Undetectable),
                                           new XElement("detectionlimit", result.DetectionLimit == null ? nullDecimal : result.DetectionLimit),
                                           new XElement("configid", result.ConfigId)
                                           )
                )));
            ClsUtility.AddParameters("@ParameterList", SqlDbType.VarChar, docX.ToString());
            int rowCount = (int)obj.ReturnObject(ClsUtility.theParams, "Laboratory_SaveTestResult", ClsUtility.ObjectEnum.ExecuteNonQuery);
            return rowCount;
        }



        public List<LabOrder> GetAll(Entities.Common.IFilter orderFilters)
        {
            LabOrderRepository repo = new LabOrderRepository();
            if (orderFilters != null)
                return repo.GetAllFilterd(orderFilters);
            else
            {
                return repo.GetAll().ToList();
            }
        }

        public void DeleteLabOrder(int labOrderId, int userId, string deleteReason)
        {
            LabOrderRepository repo = new LabOrderRepository();
            LabOrder order = repo.Find(labOrderId);
            order.DeletedBy = userId;
            order.DeleteReason = deleteReason;           
            repo.Delete(order);
        }
    }
}
