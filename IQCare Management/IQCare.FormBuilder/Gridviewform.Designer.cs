namespace IQCare.FormBuilder
{
    partial class Gridviewform
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgwDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwDetails
            // 
            this.dgwDetails.AllowUserToAddRows = false;
            this.dgwDetails.AllowUserToDeleteRows = false;
            this.dgwDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgwDetails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgwDetails.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgwDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgwDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwDetails.GridColor = System.Drawing.SystemColors.Window;
            this.dgwDetails.Location = new System.Drawing.Point(43, 51);
            this.dgwDetails.Name = "dgwDetails";
            this.dgwDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwDetails.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgwDetails.Size = new System.Drawing.Size(764, 396);
            this.dgwDetails.TabIndex = 9;
            this.dgwDetails.Tag = "dgwDataGridView";
            // 
            // Gridviewform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 504);
            this.Controls.Add(this.dgwDetails);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Gridviewform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Gridviewform";
            this.Load += new System.EventHandler(this.Gridviewform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwDetails;



    }
}