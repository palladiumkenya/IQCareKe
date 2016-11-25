using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Laboratory;
using Application.Presentation;
using System.Data;
 

public partial class FacilityStats : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void DataBind(bool raiseOnDataBinding)
    {
        base.DataBind(raiseOnDataBinding);
        PopulateData();
    }
    void PopulateData()
    {
        DataSet dsData = new DataSet("Statitics");
     
       //Home.ID,
       // Home.Name,
       // Home.FeatureID,
       // dtl.Id QueryID,
       // dtl.HomePageId,
       // dtl.IndicatorName,
       // dtl.Query
            string xsd =
                @"<?xml version=""1.0"" standalone=""yes""?>
<xs:schema id=""Statitics"" xmlns="""" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata""> 	
	<xs:element name=""Statitics"" msdata:IsDataSet=""true"" msdata:UseCurrentLocale=""true"">
		<xs:complexType> 
			<xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
				<xs:element name=""master"" maxOccurs=""1"" minOccurs=""0"">
					 <xs:complexType>
						<xs:sequence>
							<xs:element name=""ID""      type=""xs:int"" />
							<xs:element name=""Name""        type=""xs:string"" />
							<xs:element name=""FeatureID""    type=""xs:int""/>							
						</xs:sequence>
					</xs:complexType>
				</xs:element>
                <xs:element name=""detail"" maxOccurs=""1"" minOccurs=""0"">
					 <xs:complexType>
						<xs:sequence>
							<xs:element name=""FeatureID""    type=""xs:int""/>
							<xs:element name=""QueryID""   type=""xs:int""/> 
                            <xs:element name=""ID""  type=""xs:int""/>
                            <xs:element name=""IndicatorName""      type=""xs:string"" /> 
                            <xs:element name=""Query"" type=""xs:string"" /> 
						</xs:sequence>
					</xs:complexType>
				</xs:element>
	</xs:choice>
		</xs:complexType>
	</xs:element>
 </xs:schema>";

            using (System.IO.TextReader txR = new System.IO.StringReader(xsd))
            {
                //.txR.TextReader txR = new StringReader(this.DataXSDSchema);
                dsData.ReadXmlSchema(txR);
                txR.Close();
            }

         ILabFunctions LabTestsMgrDate;
        string statement=@"Select	ID,	Name,	FeatureId From dbo.Mst_HomePage Where DeleteFlag = 0; ";
        LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
        DataTable master = LabTestsMgrDate.ReturnLabQuery(statement);
        master.TableName = "master";
        string statement2 = @"Select	Home.FeatureID,dtl.Id QueryID,	Convert(int,dtl.HomePageId) HomePageId,	dtl.IndicatorName,	dtl.Query From dbo.mst_Feature Feature
            Inner Join dbo.Mst_HomePage Home	            On Home.FeatureID = Feature.FeatureID            Inner Join dbo.Dtl_HomePage dtl
	            On Home.Id = dtl.HomePageId            Where dtl.DeleteFlag = 0            Order By dtl.Seq;";

        LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
        DataTable details = LabTestsMgrDate.ReturnLabQuery(statement2);
        details.TableName = "detail";

        dsData.Tables.Clear();

        dsData.Tables.Add(master.Copy());
        dsData.Tables.Add(details.Copy());

         DataRelation relation = new DataRelation("myRelation",
                 dsData.Tables["master"].Columns["ID"],
                 dsData.Tables["detail"].Columns["HomePageId"], true);
              //  relation.Nested = true;

                dsData.Relations.Add(relation);
                rptServiceArea.DataSource = dsData.Tables["master"];
                rptServiceArea.DataBind();
        
    }
    /// <summary>
    /// Gets the stat result.
    /// </summary>
    /// <param name="statement">The statement.</param>
    /// <returns></returns>
    public int GetStatResult(string statement)
    {
        int result = 0;
        ILabFunctions LabTestsMgrDate;

        LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
        DataTable dtLabResult = LabTestsMgrDate.ReturnLabQuery(statement);

        if (dtLabResult != null && dtLabResult.Rows.Count > 0)
        {
            result = Convert.ToInt32(dtLabResult.Rows[0][0]);
        }
        return result;
    }
}