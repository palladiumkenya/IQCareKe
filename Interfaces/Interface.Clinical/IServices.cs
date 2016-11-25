using System.Collections.Generic;
using Entities.Clinical;

namespace Interface.Clinical
{
    /// <summary>
    /// 
    /// </summary>
   public interface IServiceManager
    {
        /// <summary>
        /// Deletes the service.
        /// </summary>
        /// <param name="serviceId">The service identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
       int DeleteService(int serviceId, int userId);
       /// <summary>
       /// Gets the service by identifier.
       /// </summary>
       /// <param name="serviceId">The service identifier.</param>
       /// <returns></returns>
       Service GetServiceById(int serviceId);
       /// <summary>
       /// Gets the services.
       /// </summary>
       /// <returns></returns>
       List<Service> GetServices();
       /// <summary>
       /// Adds the service.
       /// </summary>
       /// <param name="service">The service.</param>
       /// <param name="userId">The user identifier.</param>
       /// <returns></returns>
       int AddService(Service service, int userId);
    }
   /// <summary>
   /// 
   /// </summary>
   public interface IServiceRequest
   {
       /// <summary>
       /// Gets the service order.
       /// </summary>
       /// <param name="patientId">The patient identifier.</param>
       /// <param name="orderId">The order identifier.</param>
       /// <returns></returns>
       ServiceOrder GetServiceOrder(int patientId, int orderId);
       /// <summary>
       /// Gets the ordered services.
       /// </summary>
       /// <param name="orderId">The order identifier.</param>
       /// <returns></returns>
       List<OrderedService> GetOrderedServices(int orderId);
       /// <summary>
       /// Gets the service orders.
       /// </summary>
       /// <param name="patientId">The patient identifier.</param>
       /// <returns></returns>
       List<ServiceOrder> GetServiceOrders(int? patientId);
       /// <summary>
       /// Finds the name of the service by.
       /// </summary>
       /// <param name="SearchText">The search text.</param>
       /// <param name="serviceAreaId">The service area identifier.</param>
       /// <returns></returns>
       List<Service> FindServiceByName(string SearchText, int serviceAreaId);
       /// <summary>
       /// Saves the service order.
       /// </summary>
       /// <param name="order">The order.</param>
       /// <param name="userId">The user identifier.</param>
       /// <param name="LocationId">The location identifier.</param>
       /// <returns></returns>
       ServiceOrder SaveServiceOrder(ServiceOrder order, int userId, int LocationId);
       /// <summary>
       /// Deletes the service order.
       /// </summary>
       /// <param name="order">The order.</param>
       /// <param name="userId">The user identifier.</param>
       /// <returns></returns>
       int DeleteServiceOrder(ServiceOrder order, int userId);
       /// <summary>
       /// Saves the order result.
       /// </summary>
       /// <param name="services">The services.</param>
       /// <param name="orderId">The order identifier.</param>
       /// <param name="userId">The user identifier.</param>
       /// <returns></returns>
       int SaveOrderResult(List<OrderedService> services, int orderId, int userId);
   }
}
