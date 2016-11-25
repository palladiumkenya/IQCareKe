using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;


namespace Application.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    public class BindFunctions
    {

        #region "Constructor"
        /// <summary>
        /// Initializes a new instance of the <see cref="BindFunctions"/> class.
        /// </summary>
        public BindFunctions()
        {

        }
        #endregion

        /// <summary>
        /// Win_s the bind combo.
        /// </summary>
        /// <param name="theDropDown">The drop down.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theTextField">The text field.</param>
        /// <param name="theValueField">The value field.</param>
        /// <param name="SortColumnName">Name of the sort column.</param>
        public void Win_BindCombo(ComboBox theDropDown, DataTable theDT, string theTextField, string theValueField, string SortColumnName = "")
        {

            //string sort = theTextField + " Asc ";
            //try { theDropDown.Sorted = false; }
            //catch { }
            //if (SortColumnName != "")
            //{

            //    sort = SortColumnName + " Asc, " + theTextField + " Asc ";
            //    try { theDropDown.Sorted = false; }
            //    catch { }
            //}
            try
            {
               // theDT.DefaultView.Sort = sort;
                DataView view = theDT.DefaultView;

                DataTable dt = view.ToTable();
                if (dt.Rows.Count > 0)
                {
                    DataRow[] DR = theDT.Select("" + theValueField + " = 0");
                    if (DR.Length < 1)
                    {
                        DataRow theDR = dt.NewRow();
                        theDR["" + theTextField + ""] = "Select";
                        theDR["" + theValueField + ""] = 0;
                        dt.Rows.InsertAt(theDR, 0);
                    }
                    theDropDown.DataSource = dt;
                    theDropDown.DisplayMember = theTextField;
                    theDropDown.ValueMember = theValueField;

                }
            }
            catch { }
        }

        /// <summary>
        /// Win_s the bind check ListBox.
        /// </summary>
        /// <param name="theCheckListBox">The check ListBox.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theTextField">The text field.</param>
        /// <param name="theValueField">The value field.</param>
        /// <param name="SortColumnName">Name of the sort column.</param>
        public void Win_BindCheckListBox(CheckedListBox theCheckListBox, DataTable theDT, string theTextField, string theValueField, string SortColumnName = "")
        {
            //string sort = theTextField + " Asc ";
            //try { theCheckListBox.Sorted = false; }
            //catch { }
            //if (SortColumnName != "")
            //{
            //    sort = SortColumnName + " Asc, " + theTextField + " Asc ";
            //    try { theCheckListBox.Sorted = false; }
            //    catch { }
            //}
            try
            {
                DataView view = theDT.DefaultView;

                DataTable dt = view.ToTable();
                theCheckListBox.DataSource = dt;

                theCheckListBox.DisplayMember = theTextField;
                theCheckListBox.ValueMember = theValueField;
            }
            catch { }
        }

        /// <summary>
        /// Win_s the bind ListBox.
        /// </summary>
        /// <param name="theListBox">The ListBox.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theTextField">The text field.</param>
        /// <param name="theValueField">The value field.</param>
        /// <param name="SortColumnName">Name of the sort column.</param>
        public void Win_BindListBox(System.Windows.Forms.ListBox theListBox, DataTable theDT, string theTextField, string theValueField, string SortColumnName = "")
        {
           //string sort = theTextField + " Asc ";
           // try { theListBox.Sorted = true; }
           // catch { }
           // if (SortColumnName != "")
           // {
           //     sort= SortColumnName + " Asc, " + theTextField + " Asc ";
           //     try { theListBox.Sorted = false; }
           //     catch { }
           // }
            try
            {
                //theDT.DefaultView.Sort = sort;
                DataView view = theDT.DefaultView;

                DataTable dt = view.ToTable();
                theListBox.DataSource = dt;
                theListBox.DisplayMember = theTextField;
                theListBox.ValueMember = theValueField;
            }
            catch { }
        }

        /// <summary>
        /// Binds the combo.
        /// </summary>
        /// <param name="theDropDown">The drop down.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theTextField">The text field.</param>
        /// <param name="theValueField">The value field.</param>
        /// <param name="SortColumnName">Name of the sort column.</param>
        public void BindCombo(DropDownList theDropDown, DataTable theDT, string theTextField, string theValueField, string SortColumnName = "", string selectedValue="")
        {
            try
            {
                theDT.DefaultView.Sort = theTextField + " Asc ";
                if (SortColumnName != "")
                {
                    theDT.DefaultView.Sort = SortColumnName + " Asc, " + theTextField + " Asc ";
                }

                DataView view = theDT.DefaultView;

                DataTable dt = view.ToTable();

                DataRow[] DR = dt.Select("" + theValueField + " = 0");
                if (DR.Length < 1)
                {

                    DataRow theDR = dt.NewRow();
                    theDR["" + theTextField + ""] = "Select";
                    theDR["" + theValueField + ""] = 0;
                    dt.Rows.InsertAt(theDR, 0);
                }

                theDropDown.DataSource = dt;
                theDropDown.DataTextField = theTextField;
                theDropDown.DataValueField = theValueField;
                theDropDown.DataBind();
                if (selectedValue != "")
                {
                    ListItem item = theDropDown.Items.FindByValue(selectedValue);
                    if (item == null)
                    {
                        item = theDropDown.Items.FindByValue(selectedValue);
                    }
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
            catch { }
        }
       
        /// <summary>
        /// Binds the list.
        /// </summary>
        /// <param name="theListBox">The ListBox.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theTextField">The text field.</param>
        /// <param name="theValueField">The value field.</param>
        /// <param name="SortColumnName">Name of the sort column.</param>
        public void BindList(System.Web.UI.WebControls.ListBox theListBox, DataTable theDT, string theTextField, string theValueField, string SortColumnName = "")
        {
            try
            {
                theDT.DefaultView.Sort = theTextField + " Asc ";
                if (SortColumnName != "")
                {
                    theDT.DefaultView.Sort = SortColumnName + "Asc, " + theTextField + " Asc ";
                }
                theListBox.DataSource = theDT;
                theListBox.DataTextField = theTextField;
                theListBox.DataValueField = theValueField;
                theListBox.DataBind();
            }
            catch
            {


            }
        }

        /// <summary>
        /// Binds the checked list.
        /// </summary>
        /// <param name="theListBox">The ListBox.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theTextField">The text field.</param>
        /// <param name="theValueField">The value field.</param>
        /// <param name="SortColumnName">Name of the sort column.</param>
        public void BindCheckedList(CheckBoxList theListBox, DataTable theDT, string theTextField, string theValueField, string SortColumnName = "")
        {
            try
            {
                theDT.DefaultView.Sort = theTextField + " Asc ";
                if (SortColumnName != "")
                {
                    theDT.DefaultView.Sort = SortColumnName + " Asc, " + theTextField + " Asc ";
                }
                theListBox.DataSource = theDT;
                theListBox.DataTextField = theTextField;
                theListBox.DataValueField = theValueField;
                theListBox.DataBind();
            }
            catch { }
        }

        /// <summary>
        /// RadioButtons the list.
        /// </summary>
        /// <param name="theRadioButton">The RadioButton.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="theTextField">The text field.</param>
        /// <param name="theValueField">The value field.</param>
        /// <param name="SortColumnName">Name of the sort column.</param>
        public void RadioButtonList(RadioButtonList theRadioButton, DataTable theDT, string theTextField, string theValueField, string SortColumnName = "")
        {
            try
            {
                theDT.DefaultView.Sort = theTextField + " Asc ";
                theRadioButton.DataSource = theDT;
                theRadioButton.DataTextField = theTextField;
                theRadioButton.DataValueField = theValueField;
                theRadioButton.DataBind();
            }
            catch { }
        }

        /// <summary>
        /// Creates the checked list.
        /// </summary>
        /// <param name="thePannel">The pannel.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="Attribute">The attribute.</param>
        /// <param name="Event">The event.</param>
        public void CreateCheckedList(System.Web.UI.WebControls.Panel thePannel, DataTable theDT, string Attribute, String Event)
        {
            try
            {
                int i = 0;
                bool FlgOther = false;
                System.Web.UI.WebControls.TextBox theTxtBox = new System.Web.UI.WebControls.TextBox();
                for (i = 0; i < theDT.Rows.Count; i++)
                {
                    System.Web.UI.WebControls.CheckBox theChkBox = new System.Web.UI.WebControls.CheckBox();
                    theChkBox.ID = thePannel.ID + '-' + theDT.Rows[i][0].ToString();
                    theChkBox.Text = theDT.Rows[i][1].ToString();

                    if (Attribute != "")
                    {
                        string Attr = "";
                        string[] theAttr = Attribute.Split('%');
                        if (theAttr.Length > 0)
                            Attr = theAttr[0] + "%" + theChkBox.ClientID + theAttr[1];
                        else
                            Attr = theAttr[0];
                        theChkBox.Attributes.Add(Event, Attr);
                    }
                    if (theChkBox.Text == "Other")
                    {
                        theTxtBox = new System.Web.UI.WebControls.TextBox();
                        string[] theId = thePannel.ID.Split('-');
                        theTxtBox.ID = "OtherTXT-" + theId.GetValue(1).ToString() + "-" + theId.GetValue(2).ToString() + "-" + theId.GetValue(3) + "-" + theDT.Rows[i][0].ToString();
                        theTxtBox.Width = 75;
                        theChkBox.Attributes.Add("onclick", "toggle('txtother')");
                        FlgOther = true;
                    }
                    theChkBox.Width = 300;
                    if (FlgOther == false)
                    {
                        thePannel.Controls.AddAt(i, theChkBox);
                    }
                    else
                    {
                        System.Web.UI.WebControls.Panel theOtPnl = new System.Web.UI.WebControls.Panel();
                        theOtPnl.Controls.Add(new LiteralControl("<span>"));
                        theOtPnl.Controls.Add(theChkBox);
                        theOtPnl.Controls.Add(new LiteralControl("<span id='txtother' style='display:none'>"));
                        theOtPnl.Controls.Add(theTxtBox);
                        theOtPnl.Controls.Add(new LiteralControl("</span>"));
                        theOtPnl.Controls.Add(new LiteralControl("</span>"));
                        thePannel.Controls.AddAt(i, theOtPnl);

                        FlgOther = false;
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// Creates the blue checked list.
        /// </summary>
        /// <param name="thePannel">The pannel.</param>
        /// <param name="theDT">The dt.</param>
        /// <param name="Attribute">The attribute.</param>
        /// <param name="Event">The event.</param>
        public void CreateBlueCheckedList(System.Web.UI.WebControls.Panel thePannel, DataTable theDT, string Attribute, String Event)
        {
            try
            {
                int i = 0;
                bool FlgOther = false;
                System.Web.UI.WebControls.TextBox theTxtBox = new System.Web.UI.WebControls.TextBox();
                for (i = 0; i < theDT.Rows.Count; i++)
                {
                    System.Web.UI.WebControls.CheckBox theChkBox = new System.Web.UI.WebControls.CheckBox();
                    //theChkBox.ID = thePannel.ID + '-' + theDT.Rows[i][0].ToString() + '-' + theDT.Rows[i][1].ToString();
                    theChkBox.ID = "Chk-" + theDT.Rows[i][0].ToString() + '-' + theDT.Rows[i][1].ToString();
                    theChkBox.Text = theDT.Rows[i][1].ToString();
                    //theChkBox.AutoPostBack = true;
                    theChkBox.LabelAttributes.CssStyle.Add(HtmlTextWriterStyle.FontWeight, "normal");
                    //theChkBox.Style.Value = "font-weight:normal";
                    //theChkBox.LabelAttributes.CssStyle.Remove(
                    if (Attribute != "")
                    {
                        string Attr = "";
                        string[] theAttr = Attribute.Split('%');
                        if (theAttr.Length > 0)
                            Attr = theAttr[0] + "%" + theChkBox.ClientID + theAttr[1];
                        else
                            Attr = theAttr[0];
                        theChkBox.Attributes.Add(Event, Attr);
                    }
                    if (theChkBox.Text.Contains("Other") == true)
                    {
                        theTxtBox = new System.Web.UI.WebControls.TextBox();
                        //string[] theId = thePannel.ID.Split('-');
                        theTxtBox.ID = "OtherTXT" + "-" + theDT.Rows[i][0].ToString() + "-" + theDT.Rows[i][1].ToString();   ////+ theId.GetValue(1).ToString() + "-" + theId.GetValue(2).ToString() + "-" + theId.GetValue(3) + "-" + theDT.Rows[i][0].ToString();
                        theTxtBox.Width = Unit.Percentage(20);
                        theChkBox.Attributes.Add("onclick", "ChkHideUnhide('txt" + theDT.Rows[i][0].ToString() + "','ctl00_IQCareContentPlaceHolder_" + theChkBox.ClientID + "')");
                        //theChkBox.Attributes.Add("onload", "ChkHideUnhide('txt" + theDT.Rows[i][0].ToString() + "','ctl00_clinicalheaderfooter_" + theChkBox.ClientID + "')");

                        FlgOther = true;
                    }
                    theChkBox.Width = 400;
                    if (FlgOther == false)
                    {
                        thePannel.Controls.AddAt(i, theChkBox);
                    }
                    else
                    {
                        System.Web.UI.WebControls.Panel theOtPnl = new System.Web.UI.WebControls.Panel();
                        theOtPnl.ID = "Pnl" + theDT.Rows[i][0].ToString();
                        theOtPnl.Controls.Add(new LiteralControl("<span>"));
                        theChkBox.Width = 200;
                        theOtPnl.Controls.Add(theChkBox);
                        theOtPnl.Controls.Add(new LiteralControl("<span id='txt" + theDT.Rows[i][0].ToString() + "' style='display:none'>"));
                        theOtPnl.Controls.Add(theTxtBox);
                        theOtPnl.Controls.Add(new LiteralControl("</span>"));
                        theOtPnl.Controls.Add(new LiteralControl("</span>"));
                        thePannel.Controls.AddAt(i, theOtPnl);

                        FlgOther = false;
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Binds the HTML checked list.
        /// </summary>
        /// <param name="theCheckBox">The CheckBox.</param>
        /// <param name="theDT">The dt.</param>
        public void BindHTMLCheckedList(HtmlInputCheckBox theCheckBox, DataTable theDT)
        {

            int i = 0;
            for (i = 0; i < theDT.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox theChkBox = new System.Web.UI.WebControls.CheckBox();
                theCheckBox.ID = theDT.Rows[i][0].ToString();
                theCheckBox.Value = theDT.Rows[i][1].ToString();
                theCheckBox.DataBind();
                theCheckBox.Controls.AddAt(i, theCheckBox);
            }

        }

        /// <summary>
        /// Gets the years.
        /// </summary>
        /// <param name="CurrentYear">The current year.</param>
        /// <param name="theTextField">The text field.</param>
        /// <param name="theValueField">The value field.</param>
        /// <returns></returns>
        public DataTable GetYears(int CurrentYear, string theTextField, string theValueField)
        {
            int i = 0;
            DataRow theDR;
            DataTable theDT = new DataTable();
            theDT.Columns.Add("id");
            theDT.Columns.Add("Name");

            for (i = CurrentYear + 1; i >= 1930; i--)
            {
                theDR = theDT.NewRow();
                if (i == CurrentYear + 1)
                {
                    theDR["" + theTextField + ""] = "Select";
                    theDR["" + theValueField + ""] = 0;
                    theDT.Rows.InsertAt(theDR, 0);
                }
                else
                {
                    theDR[0] = i;
                    theDR[1] = i;
                    theDT.Rows.Add(theDR);
                }
            }
            return theDT;
        }

        /// <summary>
        /// Gets the months.
        /// </summary>
        /// <returns></returns>
        public DataTable GetMonths()
        {
            string[] theMonth = new string[13];
            theMonth.SetValue("January", 1);
            theMonth.SetValue("February", 2);
            theMonth.SetValue("March", 3);
            theMonth.SetValue("April", 4);
            theMonth.SetValue("May", 5);
            theMonth.SetValue("June", 6);
            theMonth.SetValue("July", 7);
            theMonth.SetValue("August", 8);
            theMonth.SetValue("September", 9);
            theMonth.SetValue("October", 10);
            theMonth.SetValue("November", 11);
            theMonth.SetValue("December", 12);

            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", System.Type.GetType("System.String"));

            for (int i = 0; i < 12; i++)
            {
                DataRow theDr = theDT.NewRow();
                theDr[0] = i + 1;
                theDr[1] = theMonth[i + 1];
                theDT.Rows.InsertAt(theDr, i);
            }
            return theDT;

        }

        /// <summary>
        /// Win_s the numeric.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public void Win_Numeric(KeyPressEventArgs e)
        {
            string strRestrictCharList = "0123456789\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }

        /// <summary>
        /// Win_s the integer.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public void Win_Integer(KeyPressEventArgs e)
        {
            string strRestrictCharList = "-0123456789\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }


        /// <summary>
        /// Win_decimals the specified e.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public void Win_decimal(KeyPressEventArgs e)
        {
            string strRestrictCharList = "0123456789.\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }
        /// <summary>
        /// Win_decimals the nagetive.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public void Win_decimalNagetive(KeyPressEventArgs e)
        {
            string strRestrictCharList = "-0123456789.\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }
        /// <summary>
        /// Win_s the string.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public void Win_String(KeyPressEventArgs e)
        {
            string strRestrictCharList = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-?\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }

        }


    }
}
