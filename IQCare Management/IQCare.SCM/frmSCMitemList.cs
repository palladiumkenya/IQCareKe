using System;
using System.Data;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.SCM;


namespace IQCare.SCM
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmSCMitemList : Form
    {
        /// <summary>
        /// The drug listing
        /// </summary>
        DataTable theDrugListing = new DataTable();
        /// <summary>
        /// Initializes a new instance of the <see cref="frmSCMitemList"/> class.
        /// </summary>
        public frmSCMitemList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init_s the form.
        /// </summary>
        private void Init_Form()
        {
            BindTreeView();
        }

        /// <summary>
        /// Binds the TreeView.
        /// </summary>
        private void BindTreeView()
        {
            IMasterList theItemManager = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList, BusinessProcess.SCM");
            DataSet theDS = theItemManager.GetItemMasterListing();
            theDrugListing = theDS.Tables[0];
            string theItemType = "";
            string theDrugTypeName="";
            string theGenericName="";
            //string theDrugName="";
            TreeNode theRootNode = new TreeNode();
            TreeNode theRootDTypeNode = new TreeNode();
            TreeNode theRootGenericNode = new TreeNode();
            foreach (DataRow theDR in theDS.Tables[0].Rows)
            {
                if (theItemType != theDR["ItemTypeName"].ToString())
                {
                    theRootNode = new TreeNode();
                    theRootNode.Text = theDR["ItemTypeName"].ToString();
                    tvSCMlist.Nodes.Add(theRootNode);
                    theItemType = theDR["ItemTypeName"].ToString();
                }
                if (theDrugTypeName != theDR["DrugTypeName"].ToString())
                {
                    theRootDTypeNode = new TreeNode();
                    theRootDTypeNode.Text = theDR["DrugTypeName"].ToString();
                    theRootNode.Nodes.Add(theRootDTypeNode);
                    theDrugTypeName = theDR["DrugTypeName"].ToString();
                }
                if (theGenericName != theDR["GenericName"].ToString())
                {
                    theRootGenericNode = new TreeNode();
                    theRootGenericNode.Text = theDR["GenericName"].ToString();
                    theRootDTypeNode.Nodes.Add(theRootGenericNode);
                    theGenericName = theDR["GenericName"].ToString();
                }
                if (theDR["DrugName"].ToString() != "")
                {
                    TreeNode theDrugNode = new TreeNode();
                    theDrugNode.Text = theDR["DrugName"].ToString();
                    theDrugNode.Tag = theItemType+"-"+theDrugTypeName+"-"+ theDR["Drug_Pk"].ToString();
                    theRootGenericNode.Nodes.Add(theDrugNode);
                }
            }
        }
        /// <summary>
        /// Handles the Load event of the frmSCMitemList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmSCMitemList_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
        }

        /// <summary>
        /// Handles the KeyUp event of the txtstockItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtstockItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtstockItem.Text != "")
            {
                lstSearch.Visible = true;
                lstSearch.Width = txtstockItem.Width;
                lstSearch.Left = txtstockItem.Left;
                lstSearch.Top = txtstockItem.Top + txtstockItem.Height;
                lstSearch.Height = 300;
                DataView theDV = new DataView(theDrugListing);
                theDV.RowFilter = "DrugName like '%" + txtstockItem.Text + "%'";
                if (theDV.Count > 0)
                {
                    DataTable theDT = theDV.ToTable();
                    BindFunctions theBindManager = new BindFunctions();
                    theBindManager.Win_BindListBox(lstSearch, theDT, "DrugName", "ListId");
                }
                else
                {
                    lstSearch.DataSource = null;
                }

            }
            else
            {
                lstSearch.Visible = false;
            }
            if (e.KeyCode == Keys.Down)
                lstSearch.Select();

        }

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList,IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the NodeMouseClick event of the tvSCMlist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeNodeMouseClickEventArgs"/> instance containing the event data.</param>
        private void tvSCMlist_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode theNode = (TreeNode)e.Node;
            if (theNode.Tag != null)
            {
                if (theNode.Tag.ToString() != "")
                {
                    GblIQCare.theItemId = theNode.Tag.ToString();
                    Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmItemMaster,IQCare.SCM"));
                    theForm.MdiParent = this.MdiParent;
                    theForm.Left = 0;
                    theForm.Top = 2;
                    theForm.Show();
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Handles the DoubleClick event of the lstSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lstSearch_DoubleClick(object sender, EventArgs e)
        {
            GblIQCare.theItemId = lstSearch.SelectedValue.ToString();
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmItemMaster,IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the lblstockItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lblstockItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the KeyDown event of the lstSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void lstSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EventArgs s = new EventArgs();
                lstSearch_DoubleClick(sender, s);
            }
        }

    }
}
