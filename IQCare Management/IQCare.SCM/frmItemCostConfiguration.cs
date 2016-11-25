using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Application.Presentation;
using Interface.Administration;
using Interface.SCM;
using Interface.Billing;


namespace IQCare.SCM
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmItemCostConfiguration : Form
    {
        /// <summary>
        /// The enable selection
        /// </summary>
        Boolean enableSelection = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="frmItemCostConfiguration"/> class.
        /// </summary>
        public frmItemCostConfiguration()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frmItemCostConfiguration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmItemCostConfiguration_Load(object sender, EventArgs e)
        {
            Init_Form();

        }

        /// <summary>
        /// Init_s the form.
        /// </summary>
        private void Init_Form()
        {
            BindItemTypeDropdown();
            ddlItemType.SelectedIndex = -1;
            enableSelection = true;
          

        }

        /// <summary>
        /// Binds the item type dropdown.
        /// </summary>
        private void BindItemTypeDropdown()
        {
            IQCareUtils theUtils = new IQCareUtils();
            ddlItemType.Items.Clear();
            IItemMaster objProgramlist = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
                //("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataTable theDT = objProgramlist.GetItemTypes;
            
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "DeleteFlag= 0 And (ItemName = 'Billables' OR ItemName = 'Consumables' OR ItemName = 'Pharmaceuticals' OR ItemName = 'Lab Tests' OR ItemName = 'Visit Type' OR ItemName= 'Ward Admission' ) ";
            theDV.Sort = "ItemName Asc";
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(ddlItemType, theDV.ToTable(), "ItemName", "ItemTypeID");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlItemType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!enableSelection) return;
            bs_ItemCost.DataSource = null;
            txtSearch.Text = "";
       
            if (ddlItemType.SelectedIndex >-1)
            {
                 loadData((int)ddlItemType.SelectedValue);
                 
            }
            
           
           
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        private void loadData(int itemType)
        {
            dgvItemSubitemDetails.Columns.Clear();
            IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
            DataTable theDT = bMgr.GetPriceList(itemType);
            bs_ItemCost.DataSource = theDT;//i can change it
            bnvItemCost.BindingSource = bs_ItemCost;
            dgvItemSubitemDetails.DataSource = bs_ItemCost;
            dgvItemSubitemDetails.Columns["ItemID"].Visible = false;
            dgvItemSubitemDetails.Columns["ItemTypeID"].Visible = false;
           // dgvItemSubitemDetails.Columns["Name"].ReadOnly = true;
            dgvItemSubitemDetails.Columns["ItemName"].HeaderText = "Name";
            dgvItemSubitemDetails.Columns["PriceDate"].HeaderText = "Effective Date";
            dgvItemSubitemDetails.Columns["SellingPrice"].HeaderText = "Item Selling Price";
           // dgvItemSubitemDetails.Columns["Effective Date"].ReadOnly = true;
            
            dgvItemSubitemDetails.Columns["PharmacyPriceType"].Visible = false;

            

            //Check if it is drugs select. if not hide the price type column
            if (ddlItemType.Text.ToLower() == "pharmaceuticals")
            {
                DataGridViewComboBoxColumn dgvc = new DataGridViewComboBoxColumn();
                dgvc.DataPropertyName = "PharmacyPriceType";
                DataTable data = new DataTable();
                data.Columns.Add(new DataColumn("Value", typeof(Int32)));
                data.Columns.Add(new DataColumn("Description", typeof(string)));

                data.Rows.Add("0", "Item");
                data.Rows.Add("1", "Dose");


                dgvc.DataSource = data;
                dgvc.ValueMember = "Value";
                dgvc.DisplayMember = "Description";
                dgvc.HeaderText = "Price Type";
                dgvItemSubitemDetails.Columns.Add(dgvc);
            }
            
         
            //dgvItemSubitemDetails.Columns["PharmacyPriceType"]

        //    DgvFilterManager filterManager = new DgvFilterManager(dgvItemSubitemDetails);

        }

        /// <summary>
        /// Handles the CellValueChanged event of the dgvItemSubitemDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvItemSubitemDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgvItemSubitemDetails.CurrentCell.Style.BackColor=Color.Yellow;
           // int row=dgvItemSubitemDetails.CurrentCell.RowIndex;

            DataRowView theRow= (DataRowView)dgvItemSubitemDetails.CurrentRow.DataBoundItem;
           // theRow["Effective Date"] = DateTime.Now;
            //if (theRow["Item Selling Price"].ToString() == "")
            //    theRow["Item Selling Price"] = 0;
            if (theRow["SellingPrice"].ToString() == "")
                theRow["SellingPrice"] = 0;
        }

        /// <summary>
        /// Handles the Click event of the btnclose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnclose_Click(object sender, EventArgs e)
        {
            if (bs_ItemCost.DataSource == null)
            {
                closeform();
                return;
            }
            DataTable theDT=((DataTable)bs_ItemCost.DataSource).GetChanges();
            if (theDT != null)
            {
                if (MessageBox.Show("Close without saving?", "Confirm Close", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    closeform();
                }
            }
            else
                closeform();
        }
        /// <summary>
        /// Closeforms this instance.
        /// </summary>
        private void closeform()
        {
            /*Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList,IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();*/
            this.Close();

        }

        /// <summary>
        /// Handles the Click event of the btn_cancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            loadData((int)ddlItemType.SelectedValue);
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable theDT=((DataTable)bs_ItemCost.DataSource).GetChanges();
            if (theDT != null)
            {
                IBilling objProgramlist = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                int i = objProgramlist.SavePriceList(theDT, GblIQCare.AppUserId);
                if (i < 1) return;
                loadData((int)ddlItemType.SelectedValue);
                MessageBox.Show("Saved Successfully.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
           
        }

        /// <summary>
        /// Handles the TextChanged event of the txtSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           // bs_ItemCost.Filter = String.Format("Name like '%{0}%'", txtSearch.Text);
            bs_ItemCost.Filter = String.Format("ItemName like '%{0}%'", txtSearch.Text);
        }
        /// <summary>
        /// The o date time picker
        /// </summary>
        DateTimePicker oDateTimePicker;
        /// <summary>
        /// Handles the CellClick event of the dgvItemSubitemDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvItemSubitemDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                //Initialized a new DateTimePicker Control  
                oDateTimePicker = new DateTimePicker();
                oDateTimePicker.MinDate = DateTime.Today;
                try
                {

                    oDateTimePicker.Text = dgvItemSubitemDetails.CurrentCell.Value.ToString();
                }
                catch// (Exception ex)
                {

                }
                //Adding DateTimePicker control into DataGridView   
                dgvItemSubitemDetails.Controls.Add(oDateTimePicker);

                // Setting the format (i.e. 2014-10-10)  
                oDateTimePicker.Format = DateTimePickerFormat.Short;

                // It returns the retangular area that represents the Display area for a cell  
                Rectangle oRectangle = dgvItemSubitemDetails.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                //Setting area for DateTimePicker Control  
                oDateTimePicker.Size = new Size(oRectangle.Width, oRectangle.Height);

                // Setting Location  
                oDateTimePicker.Location = new Point(oRectangle.X, oRectangle.Y);

                // An event attached to dateTimePicker Control which is fired when DateTimeControl is closed  
                oDateTimePicker.CloseUp += oDateTimePicker_CloseUp;

                // An event attached to dateTimePicker Control which is fired when any date is selected  
                oDateTimePicker.TextChanged += dateTimePicker_OnTextChange;

                // Now make it visible  
                oDateTimePicker.Visible = true; 
            }

        }


        /// <summary>
        /// Handles the OnTextChange event of the dateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            // Saving the 'Selected Date on Calendar' into DataGridView current cell  
            dgvItemSubitemDetails.CurrentCell.Value = oDateTimePicker.Text;
        }


        /// <summary>
        /// Handles the CloseUp event of the oDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
            oDateTimePicker.Visible = false;
            dgvItemSubitemDetails.Controls.Remove(oDateTimePicker);
        }

        /// <summary>
        /// Handles the Scroll event of the dgvItemSubitemDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ScrollEventArgs"/> instance containing the event data.</param>
        private void dgvItemSubitemDetails_Scroll(object sender, ScrollEventArgs e)
        {
            dgvItemSubitemDetails.Controls.Remove(oDateTimePicker);
        }

        /// <summary>
        /// Handles the CellLeave event of the dgvItemSubitemDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvItemSubitemDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvItemSubitemDetails.Controls.Remove(oDateTimePicker);
        }

        /// <summary>
        /// Handles the RowValidated event of the dgvItemSubitemDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvItemSubitemDetails_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItemSubitemDetails.CurrentRow.Cells[3].Value == null) return;
            if (dgvItemSubitemDetails.CurrentRow.Cells[3].Value.ToString() != "")
                if (dgvItemSubitemDetails.CurrentRow.Cells[4].Value.ToString() == "")
                {
                    dgvItemSubitemDetails.CurrentRow.Cells[4].Value = DateTime.Today;
                    dgvItemSubitemDetails.CurrentRow.Cells[4].Style.BackColor = Color.Yellow;
                }
        }

        /// <summary>
        /// Handles the ColumnWidthChanged event of the dgvItemSubitemDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewColumnEventArgs"/> instance containing the event data.</param>
        private void dgvItemSubitemDetails_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dgvItemSubitemDetails.Controls.Remove(oDateTimePicker);
        }  
    }

}
