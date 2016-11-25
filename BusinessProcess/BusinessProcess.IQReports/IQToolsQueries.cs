using System;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using DataAccess.Report;
using Interface.IQToolsReports;

namespace BusinessProcess.IQReports
{
    [Serializable()]
    public class IQToolsQueries : ProcessBase, IReportIQQuery
    {
        //    IReportQuery reportQuery;
        /// <summary>
        /// The report iq tools
        /// </summary>
        IReportIQQuery queryIQTools;
    
        /// <summary>
        /// The query XML
        /// </summary>
        XmlDocument queryXML = null;
        /// <summary>
        /// The this query
        /// </summary>
        Query thisQuery;

        /// <summary>
        /// Initialises the query document.
        /// </summary>
        void InitialiseQueryDocument()
        {
            this.queryXML = new XmlDocument();
            XmlDeclaration newChild = this.queryXML.CreateXmlDeclaration("1.0", "UTF-8", null);
            this.queryXML.AppendChild(newChild);
            XmlElement element = this.queryXML.CreateElement("Root");
            this.queryXML.AppendChild(element);
        }
        /// <summary>
        /// Prevents a default instance of the <see cref="IQToolsQueries"/> class from being created.
        /// </summary>
        public IQToolsQueries()
        {
            queryIQTools = (IReportIQQuery)this;
            // reportQuery = (IReportQuery)this;
        }     
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        DataTable IReportIQQuery.ExecQuery(DataAccess.Report.Query query)
        {
            ClsObject ReportManager = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@FromDate", SqlDbType.DateTime, query.queryParameters.DateFrom);
            ClsUtility.AddExtendedParameters("@ToDate", SqlDbType.DateTime, query.queryParameters.DateTo);
            ClsUtility.AddExtendedParameters("@CD4Cutoff", SqlDbType.Int, query.queryParameters.CD4Cutoff);
            DataTable dt = new DataTable(query.Name);
           dt= (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, query.Defination, ClsUtility.ObjectEnum.DataTable, ConnectionMode.REPORT);
           dt.TableName = query.Name;
            return dt;

        }
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="queryID">The query identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable ExecQuery(int queryID, DateTime dateFrom, DateTime dateTo, int cd4CutOff = 350)
        {
            lock (this)
            {
                ClsObject ReportManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                string queryStatement = @"Select	qryID Query_ID,
		qryName Query_Name,
		qryDefinition Defination,
		qryDescription Description
From dbo.aa_Queries
Where qryID = @Query_ID; ";
                this.thisQuery = new Query() { QueryID = queryID };
                thisQuery.queryParameters = new QueryParameters() { DateFrom = dateFrom, DateTo = dateTo ,CD4Cutoff=cd4CutOff};

                // DataMgr dataManager = new DataMgr();
                ClsUtility.AddParameters("@Query_ID", SqlDbType.Int, queryID.ToString());

                DataRow row = (DataRow)ReportManager.ReturnObject(ClsUtility.theParams, queryStatement, ClsUtility.ObjectEnum.DataRow, ConnectionMode.REPORT);

                if (row != null)
                {
                    thisQuery.Name = row["Query_Name"].ToString();
                    thisQuery.Defination = row["Defination"].ToString();
                    thisQuery.Description = row["Description"].ToString();
                    return queryIQTools.ExecQuery(thisQuery);
                };

                return null;
            }
        }


        /// <summary>
        /// Gets the full query document.
        /// </summary>
        /// <returns></returns>
        public string GetFullQueryDocument(string categoryName = "")
        {
             ClsObject queryManager = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@Category", SqlDbType.VarChar, categoryName);
            
            string queryStatement = @"Select	B.sbCategory SubCategory,
		Category,
		B.catID CategoryID,
        B.sbCatID SubCategoryID,
		Q.qryID Query_ID,
		Q.qryName Query_Name,		
		Q.qryDescription Description
From dbo.aa_Category A
Inner Join dbo.aa_sbCategory B
	On A.CatID = B.CatID
Inner Join dbo.aa_Queries Q
	On Q.qryID = B.QryID
Where (B.DeleteFlag = 0 Or B.DeleteFlag Is Null)
And (Q.DeleteFlag = 0 Or Q.DeleteFlag Is Null)
And Case 
	When @Category  Is Null Or A.Category = @Category  Then 1 
	Else 0
	End = 1;";
            DataTable dt = (DataTable)queryManager.ReturnObject(ClsUtility.theParams, queryStatement, ClsUtility.ObjectEnum.DataTable, ConnectionMode.REPORT);
            this.InitialiseQueryDocument();

            if (dt != null && dt.Rows.Count > 0)
            {
                string[] selectedColumns = new[] { "Category", "CategoryID" };
                string[] selectedSubsColumns = new[] { "Category", "CategoryID", "SubCategory" };

                DataTable cat = new DataView(dt).ToTable(true, selectedColumns);

                DataTable subcat = new DataView(dt).ToTable(true, selectedSubsColumns);

                foreach (DataRow dr in cat.Rows)
                {
                    XElement R = new XElement("Category",
                       new XAttribute("ID", dr["CategoryID"].ToString()),
                       new XAttribute("Name", dr["Category"].ToString()), 
                       (from sub in subcat.AsEnumerable()
                        where (sub["Category"].ToString() == dr["Category"].ToString())
                        select new XElement("SubCategory",
                                    new XAttribute("Name", sub["SubCategory"].ToString()),
                            (from row in dt.AsEnumerable()
                             where row["SubCategory"].ToString() == sub["SubCategory"].ToString()
                             select new XElement("Query",
                                 new XElement("Query_ID", row["Query_ID"].ToString()),
                                     new XElement("Name", row["Query_Name"].ToString()),
                                         new XElement("Description", row["Description"].ToString()),
                                             new XElement("SubCategoryID", row["SubCategoryID"].ToString())
                                     )
                                                     )
                       )
                       )
                       );
                    XmlElement rootElement = this.queryXML.DocumentElement;

                    XmlDocumentFragment queryNode = queryXML.CreateDocumentFragment();
                    queryNode.InnerXml = R.ToString();
                    rootElement.AppendChild(queryNode);
                }
                //var s = (from crow in cat.AsEnumerable()
                //         select new XElement("Category",
                //                 new XAttribute("ID", crow["CategoryID"].ToString()),
                //                 new XAttribute("Name", crow["Category"].ToString()),
                //                 (from sub in subcat.AsEnumerable()
                //                  select new XElement("SubCategory",
                //                    new XAttribute("Name", sub["SubCategory"].ToString()),
                //                        (from row in dt.AsEnumerable()
                //                         select new XElement("Query",
                //                             new XElement("Query_ID", row["Query_ID"].ToString()),
                //                                 new XElement("Name", row["Query_Name"].ToString()),
                //                                     new XElement("Description", row["Description"].ToString()),
                //                                         new XElement("SubCategoryID", row["SubCategoryID"].ToString())
                //                                 )
                //                                     )
                //    )
                //        )
                //        )
                //      );

               
            }
            return this.queryXML.OuterXml.ToString();
        }



    }
}
