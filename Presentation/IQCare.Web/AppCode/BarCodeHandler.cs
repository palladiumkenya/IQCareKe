using System;
using System.Collections.Generic;
using System.Web;
using iTextSharp.text.pdf;
namespace IQCare.Web
{

    /// <summary>
    /// Summary description for BarCodeHandler
    /// </summary>
    public class BarCodeHandler : IHttpHandler
    {

        public bool IsReusable
        {
            get { return false; }
        }


        public void ProcessRequest(System.Web.HttpContext context)
        {
            HttpRequest Request = context.Request;
            HttpResponse response = context.Response;

            Barcode39 bc39 = new Barcode39();

            if (Request["code"] != null)
            {
                bc39.Code = Request["code"];
            }
            else
            {
                bc39.Code = DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString();
            }

            System.Drawing.Image bc = bc39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
            response.ContentType = "image/gif";

            bc.Save(response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);

        }


    }
}