using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;
using System.IO;


namespace IQCare.FormBuilder
{
    public partial class frmRptFieldValidations : Form
    {
        IRptFieldValidations objRptFields;
        DataSet dsRptReportValidation = new DataSet();
        DataTable objTable = new DataTable();
        DataTable dtmodules = new DataTable();
        DataSet dsReportFileld = new DataSet();
        DataTable dtModfiltertab;
        DataTable dtRpfiltertab;
       // string Query;
        public frmRptFieldValidations()
        {
            InitializeComponent();
        }

        private void frmRptFieldValidations_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            objRptFields = (IRptFieldValidations)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BRptFieldValidations,BusinessProcess.FormBuilder");
            DataSet dsRptFielddetails = objRptFields.GetRptFieldDetails();
           
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbTechnicalArea, dsRptFielddetails.Tables[0], "ModuleName", "ModuleId");
            
            LoadTreeView(0);
            
            
        }

        private void LoadTreeView(int ModuleId)
        {
            tvCustomlist.Nodes.Clear();
            tvCustomlist.ShowLines = true;
            

            DataTable theRptDT;
            DataTable objTable ;
            //DataTable dtfiltertab;
            DataTable dtDetail;
          //  DataTable dtData ;
            TreeNode root = new TreeNode();
            TreeNode theChildRoot;
            int theImageIndex;
            
            string strImageFilePath = GblIQCare.GetPath();

            // Load the images in an ImageList.
            ImageList myImageList = new ImageList();
            myImageList.Images.Add(Image.FromFile(strImageFilePath + "\\blank.gif"));
            myImageList.Images.Add(Image.FromFile(strImageFilePath + "\\15px-Yes_check.svg.png"));
            myImageList.Images.Add(Image.FromFile(strImageFilePath + "\\No_16x.ico"));
            tvCustomlist.ImageList = myImageList;

            string strXmlFilePath = GblIQCare.GetFieldvalidationReportPath();
            string XmlFile = strXmlFilePath + "rptFieldValidationXml.xml";

            dsRptReportValidation = new DataSet();
            dsRptReportValidation.ReadXml(XmlFile);
            theRptDT = dsRptReportValidation.Tables[0];
            objTable = dsRptReportValidation.Tables[1];

            
            string strRpName = string.Empty;
            string strfilterRpName = string.Empty;
            //if (ModuleId > 0)
            //{
            //    DataView theDVModName = new DataView(objTable);
            //    theDVModName.RowFilter = "ModuleId ='" + Convert.ToString(ModuleId) + "'";
            //    theDVModName.Sort = "ListName asc";
            //    dtModfiltertab = theDVModName.ToTable();
            //    if (dtModfiltertab.Rows.Count > 0)
            //    {
            //        foreach (DataRow rpname in dtModfiltertab.Rows)
            //        {
            //            strRpName += "'" + rpname["ReportName"].ToString() + "',";

            //        }
            //        strfilterRpName = strRpName.Remove(strRpName.Length - 1);
            //        DataView theDVRpName = new DataView(theRptDT);
            //        theDVRpName.RowFilter = "ReportName in (" + Convert.ToString(strfilterRpName) + ")";
            //        //theDVRpName.Sort = "ListName asc";
            //        dtRpfiltertab = theDVRpName.ToTable();
            //    }
            //}
            //else
            //{
                dtModfiltertab = objTable;
                dtRpfiltertab = theRptDT;
            //}

            MemoryStream s = new MemoryStream();
            dtModfiltertab.WriteXml(s, true);

            //Retrieve the text from the stream
            s.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(s);
            string xmlString = null;
            xmlString = sr.ReadToEnd();

            //close
            sr.Close();
            sr.Dispose();
            objRptFields = (IRptFieldValidations)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BRptFieldValidations,BusinessProcess.FormBuilder");
            DataTable dtxml = objRptFields.ParseXml(xmlString,Convert.ToInt32(GblIQCare.AppLocationId),Convert.ToInt32(ModuleId));
            if (Convert.ToInt32(ModuleId) > 0)
            {
                DataView theDVRptName1 = new DataView(dtxml);
                theDVRptName1.RowFilter = "ModuleID IN(0," + Convert.ToInt32(ModuleId) + ")";
                theDVRptName1.Sort = "FieldName asc";
                dtxml = theDVRptName1.ToTable();
            }
            if (dtRpfiltertab.Rows.Count > 0)
            {
                foreach (DataRow drrptname in dtRpfiltertab.Rows)
                {

                    bool flagroot = true;

                    if (flagroot)
                    {
                        root.Expand();
                        flagroot = false;

                    }
                    else
                    {
                        //root.Expand();
                        root.Collapse();
                    }
                    root = tvCustomlist.Nodes.Add(drrptname["ReportName"].ToString());

                    DataView theDVRptName = new DataView(dtxml);
                    theDVRptName.RowFilter = "ReportName ='" + Convert.ToString(drrptname["ReportName"]) + "'";
                    theDVRptName.Sort = "FieldName asc";
                    dtDetail = theDVRptName.ToTable();

                    foreach (DataRow theDR in dtDetail.Rows)
                    {
                        //Query = "select " + theDR["ListName"].ToString() + " from " + theDR["TableName"].ToString() + " where " + theDR["ListName"].ToString() + " IS NOT NULL";
                        //dtData = objRptFields.ReturnDatatableQueryResult(Query);
                        if (Convert.ToInt32(theDR["Status"]) > 0)
                        {
                            theImageIndex = 1;
                        }
                        else
                            theImageIndex = 2;
                        theChildRoot = new TreeNode();
                        theChildRoot.Text = theDR["FieldName"].ToString();
                        theChildRoot.ImageIndex = theImageIndex;
                        root.Nodes.Add(theChildRoot);
                    }


                }
            }

        }

        private void cmbTechnicalArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadTreeView(Convert.ToInt32(cmbTechnicalArea.SelectedIndex));

        }

        

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTechnicalArea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadTreeView(Convert.ToInt32(cmbTechnicalArea.SelectedIndex));
        }

       


    }
}

