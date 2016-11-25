using System;
using System.Data;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.FormBuilder;
using System.IO;

namespace BusinessProcess.FormBuilder
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class BImportExportForms : ProcessBase, IImportExportForms
    {
        /// <summary>
        /// Gets all form detail.
        /// </summary>
        /// <param name="strFormStatus">The string form status.</param>
        /// <param name="strTechArea">The string tech area.</param>
        /// <param name="CountryId">The country identifier.</param>
        /// <param name="frmFormType">Type of the FRM form.</param>
        /// <returns></returns>
        public DataSet GetAllFormDetail(string strFormStatus, string strTechArea, Int32 CountryId, string frmFormType)
        {
            lock (this)
            {
                ClsObject FormDetail = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FormStatus", SqlDbType.VarChar, strFormStatus);
                ClsUtility.AddParameters("@TechArea", SqlDbType.VarChar, strTechArea);
                ClsUtility.AddParameters("@CountryId", SqlDbType.VarChar, CountryId.ToString());
                if (frmFormType == "")
                {
                    return (DataSet)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ManageForm_GetAllFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                }
                else
                {
                    return (DataSet)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ManageForm_GetAllHomeFormDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                }
            }
        }

        /// <summary>
        /// Gets the import export form detail.
        /// </summary>
        /// <param name="strFeatureName">Name of the string feature.</param>
        /// <returns></returns>
        public DataSet GetImportExportFormDetail(String strFeatureName)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataSet dsRes;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, strFeatureName.ToString());
                dsRes = (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_FetchFormsDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                return dsRes;
            }
        }

        /// <summary>
        /// Gets the import export home form detail.
        /// </summary>
        /// <param name="strFeatureName">Name of the string feature.</param>
        /// <returns></returns>
        public DataSet GetImportExportHomeFormDetail(String strFeatureName)
        {
            lock (this)
            {
                ClsObject CustomField = new ClsObject();
                DataSet dsRes;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, strFeatureName.ToString());
                dsRes = (DataSet)CustomField.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_FetchHomeFormsDetail_Futures", ClsUtility.ObjectEnum.DataSet);
                return dsRes;
            }
        }
        public int ImportForms(DataSet dsImportForms, int iUserId, int iCountryId)
        {
            int num30;
            try
            {
                DataRow row;
                base.Connection = DataMgr.GetConnection();
                base.Transaction = DataMgr.BeginTransaction(base.Connection);
                ClsObject obj2 = new ClsObject();
                obj2.Connection = base.Connection;
                obj2.Transaction = base.Transaction;
                int moduleId = 0;
                int num4 = 0;
                int num5 = 0;
                int tabId = 0;
                string fieldValue = string.Empty;
                for (int i = 0; i < dsImportForms.Tables[5].Rows.Count; i++)
                {
                    if (dsImportForms.Tables[5].Rows[i].ItemArray[0].ToString() != "0")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[5].Rows[i]["ModuleId"].ToString());
                        ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, dsImportForms.Tables[5].Rows[i]["ModuleName"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                        row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportModules_Futures", ClsUtility.ObjectEnum.DataRow);
                        moduleId = Convert.ToInt32(row[0].ToString());
                        DataView defaultView = new DataView();
                        defaultView = dsImportForms.Tables[6].DefaultView;
                        DataTable table = new DataTable();
                        defaultView.RowFilter = "ModuleId=" + dsImportForms.Tables[5].Rows[i]["ModuleId"].ToString();
                        table = defaultView.ToTable();
                        for (int k = 0; k < table.Rows.Count; k++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, moduleId.ToString());
                            ClsUtility.AddParameters("@FieldId", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["Id"].ToString());
                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["FieldName"].ToString());
                            ClsUtility.AddParameters("@FieldType", SqlDbType.Int, dsImportForms.Tables[6].Rows[k]["FieldType"].ToString());
                            if (dsImportForms.Tables[6].Columns.Contains("label"))
                            {
                                ClsUtility.AddParameters("@Label", SqlDbType.VarChar, (dsImportForms.Tables[6].Rows[k]["label"].ToString() == null) ? dsImportForms.Tables[6].Rows[k]["FieldName"].ToString() : dsImportForms.Tables[6].Rows[k]["label"].ToString());
                            }
                            else
                            {
                                ClsUtility.AddParameters("@Label", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["FieldName"].ToString());
                            }
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                            row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportModulesIdentifier_Futures", ClsUtility.ObjectEnum.DataRow);
                        }
                    }
                }
                for (int j = 0; j < dsImportForms.Tables[0].Rows.Count; j++)
                {
                    int sectionId;
                    string[] strArray2;
                    string[] strArray = new string[10];
                    strArray = dsImportForms.Tables[0].Rows[j]["FeatureName"].ToString().Split(new char[] { ' ' });
                    fieldValue = "";
                    for (int m = 0; m < strArray.Length; m++)
                    {
                        if (m > 0)
                        {
                            fieldValue = fieldValue + "_" + strArray[m];
                        }
                        else
                        {
                            fieldValue = fieldValue + strArray[m];
                        }
                    }
                    string str2 = fieldValue.ToString();
                    fieldValue = "DTL_FBCUSTOMFIELD_" + fieldValue;
                    if (dsImportForms.Tables[5].Rows[j].ItemArray[0].ToString() != "0")
                    {
                        DataView view2 = new DataView(dsImportForms.Tables[5]);
                        DataTable table2 = new DataTable();
                        view2.RowFilter = "ModuleId =" + dsImportForms.Tables[0].Rows[j]["ModuleId"];
                        string str3 = view2.ToTable().Rows[0]["ModuleName"].ToString();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, str3.ToString());
                        row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_GetNewModuleId_Futures", ClsUtility.ObjectEnum.DataRow);
                        moduleId = Convert.ToInt32(row[0].ToString());
                    }
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[0].Rows[j]["FeatureId"].ToString());
                    ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsImportForms.Tables[0].Rows[j]["FeatureName"].ToString());
                    ClsUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[j]["ReportFlag"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[j]["DeleteFlag"].ToString());
                    ClsUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[j]["AdminFlag"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                    ClsUtility.AddParameters("@OptionalFlag", SqlDbType.Int, dsImportForms.Tables[0].Columns.Contains("OptionalFlag") ? dsImportForms.Tables[0].Rows[j]["OptionalFlag"].ToString() : "");
                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[j]["SystemId"].ToString());
                    ClsUtility.AddParameters("@Published", SqlDbType.Int, dsImportForms.Tables[0].Rows[j]["Published"].ToString());
                    ClsUtility.AddParameters("@CountryId", SqlDbType.Int, iCountryId.ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, moduleId.ToString());
                    if (dsImportForms.Tables[0].Columns.Contains("MultiVisit"))
                    {
                        ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsImportForms.Tables[0].Rows[j]["MultiVisit"].ToString());
                    }
                    else
                    {
                        ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, "1");
                    }
                    row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportForm_Futures", ClsUtility.ObjectEnum.DataRow);
                    int featureId = Convert.ToInt32(row[0].ToString());
                    if (featureId.ToString() == "126")
                    {
                        DataView view3 = new DataView(dsImportForms.Tables[1]);
                        DataTable table3 = new DataTable();
                        view3.RowFilter = "FeatureId =" + dsImportForms.Tables[0].Rows[j]["FeatureId"].ToString();
                        table3 = view3.ToTable();
                        for (int num11 = 0; num11 < table3.Rows.Count; num11++)
                        {
                            if (table3.Rows[num11]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[j]["FeatureId"].ToString())
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@SectionId", SqlDbType.Int, table3.Rows[num11]["SectionId"].ToString());
                                ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, table3.Rows[num11]["SectionName"].ToString());
                                ClsUtility.AddParameters("@Seq", SqlDbType.Int, table3.Rows[num11]["Seq"].ToString());
                                ClsUtility.AddParameters("@CustomFlag", SqlDbType.Int, table3.Rows[num11]["CustomFlag"].ToString());
                                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, table3.Rows[num11]["DeleteFlag"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, featureId.ToString());
                                ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, table3.Columns.Contains("IsGridView") ? table3.Rows[num11]["IsGridView"].ToString() : "0");
                                row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportSection_Futures", ClsUtility.ObjectEnum.DataRow);
                                sectionId = Convert.ToInt32(row[0].ToString());
                                DataView view4 = new DataView(dsImportForms.Tables[2]);
                                DataTable table4 = new DataTable();
                                view4.RowFilter = "FeatureId =" + table3.Rows[num11]["FeatureId"].ToString();
                                table4 = view4.ToTable();
                                DataTable tableSelectList = new DataTable("selectList");
                                tableSelectList.Columns.Add(new DataColumn("option", Type.GetType("System.String")));
                                tableSelectList.AcceptChanges();
                                for (int num12 = 0; num12 < table4.Rows.Count; num12++)
                                {
                                    if ((table3.Rows[num11]["FeatureId"].ToString() == table4.Rows[num12]["FeatureId"].ToString()) && (table3.Rows[num11]["SectionId"].ToString() == table4.Rows[num12]["SectionId"].ToString()))
                                    {
                                       // string str4 = string.Empty;
                                        if ((dsImportForms.Tables.Count > 3) && (dsImportForms.Tables[3].Rows[0][0].ToString() != "0"))
                                        {
                                            for (int num13 = 0; num13 < dsImportForms.Tables[3].Rows.Count; num13++)
                                            {
                                                if (((dsImportForms.Tables[3].Rows[num13]["FeatureId"].ToString() == table4.Rows[num12]["FeatureId"].ToString()) && (dsImportForms.Tables[3].Rows[num13]["FieldId"].ToString() == table4.Rows[num12]["FieldId"].ToString())) && (dsImportForms.Tables[3].Rows[num13]["SectionId"].ToString() == table4.Rows[num12]["SectionId"].ToString()))
                                                {
                                                    tableSelectList.Rows.Add(new object[] { dsImportForms.Tables[3].Rows[num13]["ListVal"].ToString() });
                                                    //if (str4 == "")
                                                    //{
                                                    //    str4 = dsImportForms.Tables[3].Rows[num13]["ListVal"].ToString();
                                                    //}
                                                    //else
                                                    //{
                                                    //    str4 = str4 + ";" + dsImportForms.Tables[3].Rows[num13]["ListVal"].ToString();
                                                    //}
                                                }
                                            }
                                        }
                                        string str5 = string.Empty;
                                        if ((dsImportForms.Tables.Count > 4) && (dsImportForms.Tables[4].Rows[0][0].ToString() != "0"))
                                        {
                                            for (int num14 = 0; num14 < dsImportForms.Tables[4].Rows.Count; num14++)
                                            {
                                                if ((dsImportForms.Tables[4].Rows[num14]["FieldId"].ToString() == table4.Rows[num12]["FieldId"].ToString()) && (dsImportForms.Tables[4].Rows[num14]["Predefined"].ToString() == table4.Rows[num12]["Predefined"].ToString()))
                                                {
                                                    if (str5 == "")
                                                    {
                                                        str5 = dsImportForms.Tables[4].Rows[num14]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && (dsImportForms.Tables[4].Rows[num14]["Value"].ToString() != "")) ? dsImportForms.Tables[4].Rows[num14]["Value"].ToString() : "Null");
                                                    }
                                                    else
                                                    {
                                                        strArray2 = new string[] { str5, ",", dsImportForms.Tables[4].Rows[num14]["BusRuleId"].ToString(), "-", (dsImportForms.Tables[4].Columns.Contains("Value") && (dsImportForms.Tables[4].Rows[num14]["Value"].ToString() != "")) ? dsImportForms.Tables[4].Rows[num14]["Value"].ToString() : "Null" };
                                                        str5 = string.Concat(strArray2);
                                                    }
                                                }
                                            }
                                        }
                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@Id", SqlDbType.Int, table4.Rows[num12]["Id"].ToString());
                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, featureId.ToString());
                                        ClsUtility.AddParameters("@SectionId", SqlDbType.Int, sectionId.ToString());
                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, table4.Rows[num12]["FieldId"].ToString());
                                        ClsUtility.AddParameters("@FieldName", SqlDbType.Int, table4.Rows[num12]["FieldName"].ToString());
                                        ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, table4.Rows[num12]["FieldLabel"].ToString());
                                        ClsUtility.AddParameters("@ControlId", SqlDbType.Int, table4.Columns.Contains("ControlId") ? table4.Rows[num12]["ControlId"].ToString() : "");
                                        string xmlString = string.Empty;
                                        using (TextWriter writer = new StringWriter())
                                        {
                                            tableSelectList.WriteXml(writer);
                                            xmlString = writer.ToString();
                                        }

                                        ClsUtility.AddParameters("@SelectListVal", SqlDbType.Xml, xmlString);
                                        //ClsUtility.AddParameters("@SelectListVal", SqlDbType.VarChar, str4);
                                        ClsUtility.AddParameters("@BusRuleIdValAll", SqlDbType.VarChar, str5);
                                        ClsUtility.AddParameters("@Seq", SqlDbType.Int, table4.Rows[num12]["Seq"].ToString());
                                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, table4.Rows[num12]["Predefined"].ToString());
                                        if (featureId == 0x7e)
                                        {
                                            ClsUtility.AddParameters("@PatientRegistration", SqlDbType.Int, "1");
                                        }
                                        DataTable table5 = (DataTable)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.DataTable);
                                        if (table5.Rows.Count > 1)
                                        {
                                            DataView view5 = new DataView();
                                            view5 = table5.DefaultView;
                                            DataTable table6 = new DataTable();
                                            view5.RowFilter = "FormFieldID <>'0'";
                                            num4 = Convert.ToInt32(view5.ToTable().Rows[0][0].ToString());
                                        }
                                        else
                                        {
                                            num4 = Convert.ToInt32(table5.Rows[0][0].ToString());
                                        }
                                        if (table4.Rows[num12]["Predefined"].ToString() != "1")
                                        {
                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, fieldValue);
                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, table4.Rows[num12]["FieldName"].ToString());
                                            ClsUtility.AddParameters("@DataType", SqlDbType.Int, table4.Rows[num12]["ControlId"].ToString());
                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, table4.Rows[num12]["Predefined"].ToString());
                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, table4.Rows[num12]["FieldId"].ToString());
                                            int num1 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        }
                                        if ((table3.Rows[num11]["IsGridView"].ToString() == "1") && (table4.Rows[num12]["Predefined"].ToString() != "1"))
                                        {
                                            string str6 = "DTL_CUSTOMFORM_" + table3.Rows[num11]["SectionName"].ToString() + "_" + str2;
                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, str6);
                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, table4.Rows[num12]["FieldName"].ToString());
                                            ClsUtility.AddParameters("@DataType", SqlDbType.Int, table4.Rows[num12]["ControlId"].ToString());
                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, table4.Rows[num12]["FieldId"].ToString());
                                            int num31 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        }
                                        if ((dsImportForms.Tables[11].Rows[0].ItemArray[0].ToString() != "0") && (table4.Rows[num12]["ControlId"].ToString() == "16"))
                                        {
                                            DataView view6 = new DataView();
                                            view6 = dsImportForms.Tables[11].DefaultView;
                                            DataTable table7 = new DataTable();
                                            view6.RowFilter = "FieldId='" + table4.Rows[num12]["FieldId"].ToString() + "'";
                                            table7 = view6.ToTable();
                                            if (table7.Rows.Count > 0)
                                            {
                                                if (table4.Rows[num12]["FieldId"].ToString() == num4.ToString())
                                                {
                                                    ClsUtility.Init_Hashtable();
                                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, table4.Rows[num12]["FieldId"].ToString());
                                                    int num32 = (int)obj2.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteFieldICDCode_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                }
                                                for (int num15 = 0; num15 < table7.Rows.Count; num15++)
                                                {
                                                    ClsUtility.Init_Hashtable();
                                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, num4.ToString());
                                                    ClsUtility.AddParameters("@BlockId", SqlDbType.Int, table7.Rows[num15]["BlockId"].ToString());
                                                    ClsUtility.AddParameters("@SubBlockId", SqlDbType.Int, table7.Rows[num15]["SubBlockId"].ToString());
                                                    ClsUtility.AddParameters("@CodeId", SqlDbType.Int, table7.Rows[num15]["CodeId"].ToString());
                                                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, table7.Rows[num15]["UserId"].ToString());
                                                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                                                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, table7.Rows[num15]["Predefined"].ToString());
                                                    int num33 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                }
                                            }
                                        }
                                        tableSelectList = new DataTable("selectList");
                                        tableSelectList.Columns.Add(new DataColumn("option", Type.GetType("System.String")));
                                        tableSelectList.AcceptChanges();
                                        if (((dsImportForms.Tables.Count > 7) && (dsImportForms.Tables[7].Rows.Count > 0)) && ((dsImportForms.Tables[7].Rows[0][0].ToString() != "") && (dsImportForms.Tables[7].Rows[0][0].ToString() != "0")))
                                        {
                                            for (int num16 = 0; num16 < dsImportForms.Tables[7].Rows.Count; num16++)
                                            {
                                                if ((table3.Rows[num11]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[num16]["FeatureId"].ToString()) && (table3.Rows[num11]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[num16]["SectionId"].ToString()))
                                                {
                                                    //string str7 = string.Empty;
                                                    tableSelectList = new DataTable("selectList");
                                                    tableSelectList.Columns.Add(new DataColumn("option", Type.GetType("System.String")));
                                                    tableSelectList.AcceptChanges();
                                                    if ((dsImportForms.Tables.Count > 8) && (dsImportForms.Tables[8].Rows[0][0].ToString() != "0"))
                                                    {
                                                        for (int num17 = 0; num17 < dsImportForms.Tables[8].Rows.Count; num17++)
                                                        {
                                                            if (((dsImportForms.Tables[8].Rows[num17]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[num16]["FeatureId"].ToString()) && (dsImportForms.Tables[8].Rows[num17]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[num16]["ConditionalFieldId"].ToString())) && (dsImportForms.Tables[8].Rows[num17]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[num16]["ConditionalFieldSectionId"].ToString()))
                                                            {
                                                                tableSelectList.Rows.Add(new object[] { dsImportForms.Tables[8].Rows[num17]["ListVal"].ToString() });
                                                   
                                                                //if (str7 == "")
                                                                //{
                                                                //    str7 = dsImportForms.Tables[8].Rows[num17]["ListVal"].ToString();
                                                                //}
                                                                //else
                                                                //{
                                                                //    str7 = str7 + ";" + dsImportForms.Tables[8].Rows[num17]["ListVal"].ToString();
                                                                //}
                                                            }
                                                        }
                                                    }
                                                    string str8 = string.Empty;
                                                    if ((dsImportForms.Tables.Count > 9) && (dsImportForms.Tables[9].Rows[0][0].ToString() != "0"))
                                                    {
                                                        for (int num18 = 0; num18 < dsImportForms.Tables[9].Rows.Count; num18++)
                                                        {
                                                            if ((dsImportForms.Tables[9].Rows[num18]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[num16]["ConditionalFieldId"].ToString()) && (dsImportForms.Tables[9].Rows[num18]["Predefined"].ToString() == dsImportForms.Tables[7].Rows[num16]["ConditionalFieldPredefined"].ToString()))
                                                            {
                                                                if (str8 == "")
                                                                {
                                                                    strArray2 = new string[] { dsImportForms.Tables[9].Rows[num18]["BusRuleId"].ToString(), "-", (dsImportForms.Tables[4].Columns.Contains("Value") && (dsImportForms.Tables[9].Rows[num18]["Value"].ToString() != "")) ? dsImportForms.Tables[9].Rows[num18]["Value"].ToString() : "Null", "-", (dsImportForms.Tables[4].Columns.Contains("Value1") && (dsImportForms.Tables[9].Rows[num18]["Value1"].ToString() != "")) ? dsImportForms.Tables[9].Rows[num18]["Value1"].ToString() : "Null" };
                                                                    str8 = string.Concat(strArray2);
                                                                }
                                                                else
                                                                {
                                                                    strArray2 = new string[] { str8, ",", dsImportForms.Tables[9].Rows[num18]["BusRuleId"].ToString(), "-", (dsImportForms.Tables[9].Columns.Contains("Value") && (dsImportForms.Tables[9].Rows[num18]["Value"].ToString() != "")) ? dsImportForms.Tables[9].Rows[num18]["Value"].ToString() : "Null", "-", (dsImportForms.Tables[9].Columns.Contains("Value1") && (dsImportForms.Tables[9].Rows[num18]["Value1"].ToString() != "")) ? dsImportForms.Tables[9].Rows[num18]["Value1"].ToString() : "Null" };
                                                                    str8 = string.Concat(strArray2);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (((table4.Rows[num12]["fieldId"].ToString() == dsImportForms.Tables[7].Rows[num16]["FieldId"].ToString()) && (table4.Rows[num12]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[num16]["SectionId"].ToString())) && (table4.Rows[num12]["featureId"].ToString() == dsImportForms.Tables[7].Rows[num16]["featureId"].ToString()))
                                                    {
                                                        ClsUtility.Init_Hashtable();
                                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, featureId.ToString());
                                                        ClsUtility.AddParameters("@SectionId", SqlDbType.Int, sectionId.ToString());
                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, num4.ToString());
                                                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num16]["FieldName"].ToString());
                                                        ClsUtility.AddParameters("@ConFieldId", SqlDbType.Int, dsImportForms.Tables[7].Rows[num16]["ConditionalFieldId"].ToString());
                                                        ClsUtility.AddParameters("@ConFieldName", SqlDbType.Int, dsImportForms.Tables[7].Rows[num16]["ConditionalFieldName"].ToString());
                                                        ClsUtility.AddParameters("@ConFieldLabel", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num16]["ConditionalFieldLabel"].ToString());
                                                        ClsUtility.AddParameters("@ControlId", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num16]["ControlId"].ToString());
                                                        ClsUtility.AddParameters("@ConControlId", SqlDbType.Int, dsImportForms.Tables[7].Columns.Contains("ConditionalFieldControlId") ? dsImportForms.Tables[7].Rows[num16]["ConditionalFieldControlId"].ToString() : "");
                                                         xmlString = string.Empty;
                                                        using (TextWriter writer = new StringWriter())
                                                        {
                                                            tableSelectList.WriteXml(writer);
                                                            xmlString = writer.ToString();
                                                        }

                                                        ClsUtility.AddParameters("@ConSelectListVal", SqlDbType.Xml, xmlString);
                                                        ClsUtility.AddParameters("@ConBusRuleIdValAll", SqlDbType.VarChar, str8);
                                                        ClsUtility.AddParameters("@ConSeq", SqlDbType.Int, dsImportForms.Tables[7].Rows[num16]["ConditionalFieldSequence"].ToString());
                                                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                                        ClsUtility.AddParameters("@ConPredefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[num16]["ConditionalFieldPredefined"].ToString());
                                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[num16]["fieldpredefined"].ToString());
                                                        ClsUtility.AddParameters("@ConSectionId", SqlDbType.Int, dsImportForms.Tables[7].Rows[num16]["ConditionalFieldSectionId"].ToString());
                                                        ClsUtility.AddParameters("@ModdecodeName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num16]["Mod"].ToString());
                                                        ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[j]["SystemId"].ToString());
                                                        if (featureId == 0x7e)
                                                        {
                                                            row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportRegistrationConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                        }
                                                        else
                                                        {
                                                            row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                        }
                                                        num5 = Convert.ToInt32(row[0].ToString());
                                                        if (dsImportForms.Tables[7].Rows[num16]["ConditionalFieldPredefined"].ToString() != "1")
                                                        {
                                                            ClsUtility.Init_Hashtable();
                                                            ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, fieldValue);
                                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num16]["ConditionalFieldName"].ToString());
                                                            ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[7].Rows[num16]["ConditionalFieldControlId"].ToString());
                                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[num16]["ConditionalFieldPredefined"].ToString());
                                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, num5.ToString());
                                                            int num34 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int num19 = 0; num19 < dsImportForms.Tables[12].Rows.Count; num19++)
                        {
                            if (dsImportForms.Tables[12].Rows[num19]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[j]["FeatureId"].ToString())
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@TabId", SqlDbType.Int, dsImportForms.Tables[12].Rows[num19]["TabId"].ToString());
                                ClsUtility.AddParameters("@TabName", SqlDbType.VarChar, dsImportForms.Tables[12].Rows[num19]["TabName"].ToString());
                                ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[12].Rows[num19]["Seq"].ToString());
                                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[12].Rows[num19]["DeleteFlag"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, featureId.ToString());
                                row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportTabs_Futures", ClsUtility.ObjectEnum.DataRow);
                                tabId = Convert.ToInt32(row[0].ToString());
                            }
                            for (int num20 = 0; num20 < dsImportForms.Tables[1].Rows.Count; num20++)
                            {
                                if (dsImportForms.Tables[1].Rows[num20]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[j]["FeatureId"].ToString())
                                {
                                    ClsUtility.Init_Hashtable();
                                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsImportForms.Tables[1].Rows[num20]["SectionId"].ToString());
                                    ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, dsImportForms.Tables[1].Rows[num20]["SectionName"].ToString());
                                    if (dsImportForms.Tables[1].Columns.Contains("SectionInfo"))
                                    {
                                        ClsUtility.AddParameters("@SectionInfo", SqlDbType.VarChar, dsImportForms.Tables[1].Rows[num20]["SectionInfo"].ToString());
                                    }
                                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[1].Rows[num20]["Seq"].ToString());
                                    ClsUtility.AddParameters("@CustomFlag", SqlDbType.Int, dsImportForms.Tables[1].Rows[num20]["CustomFlag"].ToString());
                                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[1].Rows[num20]["DeleteFlag"].ToString());
                                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, featureId.ToString());
                                    ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, dsImportForms.Tables[1].Columns.Contains("IsGridView") ? dsImportForms.Tables[1].Rows[num20]["IsGridView"].ToString() : "0");
                                    row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportSection_Futures", ClsUtility.ObjectEnum.DataRow);
                                    sectionId = Convert.ToInt32(row[0].ToString());
                                    for (int num21 = 0; num21 < dsImportForms.Tables[13].Rows.Count; num21++)
                                    {
                                        if ((dsImportForms.Tables[13].Rows[num21]["SectionId"].ToString() == dsImportForms.Tables[1].Rows[num20]["SectionId"].ToString()) && (dsImportForms.Tables[12].Rows[num19]["TabId"].ToString() == dsImportForms.Tables[13].Rows[num21]["TabId"].ToString()))
                                        {
                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@TabId", SqlDbType.Int, tabId.ToString());
                                            ClsUtility.AddParameters("@SectionId", SqlDbType.Int, sectionId.ToString());
                                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, featureId.ToString());
                                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                            int num35 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkFormTabSection_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        }
                                    }
                                    for (int num22 = 0; num22 < dsImportForms.Tables[2].Rows.Count; num22++)
                                    {
                                        if ((dsImportForms.Tables[1].Rows[num20]["FeatureId"].ToString() == dsImportForms.Tables[2].Rows[num22]["FeatureId"].ToString()) && (dsImportForms.Tables[1].Rows[num20]["SectionId"].ToString() == dsImportForms.Tables[2].Rows[num22]["SectionId"].ToString()))
                                        {
                                           // string str9 = string.Empty;
                                            DataTable tableSelectList = new DataTable("selectList");
                                            tableSelectList.Columns.Add(new DataColumn("option", Type.GetType("System.String")));
                                            tableSelectList.AcceptChanges();

                                           

                                            if ((dsImportForms.Tables.Count > 3) && (dsImportForms.Tables[3].Rows[0][0].ToString() != "0"))
                                            {
                                                for (int num23 = 0; num23 < dsImportForms.Tables[3].Rows.Count; num23++)
                                                {
                                                    if (((dsImportForms.Tables[3].Rows[num23]["FeatureId"].ToString() == dsImportForms.Tables[2].Rows[num22]["FeatureId"].ToString()) && (dsImportForms.Tables[3].Rows[num23]["FieldId"].ToString() == dsImportForms.Tables[2].Rows[num22]["FieldId"].ToString())) && (dsImportForms.Tables[3].Rows[num23]["SectionId"].ToString() == dsImportForms.Tables[2].Rows[num22]["SectionId"].ToString()))
                                                    {
                                                        tableSelectList.Rows.Add(new object[] { dsImportForms.Tables[3].Rows[num23]["ListVal"].ToString() });
                                                        //if (str9 == "")
                                                        //{
                                                        //    str9 = dsImportForms.Tables[3].Rows[num23]["ListVal"].ToString();
                                                        //}
                                                        //else
                                                        //{
                                                        //    str9 = str9 + ";" + dsImportForms.Tables[3].Rows[num23]["ListVal"].ToString();
                                                        //}
                                                    }
                                                }
                                            }
                                            string str10 = string.Empty;
                                          

                                            if ((dsImportForms.Tables.Count > 4) && (dsImportForms.Tables[4].Rows[0][0].ToString() != "0"))
                                            {
                                                for (int num24 = 0; num24 < dsImportForms.Tables[4].Rows.Count; num24++)
                                                {
                                                    if ((dsImportForms.Tables[4].Rows[num24]["FieldId"].ToString() == dsImportForms.Tables[2].Rows[num22]["FieldId"].ToString()) && (dsImportForms.Tables[4].Rows[num24]["Predefined"].ToString() == dsImportForms.Tables[2].Rows[num22]["Predefined"].ToString()))
                                                    {
                                                        if (str10 == "")
                                                        {
                                                            str10 = dsImportForms.Tables[4].Rows[num24]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && (dsImportForms.Tables[4].Rows[num24]["Value"].ToString() != "")) ? dsImportForms.Tables[4].Rows[num24]["Value"].ToString() : "Null");
                                                        }
                                                        else
                                                        {
                                                            strArray2 = new string[] { str10, ",", dsImportForms.Tables[4].Rows[num24]["BusRuleId"].ToString(), "-", (dsImportForms.Tables[4].Columns.Contains("Value") && (dsImportForms.Tables[4].Rows[num24]["Value"].ToString() != "")) ? dsImportForms.Tables[4].Rows[num24]["Value"].ToString() : "Null" };
                                                            str10 = string.Concat(strArray2);
                                                        }
                                                    }
                                                }
                                            }
                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@Id", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["Id"].ToString());
                                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, featureId.ToString());
                                            ClsUtility.AddParameters("@SectionId", SqlDbType.Int, sectionId.ToString());
                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["FieldId"].ToString());
                                            ClsUtility.AddParameters("@FieldName", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["FieldName"].ToString());
                                            ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[num22]["FieldLabel"].ToString());
                                            ClsUtility.AddParameters("@ControlId", SqlDbType.Int, dsImportForms.Tables[2].Columns.Contains("ControlId") ? dsImportForms.Tables[2].Rows[num22]["ControlId"].ToString() : "");
                                            string xmlString = string.Empty;
                                            using (TextWriter writer = new StringWriter())
                                            {
                                                tableSelectList.WriteXml(writer);
                                                xmlString = writer.ToString();
                                            }

                                            ClsUtility.AddParameters("@SelectListVal", SqlDbType.Xml, xmlString);
                                            //ClsUtility.AddParameters("@SelectListVal", SqlDbType.VarChar, str9);
                                            ClsUtility.AddParameters("@BusRuleIdValAll", SqlDbType.VarChar, str10);
                                            ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["Seq"].ToString());
                                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["Predefined"].ToString());
                                            if (featureId == 0x7e)
                                            {
                                                ClsUtility.AddParameters("@PatientRegistration", SqlDbType.Int, "1");
                                            }
                                            DataTable table8 = (DataTable)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.DataTable);
                                            if (table8.Rows.Count > 1)
                                            {
                                                DataView view7 = new DataView();
                                                view7 = table8.DefaultView;
                                                DataTable table9 = new DataTable();
                                                view7.RowFilter = "FormFieldID <>'0'";
                                                num4 = Convert.ToInt32(view7.ToTable().Rows[0][0].ToString());
                                            }
                                            else
                                            {
                                                num4 = Convert.ToInt32(table8.Rows[0][0].ToString());
                                            }
                                            if (dsImportForms.Tables[2].Rows[num22]["Predefined"].ToString() != "1")
                                            {
                                                ClsUtility.Init_Hashtable();
                                                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, fieldValue);
                                                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[num22]["FieldName"].ToString());
                                                ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["ControlId"].ToString());
                                                ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["Predefined"].ToString());
                                                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["FieldId"].ToString());
                                                int num36 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                            }
                                            if ((dsImportForms.Tables[1].Rows[num20]["IsGridView"].ToString() == "1") && (dsImportForms.Tables[2].Rows[num22]["Predefined"].ToString() != "1"))
                                            {
                                                string str11 = "DTL_CUSTOMFORM_" + dsImportForms.Tables[1].Rows[num20]["SectionName"].ToString() + "_" + str2;
                                                ClsUtility.Init_Hashtable();
                                                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, str11);
                                                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[num22]["FieldName"].ToString());
                                                ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["ControlId"].ToString());
                                                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["FieldId"].ToString());
                                                int num37 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                            }
                                            if ((dsImportForms.Tables[11].Rows[0].ItemArray[0].ToString() != "0") && (dsImportForms.Tables[2].Rows[num22]["ControlId"].ToString() == "16"))
                                            {
                                                DataView view8 = new DataView();
                                                view8 = dsImportForms.Tables[11].DefaultView;
                                                DataTable table10 = new DataTable();
                                                view8.RowFilter = "FieldId='" + dsImportForms.Tables[2].Rows[num22]["FieldId"].ToString() + "'";
                                                table10 = view8.ToTable();
                                                if (table10.Rows.Count > 0)
                                                {
                                                    if (dsImportForms.Tables[2].Rows[num22]["FieldId"].ToString() == num4.ToString())
                                                    {
                                                        ClsUtility.Init_Hashtable();
                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[num22]["FieldId"].ToString());
                                                        int num38 = (int)obj2.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteFieldICDCode_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                    }
                                                    for (int num25 = 0; num25 < table10.Rows.Count; num25++)
                                                    {
                                                        ClsUtility.Init_Hashtable();
                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, num4.ToString());
                                                        ClsUtility.AddParameters("@BlockId", SqlDbType.Int, table10.Rows[num25]["BlockId"].ToString());
                                                        ClsUtility.AddParameters("@SubBlockId", SqlDbType.Int, table10.Rows[num25]["SubBlockId"].ToString());
                                                        ClsUtility.AddParameters("@Chapterid", SqlDbType.Int, table10.Rows[num25]["Chapterid"].ToString());
                                                        ClsUtility.AddParameters("@CodeId", SqlDbType.Int, table10.Rows[num25]["CodeId"].ToString());
                                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, table10.Rows[num25]["UserId"].ToString());
                                                        ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, table10.Rows[num25]["Predefined"].ToString());
                                                        int num39 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                    }
                                                }
                                            }
                                            if (((dsImportForms.Tables.Count > 7) && (dsImportForms.Tables[7].Rows.Count > 0)) && ((dsImportForms.Tables[7].Rows[0][0].ToString() != "") && (dsImportForms.Tables[7].Rows[0][0].ToString() != "0")))
                                            {
                                                for (int num26 = 0; num26 < dsImportForms.Tables[7].Rows.Count; num26++)
                                                {
                                                    if ((dsImportForms.Tables[1].Rows[num20]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[num26]["FeatureId"].ToString()) && (dsImportForms.Tables[1].Rows[num20]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[num26]["SectionId"].ToString()))
                                                    {
                                                        //string str12 = string.Empty;
                                                         tableSelectList = new DataTable("selectList");
                                                        tableSelectList.Columns.Add(new DataColumn("option", Type.GetType("System.String")));
                                                        tableSelectList.AcceptChanges();

                                                        if ((dsImportForms.Tables.Count > 8) && (dsImportForms.Tables[8].Rows[0][0].ToString() != "0"))
                                                        {
                                                            for (int num27 = 0; num27 < dsImportForms.Tables[8].Rows.Count; num27++)
                                                            {
                                                                if (((dsImportForms.Tables[8].Rows[num27]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[num26]["FeatureId"].ToString()) && (dsImportForms.Tables[8].Rows[num27]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[num26]["ConditionalFieldId"].ToString())) && (dsImportForms.Tables[8].Rows[num27]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[num26]["ConditionalFieldSectionId"].ToString()))
                                                                {
                                                                    tableSelectList.Rows.Add(new object[] 
                                                                        {
                                                                        dsImportForms.Tables[8].Rows[num27]["ListVal"].ToString() 
                                                                        }
                                                                      );
                                                        
                                                                    //if (str12 == "")
                                                                    //{
                                                                    //    str12 = dsImportForms.Tables[8].Rows[num27]["ListVal"].ToString();
                                                                    //}
                                                                    //else
                                                                    //{
                                                                    //    str12 = str12 + ";" + dsImportForms.Tables[8].Rows[num27]["ListVal"].ToString();
                                                                    //}
                                                                }
                                                            }
                                                        }
                                                        string str13 = string.Empty;
                                                        if ((dsImportForms.Tables.Count > 9) && (dsImportForms.Tables[9].Rows[0][0].ToString() != "0"))
                                                        {
                                                            for (int num28 = 0; num28 < dsImportForms.Tables[9].Rows.Count; num28++)
                                                            {
                                                                if ((dsImportForms.Tables[9].Rows[num28]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[num26]["ConditionalFieldId"].ToString()) && (dsImportForms.Tables[9].Rows[num28]["Predefined"].ToString() == dsImportForms.Tables[7].Rows[num26]["ConditionalFieldPredefined"].ToString()))
                                                                {
                                                                    if (str13 == "")
                                                                    {
                                                                        strArray2 = new string[] { dsImportForms.Tables[9].Rows[num28]["BusRuleId"].ToString(), "-", (dsImportForms.Tables[4].Columns.Contains("Value") && (dsImportForms.Tables[9].Rows[num28]["Value"].ToString() != "")) ? dsImportForms.Tables[9].Rows[num28]["Value"].ToString() : "Null", "-", (dsImportForms.Tables[4].Columns.Contains("Value1") && (dsImportForms.Tables[9].Rows[num28]["Value1"].ToString() != "")) ? dsImportForms.Tables[9].Rows[num28]["Value1"].ToString() : "Null" };
                                                                        str13 = string.Concat(strArray2);
                                                                    }
                                                                    else
                                                                    {
                                                                        strArray2 = new string[] { str13, ",", dsImportForms.Tables[9].Rows[num28]["BusRuleId"].ToString(), "-", (dsImportForms.Tables[9].Columns.Contains("Value") && (dsImportForms.Tables[9].Rows[num28]["Value"].ToString() != "")) ? dsImportForms.Tables[9].Rows[num28]["Value"].ToString() : "Null", "-", (dsImportForms.Tables[9].Columns.Contains("Value1") && (dsImportForms.Tables[9].Rows[num28]["Value1"].ToString() != "")) ? dsImportForms.Tables[9].Rows[num28]["Value1"].ToString() : "Null" };
                                                                        str13 = string.Concat(strArray2);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (((dsImportForms.Tables[2].Rows[num22]["fieldId"].ToString() == dsImportForms.Tables[7].Rows[num26]["FieldId"].ToString()) && (dsImportForms.Tables[2].Rows[num22]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[num26]["SectionId"].ToString())) && (dsImportForms.Tables[2].Rows[num22]["featureId"].ToString() == dsImportForms.Tables[7].Rows[num26]["featureId"].ToString()))
                                                        {
                                                            ClsUtility.Init_Hashtable();
                                                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, featureId.ToString());
                                                            ClsUtility.AddParameters("@SectionId", SqlDbType.Int, sectionId.ToString());
                                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, num4.ToString());
                                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num26]["FieldName"].ToString());
                                                            ClsUtility.AddParameters("@ConFieldId", SqlDbType.Int, dsImportForms.Tables[7].Rows[num26]["ConditionalFieldId"].ToString());
                                                            ClsUtility.AddParameters("@ConFieldName", SqlDbType.Int, dsImportForms.Tables[7].Rows[num26]["ConditionalFieldName"].ToString());
                                                            ClsUtility.AddParameters("@ConFieldLabel", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num26]["ConditionalFieldLabel"].ToString());
                                                            ClsUtility.AddParameters("@ControlId", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num26]["ControlId"].ToString());
                                                            ClsUtility.AddParameters("@ConControlId", SqlDbType.Int, dsImportForms.Tables[7].Columns.Contains("ConditionalFieldControlId") ? dsImportForms.Tables[7].Rows[num26]["ConditionalFieldControlId"].ToString() : "");
                                                             xmlString = string.Empty;
                                                            using (TextWriter writer = new StringWriter())
                                                            {
                                                                tableSelectList.WriteXml(writer);
                                                                xmlString = writer.ToString();
                                                            }
                                                            ClsUtility.AddParameters("@ConSelectListVal", SqlDbType.Xml, xmlString);
                                                            //ClsUtility.AddParameters("@ConSelectListVal", SqlDbType.VarChar, str12);
                                                            ClsUtility.AddParameters("@ConBusRuleIdValAll", SqlDbType.VarChar, str13);
                                                            ClsUtility.AddParameters("@ConSeq", SqlDbType.Int, dsImportForms.Tables[7].Rows[num26]["ConditionalFieldSequence"].ToString());
                                                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                                            ClsUtility.AddParameters("@ConPredefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[num26]["ConditionalFieldPredefined"].ToString());
                                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[num26]["fieldpredefined"].ToString());
                                                            ClsUtility.AddParameters("@ConSectionId", SqlDbType.Int, dsImportForms.Tables[7].Rows[num26]["ConditionalFieldSectionId"].ToString());
                                                            ClsUtility.AddParameters("@ModdecodeName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num26]["Mod"].ToString());
                                                            ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[j]["SystemId"].ToString());
                                                            if (featureId == 0x7e)
                                                            {
                                                                row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportRegistrationConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                            }
                                                            else
                                                            {
                                                                row = (DataRow)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                            }
                                                            num5 = Convert.ToInt32(row[0].ToString());
                                                            if (dsImportForms.Tables[7].Rows[num26]["ConditionalFieldPredefined"].ToString() != "1")
                                                            {
                                                                ClsUtility.Init_Hashtable();
                                                                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, fieldValue);
                                                                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[num26]["ConditionalFieldName"].ToString());
                                                                ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[7].Rows[num26]["ConditionalFieldControlId"].ToString());
                                                                ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[num26]["ConditionalFieldPredefined"].ToString());
                                                                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, num5.ToString());
                                                                int num40 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    for (int n = 0; n < dsImportForms.Tables[14].Rows.Count; n++)
                    {
                        if ((dsImportForms.Tables[14].Rows[n]["moduleid"].ToString() != "0") && (dsImportForms.Tables[14].Rows[n]["featureid"].ToString() != "0"))
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[14].Rows[n]["moduleid"].ToString());
                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[14].Rows[n]["featureid"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                            ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, dsImportForms.Tables[14].Rows[n]["modulename"].ToString());
                            int num41 = (int)obj2.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_spLnkForms_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                }
                DataMgr.CommitTransaction(base.Transaction);
                DataMgr.ReleaseConnection(base.Connection);
                num30 = 1;
            }
            catch
            {
                DataMgr.RollBackTransation(base.Transaction);
                throw;
            }
            finally
            {
                if (base.Connection != null)
                {
                    DataMgr.ReleaseConnection(base.Connection);
                }
            }
            return num30;
        }

 

 

        /// <summary>
        /// Imports the forms.
        /// </summary>
        /// <param name="dsImportForms">The ds import forms.</param>
        /// <param name="iUserId">The i user identifier.</param>
        /// <param name="iCountryId">The i country identifier.</param>
        /// <returns></returns>
      /*
       * public int ImportForms(DataSet dsImportForms, int iUserId, int iCountryId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int theRowAffected_SpLnkForms = 0;
                DataRow theDR;
               
                int iNewFeatureId; //this variable will be used to store featureid for all new rows
                int iNewModuleId = 0;
                int iNewSectionId;
                int iNewFieldId = 0;
                int iNewConFieldId = 0;
                int iNewTabId = 0;
                //string istrselectlstModecodeId = string.Empty;
                string strTableName = string.Empty;
                for (int j = 0; j < dsImportForms.Tables[5].Rows.Count; j++)
                {
                    if (dsImportForms.Tables[5].Rows[j].ItemArray[0].ToString() != "0")
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[5].Rows[j]["ModuleId"].ToString());
                        ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, dsImportForms.Tables[5].Rows[j]["ModuleName"].ToString());
                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportModules_Futures", ClsUtility.ObjectEnum.DataRow);
                        iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());

                        DataView dvModuleFieldDV = new DataView();
                        dvModuleFieldDV = dsImportForms.Tables[6].DefaultView;
                        DataTable dtModuleField = new DataTable();
                        dvModuleFieldDV.RowFilter = "ModuleId=" + dsImportForms.Tables[5].Rows[j]["ModuleId"].ToString();
                        dtModuleField = dvModuleFieldDV.ToTable();
                        for (int k = 0; k < dtModuleField.Rows.Count; k++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, iNewModuleId.ToString());
                            ClsUtility.AddParameters("@FieldId", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["Id"].ToString());
                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["FieldName"].ToString());
                            ClsUtility.AddParameters("@FieldType", SqlDbType.Int, dsImportForms.Tables[6].Rows[k]["FieldType"].ToString());
                            if (dsImportForms.Tables[6].Columns.Contains("label"))
                            {
                                ClsUtility.AddParameters("@Label", SqlDbType.VarChar, (dsImportForms.Tables[6].Rows[k]["label"].ToString() == null ? dsImportForms.Tables[6].Rows[k]["FieldName"].ToString() : dsImportForms.Tables[6].Rows[k]["label"].ToString()));
                            }
                            else
                                ClsUtility.AddParameters("@Label", SqlDbType.VarChar, dsImportForms.Tables[6].Rows[k]["FieldName"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                            theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportModulesIdentifier_Futures", ClsUtility.ObjectEnum.DataRow);
                            //iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    }
                }
                for (int i = 0; i < dsImportForms.Tables[0].Rows.Count; i++)
                {
                    string[] strFeatureName = new string[10];
                    strFeatureName = dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString().Split(' ');
                    strTableName = "";
                    for (int j = 0; j < strFeatureName.Length; j++)
                    {
                        if (j > 0)
                            strTableName += "_" + strFeatureName[j];
                        else
                            strTableName += strFeatureName[j];
                    }
                    string strgridFeaturename = strTableName.ToString();
                    strTableName = "DTL_FBCUSTOMFIELD_" + strTableName;

                    //for modules and its identifiers
                    //modules-tech area's
                    ///Get New Module Id
                    if (dsImportForms.Tables[5].Rows[i].ItemArray[0].ToString() != "0")
                    {
                        //DataRow[] foundRows = dsImportForms.Tables[5].Select("ModuleId=" + dsImportForms.Tables[0].Rows[i]["ModuleId"]);
                        DataView theFiltModDV = new DataView(dsImportForms.Tables[5]);
                        DataTable dtFiltMod = new DataTable();
                        theFiltModDV.RowFilter = "ModuleId =" + dsImportForms.Tables[0].Rows[i]["ModuleId"];
                        dtFiltMod = theFiltModDV.ToTable();
                        string strModName = dtFiltMod.Rows[0]["ModuleName"].ToString();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, strModName.ToString());
                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_GetNewModuleId_Futures", ClsUtility.ObjectEnum.DataRow);
                        iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());
                    }

                    //save mst_feature data
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString());
                    ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString());
                    ClsUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["ReportFlag"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["DeleteFlag"].ToString());
                    ClsUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["AdminFlag"].ToString());
                    //ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["UserID"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                    ClsUtility.AddParameters("@OptionalFlag", SqlDbType.Int, (dsImportForms.Tables[0].Columns.Contains("OptionalFlag")) ? dsImportForms.Tables[0].Rows[i]["OptionalFlag"].ToString() : "");
                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                    ClsUtility.AddParameters("@Published", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["Published"].ToString());
                    //ClsUtility.AddParameters("@CountryId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["CountryId"].ToString());
                    ClsUtility.AddParameters("@CountryId", SqlDbType.Int, iCountryId.ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, iNewModuleId.ToString());
                    if (dsImportForms.Tables[0].Columns.Contains("MultiVisit"))
                    {
                        ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["MultiVisit"].ToString());
                    }
                    else
                    {
                        ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, "1");
                    }

                    theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportForm_Futures", ClsUtility.ObjectEnum.DataRow);
                    iNewFeatureId = System.Convert.ToInt32(theDR[0].ToString());
                    if (iNewFeatureId.ToString() == "126")
                    {
                        #region "Import Patient Registration"

                        DataView theRegSecDV = new DataView(dsImportForms.Tables[1]);
                        DataTable dtRegSection = new DataTable();
                        theRegSecDV.RowFilter = "FeatureId =" + dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString();
                        dtRegSection = theRegSecDV.ToTable();
                        //foreach (DataRow drFormData in dsSaveFormData.Tables[1])
                        for (int j = 0; j < dtRegSection.Rows.Count; j++)
                        {
                            if (dtRegSection.Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString())
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dtRegSection.Rows[j]["SectionId"].ToString());
                                ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, dtRegSection.Rows[j]["SectionName"].ToString());
                                ClsUtility.AddParameters("@Seq", SqlDbType.Int, dtRegSection.Rows[j]["Seq"].ToString());
                                ClsUtility.AddParameters("@CustomFlag", SqlDbType.Int, dtRegSection.Rows[j]["CustomFlag"].ToString());
                                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtRegSection.Rows[j]["DeleteFlag"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                //ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString());
                                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, (dtRegSection.Columns.Contains("IsGridView")) ? dtRegSection.Rows[j]["IsGridView"].ToString() : "0");
                                // ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString());
                                theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportSection_Futures", ClsUtility.ObjectEnum.DataRow);
                                iNewSectionId = System.Convert.ToInt32(theDR[0].ToString());

                                #region "Update/Insert Fields"

                                DataView thelnkDV = new DataView(dsImportForms.Tables[2]);
                                DataTable dtlnkField = new DataTable();
                                thelnkDV.RowFilter = "FeatureId =" + dtRegSection.Rows[j]["FeatureId"].ToString();
                                dtlnkField = thelnkDV.ToTable();

                                for (int x = 0; x < dtlnkField.Rows.Count; x++)
                                {
                                    if (dtRegSection.Rows[j]["FeatureId"].ToString() == dtlnkField.Rows[x]["FeatureId"].ToString() && dtRegSection.Rows[j]["SectionId"].ToString() == dtlnkField.Rows[x]["SectionId"].ToString())
                                    {
                                        //store comma separated select list val for field
                                        string strSelectLstVal = string.Empty;
                                        DataTable tableSelectList = new DataTable("selectList");
                                        tableSelectList.Columns.Add(new DataColumn("option", Type.GetType("System.String")));
                                        tableSelectList.AcceptChanges();

                                        if (dsImportForms.Tables.Count > 3)
                                        {
                                            if (dsImportForms.Tables[3].Rows[0][0].ToString() != "0")
                                            {
                                                for (int l = 0; l < dsImportForms.Tables[3].Rows.Count; l++)
                                                {
                                                    if (dsImportForms.Tables[3].Rows[l]["FeatureId"].ToString() == dtlnkField.Rows[x]["FeatureId"].ToString() && dsImportForms.Tables[3].Rows[l]["FieldId"].ToString() == dtlnkField.Rows[x]["FieldId"].ToString() && dsImportForms.Tables[3].Rows[l]["SectionId"].ToString() == dtlnkField.Rows[x]["SectionId"].ToString())
                                                    {
                                                        tableSelectList.Rows.Add(new object[] { dsImportForms.Tables[3].Rows[l]["ListVal"].ToString() });
                                                        //if (strSelectLstVal == "")
                                                        //    strSelectLstVal = dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                        //else
                                                        //    strSelectLstVal = strSelectLstVal + ";" + dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                    }
                                                }
                                            }
                                        }

                                        //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                        string strBusRuleIdVal = string.Empty;
                                        if (dsImportForms.Tables.Count > 4)
                                        {
                                            if (dsImportForms.Tables[4].Rows[0][0].ToString() != "0")
                                            {
                                                for (int m = 0; m < dsImportForms.Tables[4].Rows.Count; m++)
                                                {
                                                    if (dsImportForms.Tables[4].Rows[m]["FieldId"].ToString() == dtlnkField.Rows[x]["FieldId"].ToString() && dsImportForms.Tables[4].Rows[m]["Predefined"].ToString() == dtlnkField.Rows[x]["Predefined"].ToString())
                                                    {
                                                        if (strBusRuleIdVal == "")
                                                            strBusRuleIdVal = dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                        else
                                                            strBusRuleIdVal = strBusRuleIdVal + "," + dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                    }
                                                }
                                            }
                                        }

                                        ClsUtility.Init_Hashtable();
                                        ClsUtility.AddParameters("@Id", SqlDbType.Int, dtlnkField.Rows[x]["Id"].ToString());
                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                        ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                        ClsUtility.AddParameters("@FieldName", SqlDbType.Int, dtlnkField.Rows[x]["FieldName"].ToString());
                                        ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dtlnkField.Rows[x]["FieldLabel"].ToString());
                                        ClsUtility.AddParameters("@ControlId", SqlDbType.Int, (dtlnkField.Columns.Contains("ControlId")) ? dtlnkField.Rows[x]["ControlId"].ToString() : "");
                                        string xmlString = string.Empty;
                                        using (TextWriter writer = new StringWriter())
                                        {
                                            tableSelectList.WriteXml(writer);
                                            xmlString = writer.ToString();
                                        }
                                        ClsUtility.AddParameters("@SelectListVal", SqlDbType.Xml, xmlString);
                                       //ClsUtility.AddParameters("@SelectListVal", SqlDbType.VarChar, strSelectLstVal);
                                        ClsUtility.AddParameters("@BusRuleIdValAll", SqlDbType.VarChar, strBusRuleIdVal);
                                        ClsUtility.AddParameters("@Seq", SqlDbType.Int, dtlnkField.Rows[x]["Seq"].ToString());
                                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtlnkField.Rows[x]["Predefined"].ToString());
                                        if (iNewFeatureId == 126)
                                        {
                                            ClsUtility.AddParameters("@PatientRegistration", SqlDbType.Int, "1");
                                        }
                                        //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        DataTable theFieldDT = (DataTable)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.DataTable);
                                        if (theFieldDT.Rows.Count > 1)
                                        {
                                            DataView dvFieldDV = new DataView();
                                            dvFieldDV = theFieldDT.DefaultView;
                                            DataTable dtField = new DataTable();
                                            dvFieldDV.RowFilter = "FormFieldID <>'0'";
                                            dtField = dvFieldDV.ToTable();
                                            iNewFieldId = System.Convert.ToInt32(dtField.Rows[0][0].ToString());
                                        }
                                        else
                                            iNewFieldId = System.Convert.ToInt32(theFieldDT.Rows[0][0].ToString());
                                        if (dtlnkField.Rows[x]["Predefined"].ToString() != "1")
                                        {
                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtlnkField.Rows[x]["FieldName"].ToString());
                                            ClsUtility.AddParameters("@DataType", SqlDbType.Int, dtlnkField.Rows[x]["ControlId"].ToString());
                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtlnkField.Rows[x]["Predefined"].ToString());
                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                            theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        }
                                        ///////Import GridView Control-Create Table//////////////////////////////////

                                        if (dtRegSection.Rows[j]["IsGridView"].ToString() == "1" && dtlnkField.Rows[x]["Predefined"].ToString() != "1")
                                        {
                                            string strTableNameSection = "DTL_CUSTOMFORM_" + dtRegSection.Rows[j]["SectionName"].ToString() + "_" + strgridFeaturename;
                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableNameSection);
                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dtlnkField.Rows[x]["FieldName"].ToString());
                                            ClsUtility.AddParameters("@DataType", SqlDbType.Int, dtlnkField.Rows[x]["ControlId"].ToString());
                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                            theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        }

                                        ////////Import Feild ICDCode Linking////////////////////////////////////////////
                                        if ((dsImportForms.Tables[11].Rows[0].ItemArray[0].ToString() != "0") && (dtlnkField.Rows[x]["ControlId"].ToString() == "16"))
                                        {
                                            DataView dvFilteredRow = new DataView();
                                            dvFilteredRow = dsImportForms.Tables[11].DefaultView;
                                            DataTable dtRow = new DataTable();
                                            dvFilteredRow.RowFilter = "FieldId='" + dtlnkField.Rows[x]["FieldId"].ToString() + "'";
                                            dtRow = dvFilteredRow.ToTable();
                                            if (dtRow.Rows.Count > 0)
                                            {
                                                if (dtlnkField.Rows[x]["FieldId"].ToString() == iNewFieldId.ToString())
                                                {
                                                    ClsUtility.Init_Hashtable();
                                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dtlnkField.Rows[x]["FieldId"].ToString());
                                                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteFieldICDCode_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                }
                                                for (int q = 0; q < dtRow.Rows.Count; q++)
                                                {
                                                    ClsUtility.Init_Hashtable();
                                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                    ClsUtility.AddParameters("@BlockId", SqlDbType.Int, dtRow.Rows[q]["BlockId"].ToString());
                                                    ClsUtility.AddParameters("@SubBlockId", SqlDbType.Int, dtRow.Rows[q]["SubBlockId"].ToString());
                                                    ClsUtility.AddParameters("@CodeId", SqlDbType.Int, dtRow.Rows[q]["CodeId"].ToString());
                                                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, dtRow.Rows[q]["UserId"].ToString());
                                                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                                                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtRow.Rows[q]["Predefined"].ToString());
                                                    theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                }
                                            }
                                        }
                                        ///////////////////////////////////////////////////

                                        #region "Update/Insert Conditional Fields"

                                        if (dsImportForms.Tables.Count > 7)
                                        {
                                            if (dsImportForms.Tables[7].Rows.Count > 0)
                                            {
                                                if (dsImportForms.Tables[7].Rows[0][0].ToString() != "")
                                                {
                                                    if (dsImportForms.Tables[7].Rows[0][0].ToString() != "0")
                                                    {
                                                        for (int n = 0; n < dsImportForms.Tables[7].Rows.Count; n++)
                                                        {
                                                            if (dtRegSection.Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dtRegSection.Rows[j]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString())
                                                            {
                                                                //store comma separated select list val for field
                                                                string strConSelectLstVal = string.Empty;
                                                                if (dsImportForms.Tables.Count > 8)
                                                                {
                                                                    if (dsImportForms.Tables[8].Rows[0][0].ToString() != "0")
                                                                    {
                                                                        for (int l = 0; l < dsImportForms.Tables[8].Rows.Count; l++)
                                                                        {
                                                                            if (dsImportForms.Tables[8].Rows[l]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dsImportForms.Tables[8].Rows[l]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[8].Rows[l]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString())
                                                                            {
                                                                                if (strConSelectLstVal == "")
                                                                                    strConSelectLstVal = dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                                else
                                                                                    strConSelectLstVal = strConSelectLstVal + ";" + dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                                                string strConBusRuleIdVal = string.Empty;
                                                                if (dsImportForms.Tables.Count > 9)
                                                                {
                                                                    if (dsImportForms.Tables[9].Rows[0][0].ToString() != "0")
                                                                    {
                                                                        for (int z = 0; z < dsImportForms.Tables[9].Rows.Count; z++)
                                                                        {
                                                                            if (dsImportForms.Tables[9].Rows[z]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[9].Rows[z]["Predefined"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString())
                                                                            {
                                                                                if (strConBusRuleIdVal == "")
                                                                                    strConBusRuleIdVal = dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                                else
                                                                                    strConBusRuleIdVal = strConBusRuleIdVal + "," + dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                if (dtlnkField.Rows[x]["fieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["FieldId"].ToString() && dtlnkField.Rows[x]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString() && dtlnkField.Rows[x]["featureId"].ToString() == dsImportForms.Tables[7].Rows[n]["featureId"].ToString())
                                                                {
                                                                    ClsUtility.Init_Hashtable();
                                                                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                                                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                                                    ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                                    ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["FieldName"].ToString());
                                                                    ClsUtility.AddParameters("@ConFieldId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString());
                                                                    ClsUtility.AddParameters("@ConFieldName", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                    ClsUtility.AddParameters("@ConFieldLabel", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldLabel"].ToString());
                                                                    ClsUtility.AddParameters("@ControlId", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ControlId"].ToString());
                                                                    ClsUtility.AddParameters("@ConControlId", SqlDbType.Int, (dsImportForms.Tables[7].Columns.Contains("ConditionalFieldControlId")) ? dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString() : "");
                                                                    ClsUtility.AddParameters("@ConSelectListVal", SqlDbType.VarChar, strConSelectLstVal);
                                                                    ClsUtility.AddParameters("@ConBusRuleIdValAll", SqlDbType.VarChar, strConBusRuleIdVal);
                                                                    ClsUtility.AddParameters("@ConSeq", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSequence"].ToString());
                                                                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                                                    ClsUtility.AddParameters("@ConPredefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                    ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["fieldpredefined"].ToString());
                                                                    ClsUtility.AddParameters("@ConSectionId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString());
                                                                    ClsUtility.AddParameters("@ModdecodeName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["Mod"].ToString());
                                                                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                                                                    //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                                    if (iNewFeatureId == 126)
                                                                    {
                                                                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportRegistrationConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                                    }
                                                                    else
                                                                    {
                                                                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                                    }
                                                                    iNewConFieldId = System.Convert.ToInt32(theDR[0].ToString());

                                                                    if (dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString() != "1")
                                                                    {
                                                                        ClsUtility.Init_Hashtable();
                                                                        ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                                                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                        ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString());
                                                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewConFieldId.ToString());
                                                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }//0 closed
                                            }
                                        }

                                        #endregion "Update/Insert Conditional Fields"
                                    }//feature id and section id if condition closes here
                                }

                                #endregion "Update/Insert Fields"
                            }
                        }

                        #endregion "Import Patient Registration"
                    }
                    else
                    {
                        //Inserting New tabs for Feature
                        for (int tab = 0; tab < dsImportForms.Tables[12].Rows.Count; tab++)
                        {
                            if (dsImportForms.Tables[12].Rows[tab]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString())
                            {
                                ClsUtility.Init_Hashtable();
                                ClsUtility.AddParameters("@TabId", SqlDbType.Int, dsImportForms.Tables[12].Rows[tab]["TabId"].ToString());
                                ClsUtility.AddParameters("@TabName", SqlDbType.VarChar, dsImportForms.Tables[12].Rows[tab]["TabName"].ToString());
                                ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[12].Rows[tab]["Seq"].ToString());
                                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[12].Rows[tab]["DeleteFlag"].ToString());
                                ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());

                                theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportTabs_Futures", ClsUtility.ObjectEnum.DataRow);
                                iNewTabId = System.Convert.ToInt32(theDR[0].ToString());
                            }
                            //foreach (DataRow drFormData in dsSaveFormData.Tables[1])
                            for (int j = 0; j < dsImportForms.Tables[1].Rows.Count; j++)
                            {
                                if (dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString())
                                {
                                    ClsUtility.Init_Hashtable();
                                    ClsUtility.AddParameters("@SectionId", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["SectionId"].ToString());
                                    ClsUtility.AddParameters("@SectionName", SqlDbType.VarChar, dsImportForms.Tables[1].Rows[j]["SectionName"].ToString());
                                    ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["Seq"].ToString());
                                    ClsUtility.AddParameters("@CustomFlag", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["CustomFlag"].ToString());
                                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["DeleteFlag"].ToString());
                                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                    //ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString());
                                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                    ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, (dsImportForms.Tables[1].Columns.Contains("IsGridView")) ? dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString() : "0");
                                    // ClsUtility.AddParameters("@IsGridView", SqlDbType.Int, dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString());
                                    theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportSection_Futures", ClsUtility.ObjectEnum.DataRow);
                                    iNewSectionId = System.Convert.ToInt32(theDR[0].ToString());
                                    for (int ts = 0; ts < dsImportForms.Tables[13].Rows.Count; ts++)
                                    {
                                        if (dsImportForms.Tables[13].Rows[ts]["SectionId"].ToString() == dsImportForms.Tables[1].Rows[j]["SectionId"].ToString() && dsImportForms.Tables[12].Rows[tab]["TabId"].ToString() == dsImportForms.Tables[13].Rows[ts]["TabId"].ToString())
                                        {
                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@TabId", SqlDbType.Int, iNewTabId.ToString());
                                            ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                            theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkFormTabSection_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                        }
                                    }

                                    #region "Update/Insert Fields"

                                    for (int x = 0; x < dsImportForms.Tables[2].Rows.Count; x++)
                                    {
                                        if (dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[2].Rows[x]["FeatureId"].ToString() && dsImportForms.Tables[1].Rows[j]["SectionId"].ToString() == dsImportForms.Tables[2].Rows[x]["SectionId"].ToString())
                                        {
                                            //store comma separated select list val for field

                                           // string strSelectLstVal = string.Empty;
                                            DataTable tableSelectList = new DataTable("selectList");
                                            tableSelectList.Columns.Add(new DataColumn("option", Type.GetType("System.String")));
                                            tableSelectList.AcceptChanges();
                                            if (dsImportForms.Tables.Count > 3)
                                            {
                                                if (dsImportForms.Tables[3].Rows[0][0].ToString() != "0")
                                                {
                                                    for (int l = 0; l < dsImportForms.Tables[3].Rows.Count; l++)
                                                    {
                                                        if (dsImportForms.Tables[3].Rows[l]["FeatureId"].ToString() == dsImportForms.Tables[2].Rows[x]["FeatureId"].ToString() && dsImportForms.Tables[3].Rows[l]["FieldId"].ToString() == dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() && dsImportForms.Tables[3].Rows[l]["SectionId"].ToString() == dsImportForms.Tables[2].Rows[x]["SectionId"].ToString())
                                                        {
                                                            tableSelectList.Rows.Add(new object[] { dsImportForms.Tables[3].Rows[l]["ListVal"].ToString() });
                                                            //if (strSelectLstVal == "")
                                                            //    strSelectLstVal = string.Format("<value>{0}</value>", dsImportForms.Tables[3].Rows[l]["ListVal"].ToString());
                                                            //else
                                                            //    strSelectLstVal = string.Format("{0}<value>{1}</value>", strSelectLstVal, dsImportForms.Tables[3].Rows[l]["ListVal"].ToString());
                                                            //        // +";" + dsImportForms.Tables[3].Rows[l]["ListVal"].ToString();
                                                        }
                                                    }
                                                }
                                            }

                                            //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                            string strBusRuleIdVal = string.Empty;
                                            if (dsImportForms.Tables.Count > 4)
                                            {
                                                if (dsImportForms.Tables[4].Rows[0][0].ToString() != "0")
                                                {
                                                    for (int m = 0; m < dsImportForms.Tables[4].Rows.Count; m++)
                                                    {
                                                        if (dsImportForms.Tables[4].Rows[m]["FieldId"].ToString() == dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() && dsImportForms.Tables[4].Rows[m]["Predefined"].ToString() == dsImportForms.Tables[2].Rows[x]["Predefined"].ToString())
                                                        {
                                                            if (strBusRuleIdVal == "")
                                                                strBusRuleIdVal = dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                            else
                                                                strBusRuleIdVal = strBusRuleIdVal + "," + dsImportForms.Tables[4].Rows[m]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[4].Rows[m]["Value"].ToString() != "") ? dsImportForms.Tables[4].Rows[m]["Value"].ToString() : "Null");
                                                        }
                                                    }
                                                }
                                            }

                                            ClsUtility.Init_Hashtable();
                                            ClsUtility.AddParameters("@Id", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Id"].ToString());
                                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                            ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                            ClsUtility.AddParameters("@FieldName", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldName"].ToString());
                                            ClsUtility.AddParameters("@FieldLabel", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[x]["FieldLabel"].ToString());
                                            ClsUtility.AddParameters("@ControlId", SqlDbType.Int, (dsImportForms.Tables[2].Columns.Contains("ControlId")) ? dsImportForms.Tables[2].Rows[x]["ControlId"].ToString() : "");
                                            string xmlString = string.Empty;
                                            using (TextWriter writer = new StringWriter())
                                            {
                                                tableSelectList.WriteXml(writer);
                                                xmlString = writer.ToString();
                                            } 
                                            ClsUtility.AddParameters("@SelectListVal", SqlDbType.Xml, xmlString);
                                            ClsUtility.AddParameters("@BusRuleIdValAll", SqlDbType.VarChar, strBusRuleIdVal);
                                            ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Seq"].ToString());
                                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Predefined"].ToString());
                                            if (iNewFeatureId == 126)
                                            {
                                                ClsUtility.AddParameters("@PatientRegistration", SqlDbType.Int, "1");
                                            }
                                            //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                            DataTable theFieldDT = (DataTable)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.DataTable);
                                            if (theFieldDT.Rows.Count > 1)
                                            {
                                                DataView dvFieldDV = new DataView();
                                                dvFieldDV = theFieldDT.DefaultView;
                                                DataTable dtField = new DataTable();
                                                dvFieldDV.RowFilter = "FormFieldID <>'0'";
                                                dtField = dvFieldDV.ToTable();
                                                iNewFieldId = System.Convert.ToInt32(dtField.Rows[0][0].ToString());
                                            }
                                            else
                                                iNewFieldId = System.Convert.ToInt32(theFieldDT.Rows[0][0].ToString());
                                            if (dsImportForms.Tables[2].Rows[x]["Predefined"].ToString() != "1")
                                            {
                                                ClsUtility.Init_Hashtable();
                                                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[x]["FieldName"].ToString());
                                                ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["ControlId"].ToString());
                                                ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["Predefined"].ToString());
                                                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                                theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                            }
                                            ///////Import GridView Control-Create Table//////////////////////////////////

                                            if (dsImportForms.Tables[1].Rows[j]["IsGridView"].ToString() == "1" && dsImportForms.Tables[2].Rows[x]["Predefined"].ToString() != "1")
                                            {
                                                string strTableNameSection = "DTL_CUSTOMFORM_" + dsImportForms.Tables[1].Rows[j]["SectionName"].ToString() + "_" + strgridFeaturename;
                                                ClsUtility.Init_Hashtable();
                                                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableNameSection);
                                                ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[x]["FieldName"].ToString());
                                                ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["ControlId"].ToString());
                                                ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                                theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreationGridView_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                            }

                                            ////////Import Feild ICDCode Linking////////////////////////////////////////////
                                            if ((dsImportForms.Tables[11].Rows[0].ItemArray[0].ToString() != "0") && (dsImportForms.Tables[2].Rows[x]["ControlId"].ToString() == "16"))
                                            {
                                                DataView dvFilteredRow = new DataView();
                                                dvFilteredRow = dsImportForms.Tables[11].DefaultView;
                                                DataTable dtRow = new DataTable();
                                                dvFilteredRow.RowFilter = "FieldId='" + dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() + "'";
                                                dtRow = dvFilteredRow.ToTable();
                                                if (dtRow.Rows.Count > 0)
                                                {
                                                    if (dsImportForms.Tables[2].Rows[x]["FieldId"].ToString() == iNewFieldId.ToString())
                                                    {
                                                        ClsUtility.Init_Hashtable();
                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, dsImportForms.Tables[2].Rows[x]["FieldId"].ToString());
                                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "pr_FormBuilder_DeleteFieldICDCode_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                    }
                                                    for (int q = 0; q < dtRow.Rows.Count; q++)
                                                    {
                                                        ClsUtility.Init_Hashtable();
                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                        ClsUtility.AddParameters("@BlockId", SqlDbType.Int, dtRow.Rows[q]["BlockId"].ToString());
                                                        ClsUtility.AddParameters("@SubBlockId", SqlDbType.Int, dtRow.Rows[q]["SubBlockId"].ToString());
                                                        ClsUtility.AddParameters("@CodeId", SqlDbType.Int, dtRow.Rows[q]["CodeId"].ToString());
                                                        ClsUtility.AddParameters("@UserId", SqlDbType.Int, dtRow.Rows[q]["UserId"].ToString());
                                                        ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, "0");
                                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dtRow.Rows[q]["Predefined"].ToString());
                                                        theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_SaveICD10CodeItems_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                    }
                                                }
                                            }
                                            ///////////////////////////////////////////////////

                                            #region "Update/Insert Conditional Fields"

                                            if (dsImportForms.Tables.Count > 7)
                                            {
                                                if (dsImportForms.Tables[7].Rows.Count > 0)
                                                {
                                                    if (dsImportForms.Tables[7].Rows[0][0].ToString() != "")
                                                    {
                                                        if (dsImportForms.Tables[7].Rows[0][0].ToString() != "0")
                                                        {
                                                            for (int n = 0; n < dsImportForms.Tables[7].Rows.Count; n++)
                                                            {
                                                                if (dsImportForms.Tables[1].Rows[j]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dsImportForms.Tables[1].Rows[j]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString())
                                                                {
                                                                    //store comma separated select list val for field
                                                                    string strConSelectLstVal = string.Empty;
                                                                    if (dsImportForms.Tables.Count > 8)
                                                                    {
                                                                        if (dsImportForms.Tables[8].Rows[0][0].ToString() != "0")
                                                                        {
                                                                            for (int l = 0; l < dsImportForms.Tables[8].Rows.Count; l++)
                                                                            {
                                                                                if (dsImportForms.Tables[8].Rows[l]["FeatureId"].ToString() == dsImportForms.Tables[7].Rows[n]["FeatureId"].ToString() && dsImportForms.Tables[8].Rows[l]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[8].Rows[l]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString())
                                                                                {
                                                                                    if (strConSelectLstVal == "")
                                                                                        strConSelectLstVal = dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                                    else
                                                                                        strConSelectLstVal = strConSelectLstVal + ";" + dsImportForms.Tables[8].Rows[l]["ListVal"].ToString();
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    //busrule id and val comma separated value, e.g. BusRuleId-Value(val used in case of min and max)
                                                                    string strConBusRuleIdVal = string.Empty;
                                                                    if (dsImportForms.Tables.Count > 9)
                                                                    {
                                                                        if (dsImportForms.Tables[9].Rows[0][0].ToString() != "0")
                                                                        {
                                                                            for (int z = 0; z < dsImportForms.Tables[9].Rows.Count; z++)
                                                                            {
                                                                                if (dsImportForms.Tables[9].Rows[z]["FieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString() && dsImportForms.Tables[9].Rows[z]["Predefined"].ToString() == dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString())
                                                                                {
                                                                                    if (strConBusRuleIdVal == "")
                                                                                        strConBusRuleIdVal = dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[4].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                                    else
                                                                                        strConBusRuleIdVal = strConBusRuleIdVal + "," + dsImportForms.Tables[9].Rows[z]["BusRuleId"].ToString() + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value") && dsImportForms.Tables[9].Rows[z]["Value"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value"].ToString() : "Null") + "-" + ((dsImportForms.Tables[9].Columns.Contains("Value1") && dsImportForms.Tables[9].Rows[z]["Value1"].ToString() != "") ? dsImportForms.Tables[9].Rows[z]["Value1"].ToString() : "Null");
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    if (dsImportForms.Tables[2].Rows[x]["fieldId"].ToString() == dsImportForms.Tables[7].Rows[n]["FieldId"].ToString() && dsImportForms.Tables[2].Rows[x]["SectionId"].ToString() == dsImportForms.Tables[7].Rows[n]["SectionId"].ToString() && dsImportForms.Tables[2].Rows[x]["featureId"].ToString() == dsImportForms.Tables[7].Rows[n]["featureId"].ToString())
                                                                    {
                                                                        ClsUtility.Init_Hashtable();
                                                                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                                                                        ClsUtility.AddParameters("@SectionId", SqlDbType.Int, iNewSectionId.ToString());
                                                                        ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewFieldId.ToString());
                                                                        ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["FieldName"].ToString());
                                                                        ClsUtility.AddParameters("@ConFieldId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldId"].ToString());
                                                                        ClsUtility.AddParameters("@ConFieldName", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                        ClsUtility.AddParameters("@ConFieldLabel", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldLabel"].ToString());
                                                                        ClsUtility.AddParameters("@ControlId", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ControlId"].ToString());
                                                                        ClsUtility.AddParameters("@ConControlId", SqlDbType.Int, (dsImportForms.Tables[7].Columns.Contains("ConditionalFieldControlId")) ? dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString() : "");
                                                                        ClsUtility.AddParameters("@ConSelectListVal", SqlDbType.VarChar, strConSelectLstVal);
                                                                        ClsUtility.AddParameters("@ConBusRuleIdValAll", SqlDbType.VarChar, strConBusRuleIdVal);
                                                                        ClsUtility.AddParameters("@ConSeq", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSequence"].ToString());
                                                                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                                                                        ClsUtility.AddParameters("@ConPredefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                        ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["fieldpredefined"].ToString());
                                                                        ClsUtility.AddParameters("@ConSectionId", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldSectionId"].ToString());
                                                                        ClsUtility.AddParameters("@ModdecodeName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["Mod"].ToString());
                                                                        ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                                                                        //theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportLnkForm_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                                        if (iNewFeatureId == 126)
                                                                        {
                                                                            theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportRegistrationConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                                        }
                                                                        else
                                                                        {
                                                                            theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportConditionalField_Futures", ClsUtility.ObjectEnum.DataRow);
                                                                        }
                                                                        iNewConFieldId = System.Convert.ToInt32(theDR[0].ToString());

                                                                        if (dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString() != "1")
                                                                        {
                                                                            ClsUtility.Init_Hashtable();
                                                                            ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, strTableName);
                                                                            ClsUtility.AddParameters("@FieldName", SqlDbType.VarChar, dsImportForms.Tables[7].Rows[n]["ConditionalFieldName"].ToString());
                                                                            ClsUtility.AddParameters("@DataType", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldControlId"].ToString());
                                                                            ClsUtility.AddParameters("@Predefined", SqlDbType.Int, dsImportForms.Tables[7].Rows[n]["ConditionalFieldPredefined"].ToString());
                                                                            ClsUtility.AddParameters("@FieldId", SqlDbType.Int, iNewConFieldId.ToString());
                                                                            theRowAffected = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_FormBuilder_CustomTableCreation_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }//0 closed
                                                }
                                            }

                                            #endregion "Update/Insert Conditional Fields"
                                        }//feature id and section id if condition closes here
                                    }

                                    #endregion "Update/Insert Fields"
                                }
                            }
                        }
                    }

                    //john - special form linking
                    for (int k = 0; k < dsImportForms.Tables[14].Rows.Count; k++)
                    {
                        if (dsImportForms.Tables[14].Rows[k]["moduleid"].ToString() != "0" && dsImportForms.Tables[14].Rows[k]["featureid"].ToString() != "0")
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[14].Rows[k]["moduleid"].ToString());
                            ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[14].Rows[k]["featureid"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                            ClsUtility.AddParameters("@ModuleName", SqlDbType.VarChar, dsImportForms.Tables[14].Rows[k]["modulename"].ToString());
                            theRowAffected_SpLnkForms = (int)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_spLnkForms_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                //return iFeatureId;
                return 1;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
      
       */
       //import home forms
        /// <summary>
        /// Imports the home forms.
        /// </summary>
        /// <param name="dsImportForms">The ds import forms.</param>
        /// <param name="iUserId">The i user identifier.</param>
        /// <param name="iCountryId">The i country identifier.</param>
        /// <returns></returns>
        public int ImportHomeForms(DataSet dsImportForms, int iUserId, int iCountryId)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject FormDetail = new ClsObject();
                FormDetail.Connection = this.Connection;
                FormDetail.Transaction = this.Transaction;

                DataRow theDR;

                int iNewFeatureId; //this variable will be used to store featureid for all new rows

                string strTableName = string.Empty;

                for (int i = 0; i < dsImportForms.Tables[0].Rows.Count; i++)
                {
                    //string[] strFeatureName = new string[10];
                    //strFeatureName = dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString().Split(' ');
                    //strTableName = "";
                    //for (int j = 0; j < strFeatureName.Length; j++)
                    //{
                    //    if (j > 0)
                    //        strTableName += "_" + strFeatureName[j];
                    //    else
                    //        strTableName += strFeatureName[j];

                    //}
                    //strTableName = "DTL_FBCUSTOMFIELD_" + strTableName;

                    //save mst_feature data
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["FeatureId"].ToString());
                    ClsUtility.AddParameters("@FeatureName", SqlDbType.VarChar, dsImportForms.Tables[0].Rows[i]["FeatureName"].ToString());
                    ClsUtility.AddParameters("@ReportFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["ReportFlag"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["DeleteFlag"].ToString());
                    ClsUtility.AddParameters("@AdminFlag", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["AdminFlag"].ToString());
                    //ClsUtility.AddParameters("@UserID", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["UserID"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                    ClsUtility.AddParameters("@OptionalFlag", SqlDbType.Int, (dsImportForms.Tables[0].Columns.Contains("OptionalFlag")) ? dsImportForms.Tables[0].Rows[i]["OptionalFlag"].ToString() : "");
                    ClsUtility.AddParameters("@SystemId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["SystemId"].ToString());
                    ClsUtility.AddParameters("@Published", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["Published"].ToString());
                    //ClsUtility.AddParameters("@CountryId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["CountryId"].ToString());
                    ClsUtility.AddParameters("@CountryId", SqlDbType.Int, iCountryId.ToString());
                    ClsUtility.AddParameters("@ModuleId", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["ModuleId"].ToString());
                    if (dsImportForms.Tables[0].Columns.Contains("MultiVisit"))
                        ClsUtility.AddParameters("@MultiVisit", SqlDbType.Int, dsImportForms.Tables[0].Rows[i]["MultiVisit"].ToString());

                    theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportHomeForm_Futures", ClsUtility.ObjectEnum.DataRow);
                    iNewFeatureId = System.Convert.ToInt32(theDR[0].ToString());

                    //for home page and its dtl
                    //
                    for (int j = 0; j < dsImportForms.Tables[1].Rows.Count; j++)
                    {
                        int iHomePageId;
                        ClsUtility.Init_Hashtable();

                        ClsUtility.AddParameters("@HomePageName", SqlDbType.VarChar, dsImportForms.Tables[1].Rows[j]["Name"].ToString());
                        ClsUtility.AddParameters("@FeatureId", SqlDbType.Int, iNewFeatureId.ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, iUserId.ToString());
                        theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportMstHomePage_Futures", ClsUtility.ObjectEnum.DataRow);
                        iHomePageId = System.Convert.ToInt32(theDR[0].ToString());

                        for (int k = 0; k < dsImportForms.Tables[2].Rows.Count; k++)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@HomePageId", SqlDbType.Int, iHomePageId.ToString());
                            ClsUtility.AddParameters("@IndicatorName", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[k]["IndicatorName"].ToString());
                            if (dsImportForms.Tables[2].Columns.Contains("Query"))
                                ClsUtility.AddParameters("@Query", SqlDbType.VarChar, dsImportForms.Tables[2].Rows[k]["Query"].ToString());

                            ClsUtility.AddParameters("@Seq", SqlDbType.Int, dsImportForms.Tables[2].Rows[k]["Seq"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, iUserId.ToString());
                            theDR = (DataRow)FormDetail.ReturnObject(ClsUtility.theParams, "Pr_ImportExportForms_ImportdtlHomePage_Futures", ClsUtility.ObjectEnum.DataRow);
                            //iNewModuleId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    }
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                //return iFeatureId;
                return 1;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
    }
}