using System;
using Entities.Common;

namespace Entities.FormBuilder
{
    [Serializable]
    public class FormRule
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get;
            set;
        }
        public FormObject Form { get; set; }
        public int FormId { get; set; }
        /// <summary>
        /// Gets or sets the business rule.
        /// </summary>
        /// <value>
        /// The business rule.
        /// </value>
        public BusinessRule BusinessRule { get; set; }
        /// <summary>
        /// Gets the rule identifier.
        /// </summary>
        /// <value>
        /// The rule identifier.
        /// </value>
        public int RuleId { get { return BusinessRule.RuleId; } }
        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        /// <value>
        /// The name of the rule.
        /// </value>
        public string RuleName { get { return BusinessRule.RuleName; } }
        /// <summary>
        /// Gets the rule reference identifier.
        /// </summary>
        /// <value>
        /// The rule reference identifier.
        /// </value>
        public string RuleReferenceId { get { return BusinessRule.ReferenceId; } }
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public string MaxValue
        {
            get;
            set;
        }
        public int RuleSet { get; set; }
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public string MinValue
        {
            get;
            set;
        }
        
    }
}
