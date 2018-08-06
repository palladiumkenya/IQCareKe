using Entities.CCC.Tanners;
using System;
using System.Web.Services;
using IQCare.CCC.UILogic;
using System.Web.Script.Services;
using System.Collections;
using System.Collections.Generic;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for TannersStagingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TannersStagingService : System.Web.Services.WebService
    {
        private string Msg { get; set; }
        private int Result { get; set; }
        [WebMethod(EnableSession = true)]
        public string addTannersStaging(int patientId, int patientMasterVisitId, int createdBy,DateTime tannersStagingDate, int breastsGenitals, int pubicHair)
        {
            try
            {
                PatientTannersStaging tannersStaging = new PatientTannersStaging()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    CreatedBy = createdBy,
                    TannersStagingDate = tannersStagingDate,
                    BreastsGenitalsId = breastsGenitals,
                    PubicHairId = pubicHair
                };
                var tanners = new TannersStagingManager();
                Result = tanners.AddTannersStaging(tannersStaging);
                if (Result > 0)
                {
                    Msg = "Tanner Stage Added Successfully";
                }
                else
                {
                    Msg = "Incremental Error";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList LoadTannersStaging()
        {
            ArrayList rows = new ArrayList();
            var tanersLogic = new TannersStagingManager();
            List<PatientTannersStaging> list = new List<PatientTannersStaging>();
            list = tanersLogic.getTannersStaging(Convert.ToInt32(Session["PatientPK"]));
            foreach (var items in list)
            {
                string[] i = new string[5] { items.Id.ToString(), Convert.ToDateTime(items.TannersStagingDate).ToString("dd-MMM-yyyy"), LookupLogic.GetLookupNameById(items.BreastsGenitalsId).ToString(), LookupLogic.GetLookupNameById(items.PubicHairId).ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }
        [WebMethod]
        public string DeleteTanners(int tannersId)
        {
            try
            {
                var tannersLogic = new TannersStagingManager();
                tannersLogic.DeleteTanners(tannersId);
                Msg = "Deleted";
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod]
        public string recordTannersStaging(int patientId, int patientMasterVisitId, int createdBy, int recordTannersStaging)
        {
            var tanners = new TannersStagingManager();
            Result = tanners.recordTannersStaging(patientId, patientMasterVisitId,createdBy,recordTannersStaging);
            if (Result > 0)
            {
                Msg = "Tanners staging recorded";
            }
            else
            {
                Msg = "Tanners staging not recorded";
            }
            return Msg;
        }
        [WebMethod]
        public List<TannersStaging> getRecordTannersStaging()
        {
            ArrayList rows = new ArrayList();
            var tanersLogic = new TannersStagingManager();
            List<TannersStaging> list = new List<TannersStaging>();
            list = tanersLogic.getRecordTannersStaging(Convert.ToInt32(Session["PatientPK"]));
            return list;
        }
        [WebMethod(EnableSession = true)]
        public string updateRecordTannersStaging(int patientId, int patientMasterVisitId, int recordTannersStaging, int tannersId)
        {
            try
            {
                var SH = new TannersStagingManager();
                Result = SH.updateRecordTannersStaging(patientId, patientMasterVisitId, recordTannersStaging, tannersId);
                if (Result > 0)
                {
                    Msg = "Tanners staging updated";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
    }
}
