using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace IQCare.FormBuilder
{
    public class clsCommon
    {

        #region "Public Declarations"
        #endregion
        /// <summary>
        /// this function is used to clear the grid rows column and value
        /// </summary>
        public void ClearGrid(DataGridView dgwDataGrid)
        {
            dgwDataGrid.Columns.Clear();
            dgwDataGrid.Rows.Clear();
            dgwDataGrid.Refresh();
        }

        /// <summary>
        /// this function is used to create datatable for mst_feature on runtime 
        /// </summary>
        /// <returns></returns>
        public static DataTable RemoveFieldLnk_Forms()
        {

            DataTable dtRemoveFieldLnk_Forms = new DataTable();
            DataColumn dtDataColumn;
            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "Id";
            dtRemoveFieldLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "FieldName";
            dtRemoveFieldLnk_Forms.Columns.Add(dtDataColumn);

            return dtRemoveFieldLnk_Forms;
        }
        public static DataTable RemoveFieldLnkGridView_Forms()
        {

            DataTable dtRemoveFieldLnk_Forms = new DataTable();
            DataColumn dtDataColumn;
            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "Id";
            dtRemoveFieldLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "FieldName";
            dtRemoveFieldLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "SectionName";
            dtRemoveFieldLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "IsGridView";
            dtRemoveFieldLnk_Forms.Columns.Add(dtDataColumn);

            return dtRemoveFieldLnk_Forms;
        }

        /// <summary>
        /// this function is used to create datatable for mst_feature on runtime 
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateTableMstFeature()
        {

            DataTable dtMst_Feature = new DataTable();
            DataTable dtLnk_Forms = new DataTable();
            DataColumn dtDataColumn;

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "FeatureId";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "FeatureName";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "FormDescription";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "ReportFlag";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "DeleteFlag";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "AdminFlag";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "UserId";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "OptionalFlag";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "SystemId";
            dtMst_Feature.Columns.Add(dtDataColumn);


            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "Published";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "CountryId";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "ModuleId";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "MultiVisit";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "InsertUpdateStatus";
            dtMst_Feature.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "Seq";
            dtMst_Feature.Columns.Add(dtDataColumn);

            return dtMst_Feature;
        }

        /// <summary>
        /// this function is used to create datatable for Lnk_Forms on runtime 
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateTableMstSection()
        {

            DataTable dtMstSection = new DataTable();
            DataColumn dtDataColumn;

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "SectionId";
            dtMstSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "SectionName";
            dtMstSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "SectionInfo";
            dtMstSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "Sequence";
            dtMstSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "CustomFlag";
            dtMstSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "DeleteFlag";
            dtMstSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "UserId";
            dtMstSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "FeatureId";
            dtMstSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "InsertUpdateStatus";
            dtMstSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "IsGridView";
            dtMstSection.Columns.Add(dtDataColumn);

            return dtMstSection;
        }

        /// <summary>
        /// this function is used to create datatable for Lnk_Forms on runtime 
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateTableLnk_Forms()
        {

            DataTable dtLnk_Forms = new DataTable();
            DataColumn dtDataColumn;

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "Id"; //FieldId
            dtLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "FeatureId";
            dtLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "SectionId";
            dtLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "FieldId";
            dtLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "FieldName";
            dtLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "FieldLabel";
            dtLnk_Forms.Columns.Add(dtDataColumn);


            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "Sequence";
            dtLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "UserId";
            dtLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "Predefined";
            dtLnk_Forms.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "InsertUpdateStatus";
            dtLnk_Forms.Columns.Add(dtDataColumn);

       

            return dtLnk_Forms;
        }
        public static void CreateGridColumnsForFormBuilder(DataGridView dgwDataGrid, DataSet dsBindColumns)
        {
            DataGridViewComboBoxColumn ddlDgwFieldsName = new DataGridViewComboBoxColumn();


            DataTable dt;
            
            DataView dvActiveFields = dsBindColumns.Tables[0].DefaultView;
            dvActiveFields.RowFilter = "DeleteFlag=0";
            dvActiveFields.Sort = "FieldName";

            dt = dvActiveFields.ToTable();
            DataRow drAddSelect;
            drAddSelect = dt.NewRow();
            drAddSelect["Id"] = 0;
            drAddSelect["FieldName"] = "Select";
            drAddSelect["DeleteFlag"] = 0;
            dt.Rows.InsertAt(drAddSelect, 0);

            
            //ddlDgwFieldsName.DataSource = dsBindColumns.Tables[0];
            //ddlDgwFieldsName.DataSource = dvActiveFields.ToTable();
            ddlDgwFieldsName.DataSource = dt;
            ddlDgwFieldsName.DisplayMember = "FieldName";
            ddlDgwFieldsName.ValueMember = "FieldName";
            ddlDgwFieldsName.DataPropertyName = "FieldName";
            ddlDgwFieldsName.DefaultCellStyle.NullValue = "Select";

            DataGridViewTextBoxColumn txtFieldLabel = new DataGridViewTextBoxColumn();
            txtFieldLabel.MaxInputLength = 100;
            

            //dgwDataGrid.AllowUserToAddRows = true ; //need to set true when required a blank row
            //dgwDataGrid.Columns.AddRange(ddlDgwFieldsName, new DataGridViewTextBoxColumn(), new DataGridViewTextBoxColumn(), new DataGridViewImageColumn(), new DataGridViewImageColumn(), new DataGridViewTextBoxColumn(), new DataGridViewTextBoxColumn(), new DataGridViewTextBoxColumn(), new DataGridViewTextBoxColumn());
            dgwDataGrid.Columns.AddRange(ddlDgwFieldsName
                , txtFieldLabel
                , new DataGridViewTextBoxColumn()
                , new DataGridViewImageColumn()
                , new DataGridViewImageColumn()
                , new DataGridViewTextBoxColumn()
                , new DataGridViewTextBoxColumn()
                , new DataGridViewTextBoxColumn()
                , new DataGridViewTextBoxColumn()
             );
            //dgwDataGrid.Columns.AddRange();
            dgwDataGrid.Columns[0].Name = "Table Field Name";
            dgwDataGrid.Columns[0].Width = 300;
            dgwDataGrid.Columns[1].Name = "Field Label";
            dgwDataGrid.Columns[1].Width = 300;
            dgwDataGrid.Columns[2].Name = "Display Type";
            dgwDataGrid.Columns[2].Width = 100;
            dgwDataGrid.Columns[2].ReadOnly = true;
            //dgwDataGrid.Columns[2] = true;
            dgwDataGrid.Columns[3].Name = "List";
            dgwDataGrid.Columns[3].Width = 50;
            dgwDataGrid.Columns[4].Name = "Business Rule";
            dgwDataGrid.Columns[4].Width = 85;
            dgwDataGrid.Columns[5].Name = "ID"; // id of lnk_forms table
            dgwDataGrid.Columns[5].Visible = false;
            dgwDataGrid.Columns[6].Name = "ControlId"; //for display type
            dgwDataGrid.Columns[6].Visible = false;
            dgwDataGrid.Columns[7].Name = "Predefined"; //for predefined check
            dgwDataGrid.Columns[7].Visible = false;
            dgwDataGrid.Columns[8].Name = "FieldId"; //for predefined check
            dgwDataGrid.Columns[8].Visible = false;


        }

        /// <summary>
        /// This function is used to find out there is any business rules is associated with field or not
        /// </summary>
        /// <param name="fieldID"></param>
        /// <param name="dtbRule"></param>
        /// <returns></returns>
        public static bool CheckDataExists(int fieldID, DataTable dtData)
        {
            DataView dvCheckData = new DataView();
            dvCheckData = dtData.DefaultView;
            dvCheckData.RowFilter = "FieldId=" + fieldID;

            Boolean findData = false;
            if (dvCheckData.Count != 0)
            {
                findData = true;
            }


            return findData;
        }

        /// <summary>
        /// This function is used to find out there is any check list items is associated with field or not
        /// </summary>
        /// <param name="fieldID"></param>
        /// <param name="dtbRule"></param>
        /// <returns></returns>
        public static bool FetchSearchValue(string[] strSearchColumn, string[] strValSearchColumn, DataTable dtSourceTable)
        {
            DataView dvData = new DataView();
            dvData = dtSourceTable.DefaultView;
            string strSearch=string.Empty;
            for (int i = 0; i < strSearchColumn.Length; i++)
            {
                if(strSearch.Length>0)
                {
                    strSearch += " and " + strSearchColumn[i] + "='" + strValSearchColumn[i] + "'";
                }
                else
                {
                    strSearch = strSearchColumn[i] + "='" + strValSearchColumn[i] + "'";
                }
            }
            if (strSearch != "")
                dvData.RowFilter =strSearch;

            //return dvData.ToTable();
            Boolean findData = false;
            if (dvData.Count != 0)
            {
                findData = true;
            }


            return findData;

        }

        public static DataTable ManageSectionPos()
        {

            DataTable dtManageSection = new DataTable();
            DataColumn dtDataColumn;

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "SectionId";
            dtManageSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "DeleteFlag";
            dtManageSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "SectionName";
            dtManageSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "SectionInfo";
            dtManageSection.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "TopPos";
            dtManageSection.Columns.Add(dtDataColumn);

            return dtManageSection;
        }


        public static DataTable ManageTabPos()
        {

            DataTable dtManageTab = new DataTable();
            DataColumn dtDataColumn;

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "TabId";
            dtManageTab.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "DeleteFlag";
            dtManageTab.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "TabName";
            dtManageTab.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "TopPos";
            dtManageTab.Columns.Add(dtDataColumn);

            return dtManageTab;
        }


        public static DataTable ManageDeleteTab()
        {

            DataTable dtManageTab = new DataTable();
            DataColumn dtDataColumn;

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "TabId";
            dtManageTab.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.Int32");
            dtDataColumn.ColumnName = "DeleteFlag";
            dtManageTab.Columns.Add(dtDataColumn);

            dtDataColumn = new DataColumn();
            dtDataColumn.DataType = Type.GetType("System.String");
            dtDataColumn.ColumnName = "TabName";
            dtManageTab.Columns.Add(dtDataColumn);

           

            return dtManageTab;
        }
    }
}
