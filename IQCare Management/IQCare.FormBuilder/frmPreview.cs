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
using Interface.Clinical;

namespace IQCare.FormBuilder
{
    public partial class frmPreview : Form
    {
        string ObjFactoryParameter = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
        clsCssStyle theStyle = new clsCssStyle();
        DataSet theDSXML = new DataSet();
        BindFunctions theBind = new BindFunctions();
       // Boolean theConditional;
        public frmPreview()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Used to create table for values of Select List/MultiSelect list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private DataTable CreateTable(string[] value)
        {
            DataTable TmpDT = new DataTable();
            DataColumn ID = new DataColumn();
            ID.DataType = System.Type.GetType("System.Int32");
            ID.ColumnName = "ID";
            TmpDT.Columns.Add(ID);

            DataColumn Name = new DataColumn();
            Name.DataType = System.Type.GetType("System.String");
            Name.ColumnName = "Name";
            TmpDT.Columns.Add(Name);

            DataRow tmpdr;

            for (int i = 1; i < value.Length + 1; i++)
            {
                tmpdr = TmpDT.NewRow();
                tmpdr[0] = i;
                tmpdr[1] = value[i - 1];
                TmpDT.Rows.Add(tmpdr);
            }
            return TmpDT;

        }

        private void frmPreview_Load(object sender, EventArgs e)
        {
             
            Panel pnlPreview = new Panel();
            pnlPreview.Width =477;
            pnlPreview.Height = 40 ;
            pnlPreview.Left =12 ;
            pnlPreview.Top =14 ;
            if (GblIQCare.iFormBuilderId == 126)
            {
                lblFormName.Text = "IQCare Registration";
                panel1.Visible = false;
                panel3.Visible = true;
                this.panel3.Location = new System.Drawing.Point(0, 0);
            }
            LoadPredefinedLabel_Field(GblIQCare.iFormBuilderId, 0);
            //set css begin
            theStyle.setStyle(this);
            //set css end 


        }
        /// <summary>
        /// For loading Controls
        /// 
        /// </summary>
        /// <param name="ControlID"></param>
        /// <param name="PnlAddControl"></param>
        /// <param name="Column"></param>
        /// <param name="ID"></param>
        /// <param name="FieldValue"></param>
        /// <param name="Label"></param>
        /// <param name="Table"></param>
        private void LoadFieldTypeControl(string ControlID, Panel PnlAddControl, string Column, string ID, string CodeID, string Label, string Table)
        {
            string strNewLine;
            int lblength = 0;
            
            lblength = Label.Length;
            Label lblControlLabel = new Label();
            lblControlLabel.Name = "lblControlLabel_" + Label;
            lblControlLabel.Text = Label + ":";
            //lblControlLabel.Tag = "lblLabelPreview";
            lblControlLabel.AutoSize = false;
            lblControlLabel.Top = 4;
            lblControlLabel.Height = 25;
            int iStartVar = 14;
            int iEndVar = lblControlLabel.Text.Length;
            lblControlLabel.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold | FontStyle.Regular);
            while (iEndVar > iStartVar)
            {
                strNewLine = lblControlLabel.Text.Insert(iStartVar, "\n");
                lblControlLabel.Text = strNewLine;
                iStartVar = iStartVar + 14;
                if(iStartVar>iEndVar)
                {
                    break;
                }
            }
                lblControlLabel.Left = 65;

                 if (ControlID == "1" ) ///SingleLine Text Box
            {
                PnlAddControl.Controls.Add(lblControlLabel);

                TextBox theSingleText = new TextBox();
                theSingleText.Name = "TXTSingle-" + Column + "-" + Table;
                theSingleText.Width = 180;
                theSingleText.Height = 20;
                theSingleText.Top = 4;
                theSingleText.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
                theSingleText.MaxLength = 50;
                theSingleText.Tag = "txtTextBox";
                PnlAddControl.Controls.Add(theSingleText);
            }

