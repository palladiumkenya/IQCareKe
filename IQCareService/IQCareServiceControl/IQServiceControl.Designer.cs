using System.ComponentModel;
using System.Drawing;
using System;
using System.Windows.Forms;
namespace IQCareServiceControl
{
    partial class IQServiceControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IQServiceControl));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.stsImage = new System.Windows.Forms.PictureBox();
            this.theNotifier = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnMinimise = new System.Windows.Forms.Button();
            this.thetimer = new System.Windows.Forms.Timer(this.components);
            this.btnProperty = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 98);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(158, 118);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(66, 29);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(158, 153);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(66, 29);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "St&op";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // stsImage
            // 
            this.stsImage.Location = new System.Drawing.Point(12, 118);
            this.stsImage.Name = "stsImage";
            this.stsImage.Size = new System.Drawing.Size(117, 143);
            this.stsImage.TabIndex = 3;
            this.stsImage.TabStop = false;
            // 
            // theNotifier
            // 
            this.theNotifier.Visible = true;
            this.theNotifier.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.theNotifier_MouseDoubleClick);
            // 
            // btnMinimise
            // 
            this.btnMinimise.Location = new System.Drawing.Point(158, 224);
            this.btnMinimise.Name = "btnMinimise";
            this.btnMinimise.Size = new System.Drawing.Size(66, 29);
            this.btnMinimise.TabIndex = 4;
            this.btnMinimise.Text = "&Close";
            this.btnMinimise.UseVisualStyleBackColor = true;
            this.btnMinimise.Click += new System.EventHandler(this.btnMinimise_Click);
            // 
            // thetimer
            // 
            this.thetimer.Enabled = true;
            this.thetimer.Interval = 10;
            this.thetimer.Tick += new System.EventHandler(this.thetimer_Tick);
            // 
            // btnProperty
            // 
            this.btnProperty.Location = new System.Drawing.Point(158, 188);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new System.Drawing.Size(66, 29);
            this.btnProperty.TabIndex = 5;
            this.btnProperty.Text = "P&roperties";
            this.btnProperty.UseVisualStyleBackColor = true;
            this.btnProperty.Click += new System.EventHandler(this.btnProperty_Click);
            // 
            // IQServiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnProperty);
            this.Controls.Add(this.btnMinimise);
            this.Controls.Add(this.stsImage);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IQServiceControl";
            this.Text = "IQCare Service Controller";
            this.Load += new System.EventHandler(this.IQServiceControl_Load);
            this.Shown += new System.EventHandler(this.IQServiceControl_Shown);
            this.Resize += new System.EventHandler(this.IQServiceControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

       // private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnMinimise;
        private System.Windows.Forms.Button btnProperty;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox stsImage;
        private System.ServiceProcess.ServiceController theControl = new System.ServiceProcess.ServiceController();
        private System.Windows.Forms.NotifyIcon theNotifier;
        private System.Windows.Forms.Timer thetimer;

    }
}

