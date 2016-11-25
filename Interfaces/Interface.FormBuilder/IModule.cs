using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Entities.Administration;

namespace Interface.FormBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Gets the module detail.
        /// </summary>
        /// <returns></returns>
        DataSet GetModuleDetail();
        /// <summary>
        /// Gets the module identifier.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        DataSet GetModuleIdentifier(Int32 ModuleId);
       // int SaveUpdateModuleDetail(Hashtable ht, DataTable moduleDetail);
        /// <summary>
        /// Saves the update module detail.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <param name="moduleDetail">The module detail.</param>
        /// <param name="businessRule">The business rule.</param>
        /// <returns></returns>
        int SaveUpdateModuleDetail(Hashtable ht, DataTable moduleDetail, DataTable businessRule);
        /// <summary>
        /// Statuses the update.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <returns></returns>
        int StatusUpdate(Hashtable ht);
        /// <summary>
        /// Deletes the module.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        void DeleteModule(int ModuleId);

        List<ServiceRule> GetBusinessRule(int? moduleId= null);
        

    }
}