            else if (ControlID == "2") ///DecimalTextBox
            {

                PnlAddControl.Controls.Add(lblControlLabel);

                TextBox theSingleDecimalText = new TextBox();
                theSingleDecimalText.Name = "TXT-" + Column + "-" + Table;
                theSingleDecimalText.Width = 180;
                theSingleDecimalText.Height = 20;
                theSingleDecimalText.Top = 4;
                theSingleDecimalText.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
                theSingleDecimalText.MaxLength = 50;
                theSingleDecimalText.Tag = "txtTextBox";
                PnlAddControl.Controls.Add(theSingleDecimalText);
               
            }
            else if (ControlID == "3")   /// Numeric
            {
               
                PnlAddControl.Controls.Add(lblControlLabel);
               
                TextBox theNumberText = new TextBox();
                theNumberText.Name = "TXTNUM-" + Column + "-" + Table;
                theNumberText.Width = 100;
                theNumberText.Height = 20;
                theNumberText.MaxLength = 9;
                theNumberText.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
                theNumberText.Top =4;
                theNumberText.Tag = "txtTextBox";
                PnlAddControl.Controls.Add(theNumberText);
                
            }

            else if (ControlID == "4") /// Dropdown
            {
           
                PnlAddControl.Controls.Add(lblControlLabel);

                ComboBox ddlSelectList = new ComboBox();
                ddlSelectList.Name = "SELECTLIST-" + Column + "-" + Table;
                ddlSelectList.DropDownStyle = ComboBoxStyle.DropDownList;
                ddlSelectList.Tag = "ddlDropDownList";
                if (CodeID == "")
                {
                    CodeID = "0";
                }
                ddlSelectList.Width = 220;
                ddlSelectList.Height = 20;
                ddlSelectList.Top = 4;
                ddlSelectList.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
             
                ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
                DataSet theDS = CustomFormMgr.GetPmtctDecodeTable((CodeID));
                DataTable theDT = theDS.Tables[0];
                theBind.Win_BindCombo(ddlSelectList, theDT, "Name", "ID");
                PnlAddControl.Controls.Add(ddlSelectList);
               
            }
            else if (ControlID == "5") ///Date
            {
              
                PnlAddControl.Controls.Add(lblControlLabel);

                TextBox theDateText = new TextBox();
                theDateText.Name = "TXTDT-" + Column + "-" + Table;
                Control ctl = (TextBox)theDateText;
                theDateText.Width = 83;
                theDateText.Height = 20;
                theDateText.MaxLength = 11;
                theDateText.Top = 4;
                theDateText.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
                theDateText.Tag = "txtTextBox";
                PnlAddControl.Controls.Add(theDateText);
                                
                string strGetPath = GetPath();
                PictureBox theDateImage = new PictureBox();
                theDateImage.Size = new System.Drawing.Size(22, 22);
                theDateImage.Left=(theDateText.Width + theDateText.Left) + 2;
                theDateImage.Image = new Bitmap(strGetPath + "\\Preview_Img.bmp");
                PnlAddControl.Controls.Add(theDateImage);

                Label lblDate = new Label();
                lblDate.Name = "lblControlLabelDate_"  + Label;
                lblDate.Text = "(DD-MMM-YY)";
                lblDate.Top = 4;
                lblDate.Left = (theDateImage.Width + theDateImage.Left) + 2;
                PnlAddControl.Controls.Add(lblDate);


            }
            else if (ControlID == "6")  /// Radio Button
            {

                PnlAddControl.Controls.Add(lblControlLabel);

                RadioButton theYesNoRadio1 = new RadioButton();
                theYesNoRadio1.Name = "RADIO1-" + Column + "-" + Table;
                theYesNoRadio1.Text = "Yes";
                theYesNoRadio1.Name = "" + Column + "";
                theYesNoRadio1.Top = 4;
                theYesNoRadio1.Width =55;
                theYesNoRadio1.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
                PnlAddControl.Controls.Add(theYesNoRadio1);
                                          
                RadioButton theYesNoRadio2 = new RadioButton();
                theYesNoRadio2.Name = "RADIO2-" + Column + "-" + Table;
                theYesNoRadio2.Text = "No";
                theYesNoRadio2.Width =55;
                theYesNoRadio2.Name = "" + Column + "";
                theYesNoRadio2.Top = 4;
                theYesNoRadio2.Left = (theYesNoRadio1.Left + theYesNoRadio1.Width) + 2;
                PnlAddControl.Controls.Add(theYesNoRadio2);


            }
            else if (ControlID == "7") //Checkbox
            {
                
                PnlAddControl.Controls.Add(lblControlLabel);

                TextBox theMultiText = new TextBox();
                CheckBox theChk = new CheckBox();
                theChk.Name = "Chk-" + Column + "-" + Table;
                theChk.Top = 4;
                theChk.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
                PnlAddControl.Controls.Add(theChk);
               

            }
            else if (ControlID == "8")  /// MultiLine TextBox
            {
                              
                PnlAddControl.Controls.Add(lblControlLabel);

                TextBox theMultiText = new TextBox();
                theMultiText.Name = "TXTMulti-" + Column + "-" + Table;
                theMultiText.Width = 200;
                theMultiText.Height = 30;
                theMultiText.Top = 4;
                theMultiText.Multiline = true;
                theMultiText.ScrollBars= System.Windows.Forms.ScrollBars.Both;
                theMultiText.MaxLength = 200;
                theMultiText.Tag = "txtTextBox";
                theMultiText.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
               
                PnlAddControl.Controls.Add(theMultiText);
                

            }
            else if (ControlID == "9") ///  MultiSelect List 
            {
                            
                lblControlLabel.Top = 6;
                PnlAddControl.Controls.Add(lblControlLabel);

                CheckedListBox chkMultiList = new CheckedListBox();
                chkMultiList.Name = "MULTISELECTLIST-" + Column + "-" + Table;
                if (CodeID == "")
                {
                    CodeID = "0";
                }
                chkMultiList.Tag = "check";
                chkMultiList.Width = 240;
                chkMultiList.Top = 4;
                chkMultiList.Height =80;
                chkMultiList.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;


                ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
                DataSet theDS = CustomFormMgr.GetPmtctDecodeTable((CodeID));
                DataTable theDT = theDS.Tables[0];
                theBind.Win_BindCheckListBox(chkMultiList, theDT, "Name", "ID");
                PnlAddControl.Controls.Add(chkMultiList);

            }


