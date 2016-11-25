using System;
using System.Collections;
using System.Data;

namespace Interface.FormBuilder
{
    /// <summary>
    /// 
    /// </summary>
     public interface IFormModuleLink
    {
        /// <summary>
        /// Gets the form module link detail.
        /// </summary>
        /// <param name="moduleID">The module identifier.</param>
        /// <returns></returns>
         DataSet GetFormModuleLinkDetail(Int32 moduleId);
         /// <summary>
         /// Saves the update form module link detail.
         /// </summary>
         /// <param name="intModuleID">The int module identifier.</param>
         /// <param name="list">The list.</param>
         /// <param name="userId">The user identifier.</param>
         void SaveUpdateFormModuleLinkDetail(int moduleId, ArrayList list,int userId);
         /// <summary>
         /// Forms the module linking.
         /// </summary>
         /// <param name="ModuleId">The module identifier.</param>
         /// <param name="CountryID">The country identifier.</param>
         /// <returns></returns>
         DataSet FormModuleLinking(int moduleId, int countryId);
    }
}
