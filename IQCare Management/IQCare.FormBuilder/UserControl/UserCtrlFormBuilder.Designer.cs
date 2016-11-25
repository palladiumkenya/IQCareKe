namespace IQCare.FormBuilder
{
    partial class UserCtrlFormBuilder
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSectionBuilder = new System.Windows.Forms.Panel();
            this.btnAddSection = new System.Windows.Forms.Button();
            this.pnlMainPanel = new System.Windows.Forms.Panel();
            this.btnManageSection = new System.Windows.Forms.Button();
            this.pnlSectionBuilder.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSectionBuilder
            // 
            this.pnlSectionBuilder.Controls.Add(this.btnAddSection);
            this.pnlSectionBuilder.Controls.Add(this.pnlMainPanel);
            this.pnlSectionBuilder.Controls.Add(this.btnManageSection);
            this.pnlSectionBuilder.Location = new System.Drawing.Point(5, -1);
            this.pnlSectionBuilder.Name = "pnlSectionBuilder";
            this.pnlSectionBuilder.Size = new System.Drawing.Size(977, 441);
            this.pnlSectionBuilder.TabIndex = 13;
            this.pnlSectionBuilder.Tag = "pnlSubPanel";
            // 
            // btnAddSection
            // 
            this.btnAddSection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAddSection.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSection.ForeColor = System.Drawing.Color.Black;
            this.btnAddSection.Location = new System.Drawing.Point(379, 416);
            this.btnAddSection.Name = "btnAddSection";
            this.btnAddSection.Size = new System.Drawing.Size(106, 23);
            this.btnAddSection.TabIndex = 3;
            this.btnAddSection.Tag = "btnH25WFlexi";
            this.btnAddSection.Text = "&Add Section";
            this.btnAddSection.UseVisualStyleBackColor = false;
            this.btnAddSection.Click += new System.EventHandler(this.btnAddSection_Click);
            // 
            // pnlMainPanel
            // 
            this.pnlMainPanel.AutoScroll = true;
            this.pnlMainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMainPanel.Location = new System.Drawing.Point(2, 6);
            this.pnlMainPanel.Name = "pnlMainPanel";
            this.pnlMainPanel.Size = new System.Drawing.Size(972, 408);
            this.pnlMainPanel.TabIndex = 2;
            this.pnlMainPanel.Tag = "pnlSubPanel1Css";
            // 
            // btnManageSection
            // 
            this.btnManageSection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnManageSection.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageSection.ForeColor = System.Drawing.Color.Black;
            this.btnManageSection.Location = new System.Drawing.Point(488, 416);
            this.btnManageSection.Name = "btnManageSection";
            this.btnManageSection.Size = new System.Drawing.Size(106, 23);
            this.btnManageSection.TabIndex = 4;
            this.btnManageSection.Tag = "btnH25WFlexi";
            this.btnManageSection.Text = "&Manage Section";
            this.btnManageSection.UseVisualStyleBackColor = false;
            this.btnManageSection.Click += new System.EventHandler(this.btnManageSection_Click);
            // 
            // UserCtrlFormBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSectionBuilder);
            this.Name = "UserCtrlFormBuilder";
            this.Size = new System.Drawing.Size(989, 445);
            this.Tag = "";
            this.Load += new System.EventHandler(this.UserCtrlFormBuilder_Load);
            this.pnlSectionBuilder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSectionBuilder;
        private System.Windows.Forms.Button btnAddSection;
        private System.Windows.Forms.Panel pnlMainPanel;
        private System.Windows.Forms.Button btnManageSection;

    }
}
