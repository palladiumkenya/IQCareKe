using System;
using Entities.Common;

namespace Entities.FormBuilder
{
    public interface IFormField:IBaseObject
    {
        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        /// <value>
        /// The type of the field.
        /// </value>
          FieldControlType FieldType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [care end].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [care end]; otherwise, <c>false</c>.
        /// </value>
         bool CareEnd { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FormField"/> is registration.
        /// </summary>
        /// <value>
        ///   <c>true</c> if registration; otherwise, <c>false</c>.
        /// </value>
         bool Registration { get; set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="FormField"/> is predefined.
        /// </summary>
        /// <value>
        ///   <c>true</c> if predefined; otherwise, <c>false</c>.
        /// </value>
          bool Predefined { get; }
        /// <summary>
        /// Gets or sets the bind table.
        /// </summary>
        /// <value>
        /// The bind table.
        /// </value>
         string BindTable { get; set; }
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
         int? CategoryId { get; set; }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
          string ToString();
        
    }

    /// <summary>
    /// 
    /// </summary>
     [Serializable]
    public class FormField
    {
         public int Id { get; set; }
         /// <summary>
        /// Gets or sets the feature identifier.
        /// </summary>
        /// <value>
        /// The feature identifier.
        /// </value>
         public int FeatureId { get; set; }
         /// <summary>
         /// Gets or sets the field identifier.
         /// </summary>
         /// <value>
         /// The field identifier.
         /// </value>
         public int FieldId { get; set; }
         /// <summary>
         /// Gets or sets the form.
         /// </summary>
         /// <value>
         /// The form.
         /// </value>
         public virtual FormObject Form { get; set; }
         /// <summary>
         /// Gets or sets the field.
         /// </summary>
         /// <value>
         /// The field.
         /// </value>
         public IFormField Field { get; set; }
         /// <summary>
         /// Gets or sets the section identifier.
         /// </summary>
         /// <value>
         /// The section identifier.
         /// </value>
         public int SectionId { get; set; }
         /// <summary>
         /// Gets or sets the section.
         /// </summary>
         /// <value>
         /// The section.
         /// </value>
         public virtual FormSection Section { get; set; }
         /// <summary>
         /// Gets or sets the field label.
         /// </summary>
         /// <value>
         /// The field label.
         /// </value>
         public string FieldLabel { get; set; }
         /// <summary>
         /// Gets or sets the rank.
         /// </summary>
         /// <value>
         /// The rank.
         /// </value>
         public double Rank { get; set; }
         public string PersistField
         {
             get;
             set;
         }

         /// <summary>
         /// Gets or sets the persist table.
         /// </summary>
         /// <value>
         /// The persist table.
         /// </value>
         public string PersistTable { get; set; }
    }
    //public abstract class FormField : BaseObject, IFormField
    //{
        
         
    //     /// <summary>
    //     /// Gets or sets the type of the field.
    //     /// </summary>
    //     /// <value>
    //     /// The type of the field.
    //     /// </value>
    //     public virtual FieldControlType FieldType { get; set; }    
        
    //     /// <summary>
    //     /// Gets or sets a value indicating whether [care end].
    //     /// </summary>
    //     /// <value>
    //     ///   <c>true</c> if [care end]; otherwise, <c>false</c>.
    //     /// </value>
    //     public bool CareEnd { get; set; }
    //     /// <summary>
    //     /// Gets or sets a value indicating whether this <see cref="FormField"/> is registration.
    //     /// </summary>
    //     /// <value>
    //     ///   <c>true</c> if registration; otherwise, <c>false</c>.
    //     /// </value>
    //     public bool Registration { get; set; }
    //     /// <summary>
    //     /// Gets a value indicating whether this <see cref="FormField"/> is predefined.
    //     /// </summary>
    //     /// <value>
    //     ///   <c>true</c> if predefined; otherwise, <c>false</c>.
    //     /// </value>
    //     public abstract bool Predefined { get;   }
    //     /// <summary>
    //     /// Gets or sets the bind table.
    //     /// </summary>
    //     /// <value>
    //     /// The bind table.
    //     /// </value>
    //     public string BindTable { get; set; }
    //     /// <summary>
    //     /// Gets or sets the category identifier.
    //     /// </summary>
    //     /// <value>
    //     /// The category identifier.
    //     /// </value>
    //     public int CategoryId { get; set; }
    //     /// <summary>
    //     /// Returns a <see cref="System.String" /> that represents this instance.
    //     /// </summary>
    //     /// <returns>
    //     /// A <see cref="System.String" /> that represents this instance.
    //     /// </returns>
    //     public  override string ToString()
    //     {
    //         return this.Name;
    //     }
    //}
     /// <summary>
     /// 
     /// </summary>
     /// <seealso cref="Entities.FormBuilder.FormField" />
    [Serializable]
     public class CustomFormField: IFormField
     {

         /// <summary>
         /// Gets or sets the type of the field.
         /// </summary>
         /// <value>
         /// The type of the field.
         /// </value>
         public virtual FieldControlType FieldType { get; set; }
        
         /// <summary>
         /// Gets or sets a value indicating whether [care end].
         /// </summary>
         /// <value>
         ///   <c>true</c> if [care end]; otherwise, <c>false</c>.
         /// </value>
         public bool CareEnd { get; set; }
         /// <summary>
         /// Gets or sets a value indicating whether this <see cref="FormField"/> is registration.
         /// </summary>
         /// <value>
         ///   <c>true</c> if registration; otherwise, <c>false</c>.
         /// </value>
         public bool Registration { get; set; }
         /// <summary>
         /// Gets a value indicating whether this <see cref="FormField"/> is predefined.
         /// </summary>
         /// <value>
         ///   <c>true</c> if predefined; otherwise, <c>false</c>.
         /// </value>
         public bool Predefined { get { return false; } }
         /// <summary>
         /// Gets or sets the bind table.
         /// </summary>
         /// <value>
         /// The bind table.
         /// </value>
         public string BindTable { get; set; }
         /// <summary>
         /// Gets or sets the category identifier.
         /// </summary>
         /// <value>
         /// The category identifier.
         /// </value>
         public int? CategoryId { get; set; }
         /// <summary>
         /// Returns a <see cref="System.String" /> that represents this instance.
         /// </summary>
         /// <returns>
         /// A <see cref="System.String" /> that represents this instance.
         /// </returns>
         public override string ToString()
         {
             return this.Name;
         }

         public int Id
         {
             get;
             set;
         }

         public string Name
         {
             get;
             set;
         }

         public bool DeleteFlag
         {
             get;
             set;
         }

         public bool Active
         {
             get;
             set;
         }

         public string Description
         {
             get;
             set;
         }

         public DateTime CreateDate
         {
             get;
             set;
         }

        public int? DeletedBy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime? DeleteDate
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int CreatedBy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
     //public class CustomField : FormField
     //{

     //    /// <summary>
     //    /// Gets a value indicating whether this <see cref="CustomField"/> is predefined.
     //    /// </summary>
     //    /// <value>
     //    ///   <c>true</c> if predefined; otherwise, <c>false</c>.
     //    /// </value>
     //    public override bool Predefined
     //    {
     //        get
     //        {
     //            return false;
     //        }
         
     //    }
     //}
    [Serializable]
    public class PredefinedFormField :  IFormField
     {
         /// <summary>
         /// Gets or sets the type of the field.
         /// </summary>
         /// <value>
         /// The type of the field.
         /// </value>
         public virtual FieldControlType FieldType { get; set; }

         /// <summary>
         /// Gets or sets a value indicating whether [care end].
         /// </summary>
         /// <value>
         ///   <c>true</c> if [care end]; otherwise, <c>false</c>.
         /// </value>
         public bool CareEnd { get; set; }
         /// <summary>
         /// Gets or sets a value indicating whether this <see cref="FormField"/> is registration.
         /// </summary>
         /// <value>
         ///   <c>true</c> if registration; otherwise, <c>false</c>.
         /// </value>
         public bool Registration { get; set; }
         /// <summary>
         /// Gets a value indicating whether this <see cref="FormField"/> is predefined.
         /// </summary>
         /// <value>
         ///   <c>true</c> if predefined; otherwise, <c>false</c>.
         /// </value>
         public  bool Predefined
         {
             get
             {
                 return true;
             }

         }

         /// <summary>
         /// Gets or sets the bind table.
         /// </summary>
         /// <value>
         /// The bind table.
         /// </value>
         public string BindTable { get; set; }
         /// <summary>
         /// Gets or sets the category identifier.
         /// </summary>
         /// <value>
         /// The category identifier.
         /// </value>
         public int? CategoryId { get; set; }
         /// <summary>
         /// Returns a <see cref="System.String" /> that represents this instance.
         /// </summary>
         /// <returns>
         /// A <see cref="System.String" /> that represents this instance.
         /// </returns>
         public override string ToString()
         {
             return this.Name;
         }
        //public string PersistField
        // {
        //     get;
        //     set;
        // }

        ///// <summary>
        ///// Gets or sets the persist table.
        ///// </summary>
        ///// <value>
        ///// The persist table.
        ///// </value>
        //public string PersistTable { get; set; }
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool DeleteFlag
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }

        public int? DeletedBy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime? DeleteDate
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int CreatedBy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Entities.FormBuilder.FormField" />
    //[Serializable]
    // public class PredefinedField : FormField
    // {

    //     /// <summary>
    //     /// Gets a value indicating whether this <see cref="PredefinedField"/> is predefined.
    //     /// </summary>
    //     /// <value>
    //     ///   <c>true</c> if predefined; otherwise, <c>false</c>.
    //     /// </value>
    //     public override bool Predefined
    //     {
    //         get
    //         {
    //             return true;
    //         }
             
    //     }

    //     /// <summary>
    //     /// Gets or sets the persist field.
    //     /// </summary>
    //     /// <value>
    //     /// The persist field.
    //     /// </value>
    //     public string PersistField
    //     {
    //         get;
    //         set;
    //     }

    //     /// <summary>
    //     /// Gets or sets the persist table.
    //     /// </summary>
    //     /// <value>
    //     /// The persist table.
    //     /// </value>
    //     public string PersistTable{ get; set; }
    // }
}
