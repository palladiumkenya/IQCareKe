using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Report
{

    [Serializable]
    public class Query
    {
        /// <summary>
        /// Gets or sets the query identifier.
        /// </summary>
        /// <value>
        /// The query identifier.
        /// </value>
        public int QueryID { get; set; }
        /// <summary>
        /// Gets or sets the sub category identifier.
        /// </summary>
        /// <value>
        /// The sub category identifier.
        /// </value>
        public int SubCategoryID { get; set; }
        /// <summary>
        /// Gets or sets the sub category.
        /// </summary>
        /// <value>
        /// The sub category.
        /// </value>
        public string SubCategory { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the defination.
        /// </summary>
        /// <value>
        /// The defination.
        /// </value>
        public string Defination { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the query maps.
        /// </summary>
        /// <value>
        /// The query maps.
        /// </value>
        List<QueryMapping> QueryMaps { get; set; }

        /// <summary>
        /// Gets or sets the query parameters.
        /// </summary>
        /// <value>
        /// The query parameters.
        /// </value>
        public QueryParameters queryParameters { get; set; }

    }
    //   [Serializable()]
    //public class QueryCategory
    //{

    //    /// <summary>
    //    /// Gets or sets the name.
    //    /// </summary>
    //    /// <value>
    //    /// The name.
    //    /// </value>
    //       public string Name { get; set; }
    //    /// <summary>
    //    /// Gets or sets the identifier.
    //    /// </summary>
    //    /// <value>
    //    /// The identifier.
    //    /// </value>
    //       public int ID { get; set; }
    //    /// <summary>
    //    /// Gets or sets the sub categories.
    //    /// </summary>
    //    /// <value>
    //    /// The sub categories.
    //    /// </value>
    //       public List<QuerySubCategory> SubCategories { get; set; }
    //}
    //   [Serializable()]
    //public class QuerySubCategory
    //{

    //    /// <summary>
    //    /// Gets or sets the name.
    //    /// </summary>
    //    /// <value>
    //    /// The name.
    //    /// </value>
    //       public string Name { get; set; }        
    //    /// <summary>
    //    /// Gets or sets the category identifier.
    //    /// </summary>
    //    /// <value>
    //    /// The category identifier.
    //    /// </value>
    //       public int CategoryID { get; set; }
    //    /// <summary>
    //    /// Gets or sets the queries.
    //    /// </summary>
    //    /// <value>
    //    /// The queries.
    //    /// </value>
    //       public List<Query> Queries { get; set; }
    //}
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    
    public class QueryParameters
    {
        /// <summary>
        /// Gets or sets the date from.
        /// </summary>
        /// <value>
        /// The date from.
        /// </value>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// Gets or sets the date to.
        /// </summary>
        /// <value>
        /// The date to.
        /// </value>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Gets or sets the c d4 cutoff.
        /// </summary>
        /// <value>
        /// The c d4 cutoff.
        /// </value>
        public int CD4Cutoff { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class QueryMapping
    {
        /// <summary>
        /// Gets or sets the cell.
        /// </summary>
        /// <value>
        /// The cell.
        /// </value>
        public string Cell { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the query identifier.
        /// </summary>
        /// <value>
        /// The query identifier.
        /// </value>
        public int QueryID { get; set; }
    }
}
