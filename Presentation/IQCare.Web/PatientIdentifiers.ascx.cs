using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace IQCare.Web
{
    public partial class PatientIdentifiers : System.Web.UI.UserControl
    {
        int patientId= 0;
        int moduleId=0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region "events"
       
        /// <summary>
        /// Occurs when [save continue button clicked].
        /// </summary>
        [System.ComponentModel.Description("Raised when the Save Continue button are clicked.")]
        public event EventHandler SaveContinueButtonClicked;      
        /// <summary>
        /// Occurs when [pre render complete].
        /// </summary>
        [System.ComponentModel.Description("Raised on Render complete")]
        public event EventHandler PreRenderComplete;
        /// <summary>
        /// Occurs when [need data bind].
        /// </summary>
        [System.ComponentModel.Description("Raised on need to databind")]
        public event EventHandler NeedDataBind;


        void OnNeedDataBind(object sender, EventArgs e)
        {
            if(NeedDataBind != null)
            {
                this.NeedDataBind(sender, e);
            }
        }
        void OnPreRenderComplete(object sender, EventArgs e)
        {
            if (PreRenderComplete != null)
            {
                this.PreRenderComplete(sender, e);
            }
        }
        void OnSaveContinue(object sender, EventArgs e)
        {
            if (SaveContinueButtonClicked != null)
            {
                this.SaveContinueButtonClicked(sender, e);
            }
        }
        #endregion
        /// <summary>
        /// Gets the patient_ identifier.
        /// </summary>
        /// <value>
        /// The patient_ identifier.
        /// </value>
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("Settings")]
        [System.ComponentModel.Description("Patient ID")]
        public int PatientId
        {
            get
            {
                return this.patientId;
            }
        }
        /// <summary>
        /// Gets the module_ identifier.
        /// </summary>
        /// <value>
        /// The module_ identifier.
        /// </value>
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("Settings")]
        [System.ComponentModel.Description("Service Area ID")]
        public int ModuleId
        {
            get
            {
                return this.moduleId;
            }
        }
        /// <summary>
        /// Gets or sets the control properties.
        /// </summary>
        /// <value>
        /// The control properties.
        /// </value>
        public Delegate ControlProperties
        {
            private get;
            set;

        }
        /// <summary>
        /// Gets the data item.
        /// </summary>
        /// <value>
        /// The data item.
        /// </value>
        Hashtable DataItem
        {
            get
            {
                return (Hashtable)ControlProperties.DynamicInvoke();
            }
        }


    }
}