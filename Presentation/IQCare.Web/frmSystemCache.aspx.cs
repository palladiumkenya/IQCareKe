using System;
using System.Data;
using Application.Presentation;
using Interface.Security;

public partial class frmSystemCache : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.IO.FileInfo theFileInfo1 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\AllMasters.con").ToString());
            System.IO.FileInfo theFileInfo2 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\DrugMasters.con").ToString());
            System.IO.FileInfo theFileInfo3 = new System.IO.FileInfo(Server.MapPath(".\\XMLFiles\\LabMasters.con").ToString());
            theFileInfo1.Delete();
            theFileInfo2.Delete();
            theFileInfo3.Delete();
        }
        catch { }

        IIQCareSystem theCacheManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
        DataSet theMainDS = theCacheManager.GetSystemCache();
        DataSet WriteXMLDS = new DataSet();

        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingType"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingTopic"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Provider"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Division"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Ward"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_District"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Reason"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Education"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Designation"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Employee"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Occupation"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Province"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Village"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Code"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVAIDSCareTypes"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARTSponsor"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisease"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Assessment"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Symptom"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Decode"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Feature"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Function"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisclosure"].Copy());
        //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_Satellite"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LPTF"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["mst_StoppedReason"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["mst_facility"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVCareStatus"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_RelationshipType"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_TBStatus"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARVStatus"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LostFollowreason"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Regimen"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_pmtctDeCode"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Module"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ModDecode"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARVSideEffects"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ModCode"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Country"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Town"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["VWDiseaseSymptom"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["VW_ICDList"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["mst_RegimenLine"].Copy());
        if (theMainDS.Tables.Contains("Users"))
        {
            WriteXMLDS.Tables.Add(theMainDS.Tables["Users"].Copy());
        }
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Store"].Copy());
        try
        {
            WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "AllMasters.con", XmlWriteMode.WriteSchema);
        }
        catch { }
        WriteXMLDS.Tables.Clear();
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Strength"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_FrequencyUnits"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Drug"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Generic"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_DrugType"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Frequency"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_DrugSchedule"].Copy());
        WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "DrugMasters.con",XmlWriteMode.WriteSchema);

        WriteXMLDS.Tables.Clear();
        WriteXMLDS.Tables.Clear();
        WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LabTest"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_TestParameter"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_LabValue"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_ParameterResult"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["LabTestOrder"].Copy());
        WriteXMLDS.Tables.Add(theMainDS.Tables["mst_PatientLabPeriod"].Copy());
        try
        {
            WriteXMLDS.WriteXml(Server.MapPath(".\\XMLFiles\\").ToString() + "LabMasters.con", XmlWriteMode.WriteSchema);
        }
        catch { }
        try
        {
            WriteXMLDS.Tables.Clear();
            WriteXMLDS = new DataSet("QBReportList");
            WriteXMLDS.Tables.Add(theMainDS.Tables["QueryBuilderReports"].Copy());
            WriteXMLDS.WriteXml(Server.MapPath("~\\XMLFiles\\QueryBuilderReports.con"), XmlWriteMode.WriteSchema);
        }
        catch { }
        Response.Redirect("frmFacilityHome.aspx");

    }
}
