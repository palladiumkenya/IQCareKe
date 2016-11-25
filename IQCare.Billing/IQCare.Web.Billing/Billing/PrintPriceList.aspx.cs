using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Application.Common;
using Application.Presentation;
using Interface.Billing;
using IQCare.Web.UILogic;

namespace IQCare.Web.Billing
{
    public partial class PrintPriceList : System.Web.UI.Page
    {
        /// <summary>
        /// The XML document
        /// </summary>
        private XmlDocument xmlDoc = new XmlDocument();
        /// <summary>
        /// The XSL document
        /// </summary>
        private XmlDocument xslDoc;//BusinessProcess.Billing.dll
        IBilling bMGR = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
        /// <summary>
        /// The authentication
        /// </summary>
       // AuthenticationManager Authentication = new AuthenticationManager();
        CurrentSession thisSession = CurrentSession.Current;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                thisSession = CurrentSession.Current;
                if (thisSession == null)
                {
                    Response.Redirect("~/frmLogOff.aspx");
                }
                if (thisSession.HasBilling && thisSession.HasFeaturePermission("BILLING_CONFIGURATION") == true)
                {
                    if (Session["ReportData"] != null)
                    {
                        Hashtable ht = (Hashtable)Session["ReportData"];
                        string strOut_XSL;
                        strOut_XSL = ht["style"].ToString();
                        xslDoc = new XmlDocument();
                        this.xmlDoc = new XmlDocument();
                        
                        xslDoc.LoadXml(strOut_XSL);
                        xmlDoc.LoadXml(ht["data"].ToString());

                        bool hasDec = xmlDoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration;
                        if (!hasDec)
                        {
                            //Create an XML declaration. 
                            XmlDeclaration xmldecl;
                            xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                            //Add the new node to the document.
                            XmlElement root = xmlDoc.DocumentElement;
                            xmlDoc.InsertBefore(xmldecl, root);
                        }
                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xslDoc.NameTable);
                        nsmgr.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");

                        XPathNavigator xslNav = xslDoc.CreateNavigator();

                        XslCompiledTransform trans = new XslCompiledTransform();

                        Response.Clear();
                        Response.ContentType = "text/html; charset=UTF-8";

                        XsltSettings xslSessings = new XsltSettings();
                        xslSessings.EnableScript = true;

                        trans.Load(xslNav, xslSessings, new XmlUrlResolver());

                        trans.Transform(xmlDoc.CreateNavigator(), null, Response.OutputStream);

                        Response.Flush();
                        Session["ReportData"] = null;
                    }
                  /*  this.xmlDoc = new XmlDocument();
                    string strfile = Server.MapPath("~/Billing/PriceList_1_0.xsl");

                    byte[] fileData = File.ReadAllBytes(strfile);

                    string strOut_XSL;

                    strOut_XSL = XmlEncodingBOM.GetBOMString(fileData);

                    xslDoc = new XmlDocument();

                    xslDoc.LoadXml(strOut_XSL);

                    // xmlDoc.LoadXml(docX.ToString());
                    xmlDoc.LoadXml(bMGR.GetPriceListXML(Session["AppLocation"].ToString(), Session["AppUserName"].ToString()));
                    //Create an XML declaration. 
                    XmlDeclaration xmldecl;
                    xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    //Add the new node to the document.
                    XmlElement root = xmlDoc.DocumentElement;
                    xmlDoc.InsertBefore(xmldecl, root);

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xslDoc.NameTable);
                    nsmgr.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");

                    XPathNavigator xslNav = xslDoc.CreateNavigator();

                    XslCompiledTransform trans = new XslCompiledTransform();

                    Response.Clear();
                    Response.ContentType = "text/html; charset=UTF-8";

                    XsltSettings xslSessings = new XsltSettings();
                    xslSessings.EnableScript = true;

                    trans.Load(xslNav, xslSessings, new XmlUrlResolver());

                    trans.Transform(xmlDoc.CreateNavigator(), null, Response.OutputStream);

                    Response.Flush();*/
                }
                else
                {
                    Response.Redirect("~/frmLogin.aspx?error=true");
                }
            }
            catch (Exception ex)
            {
                Session["ReportData"] = null;
                Response.Write(@"<span style=""background-color: #FFFFC0; border: solid 1px #C00000;font-weight: bold; color: #800000"">" + ex.Message + "</span>");
                Response.Flush();
                Response.End();
            }
        }
    }
}