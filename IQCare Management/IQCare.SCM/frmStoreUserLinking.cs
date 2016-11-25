using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.SCM;

/// <summary>
/// 
/// </summary>
namespace IQCare.SCM
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmStoreUserLinking : Form
    {
        /// <summary>
        /// The ds store list
        /// </summary>
        DataSet dsStoreList = new DataSet();
        /// <summary>
        /// Initializes a new instance of the <see cref="frmStoreUserLinking"/> class.
        /// </summary>
        public frmStoreUserLinking()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frmStoreUserLinking control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmStoreUserLinking_Load(object sender, EventArgs e)
        {
            Init_Form();
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);

        }
        /// <summary>
        /// Init_s the form.
        /// </summary>
        private void Init_Form()
        {
             IMasterList objStoreList = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
             dsStoreList = objStoreList.GetStoreUserLink(0);
             BindStoreDropdown();

        }


        /// <summary>
        /// Binds the store dropdown.
        /// </summary>
        private void BindStoreDropdown()
        {
            try
            {
               
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCombo(ddlStore, dsStoreList.Tables[0], "Name", "Id", "Name");

                //BindFunctions theBind = new BindFunctions();
                chkItemList.DataSource = null;

               // theBind.Win_BindCheckListBox(chkItemList, dsStoreList.Tables[1], "Name", "UserID");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the chkItemList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        /// <summary>
        /// Binds the item list.
        /// </summary>
        private void BindItemList()
        {
            try
            {
                
                //for (int k = 0; k < chkItemList.Items.Count; k++)
                //{
                //    this.chkItemList.SetItemChecked(k, false);
                //}
                chkItemList.DataSource = null;
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCheckListBox(chkItemList, dsStoreList.Tables[1], "Name", "UserID", "SortIndex");
                
                for (int i = 0; i < dsStoreList.Tables[2].Rows.Count; i++)
                {
                    for (int j = 0; j < chkItemList.Items.Count; j++)
                    {
                        if (Convert.ToInt32(dsStoreList.Tables[2].Rows[i]["UserID"]) == Convert.ToInt32((((System.Data.DataRowView)(chkItemList.Items[j])).Row.ItemArray[0]).ToString()))
                        {
                            this.chkItemList.SetItemChecked(j, true);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {


            try
            {
                
                List<int> UserList = new List<int>();
                int storeId = Convert.ToInt32(ddlStore.SelectedValue);
                for (int i = 0; i < chkItemList.Items.Count; i++)
                {
                    if (chkItemList.GetItemChecked(i) == true)
                    {
                        UserList.Add(Convert.ToInt32((((System.Data.DataRowView)(chkItemList.Items[i])).Row.ItemArray[0]).ToString()));                 

                    }
                }

                //string ItemType = ddlItemType.SelectedValue.ToString();
                IMasterList objMasterlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                int ret = objMasterlist.SaveUpdateStoreUserLinking(storeId,UserList);
                dsStoreList = objMasterlist.GetStoreUserLink(storeId);
                BindItemList();
              //  BindStoreDropdown();
                if (ret > 0)
                {
                    IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                    return;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }





        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlStore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStore.SelectedIndex != 0 && Convert.ToInt32(ddlStore.SelectedValue.ToString()) > 0)
            {
                IMasterList objStoreList = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                dsStoreList = objStoreList.GetStoreUserLink(Convert.ToInt32(ddlStore.SelectedValue.ToString()));
                BindItemList();
            }
            else
            {
                chkItemList.DataSource = null;
            }

        }
        //deepika
        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        
         {
            Form theForm = new Form();
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }

        }

    }

