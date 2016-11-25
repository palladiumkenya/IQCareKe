using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.Clinical;
using Interface.Clinical;
namespace BusinessProcess.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DataAccess.Base.ProcessBase" />
    /// <seealso cref="Interface.Clinical.IServiceManager" />
    public class BServicesManager : ProcessBase, IServiceManager
    {
        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int AddService(Service service, int userId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                ClsUtility.AddExtendedParameters("@ModuleId", System.Data.SqlDbType.Int, service.ServiceAreaId);
                ClsUtility.AddExtendedParameters("@Name", System.Data.SqlDbType.VarChar, service.Name);
                ClsUtility.AddExtendedParameters("@Description", System.Data.SqlDbType.VarChar, service.Description);
                ClsUtility.AddExtendedParameters("@UserId", System.Data.SqlDbType.Int, userId);

                int dt = (int)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_Save", ClsUtility.ObjectEnum.ExecuteNonQuery);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the service.
        /// </summary>
        /// <param name="serviceId">The service identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int DeleteService(int serviceId, int userId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                ClsUtility.AddExtendedParameters("@ServiceId", System.Data.SqlDbType.Int, serviceId);
                ClsUtility.AddExtendedParameters("@UserId", System.Data.SqlDbType.Int, userId);

                int dt = (int)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_Delete", ClsUtility.ObjectEnum.ExecuteNonQuery);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the service by identifier.
        /// </summary>
        /// <param name="serviceId">The service identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Service GetServiceById(int serviceId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                ClsUtility.AddExtendedParameters("@ServiceId", System.Data.SqlDbType.Int, serviceId);

                DataRow row = (DataRow)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_GetServices", ClsUtility.ObjectEnum.DataRow);

                Service service = new Service()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                    ServiceArea = row["Module"].ToString(),
                    ServiceAreaId = Convert.ToInt32(row["ModuleId"])
                };
                return service;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Service> GetServices()
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_GetServices", ClsUtility.ObjectEnum.DataTable);
                var services = (
                    from row in dt.AsEnumerable()
                    select new Service()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                        ServiceArea = row["Module"].ToString(),
                        ServiceAreaId = Convert.ToInt32(row["ModuleId"])
                    }).ToList();
                return services;
            }
            catch
            {
                throw;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DataAccess.Base.ProcessBase" />
    /// <seealso cref="Interface.Clinical.IServiceRequest" />
    public class BServiceRequest : ProcessBase, IServiceRequest
    {

        /// <summary>
        /// Deletes the service order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int DeleteServiceOrder(ServiceOrder order, int userId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                ClsUtility.AddExtendedParameters("@OrderId", System.Data.SqlDbType.Int, order.Id);
                ClsUtility.AddExtendedParameters("@UserId", System.Data.SqlDbType.Int, userId);

                int dt = (int)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_DeleteOrder", ClsUtility.ObjectEnum.ExecuteNonQuery);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the service order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceOrder GetServiceOrder(int patientId, int orderId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                ClsUtility.AddExtendedParameters("@PatientId", System.Data.SqlDbType.Int, patientId);

                ClsUtility.AddExtendedParameters("@OrderId", System.Data.SqlDbType.Int, orderId);


                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_GetOrder", ClsUtility.ObjectEnum.DataTable);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                DataRow row = dt.Rows[0];

                ServiceOrder order = new ServiceOrder()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    LocationId = Convert.ToInt32(row["LocationId"]),
                    VisitId = Convert.ToInt32(row["VisitId"]),
                    ModuleId = Convert.ToInt32(row["ModuleId"]),
                    TargetModuleId = Convert.ToInt32(row["TargetModuleId"]),
                    OrderNumber = row["OrderNumber"].ToString(),
                    OrderDate = Convert.ToDateTime(row["OrderDate"]),
                    OrderedBy = Convert.ToInt32(row["OrderedBy"]),
                    OrderStatus = row["OrderStatus"].ToString(),
                    ClinicalNotes = row["ClinicalNotes"].ToString(),
                    UserId = Convert.ToInt32(row["UserId"]),
                    PatientId = Convert.ToInt32(row["PatientId"]),
                    Services = this.GetOrderedServices(Convert.ToInt32(row["Id"]))
                };

                return order;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Saves the order result.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int SaveOrderResult(List<OrderedService> services, int orderId, int userId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                ClsUtility.AddExtendedParameters("@OrderId", System.Data.SqlDbType.Int, orderId);
                ClsUtility.AddExtendedParameters("@UserId", System.Data.SqlDbType.Int, userId);
                XDocument docX = new XDocument(
                new XElement("root", (from service in services
                                      select new XElement("result",
                                           new XElement("id", service.Id),
                                           new XElement("serviceid", service.ServiceId),
                                           new XElement("testnotes", service.RequestNotes),
                                            new XElement("quantity",service.Quantity),
                                           new XElement("resultnotes", service.ResultNotes),
                                           new XElement("resultdate", service.ResultDate == null ? null : service.ResultDate),
                                           new XElement("resultby", service.ResultBy == null ? null : service.ResultBy)
                                       )
                                     )
                                    )
                                   );
                ClsUtility.AddParameters("@ServiceList", SqlDbType.VarChar, docX.ToString());

                int num = (int)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_SaveResult", ClsUtility.ObjectEnum.ExecuteNonQuery);

                return num;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Saves the service order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="LocationId">The location identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceOrder SaveServiceOrder(ServiceOrder order, int userId, int locationId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, order.PatientId);
                ClsUtility.AddExtendedParameters("@LocationId", SqlDbType.Int, locationId);
                ClsUtility.AddExtendedParameters("@ModuleId", SqlDbType.Int, order.ModuleId);
                ClsUtility.AddExtendedParameters("@TargetModuleId", SqlDbType.Int, order.TargetModuleId);
                ClsUtility.AddExtendedParameters("@OrderDate", SqlDbType.DateTime, Convert.ToDateTime(order.OrderDate));
                ClsUtility.AddExtendedParameters("@Orderedby", SqlDbType.Int, order.OrderedBy);
                ClsUtility.AddExtendedParameters("@ClinicalNotes", SqlDbType.VarChar, order.ClinicalNotes);
                ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, userId);

                XDocument docX = new XDocument(
                new XElement("root", (from service in order.Services
                                      select new XElement("result",
                                          new XElement("id", service.Id),
                                          new XElement("serviceid", service.ServiceId),
                                          new XElement("servicename", service.ServiceName),
                                          new XElement("testnotes", service.RequestNotes),
                                          new XElement("quantity",service.Quantity),
                                          new XElement("resultnotes", service.ResultNotes),
                                          new XElement("resultdate", service.ResultDate == null ? null : service.ResultDate),
                                          new XElement("resultby", service.ResultBy == null ? null : service.ResultBy)
                                      )
                                     )
                                    )
                                   );
                ClsUtility.AddParameters("@ServiceList", SqlDbType.VarChar, docX.ToString());

                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_SaveOrder", ClsUtility.ObjectEnum.DataTable);

                int orderId = Convert.ToInt32(dt.Rows[0]["OrderId"]);
                int patientId = Convert.ToInt32(dt.Rows[0]["PatientId"]);
                order = this.GetServiceOrder(patientId, orderId);
                return order;

            }
            catch
            {
                throw;
            }
        }

        public List<ServiceOrder> GetServiceOrders(int? patientId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                if (patientId.HasValue)
                {
                    ClsUtility.AddExtendedParameters("@PatientId", System.Data.SqlDbType.Int, patientId.Value);
                }

                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_GetOrder", ClsUtility.ObjectEnum.DataTable);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                var orders = (from row in dt.AsEnumerable()
                              select new ServiceOrder()
                              {
                                  Id = Convert.ToInt32(row["Id"]),
                                  LocationId = Convert.ToInt32(row["LocationId"]),
                                  VisitId = Convert.ToInt32(row["VisitId"]),
                                  ModuleId = Convert.ToInt32(row["ModuleId"]),
                                  TargetModuleId = Convert.ToInt32(row["TargetModuleId"]),
                                  OrderNumber = row["OrderNumber"].ToString(),
                                  OrderDate = Convert.ToDateTime(row["OrderDate"]),
                                  OrderedBy = Convert.ToInt32(row["OrderedBy"]),
                                  OrderStatus = row["OrderStatus"].ToString(),
                                  ClinicalNotes = row["ClinicalNotes"].ToString(),
                                  UserId = Convert.ToInt32(row["UserId"]),
                                  PatientId = Convert.ToInt32(row["PatientId"]),
                                  Services = this.GetOrderedServices(Convert.ToInt32(row["Id"]))
                              }).ToList();
                return orders;
            }
            catch
            {
                throw;
            }
        }

        public List<OrderedService> GetOrderedServices(int orderId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {

                ClsUtility.AddExtendedParameters("@OrderId", System.Data.SqlDbType.Int, orderId);
                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_GetOrderedServices", ClsUtility.ObjectEnum.DataTable);
                int? nullInt = null;
                DateTime? nullDate = null;
                var services = (from row in dt.AsEnumerable()
                                select new OrderedService()
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    OrderId = Convert.ToInt32(row["OrderId"]),
                                    RequestNotes = row["RequestNotes"].ToString(),
                                    Quantity = Convert.ToInt32(row["Quantity"]),
                                    ResultBy = row["ResultBy"] == DBNull.Value ? nullInt : Convert.ToInt32(row["ResultBy"]),
                                    ResultDate = row["ResultDate"] == DBNull.Value ? nullDate : Convert.ToDateTime(row["ResultDate"]),
                                    ResultNotes = row["ResultNotes"].ToString(),
                                    Service = new Service()
                                    {
                                        Id = Convert.ToInt32(row["ClinicalServiceId"]),
                                        Name = row["ServiceName"].ToString(),
                                        Description = row["ServiceDescription"].ToString(),
                                        DeleteFlag = Convert.ToBoolean(row["ServiceDeleteFlag"]),
                                        ServiceArea = row["ServiceModuleName"].ToString(),
                                        ServiceAreaId = Convert.ToInt32(row["ServiceModuleId"])

                                    }

                                }).ToList();
                return services;
            }
            catch
            {
                throw;
            }
        }

        public List<Service> FindServiceByName(string SearchText, int serviceAreaId)
        {
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            try
            {
                ClsUtility.AddExtendedParameters("@SearchText", System.Data.SqlDbType.VarChar, SearchText);
                ClsUtility.AddExtendedParameters("@ModuleId", System.Data.SqlDbType.Int, serviceAreaId);
                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "ClinicalService_FindServiceByName", ClsUtility.ObjectEnum.DataTable);
                var services = (
                    from row in dt.AsEnumerable()
                    select new Service()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                        ServiceArea = row["Module"].ToString(),
                        ServiceAreaId = Convert.ToInt32(row["ModuleId"])
                    }).ToList();
                return services;
            }
            catch
            {
                throw;
            }
        }
    }
}
