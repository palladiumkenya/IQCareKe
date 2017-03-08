using Entities.CCC.Baseline;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Baseline;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Protocols;

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
        private DateTime? IsCompletionDate { get; set; }
        private DateTime? IsINHStartDateValue { get; set; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
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

                int patientId = int.Parse(HttpContext.Current.Session["PatientId"].ToString());
                //int patientId = int.Parse(Session["PatientId"].ToString());
                int patientMasterVisitId = int.Parse(Session["PatientMasterVisitId"].ToString());

                if (String.IsNullOrEmpty(CompletionDate))
                {
                    IsCompletionDate = null;
                }
                else
                {
                    IsCompletionDate = DateTime.Parse(CompletionDate);
                }

                if (String.IsNullOrEmpty(INHStartDateValue))
                {
                    IsINHStartDateValue = null;
                }
                else
                {
                    IsINHStartDateValue = DateTime.Parse(INHStartDateValue);
                }


                if (!String.IsNullOrWhiteSpace(Stage1DateValue))
                {
                    List<PatientDisclosure> patientDisclosures =  disclosure.GetPatientDisclosure(patientId, "Adolescents", "Stage1");
                    if (patientDisclosures.Count > 0)
                    {
                        patientDisclosures[0].DisclosureDate = DateTime.Parse(Stage1DateValue);
                        disclosure.UpdatePatientDisclosure(patientDisclosures[0]);
                    }else
                        disclosure.AddPatientDisclosure(patientId, patientMasterVisitId, "Adolescents", "Stage1", DateTime.Parse(Stage1DateValue));
                }

                if (!String.IsNullOrWhiteSpace(Stage2DateValue))
                {
                    List<PatientDisclosure> patientDisclosures = disclosure.GetPatientDisclosure(patientId, "Adolescents", "Stage2");
                    if (patientDisclosures.Count > 0)
                    {
                        patientDisclosures[0].DisclosureDate = DateTime.Parse(Stage2DateValue);
                        disclosure.UpdatePatientDisclosure(patientDisclosures[0]);
                    }
                    else
                        disclosure.AddPatientDisclosure(patientId, patientMasterVisitId, "Adolescents", "Stage2", DateTime.Parse(Stage2DateValue));
                }

                if (!String.IsNullOrWhiteSpace(Stage3DateValue))
                {
                    List<PatientDisclosure> patientDisclosures = disclosure.GetPatientDisclosure(patientId, "Adolescents", "Stage3");
                    if (patientDisclosures.Count > 0)
                    {
                        patientDisclosures[0].DisclosureDate = DateTime.Parse(Stage3DateValue);
                        disclosure.UpdatePatientDisclosure(patientDisclosures[0]);
                    }
                    else
                        disclosure.AddPatientDisclosure(patientId, patientMasterVisitId, "Adolescents", "Stage3", DateTime.Parse(Stage3DateValue));
                }

                if (!String.IsNullOrWhiteSpace(SexPartnerDateValue))
                {
                    List<PatientDisclosure> patientDisclosures = disclosure.GetPatientDisclosure(patientId, "Adolescents", "SexPartner");
                    if (patientDisclosures.Count > 0)
                    {
                        patientDisclosures[0].DisclosureDate = DateTime.Parse(SexPartnerDateValue);
                        disclosure.UpdatePatientDisclosure(patientDisclosures[0]);
                    }
                    else
                        disclosure.AddPatientDisclosure(patientId, patientMasterVisitId, "Adolescents", "SexPartner", DateTime.Parse(SexPartnerDateValue));
                }

                List<INHProphylaxis> inhListsProphylaxes = inhProphylaxis.GetPatientProphylaxes(patientId);
                if (inhListsProphylaxes.Count > 0)
                {
                    inhListsProphylaxes[0].StartDate = IsINHStartDateValue;
                    inhListsProphylaxes[0].CompletionDate = IsCompletionDate;
                    inhListsProphylaxes[0].Complete = Boolean.Parse(INHCompletion);
                    inhProphylaxis.updateINHProphylaxis(inhListsProphylaxes[0]);
                }
                else
                {
                    INHProphylaxis prophylaxis = new INHProphylaxis()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        StartDate = IsINHStartDateValue,
                        Complete = Boolean.Parse(INHCompletion),
                        CompletionDate = IsCompletionDate
                    };

                    inhProphylaxis.addINHProphylaxis(prophylaxis);
                }

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

                    if (dataAdult[i] != "")
                    {
                        PatientVaccination patientVaccine = new PatientVaccination()
                        {
                            PatientId = patientId,
                            PatientMasterVisitId = patientMasterVisitId,
                            Vaccine = vaccine,
                            VaccineStage = dataAdult[i].ToString()
                        };

                        patientVaccination.addPatientVaccination(patientVaccine);
                    }
                }

                Msg = "Successfully Added OnIime Event Tracker";
            }
            catch (SoapException ex)
            {
                Msg = ex.Message + ' ' + ex.InnerException;
            }
            return Msg;
        }
    }
}
