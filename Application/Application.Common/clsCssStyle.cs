using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace Application.Common
{
    public class clsCssStyle
    {
        private static XmlDocument xmlDoc = null;

        /// <summary>
        /// The function need to be called at form load.
        /// </summary>
        /// <param name="theControl">The form control passed here</param>
        public void setStyle(Control theControl)
        {
            try
            {
                if (xmlDoc == null)
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(ConfigurationManager.AppSettings.Get("StyleSheetRepository"));
                }

                XmlElement Root = xmlDoc.DocumentElement;
                //set form css
                XmlNode Node = Root.SelectSingleNode("Style[@Id = '" + theControl.Tag.ToString() + "']");
                if (Node != null)
                {
                    theControl.BackColor = System.Drawing.Color.FromName(Node.Attributes["BackColor"].Value);
                    theControl.ForeColor = System.Drawing.Color.FromName(Node.Attributes["ForeColor"].Value);
                    int theFontSize = Convert.ToInt32(Node.Attributes["FontSize"].Value);
                    if (Node.Attributes["FontStyle"].Value == "1")
                        theControl.Font = new System.Drawing.Font((Node.Attributes["Font"].Value.ToString()), ((float)(theFontSize)), System.Drawing.FontStyle.Bold);
                    else
                        theControl.Font = new System.Drawing.Font((Node.Attributes["Font"].Value.ToString()), ((float)(theFontSize)));

                    if (Node.Attributes["Width"].Value != "" && Node.Attributes["Height"].Value != "")
                        theControl.Size = new System.Drawing.Size(Convert.ToInt32(Node.Attributes["Width"].Value), Convert.ToInt32(Node.Attributes["Height"].Value));
                }
                //to set all sub controls properties
                if (xmlDoc != null)
                    setValue(theControl, Root);
                //foreach (Control Cntrl in theControl.Controls)
                //{
                //    SetStyleAllControls(Cntrl, Root);
                //}
            }
            catch
            {
                //// On Exception-- Continue
            }
        }
        /// <summary>
        /// To the set the value of the property of the control
        /// </summary>
        /// <param name="theControl">Control for which property value need to be set</param>
        /// <param name="Root">xml containing all the values for the controls</param>
        /// 
        //private void SetStyleAllControls(Control theControl, XmlElement theRoot)
        //{
        //    if (theControl.Controls.Count > 0)
        //    {
        //        foreach (Control ctrl in theControl.Controls)
        //        {
        //            if (ctrl.GetType().Name == "Panel" || ctrl.GetType().Name == "GroupBox" || ctrl.GetType().Name == "TabControl" || ctrl.GetType().Name == "Splitter" || ctrl.GetType().Name == "TabPage" || 
        //                ctrl.GetType().Name == "TableLayoutPanel" || ctrl.GetType().Name == "FlowLayoutPanel" || ctrl.GetType().Name == "SplitterPanel" || ctrl.GetType().Name == "SplitContainer") //pannel or groupbox
        //            {
        //                SetStyleAllControls(ctrl, theRoot);
        //            }
        //            else
        //            {
        //                setValue(ctrl, theRoot);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        setValue(theControl, theRoot);
        //    }
        //}

        public void setValue(Control theControl, XmlElement Root)
        {
            XmlNode Node;
            string theImgPath = (ConfigurationManager.AppSettings.Get("ImagePath"));
            foreach (Control ctrl in theControl.Controls)
            {
                Node = null;
                if (ctrl.Tag != null)
                {
                    Node = Root.SelectSingleNode("Style[@Id = '" + ctrl.Tag.ToString().Trim() + "']");
                    if (Node != null)
                    {
                        ctrl.ForeColor = System.Drawing.Color.FromName(Node.Attributes["ForeColor"].Value);
                        int theFontSize = Convert.ToInt32(Node.Attributes["FontSize"].Value);
                        if (Node.Attributes["FontStyle"].Value == "1")
                            ctrl.Font = new System.Drawing.Font((Node.Attributes["Font"].Value.ToString()), ((float)(theFontSize)), System.Drawing.FontStyle.Bold);
                        else
                            ctrl.Font = new System.Drawing.Font((Node.Attributes["Font"].Value.ToString()), ((float)(theFontSize)));

                        if (Node.Attributes["Width"].Value != "" && Node.Attributes["Height"].Value != "")
                            ctrl.Size = new System.Drawing.Size(Convert.ToInt32(Node.Attributes["Width"].Value), Convert.ToInt32(Node.Attributes["Height"].Value));
                        else if (Node.Attributes["Height"].Value != "")
                            ctrl.Height = Convert.ToInt32(Node.Attributes["Height"].Value);
                    }
                }
                if (ctrl.GetType().Name == "Panel" || ctrl.GetType().Name == "GroupBox" || ctrl.GetType().Name == "TabControl" || ctrl.GetType().Name == "Splitter" || ctrl.GetType().Name == "TabPage" ||
                    ctrl.GetType().Name == "TableLayoutPanel" || ctrl.GetType().Name == "FlowLayoutPanel" || ctrl.GetType().Name == "SplitterPanel" || ctrl.GetType().Name == "SplitContainer" || ctrl.GetType().Name=="UserControl") //pannel or groupbox
                {
                    setValue(ctrl, Root);
                }
            }//end of function

        }
    }

}