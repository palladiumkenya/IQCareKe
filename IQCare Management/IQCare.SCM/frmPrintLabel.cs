using Application.Common;
using Application.Presentation;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace IQCare.SCM
{
    public class frmPrintLabel : Form
    {
        private IContainer components;
        private PrintDialog printDialog1;
        private Button btn_print;
        private Panel pnlusercontrol;
        private Button btnClose;

        public frmPrintLabel()
        {
            this.InitializeComponent();
        }

        public void PassUserControl(Panel print, short pages)
        {
            try
            {
                foreach (Control control in (ArrangedElementCollection)print.Controls)
                {
                    if (control is CheckBox && ((CheckBox)control).Checked)
                    {
                        PrintDocument printDocument = new PrintDocument();
                        printDocument.PrintPage += (PrintPageEventHandler)((sender, e) => this.pd_PrintPage(sender, e, print));
                        PrintDialog printDialog = new PrintDialog()
                        {
                            Document = printDocument,
                            PrinterSettings =
                            {
                                Copies = pages
                            }
                        };
                        printDocument.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e, Panel print)
        {
            try
            {
                Bitmap bitmap = new Bitmap(print.Width, print.Height, print.CreateGraphics());
                print.DrawToBitmap(bitmap, new Rectangle(0, 0, print.Width, print.Height));
                RectangleF printableArea = e.PageSettings.PrintableArea;
                e.Graphics.DrawImage((Image)bitmap, printableArea.Left, printableArea.Top, (float)print.Width, (float)print.Height);
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private void frmPrintLabel_Load(object sender, EventArgs e)
        {
            try
            {
                new clsCssStyle().setStyle((Control)this);
                int y = 0;
                this.pnlusercontrol.AutoScroll = true;
                foreach (DataRow dataRow in (InternalDataCollectionBase)GblIQCare.dtPrintLabel.Rows)
                {
                    UserPrintControl userPrintControl = new UserPrintControl();
                    userPrintControl.Name = "wucpnl" + dataRow["ItemId"].ToString();
                    userPrintControl.Location = new Point(this.pnlusercontrol.Location.X, y);
                    userPrintControl.Facility.Text = GblIQCare.AppLocation + "  Tel:" + GblIQCare.AppLocTelNo;
                    userPrintControl.Store.Text = GblIQCare.StoreName + "  " + GblIQCare.CurrentDate;
                    userPrintControl.PatientName.Text = GblIQCare.PatientName.ToUpper();
                    userPrintControl.chkDName.Text = dataRow["ItemName"].ToString();
                    userPrintControl.lbldrugquantity.Text = dataRow["qtydisp"].ToString() + " " + dataRow["DispensingUnitName"].ToString() + "   Exp:" + dataRow["ExpiryDate"].ToString();
                    userPrintControl.txtInstruction.Text = dataRow["PatientInstructions"].ToString();
                    this.pnlusercontrol.Controls.Add((Control)userPrintControl);
                    y = y + userPrintControl.Height + 25;
                }
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control1 in (ArrangedElementCollection)this.pnlusercontrol.Controls)
                {
                    if (control1 is UserControl)
                    {
                        short pages = 1;
                        foreach (Control control2 in (ArrangedElementCollection)control1.Controls)
                        {
                            if (control2 is ComboBox)
                                pages = Convert.ToInt16(((ComboBox)control2).SelectedItem);
                            if (control2 is Panel)
                                this.PassUserControl((Panel)control2, pages);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.ToString());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintLabel));
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.btn_print = new System.Windows.Forms.Button();
            this.pnlusercontrol = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.AllowSelection = true;
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.UseEXDialog = true;
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(294, 466);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(144, 23);
            this.btn_print.TabIndex = 4;
            this.btn_print.Tag = "lblLabel";
            this.btn_print.Text = "Print Label";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // pnlusercontrol
            // 
            this.pnlusercontrol.Location = new System.Drawing.Point(12, 12);
            this.pnlusercontrol.Name = "pnlusercontrol";
            this.pnlusercontrol.Size = new System.Drawing.Size(828, 448);
            this.pnlusercontrol.TabIndex = 5;
            this.pnlusercontrol.Tag = "";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(444, 466);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(153, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Tag = "lblLabel";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmPrintLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 537);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlusercontrol);
            this.Controls.Add(this.btn_print);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrintLabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Print Label";
            this.Load += new System.EventHandler(this.frmPrintLabel_Load);
            this.ResumeLayout(false);

        }
    }
}
