using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.HIVEducation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using Entities.CCC.Enrollment;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for HIVEducationService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class HIVEducationService1 : System.Web.Services.WebService
    {

        [WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetCounsellingTopics(int counsellingtopics)
        {
            LookupLogic lookupLogic = new LookupLogic();
            var data = lookupLogic.GetLnkCouncellingTypeTopics();
            var topics = lookupLogic.GetCouncellingTopics();
            var drows= data.AsEnumerable().Where(r => r.Field<int>("CouncellingTypeId") == counsellingtopics);

            ArrayList arrayList = new ArrayList();
            foreach (var row in drows)
            {

                var topic = topics.AsEnumerable()
                    .SingleOrDefault(y => y.Field<int>("ID") == Convert.ToInt32(row["CouncellingTopicId"]));

                string topicName = topic["Name"].ToString();
                int topicId = Convert.ToInt32(topic["ID"].ToString());

                arrayList.Add(new
                {
                    Id = topicId,
                    Value = topicName
                });
            }
            return arrayList;
        }
        private string Msg { get; set; }
        private int Result { get; set; }

        [WebMethod(EnableSession = true)]
        public string addHIVEDucation(int patientId, string visitdate, int councellingTypeId, string councellingType, int councellingTopicId, string councellingTopic, string comments, string other)
        {
            try
            {
                PatientManager patientManager = new PatientManager();
                LookupLogic lookupLogic = new LookupLogic();
                PatientEntity patient = patientManager.GetPatientEntity(patientId);
                int ptn_pk = patient.ptn_pk.HasValue ? patient.ptn_pk.Value : 0;
                int posID = Convert.ToInt32(Session["AppPosID"]);
                var facility = lookupLogic.GetFacility(posID.ToString());
                int facilityId = 0;
                if (facility != null)
                {
                    facilityId = facility.FacilityID;
                }
                var mstPatientLogic = new MstPatientLogic();
                int userId = Convert.ToInt32(Session["AppUserId"]);

                int visit_Pk = mstPatientLogic.AddOrdVisit(ptn_pk, facilityId, DateTime.Parse(visitdate), 10, userId, DateTime.Now, 203);
                var HEF = new HIVEducationLogic();
                Result = HEF.AddPatientHIVEducation(ptn_pk, facilityId, userId, visit_Pk, DateTime.Parse(visitdate), councellingTypeId, councellingType, councellingTopicId, councellingTopic, comments, other);
                if (Result > 0)
                {
                    Msg = "Notes Added";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public List<CounsellingData> GetPatientFollowupEducationData()
        {
            LookupLogic lookupLogic = new LookupLogic();
            PatientManager patientManager = new PatientManager();
            int patientId = Convert.ToInt32(Session["PatientPK"]);
            PatientEntity patient = patientManager.GetPatientEntity(patientId);
            int ptn_pk = patient.ptn_pk.HasValue ? patient.ptn_pk.Value : 0;

            HIVEducationLogic hivEducationLogic = new HIVEducationLogic();
            var types = lookupLogic.GetCouncellingTypes();
            var topics = lookupLogic.GetCouncellingTopics();

            var data = hivEducationLogic.GetPatientFollowupEducationData(ptn_pk);

            List<CounsellingData> counsellingData = new List<CounsellingData>();
            foreach (DataRow row  in data.Rows)
            {
                var councellingTypeId = Convert.ToInt32(row["CouncellingTypeId"].ToString());
                var councellingTopicId = Convert.ToInt32(row["CouncellingTopicId"].ToString());
                var visitDate = row["VisitDate"].ToString();
                var comments = row["Comments"].ToString();

                DataRow dr = types.AsEnumerable().SingleOrDefault(r => r.Field<int>("ID") == councellingTypeId);
                DataRow dataRow = topics.AsEnumerable().SingleOrDefault(r => r.Field<int>("ID") == councellingTopicId);

                counsellingData.Add(new CounsellingData()
                {
                    CouncellingType = dr["Name"].ToString(),
                    CouncellingTopic = dataRow["Name"].ToString(),
                    VisitDate = visitDate,
                    Comments = comments
                });
            }

            return counsellingData;
        }
    }

    public class CounsellingData
    {
        public string CouncellingType { get; set; }
        public string CouncellingTopic { get; set; }
        public string VisitDate { get; set; }
        public string Comments { get; set; }
    }
}