               else if (ControlID == "10") ///SingleLine Text Box(Regimen) 
               {
                   PnlAddControl.Controls.Add(lblControlLabel);

                   TextBox theSingleText = new TextBox();
                   theSingleText.Name = "TXTSingle-" + Column + "-" + Table;
                   theSingleText.Width = 165;
                   theSingleText.Height = 21;
                   theSingleText.Top = 4;
                   theSingleText.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
                   theSingleText.MaxLength = 50;
                   theSingleText.Tag = "txtTextBox";
                   PnlAddControl.Controls.Add(theSingleText);

                   Button theButton = new Button();
                   theButton.Name = "Button-" + Column + "-" + Table;
                   theButton.Width = 30;
                   theButton.Height = 5;
                   theButton.Top = 4;
                   theButton.Left = (lblControlLabel.Width + lblControlLabel.Left) + theSingleText.Left;
                   theButton.Tag = "btnH25WFlexi";
                   PnlAddControl.Controls.Add(theButton);
               }

               else if(ControlID == "11")

               {
                   PnlAddControl.Controls.Add(lblControlLabel);

                   Button theButton=new Button();
                   theButton.Name="Drug Selection";
                   theButton.Width=55;
                   theButton.Height=5;
                   theButton.Top = 5;
                   theButton.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
                   theButton.Tag="btnH25WFlexi";
                   PnlAddControl.Controls.Add(theButton);

               }
               else if(ControlID == "12")
               {
                   PnlAddControl.Controls.Add(lblControlLabel);
                   Button theButton=new Button();
                   theButton.Name="Lab Selection";
                   theButton.Width=55;
                   theButton.Height=5;
                   theButton.Top = 5;
                   theButton.Left = (lblControlLabel.Width + lblControlLabel.Left) + 2;
                   theButton.Tag="btnH25WFlexi";
                   PnlAddControl.Controls.Add(theButton);
               }
       
        }
      
