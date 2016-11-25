using System.Configuration;

namespace IQCare.CustomConfig
{
    public class ReportConfigSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the instances.
        /// </summary>
        /// <value>
        /// The instances.
        /// </value>
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public ReportInstanceCollection Instances
        {
            get 
            { 
                return (ReportInstanceCollection)this[""]; 
            }
            set 
            { 
                this[""] = value; 
            }
        }

    }
    
}