using System;
using System.IO;
using System.Web.UI;
namespace IQCare.Web
{
    public abstract class BasePage : Page
    {
        private ObjectStateFormatter _formatter =
            new ObjectStateFormatter();
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            
        }

        protected override void
            SavePageStateToPersistenceMedium(object viewState)
        {
            MemoryStream ms = new MemoryStream();
            _formatter.Serialize(ms, viewState);
            byte[] viewStateArray = ms.ToArray();
            ClientScript.RegisterHiddenField("__COMPRESSEDVIEWSTATE",
                Convert.ToBase64String(CompressViewState.Compress(viewStateArray)));
        }
        protected override object
            LoadPageStateFromPersistenceMedium()
        {
            string vsString = Request.Form["__COMPRESSEDVIEWSTATE"];
            byte[] bytes = Convert.FromBase64String(vsString);
            bytes = CompressViewState.Decompress(bytes);
            return _formatter.Deserialize(Convert.ToBase64String(bytes));
        }
      protected  int PatientId
        {
            get
            {
                return Convert.ToInt32(Session["PatientId"]);
            }
        }
        protected int UserId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserId"]);
            }
        }
        protected int LocationId
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }
        //protected override void OnUnload(EventArgs e)
        //{
        //    Session["PatientVisitId"] = 0;
        //    Session["ServiceLocationId"] = 0;
        //}

    }
}