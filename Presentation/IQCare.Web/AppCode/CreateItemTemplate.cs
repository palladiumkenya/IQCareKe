using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace IQCare.Web
{

    public class CreateItemTemplate : ITemplate
    {
        private string columnName;
        private ConType theControl;
        private DataTable theDataTable;
        private DataTable theAttributes;
        private Int32 theFieldLength;

        public CreateItemTemplate(ConType controlType, string colname, DataTable DT, DataTable Attributes, Int32 FieldLength)
        {
            columnName = colname;
            theControl = controlType;
            theDataTable = DT;
            theAttributes = Attributes;
            theFieldLength = FieldLength;

        }

        public enum ConType
        {
            Checkbox, Textbox, Dropdown, label, label1
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (theControl)
            {
                case ConType.Checkbox:
                    CheckBox chkbox = new CheckBox();
                    chkbox.ID = "ChkBox";
                    chkbox.DataBinding += new EventHandler(chkbox_DataBinding);
                    container.Controls.Add(chkbox);
                    //container.ID = "ChkBox";
                    break;

                case ConType.Textbox:
                    TextBox thetxtbox = new TextBox();
                    thetxtbox.DataBinding += new EventHandler(thetxtbox_DataBinding);
                    thetxtbox.ID = thetxtbox.Text;
                    container.Controls.Add(thetxtbox);
                    //container.ID = "";
                    break;

                case ConType.Dropdown:
                    DropDownList theDropdown = new DropDownList();
                    theDropdown.DataBinding += new EventHandler(theDropdown_DataBinding);
                    theDropdown.ID = theDropdown.Text;
                    container.Controls.Add(theDropdown);
                    break;

                case ConType.label:
                    Label theLabel = new Label();
                    theLabel.DataBinding += new EventHandler(theLabel_DataBinding);
                    theLabel.Text = columnName;
                    container.Controls.Add(theLabel);
                    break;

            }

        }
        void theDropdown_DataBinding(object sender, EventArgs e)
        {
            DropDownList theDrop = (DropDownList)sender;
            GridViewRow theRow = (GridViewRow)theDrop.NamingContainer;
            theDrop.DataSource = theDataTable;
            theDrop.DataTextField = theDataTable.Columns[1].ColumnName;
        }

        void thetxtbox_DataBinding(object sender, EventArgs e)
        {
            TextBox theTxtBox = (TextBox)sender;
            GridViewRow theRow = (GridViewRow)theTxtBox.NamingContainer;
            ////////////////////Adding Attributes ///////////////////////////////
            if (theAttributes.Rows.Count > 0)
            {
                foreach (DataRow dr in theAttributes.Rows)
                {
                    theTxtBox.Attributes.Add(dr[0].ToString(), dr[1].ToString());
                }
                theTxtBox.Text = "";
            }
            //////////////////////////////////////////////////////////////////////

            /////////////////////Setting Length //////////////////////////////////
            if (theFieldLength > 0)
                theTxtBox.MaxLength = theFieldLength;
            //////////////////////////////////////////////////////////////////////
        }

        void chkbox_DataBinding(object sender, EventArgs e)
        {
            CheckBox theChk = (CheckBox)sender;
            GridViewRow theRow = (GridViewRow)theChk.NamingContainer;
            theChk.Text = DataBinder.Eval(theRow.DataItem, columnName).ToString();
            theChk.ID = "ChkBox";
            //throw new Exception("The method or operation is not implemented.");
        }

        void theLabel_DataBinding(object sender, EventArgs e)
        {
            Label theLabel = (Label)sender;
            GridViewRow theRow = (GridViewRow)theLabel.NamingContainer;
            theLabel.Text = columnName;
        }
    }
}