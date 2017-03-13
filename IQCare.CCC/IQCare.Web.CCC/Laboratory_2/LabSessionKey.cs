using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQCare.Web.Laboratory
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabSesssionKey"/> class.
        /// </summary>
        public SessionKey()
        {

        }
        public static string LabClient
        {
            get { return "Client_Selected?&"; }
        }
        /// <summary>
        /// The unit configuration
        /// </summary>
        public static string UnitConfig
        {
            get { return "Units_Config_?&"; }
        }
        public static string ResultOptions
        {
            get { return "Units_Options_?&"; }
        }
        /// <summary>
        /// The selected lab
        /// </summary>
        public static string SelectedLab
        {
            get { return "LABTEST_X#$"; }
        }
        public static string SelectedLabTestOrder
        {
            get { return "_=LABTESTORDED_X#$"; }
        }
        /// <summary>
        /// The test parameters
        /// </summary>
        public static string TestParameters
        {
            get { return "TESTPARAM_X#$"; }
        }
        /// <summary>
        /// The result unit
        /// </summary>
        public static string ResultUnit
        {
            get { return "RESULTUNITS_CDSF"; }
        }
        /// <summary>
        /// The selected group
        /// </summary>
        public static string SelectedGroup
        {
            get { return "GROUPLABTEST_X#$"; }
        }
    }
}