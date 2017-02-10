using Entities.CCC.Baseline;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Baseline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace IQCare.Web.CCC.WebService
{
    public class ListVaccination
    {
        public string vaccineType { get; set; }
        public string vaccineStage { get; set; }
    }
    /// <summary>
    /// Summary description for OneTimeEventsTrackerService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class OneTimeEventsTrackerService : System.Web.Services.WebService
    {
        private string Msg { get; set; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string addOneTimeEventsTracker(string Stage1DateValue, string Stage2DateValue, string Stage3DateValue,
    string SexPartnerDateValue, string INHStartDateValue, string INHCompletion, string CompletionDate, string adultVaccine, string vaccines)
        {
            try
            {
                var jss = new JavaScriptSerializer();
                IList<ListVaccination> data = jss.Deserialize<IList<ListVaccination>>(vaccines);

                var dataAdult = jss.Deserialize<IList<Object>>(adultVaccine);


                PatientDisclosureManager disclosure = new PatientDisclosureManager();
                INHProphylaxisManager inhProphylaxis = new INHProphylaxisManager();
                PatientVaccinationManager patientVaccination = new PatientVaccinationManager();

                int patientId = int.Parse(Session["PatientId"].ToString());
                int patientMasterVisitId = int.Parse(Session["PatientMasterVisitId"].ToString());
                //int patientId = 15;
                //int patientMasterVisitId = 10;


                if (Stage1DateValue != null)
                {
                    disclosure.AddPatientDisclosure(patientId, patientMasterVisitId, "Adolescents", "Stage1", DateTime.Parse(Stage1DateValue));
                }
                else if (Stage2DateValue != null)
                {
                    disclosure.AddPatientDisclosure(patientId, patientMasterVisitId, "Adolescents", "Stage2", DateTime.Parse(Stage2DateValue));
                }
                else if (Stage3DateValue != null)
                {
                    disclosure.AddPatientDisclosure(patientId, patientMasterVisitId, "Adolescents", "Stage3", DateTime.Parse(Stage3DateValue));
                }
                else if (SexPartnerDateValue != null)
                {
                    disclosure.AddPatientDisclosure(patientId, patientMasterVisitId, "Adolescents", "SexPartner", DateTime.Parse(SexPartnerDateValue));
                }


                INHProphylaxis prophylaxis = new INHProphylaxis()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    StartDate = DateTime.Parse(INHStartDateValue),
                    Complete = Boolean.Parse(INHCompletion),
                    CompletionDate = DateTime.Parse(CompletionDate)
                };

                inhProphylaxis.addINHProphylaxis(prophylaxis);

                for (var i = 0; i < data.Count; i++)
                {
                    PatientVaccination patientVaccine = new PatientVaccination()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        Vaccine = LookupLogic.GetLookUpMasterId(data[i].vaccineType),
                        VaccineStage = data[i].vaccineStage
                    };

                    patientVaccination.addPatientVaccination(patientVaccine);
                }

                for (var i = 0; i < dataAdult.Count; i++)
                {
                    int vaccine = 0;
                    if (dataAdult[i].ToString() == "FluVaccine" || dataAdult[i].ToString() == "HBV")
                    {
                        vaccine = LookupLogic.GetLookUpMasterId(dataAdult[i].ToString());
                    }

                    PatientVaccination patientVaccine = new PatientVaccination()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        Vaccine = vaccine,
                        VaccineStage = dataAdult[i].ToString()
                    };

                    patientVaccination.addPatientVaccination(patientVaccine);
                }

                Msg = "Successfully Added OnIime Event Tracker";
            }
            catch (Exception ex)
            {
                Msg = ex.Message + ' ' + ex.InnerException;
            }
            return Msg;
        }
    }
}
