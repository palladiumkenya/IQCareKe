namespace IQCare.FormBuilder
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.queryBuilder1 = new ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder();
            this.plainTextSQLBuilder1 = new ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder(this.components);
            this.SuspendLayout();
            // 
            // queryBuilder1
            // 
            this.queryBuilder1.AddObjectFormOptions.MinimumSize = new System.Drawing.Size(430, 430);
            this.queryBuilder1.CriteriaListOptions.CriteriaListFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.queryBuilder1.DiagramObjectColor = System.Drawing.SystemColors.Window;
            this.queryBuilder1.DiagramObjectFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.queryBuilder1.FieldListOptions.DescriptionColumnOptions.Color = System.Drawing.Color.LightBlue;
            this.queryBuilder1.FieldListOptions.MarkColumnOptions.PKIcon = ((System.Drawing.Image)(resources.GetObject("resource.PKIcon")));
            this.queryBuilder1.FieldListOptions.NameColumnOptions.Color = System.Drawing.SystemColors.WindowText;
            this.queryBuilder1.FieldListOptions.NameColumnOptions.PKColor = System.Drawing.SystemColors.WindowText;
            this.queryBuilder1.FieldListOptions.TypeColumnOptions.Color = System.Drawing.SystemColors.GrayText;
            this.queryBuilder1.Location = new System.Drawing.Point(108, 107);
            this.queryBuilder1.MetadataProvider = null;
            this.queryBuilder1.MetadataTreeOptions.ImageList = null;
            this.queryBuilder1.MetadataTreeOptions.ProceduresNodeText = null;
            this.queryBuilder1.MetadataTreeOptions.SynonymsNodeText = null;
            this.queryBuilder1.MetadataTreeOptions.TablesNodeText = null;
            this.queryBuilder1.MetadataTreeOptions.ViewsNodeText = null;
            this.queryBuilder1.Name = "queryBuilder1";
            this.queryBuilder1.QueryStructureTreeOptions.ImageList = null;
            this.queryBuilder1.Size = new System.Drawing.Size(386, 344);
            this.queryBuilder1.SleepModeText = null;
            this.queryBuilder1.SnapSize = new System.Drawing.Size(5, 5);
            this.queryBuilder1.SyntaxProvider = null;
            this.queryBuilder1.TabIndex = 0;
            this.queryBuilder1.TreeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // plainTextSQLBuilder1
            // 
            this.plainTextSQLBuilder1.DynamicIndents = false;
            this.plainTextSQLBuilder1.DynamicRightMargin = false;
            this.plainTextSQLBuilder1.ExpressionSubqueryFormat.FromClauseFormat.NewLineAfterDatasource = false;
            this.plainTextSQLBuilder1.ExpressionSubqueryFormat.MainPartsFromNewLine = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 552);
            this.Controls.Add(this.queryBuilder1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder queryBuilder1;
        private ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder plainTextSQLBuilder1;
    }
}