        private void LoadPredefinedLabel_Field(int FeatureID, int PatientID)
        {
            ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = CustomFormMgr.GetFieldName_and_Label(GblIQCare.iFormBuilderId, 0);
            if (theDS.Tables[1].Rows.Count <= 0)
            {
                return;
            }
            lblFormName.Text = theDS.Tables[1].Rows[0]["FeatureName"].ToString();
            DataTable DT = theDS.Tables[1].DefaultView.ToTable(true, "SectionID", "SectionName").Copy();
            DataTable theConditionalFields = theDS.Tables[6].Copy();
            DataView DV;
            DV=theDS.Tables[1].DefaultView;
            int iCountCtrl;
            
            int Numtds = 2, iPanelTop=93;

            //section
            Panel PnlSectionHeading = new Panel();
            PnlSectionHeading.Name = "PnlSectionHeading_SS";
            PnlSectionHeading.BackColor = System.Drawing.Color.LightGray;
            PnlSectionHeading.Left = 10;
            PnlSectionHeading.Width = 925;
            if (GblIQCare.iFormBuilderId == 126)
            {
                PnlSectionHeading.Top = panel3.Height + 10;//iPanelTop;
            }
            else{
                PnlSectionHeading.Top = iPanelTop + 10;
            }
            PnlSectionHeading.Height = iPanelTop + 10;
            PnlSectionHeading.BorderStyle = BorderStyle.FixedSingle;
            PnlSectionHeading.Tag = "pnlPanelPreview";
            int iValueLeft = 20, iValueRight = 20,iTest=0;
            string strPnlName = string.Empty;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                int td=1,iTestCount=0,iTotalCtrl=0,iVerify=0;
                DV.RowFilter = " SectionID=" + DT.Rows[i]["SectionID"];
                iTotalCtrl = DV.Count;
                iCountCtrl = (DV.Count)%2;
                if (iCountCtrl != 0)
                {
                    iCountCtrl = Convert.ToInt32((DV.Count + 1) / 2);
                    iTestCount = 1;
                }
                else
                {
                    iCountCtrl = Convert.ToInt32(DV.Count  / 2);
                    iTestCount = 2;
                }
                Label lblSectionHeading = new Label();
                lblSectionHeading.Name = "lblSectionHeading_" + i;
                lblSectionHeading.Text = DT.Rows[i]["SectionName"].ToString();
                lblSectionHeading.Tag = "lblLabelPreviewHeader";
                lblSectionHeading.Top = iValueLeft;
                lblSectionHeading.AutoSize = true;
                PnlSectionHeading.Controls.Add(lblSectionHeading);
                iValueLeft = lblSectionHeading.Height + iValueLeft+10;
                iValueRight = lblSectionHeading.Height + iValueRight + 10;
                pnlPreviewForm.Tag = "pnlPanel";
                theStyle.setStyle(pnlPreviewForm); 
                
                //controls
                foreach (DataRow DRLnkTable in theDS.Tables[1].Rows)
                {
                    if (DT.Rows[i]["SectionID"].ToString() == DRLnkTable["SectionID"].ToString())
                    {
                        iVerify++;
                        if (td <= Numtds)
                        {
                                Panel pnlControlCol = new Panel();
                                pnlControlCol.Height = panel3.Height + 3;
                                if (td == 1 && DRLnkTable["Controlid"].ToString()!="9")
                                {
                                   
                                    if (iVerify == iTotalCtrl && iTestCount == 1)
                                    {
                                        pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                        strPnlName = pnlControlCol.Name.ToString();
                                        pnlControlCol.Left = 12;
                                        pnlControlCol.Width = 445;
                                        pnlControlCol.Top = iValueLeft;
                                        pnlControlCol.Height = 40;
                                        pnlControlCol.BackColor = System.Drawing.Color.White;
                                        pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                        iValueLeft = iValueLeft + 45;
                                        td++;
                                        Panel pnlControlRow = new Panel();
                                        pnlControlRow.Left = 464;
                                        pnlControlRow.Width = 445;
                                        pnlControlRow.Top = iValueRight;
                                        pnlControlRow.Height = 40;
                                        pnlControlRow.BackColor = System.Drawing.Color.White;
                                        iValueRight = iValueRight + 45;
                                        td = 1;
                                        iTest = 1;
                                        PnlSectionHeading.Controls.Add(pnlControlRow);
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 45;
                                        iTest = 1;
                                    }
                                    else
                                    {

                                        pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                        strPnlName = pnlControlCol.Name.ToString();
                                        pnlControlCol.Left = 12;
                                        pnlControlCol.Width = 445;
                                        pnlControlCol.Top = iValueLeft;
                                        pnlControlCol.Height = 40;
                                        pnlControlCol.BackColor = System.Drawing.Color.White;
                                        pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                        iValueLeft = iValueLeft + 45;
                                        td++;
                                        iTest = 1;
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 45;
                                    }
                                }
                                else if (td == 2 && DRLnkTable["Controlid"].ToString() != "9")
                                {
                                    pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                    pnlControlCol.Left = 464;
                                    pnlControlCol.Width = 445;
                                    pnlControlCol.Top = iValueRight;
                                    if (iTest == 1)
                                    {
                                        pnlControlCol.Height = 40;
                                    }
                                    else if(iTest==2)
                                    {
                                        pnlControlCol.Height = 90;
                                        PnlSectionHeading.Height = PnlSectionHeading.Height +15;
                                        iValueRight = iValueRight + 60;
                                    }
                                    pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                    pnlControlCol.BackColor = System.Drawing.Color.White;
                                    iValueRight = iValueRight + 45;
                                    td = 1;
                                    iTest = 1;
                                }
                                else if (td == 1 && DRLnkTable["Controlid"].ToString() == "9")
                                {
                                    if (iVerify == iTotalCtrl && iTestCount == 1)
                                    {
                                        pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                        strPnlName = pnlControlCol.Name.ToString();
                                        pnlControlCol.Left = 12;
                                        pnlControlCol.Width = 445;
                                        pnlControlCol.Top = iValueLeft;
                                        pnlControlCol.Height = 90;
                                        pnlControlCol.BackColor = System.Drawing.Color.White;
                                        pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                        iValueLeft = iValueLeft + 105;
                                        iTest = 2;
                                        td++;

                                        Panel pnlControlRow = new Panel();
                                        pnlControlRow.Left = 464;
                                        pnlControlRow.Width = 445;
                                        pnlControlRow.Top = iValueRight;
                                        pnlControlRow.Height = 90;
                                        pnlControlRow.BackColor = System.Drawing.Color.White;
                                        iValueRight = iValueRight + 105;
                                       
                                        td = 1;
                                        PnlSectionHeading.Controls.Add(pnlControlRow);
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 60;
                                    }
                                    else
                                    {

                                        pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                        strPnlName = pnlControlCol.Name.ToString();
                                        pnlControlCol.Left = 12;
                                        pnlControlCol.Width = 445;
                                        pnlControlCol.Top = iValueLeft;
                                        pnlControlCol.Height = 90;
                                        pnlControlCol.BackColor = System.Drawing.Color.White;
                                        pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                        iValueLeft = iValueLeft + 105;
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 60;
                                        td++;
                                        iTest = 2;
                                    }
                                }
                                else if (td == 2 && DRLnkTable["Controlid"].ToString() == "9")
                                {
                                    if (iTest == 1)
                                    {
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 15;
                                        Panel pnlFindpanel;
                                        pnlFindpanel = (Panel)PnlSectionHeading.Controls.Find(strPnlName, true)[0];
                                        pnlFindpanel.Height = 90;
                                        iValueLeft = iValueLeft + 60;
                                       
                                    }
                                    pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"]+"";
                                    pnlControlCol.Left = 464;
                                    pnlControlCol.Width = 445;
                                    pnlControlCol.Top = iValueRight;
                                    pnlControlCol.Height = 90;
                                    pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                    pnlControlCol.BackColor = System.Drawing.Color.White;
                                    iValueRight = iValueRight + 105;
                                    td = 1;
                                    iTest =2;
                                }

                                LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), pnlControlCol, DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString());
                                PnlSectionHeading.Controls.Add(pnlControlCol);
                                theStyle.setStyle(PnlSectionHeading); 
                        }
                     
                        //Conditional Field
                        /*if (theConditional == true)
                        {
                            if (td <= Numtds)
                            {
                                Panel pnlControlCol = new Panel();
                                pnlControlCol.Height = panel3.Height + 3;
                                if (td == 1 && DRLnkTable["Controlid"].ToString() != "9")
                                {

                                    if (iVerify == iTotalCtrl && iTestCount == 1)
                                    {
                                        pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                        strPnlName = pnlControlCol.Name.ToString();
                                        pnlControlCol.Left = 12;
                                        pnlControlCol.Width = 445;
                                        pnlControlCol.Top = iValueLeft;
                                        pnlControlCol.Height = 40;
                                        pnlControlCol.BackColor = System.Drawing.Color.White;
                                        pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                        iValueLeft = iValueLeft + 45;
                                        td++;
                                        Panel pnlControlRow = new Panel();
                                        pnlControlRow.Left = 464;
                                        pnlControlRow.Width = 445;
                                        pnlControlRow.Top = iValueRight;
                                        pnlControlRow.Height = 40;
                                        pnlControlRow.BackColor = System.Drawing.Color.White;
                                        iValueRight = iValueRight + 45;
                                        td = 1;
                                        iTest = 1;
                                        PnlSectionHeading.Controls.Add(pnlControlRow);
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 45;
                                        iTest = 1;
                                    }
                                    else
                                    {

                                        pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                        strPnlName = pnlControlCol.Name.ToString();
                                        pnlControlCol.Left = 12;
                                        pnlControlCol.Width = 445;
                                        pnlControlCol.Top = iValueLeft;
                                        pnlControlCol.Height = 40;
                                        pnlControlCol.BackColor = System.Drawing.Color.White;
                                        pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                        iValueLeft = iValueLeft + 45;
                                        td++;
                                        iTest = 1;
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 45;
                                    }
                                }
                                else if (td == 2 && DRLnkTable["Controlid"].ToString() != "9")
                                {
                                    pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                    pnlControlCol.Left = 464;
                                    pnlControlCol.Width = 445;
                                    pnlControlCol.Top = iValueRight;
                                    if (iTest == 1)
                                    {
                                        pnlControlCol.Height = 40;
                                    }
                                    else if (iTest == 2)
                                    {
                                        pnlControlCol.Height = 90;
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 15;
                                        iValueRight = iValueRight + 60;
                                    }
                                    pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                    pnlControlCol.BackColor = System.Drawing.Color.White;
                                    iValueRight = iValueRight + 45;
                                    td = 1;
                                    iTest = 1;
                                }
                                else if (td == 1 && DRLnkTable["Controlid"].ToString() == "9")
                                {
                                    if (iVerify == iTotalCtrl && iTestCount == 1)
                                    {
                                        pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                        strPnlName = pnlControlCol.Name.ToString();
                                        pnlControlCol.Left = 12;
                                        pnlControlCol.Width = 445;
                                        pnlControlCol.Top = iValueLeft;
                                        pnlControlCol.Height = 90;
                                        pnlControlCol.BackColor = System.Drawing.Color.White;
                                        pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                        iValueLeft = iValueLeft + 105;
                                        iTest = 2;
                                        td++;

                                        Panel pnlControlRow = new Panel();
                                        pnlControlRow.Left = 464;
                                        pnlControlRow.Width = 445;
                                        pnlControlRow.Top = iValueRight;
                                        pnlControlRow.Height = 90;
                                        pnlControlRow.BackColor = System.Drawing.Color.White;
                                        iValueRight = iValueRight + 105;

                                        td = 1;
                                        PnlSectionHeading.Controls.Add(pnlControlRow);
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 60;
                                    }
                                    else
                                    {

                                        pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                        strPnlName = pnlControlCol.Name.ToString();
                                        pnlControlCol.Left = 12;
                                        pnlControlCol.Width = 445;
                                        pnlControlCol.Top = iValueLeft;
                                        pnlControlCol.Height = 90;
                                        pnlControlCol.BackColor = System.Drawing.Color.White;
                                        pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                        iValueLeft = iValueLeft + 105;
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 60;
                                        td++;
                                        iTest = 2;
                                    }
                                }
                                else if (td == 2 && DRLnkTable["Controlid"].ToString() == "9")
                                {
                                    if (iTest == 1)
                                    {
                                        PnlSectionHeading.Height = PnlSectionHeading.Height + 15;
                                        Panel pnlFindpanel;
                                        pnlFindpanel = (Panel)PnlSectionHeading.Controls.Find(strPnlName, true)[0];
                                        pnlFindpanel.Height = 90;
                                        iValueLeft = iValueLeft + 60;

                                    }
                                    pnlControlCol.Name = "pnl_" + DRLnkTable["FieldName"] + "";
                                    pnlControlCol.Left = 464;
                                    pnlControlCol.Width = 445;
                                    pnlControlCol.Top = iValueRight;
                                    pnlControlCol.Height = 90;
                                    pnlControlCol.BorderStyle = BorderStyle.FixedSingle;
                                    pnlControlCol.BackColor = System.Drawing.Color.White;
                                    iValueRight = iValueRight + 105;
                                    td = 1;
                                    iTest = 2;
                                }

                                LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), pnlControlCol, DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString());
                                PnlSectionHeading.Controls.Add(pnlControlCol);
                                theStyle.setStyle(PnlSectionHeading);
                            }
                        }*/
                    }
                }
            }

  ///For Add/Save/Close buttons
            
            Panel pnlButton = new Panel();
            pnlButton.Left = 10;
            pnlButton.Width = 925;
            pnlButton.Top = PnlSectionHeading.Height + iPanelTop+20;
            pnlButton.Height = 70;
            pnlButton.Tag = "lblLabelPreviewHeader";

            pnlButton.BackColor = System.Drawing.Color.LightGray;
            pnlButton.BorderStyle = BorderStyle.FixedSingle;
            if (GblIQCare.iFormBuilderId != 126)
            {
                Panel pnlSignature = new Panel();
                pnlSignature.Left = 12;
                pnlSignature.Width = 900;
                pnlSignature.Top = 4;
                pnlSignature.Height = 35;
                //pnlSignature.Tag = "lblLabelPreviewHeader";

                pnlSignature.BackColor = System.Drawing.Color.White;
                pnlSignature.BorderStyle = BorderStyle.FixedSingle;

                Label lblSignature = new Label();
                lblSignature.ForeColor = System.Drawing.Color.Black;
                lblSignature.Left = 365;
                lblSignature.Text = "Signature:";
                lblSignature.Top = 4;
                lblSignature.Font = new Font(lblSignature.Font, FontStyle.Bold | FontStyle.Regular);


                ComboBox ddlSignature = new ComboBox();
                ddlSignature.DropDownStyle = ComboBoxStyle.DropDownList;
                ddlSignature.Tag = "ddlDropDownList";
                ddlSignature.Width = 220;
                ddlSignature.Height = 2;
                ddlSignature.Top = 4;
                ddlSignature.Left = (lblSignature.Width + lblSignature.Left);
                ddlSignature.Items.Insert(0, "Select");
                ddlSignature.Items.Add(GblIQCare.AppUserName);
                ddlSignature.SelectedIndex = 0;

                pnlSignature.Controls.Add(lblSignature);
                pnlSignature.Controls.Add(ddlSignature);
                pnlButton.Controls.Add(pnlSignature);
            }
            Button btnSave = new Button();
            btnSave.Tag = "btnSingleText";
            btnSave.Left = 365;
            btnSave.Text = "Save";
            btnSave.Top = 40;
            btnSave.Enabled = true;

            Button btnClose = new Button();
            btnClose.Tag = "btnSingleText";
            btnClose.Left = 445;
            btnClose.Text = "Close";
            btnClose.Top = 40;
            btnClose.Enabled = true;

            Button btnPrint = new Button();
            btnPrint.Tag = "btnSingleText";
            btnPrint.Left = 525;
            btnPrint.Top = 40;
            btnPrint.Text = "Print";
            btnPrint.Enabled = true;
           
            pnlButton.Controls.Add(btnSave);
            pnlButton.Controls.Add(btnClose);
            pnlButton.Controls.Add(btnPrint);
            theStyle.setStyle(pnlButton);
            pnlPreviewForm.Controls.Add(pnlButton);
            pnlPreviewForm.Controls.Add(PnlSectionHeading);
          
        }
        /// <summary>
        /// This function is used to get the full path of image folder
        /// </summary>
        ///<returns></returns>
        public string GetPath()
        {
            return (System.Configuration.ConfigurationManager. AppSettings["ImagePath"]);
        }

      
  }
}
