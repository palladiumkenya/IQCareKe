using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


    namespace IQCare.FormBuilder
    {
    partial class frmQuery : Form
    {
        IHomePageConfiguration objTechArea;

       
        public frmQuery()
        {
            InitializeComponent();
            
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
           objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration,BusinessProcess.FormBuilder");
           string strParse = rtbQuery.Text.Trim().ToString().ToUpper();
            
          //  string strParse = (string)objTechArea.ParseSQLColoumns(rtbQuery.Text.Trim().ToString());

         if (strParse.Contains("@PatientId".ToUpper()))
         {
             strParse = strParse.Replace("@PatientId".ToUpper(), "0");
         }
         //else
         //{
         //    IQCareWindowMsgBox.ShowWindow("ParameterPatientId", this);
         //    return;
         //}
         strParse = (string)objTechArea.ParseSQLColoumns(strParse);

         if (strParse == "Valid Value" || strParse == "No Records")
         {
             string sqlstr = rtbQuery.Text;
             char[] charsToTrim = {';'};
             sqlstr = sqlstr.TrimEnd(charsToTrim);
             GblIQCare.Query = sqlstr.ToString();
             this.Close();
         }
         else
         {
             MsgBuilder theBuilder = new MsgBuilder();
             theBuilder.DataElements["MessageText"] = strParse.ToString();
             IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
             return;
          }
          
        }

        private void frmQuery_Load(object sender, EventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            MenuItem m1 = new MenuItem("Cut");
            m1.Click += new EventHandler(m1_Cut);
            cm.MenuItems.Add(m1);

            m1 = new MenuItem("Copy");
            m1.Click += new EventHandler(m1_Copy);
            cm.MenuItems.Add(m1);

            m1 = new MenuItem("Paste");
            m1.Click += new EventHandler(m1_Paste);
            cm.MenuItems.Add(m1);

           // rtbQuery.ContextMenu = cm;

            if (GblIQCare.blnIsPatientHomePage)
            {
                m1 = new MenuItem("@PatientId");
                m1.Click += new EventHandler(m1_PatientId);
                cm.MenuItems.Add(m1);
            }

            rtbQuery.ContextMenu = cm;

            rtbQuery.Text = GblIQCare.Query;
            
        }

        void m1_Cut(object sender, EventArgs e)
        {
            rtbQuery.Cut();
        
        }

        void m1_Copy(object sender, EventArgs e)
        {
            rtbQuery.Copy();
        }

        void m1_Paste(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                rtbQuery.SelectedRtf = Clipboard.GetData(DataFormats.Rtf).ToString();
            }

        }
        void m1_PatientId(object sender, EventArgs e)
        {
            string source = rtbQuery.Text + "@PatientId";
            if (source.Count(f => f == '@') > 1)
            {
                IQCareWindowMsgBox.ShowWindow("TwoParameters", this);
                return;
            }
            else
            {
                rtbQuery.Text = rtbQuery.Text + "@PatientId";
            }
        }
        private void btnTestQuery_Click(object sender, EventArgs e)
        {
            if (rtbQuery.Text.Trim() == "")
            {
                IQCareWindowMsgBox.ShowWindow("BlankQuery", this);
                return;
            }

           try
            {
                //objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration,BusinessProcess.FormBuilder");
                string strParse = rtbQuery.Text.Trim().ToString().ToUpper();
                if (strParse.Contains("@PatientId".ToUpper()))
                {
                    strParse = strParse.Replace("@PatientId".ToUpper(), "0");
                }
                //else
                //{
                //    IQCareWindowMsgBox.ShowWindow("ParameterPatientId", this);
                //    return;
                //}

                objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration,BusinessProcess.FormBuilder");
                //strParse = (string)objTechArea.ParseSQLColoumns(rtbQuery.Text.Trim().ToString());
                strParse = (string)objTechArea.ParseSQLColoumns(strParse);
                if (strParse == "Valid Value" || strParse == "No Records")
                {
                    IQCareWindowMsgBox.ShowWindow("CheckSyntax", this);
                    return;
                }
                else
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = strParse.ToString();
                    IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                    return;

                }
          }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
            finally
            {
                objTechArea = null;
            }

        }

     }
    }
