namespace IQCare.SCM
{
    partial class frmSCMitemList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSCMitemList));
            this.tvSCMlist = new System.Windows.Forms.TreeView();
            this.txtstockItem = new System.Windows.Forms.TextBox();
            this.lblstockItem = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lstSearch = new System.Windows.Forms.ListBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvSCMlist
            // 
            this.tvSCMlist.Location = new System.Drawing.Point(0, 37);
            this.tvSCMlist.Name = "tvSCMlist";
            this.tvSCMlist.Size = new System.Drawing.Size(852, 417);
            this.tvSCMlist.TabIndex = 3;
            this.tvSCMlist.Tag = "pnlSubPanel";
            this.tvSCMlist.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSCMlist_NodeMouseClick);
            // 
            // txtstockItem
            // 
            this.txtstockItem.Location = new System.Drawing.Point(196, 11);
            this.txtstockItem.Name = "txtstockItem";
            this.txtstockItem.Size = new System.Drawing.Size(509, 20);
            this.txtstockItem.TabIndex = 49;
            this.txtstockItem.Tag = "txtTextBox";
            this.txtstockItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtstockItem_KeyUp);
            // 
            // lblstockItem
            // 
            this.lblstockItem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblstockItem.AutoSize = true;
            this.lblstockItem.Location = new System.Drawing.Point(124, 14);
            this.lblstockItem.Name = "lblstockItem";
            this.lblstockItem.Size = new System.Drawing.Size(67, 13);
            this.lblstockItem.TabIndex = 48;
            this.lblstockItem.Tag = "lblLabel";
            this.lblstockItem.Text = "Search Item:";
            this.lblstockItem.Click += new System.EventHandler(this.lblstockItem_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 59;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Window;
            this.btnExit.Location = new System.Drawing.Point(770, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 25);
            this.btnExit.TabIndex = 45;
            this.btnExit.Tag = "btnSingleText";
            this.btnExit.Text = "&Close";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lstSearch
            // 
            this.lstSearch.FormattingEnabled = true;
            this.lstSearch.Location = new System.Drawing.Point(710, 12);
            this.lstSearch.Name = "lstSearch";
            this.lstSearch.Size = new System.Drawing.Size(29, 17);
            this.lstSearch.TabIndex = 60;
            this.lstSearch.Tag = "lstListBox";
            this.lstSearch.Visible = false;
            this.lstSearch.DoubleClick += new System.EventHandler(this.lstSearch_DoubleClick);
            this.lstSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstSearch_KeyDown);
            // 
            // frmSCMitemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.lstSearch);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtstockItem);
            this.Controls.Add(this.lblstockItem);
            this.Controls.Add(this.tvSCMlist);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSCMitemList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Items List";
            this.Load += new System.EventHandler(this.frmSCMitemList_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvSCMlist;
        private System.Windows.Forms.TextBox txtstockItem;
        private System.Windows.Forms.Label lblstockItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListBox lstSearch;
    }
}