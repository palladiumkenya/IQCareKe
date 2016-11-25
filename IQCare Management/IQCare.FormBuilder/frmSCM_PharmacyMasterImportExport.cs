using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Threading;
using System.Xml;
using System.IO;
using Application.Common;
using Application.Presentation;
using Interface.Security;
using Interface.FormBuilder;

namespace IQCare.FormBuilder
{
    public partial class frmSCM_PharmacyMasterImportExport : Form
    {
        public frmSCM_PharmacyMasterImportExport()
        {
            InitializeComponent();
            lblmessage.Text = "";
        }

        

        private void btnexport_Click(object sender, EventArgs e)
        {
            if (!fnValidate())
            {
                IQCareWindowMsgBox.ShowWindow("SelectSCMList", this);
                return;
            }
            string dir = @"C:\SCMExport";
            IImportExport theExportManager = (IImportExport)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExport, BusinessProcess.FormBuilder");
            DataSet theDS = theExportManager.ExportSCMIQCare();

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            else
            {
                string[] filePaths = Directory.GetFiles(@"c:\SCMExport\");
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
            }
            theDS.WriteXml("C:\\SCMExport\\SCMMaster.xml", XmlWriteMode.WriteSchema);
            lblmessage.Text = "Exported file has saved in c:\\SCMExport\\SCMMaster.xml";
            exportItem();
            dropDBBackup();
          
            
        }
        public Boolean fnValidate()
        {
            Boolean blnselect = false;
            if (chk1.Checked)
            {
                blnselect = true;
            }
            if (chk2.Checked)
            {
                blnselect = true;
            }
            if (chk3.Checked)
            {
                blnselect = true;
            }
            if (chk4.Checked)
            {
                blnselect = true;
            }
            if (chk5.Checked)
            {
                blnselect = true;
            }
            if (chk6.Checked)
            {
                blnselect = true;
            }
            if (chk7.Checked)
            {
                blnselect = true;
            }
            if (chk8.Checked)
            {
                blnselect = true;
            }
            if (chk9.Checked)
            {
                blnselect = true;
            }
            if (chk10.Checked)
            {
                blnselect = true;
            }
            if (chk11.Checked)
            {
                blnselect = true;
            }
            if (chk12.Checked)
            {
                blnselect = true;
            }
            if (chk13.Checked)
            {
                blnselect = true;
            }
            if (chk14.Checked)
            {
                blnselect = true;
            }
            if (chk15.Checked)
            {
                blnselect = true;
            }
            if (chk16.Checked)
            {
                blnselect = true;
            }
            return blnselect;
        }
        public void dropDBBackup()
        {
            if (Directory.Exists(@"c:\IQCareSCMDBBackup"))
            {
                string[] filePaths = Directory.GetFiles(@"c:\IQCareSCMDBBackup\");
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
            }
        }
        public void exportItem()
        {
            string dir = @"C:\SCMExportFile";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            else
            {
                string[] filePaths = Directory.GetFiles(@"c:\SCMExportFile\");
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
            }
            DataTable dtExport = new DataTable();
            DataSet theDS = new DataSet();
            dtExport.Columns.Add("Name", typeof(string));
            if (chk1.Checked)
            {
                dtExport.Rows.Add("PharmacyMaster");
            }
            if (chk2.Checked)
            {
                dtExport.Rows.Add("ItemConfiguration");
            }
            if (chk3.Checked)
            {
                dtExport.Rows.Add("Manufacturerdetail");
            }
            if (chk4.Checked)
            {
                dtExport.Rows.Add("Programitemlinking");
            }
            if (chk5.Checked)
            {
                dtExport.Rows.Add("Storeitemlinking");
            }
            if (chk6.Checked)
            {
                dtExport.Rows.Add("Supplieritemlinking");
            }
            if (chk7.Checked)
            {
                dtExport.Rows.Add("Program");
            }
            if (chk8.Checked)
            {
                dtExport.Rows.Add("Donor");
            }
            if (chk9.Checked)
            {
                dtExport.Rows.Add("Programdonorlinking");
            }
            if (chk10.Checked)
            {
                dtExport.Rows.Add("Costallocationcategory");
            }
            if (chk11.Checked)
            {
                dtExport.Rows.Add("Storedetail");
            }
            if (chk12.Checked)
            {
                dtExport.Rows.Add("Storesourcedestinationlinking");
            }
            if (chk13.Checked)
            {
                dtExport.Rows.Add("Supplier");
            }
            if (chk14.Checked)
            {
                dtExport.Rows.Add("Adjustmentreason");
            }
            if (chk15.Checked)
            {
                dtExport.Rows.Add("Returnreason");
            }
            if (chk16.Checked)
            {
                dtExport.Rows.Add("Labtestlocation");
            }
            dtExport.Rows.Add("AddConstraint");
            theDS.Tables.Add(dtExport);
            theDS.WriteXml("C:\\SCMExportFile\\ExportMaster.xml", XmlWriteMode.WriteSchema);
        }
        private void btnImport_Click(object sender, EventArgs e)
        {

            if (txtfile.Text != "")
            {
                lblmessage.Text = "";
                string strExportList = "C:\\SCMExportFile\\ExportMaster.xml";
                

                string myXMLfile = txtfile.Text;

                string strext = Path.GetExtension(myXMLfile);
                if (strext != ".xml")
                {
                    IQCareWindowMsgBox.ShowWindow("BrowseFile", this);
                    return;
                }
                if (myXMLfile.ToUpper() != "C:\\SCMEXPORT\\SCMMASTER.XML")
                {
                    IQCareWindowMsgBox.ShowWindow("Browswrongfile", this);
                    return;
                }

                DataSet dsExportList = new DataSet();
                System.IO.FileStream fsReadXml1 = new System.IO.FileStream(strExportList, System.IO.FileMode.Open);
                dsExportList.ReadXml(fsReadXml1);
                fsReadXml1.Close();

                DataSet ds = new DataSet();
                System.IO.FileStream fsReadXml = new System.IO.FileStream
                    (myXMLfile, System.IO.FileMode.Open);

            
                try
                {
                    ds.ReadXml(fsReadXml);

                    //if (ds.Tables[0].Rows[0]["AppVer"].ToString() != GblIQCare.AppVersion || ((DateTime)ds.Tables[0].Rows[0]["RelDate"]).ToString("dd-MMM-yyyy") != GblIQCare.ReleaseDate)
                    //{
                    //    IQCareWindowMsgBox.ShowWindow("ImportScmMasterCheckVersion", this);
                    //    return;

                    //}
                    IImportExport theExportManager = (IImportExport)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExport, BusinessProcess.FormBuilder");
                    if (ds.Tables.Count == 21)
                    {
                        theExportManager.DBBackUpImportData();
                        foreach (DataRow r in dsExportList.Tables[0].Rows)
                        {
                            if (r["Name"].ToString() == "PharmacyMaster")
                            {
                                theExportManager.BulkInsert(ds.Tables[1], "Mst_Drug");
                                theExportManager.BulkInsert(ds.Tables[2], "Mst_Generic");
                                theExportManager.BulkInsert(ds.Tables[3], "Lnk_DrugGeneric");
                                theExportManager.BulkInsert(ds.Tables[4], "Mst_DrugType");
                                theExportManager.BulkInsert(ds.Tables[5], "Lnk_DrugTypeGeneric");
                            }
                            if (r["Name"].ToString() == "ItemConfiguration")
                            {
                                theExportManager.ImportSCM(ds.Tables[6], "ItemConfiguration");
                            }
                            if (r["Name"].ToString() == "Manufacturerdetail")
                            {
                                theExportManager.BulkInsert(ds.Tables[7], "mst_manufacturer");
                            }
                            if (r["Name"].ToString() == "Programitemlinking")
                            {
                                theExportManager.ImportSCM(ds.Tables[8], "Programitemlinking");
                            }
                            if (r["Name"].ToString() == "Storeitemlinking")
                            {
                                theExportManager.ImportSCM(ds.Tables[9], "Storeitemlinking");
                            }
                            if (r["Name"].ToString() == "Supplieritemlinking")
                            {
                                theExportManager.ImportSCM(ds.Tables[10], "Supplieritemlinking");
                            }
                            if (r["Name"].ToString() == "Program")
                            {
                                theExportManager.BulkInsert(ds.Tables[11], "mst_program");
                            }
                            if (r["Name"].ToString() == "Donor")
                            {
                                theExportManager.BulkInsert(ds.Tables[12], "mst_donor");
                            }
                            if (r["Name"].ToString() == "Programdonorlinking")
                            {
                                theExportManager.BulkInsert(ds.Tables[13], "Lnk_DonorProgram");
                            }
                            if (r["Name"].ToString() == "Costallocationcategory")
                            {
                                theExportManager.ImportSCM(ds.Tables[14], "Costallocationcategory");
                            }
                            if (r["Name"].ToString() == "Storedetail")
                            {
                                theExportManager.BulkInsert(ds.Tables[15], "mst_store");
                            }
                            if (r["Name"].ToString() == "Storesourcedestinationlinking")
                            {
                                theExportManager.BulkInsert(ds.Tables[16], "lnk_StoreSourceDestination");
                            }
                            if (r["Name"].ToString() == "Supplier")
                            {
                                theExportManager.BulkInsert(ds.Tables[17], "mst_supplier");
                            }
                         
                            if (r["Name"].ToString() == "Adjustmentreason")
                            {
                                theExportManager.ImportSCM(ds.Tables[18], "Adjustmentreason");
                            }
                            if (r["Name"].ToString() == "Returnreason")
                            {
                                theExportManager.ImportSCM(ds.Tables[19], "Returnreason");
                            }
                            if (r["Name"].ToString() == "Labtestlocation")
                            {
                                theExportManager.ImportSCM(ds.Tables[20], "Labtestlocation");
                            }
                            
                            if (r["Name"].ToString() == "AddConstraint")
                            {
                                theExportManager.ImportSCM(ds.Tables[10], "AddConstraint");
                            }
                          
                        }
                        

                        IQCareWindowMsgBox.ShowWindow("ImportSuccess", this);
                    }
                    else
                    {
                        IQCareWindowMsgBox.ShowWindow("Browswrongfile", this);

                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    fsReadXml.Close();
                }
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("BrowseFile", this);
                return;
            }
        }

        private void frmSCM_PharmacyMasterImportExport_Load(object sender, EventArgs e)
        {
         
            //grpimport.Enabled = false;
            txtfile.Text = "";
        }

        

        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "*.xml";
            openFileDialog1.InitialDirectory = "c:\\SCMExport";
            openFileDialog1.ShowDialog();
            txtfile.Text = openFileDialog1.FileName; 
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
