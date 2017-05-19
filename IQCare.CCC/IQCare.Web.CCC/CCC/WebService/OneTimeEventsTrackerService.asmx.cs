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
using Newtonsoft.Json;

namespace IQCare.Web.CCC.WebService
{
    public class OneTimeEventsDetails
    {
        public string Stage1Date { get; set; }
        public string Stage2Date { get; set; }
        public string Stage3Date { get; set; }
        public string SexPartner { get; set; }
        public string StartDate { get; set; }
        public int Complete { get; set; }
        public string CompletionDate { get; set; }
        public string HBV { get; set; }
        public string FluVaccine { get; set; }
        public string OtherVaccination { get; set; }
    }
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

                int patientId = int.Parse(HttpContext.Current.Session["PatientPK"].ToString());
                //int patientId = int.Parse(Session["PatientId"].ToString());
                int patientMasterVisitId = int.Parse(Session["PatientMasterVisitId"].ToString());

                if (String.IsNullOrEmpty(INHCompletion) || String.IsNullOrWhiteSpace(INHCompletion) ||
                    INHCompletion == "null")
                {
                    INHCompletion = "false";
                }

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

                    if (dataAdult[i].ToString() != "")
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

        [WebMethod(EnableSession = true)]
        public string GetOneTimeEventsTrackerDetails()
        {
            try
            {
                int patientId = int.Parse(HttpContext.Current.Session["PatientPK"].ToString());
                PatientDisclosureManager disclosure = new PatientDisclosureManager();
                INHProphylaxisManager inhProphylaxis = new INHProphylaxisManager();
                PatientVaccinationManager vaccinationManager = new PatientVaccinationManager();
                OneTimeEventsDetails oneTimeEventsDetails = new OneTimeEventsDetails();


                List<PatientDisclosure> patientDisclosures = disclosure.GetAllPatientDisclosures(patientId);
                List<INHProphylaxis> inhListsProphylaxes = inhProphylaxis.GetPatientProphylaxes(patientId);
                var vaccinations = vaccinationManager.GetPatientVaccinations(patientId);

                if (patientDisclosures.Count > 0)
                {
                    foreach (var itemDisclosure in patientDisclosures)
                    {
                        if (itemDisclosure.Category == "Adolescents" && itemDisclosure.DisclosureStage == "Stage1")
                        {
                            oneTimeEventsDetails.Stage1Date = itemDisclosure.DisclosureDate.ToString();
                        }

                        if (itemDisclosure.Category == "Adolescents" && itemDisclosure.DisclosureStage == "Stage2")
                        {
                            oneTimeEventsDetails.Stage2Date = itemDisclosure.DisclosureDate.ToString();
                        }

                        if (itemDisclosure.Category == "Adolescents" && itemDisclosure.DisclosureStage == "Stage3")
                        {
                            oneTimeEventsDetails.Stage3Date = itemDisclosure.DisclosureDate.ToString();
                        }

                        if (itemDisclosure.Category == "Adolescents" && itemDisclosure.DisclosureStage == "SexPartner")
                        {
                            oneTimeEventsDetails.SexPartner = itemDisclosure.DisclosureDate.ToString();
                        }
                    }
                }

                if (inhListsProphylaxes.Count > 0)
                {
                    oneTimeEventsDetails.StartDate = inhListsProphylaxes[0].StartDate.ToString();
                    oneTimeEventsDetails.Complete = Convert.ToInt32(inhListsProphylaxes[0].Complete);
                    oneTimeEventsDetails.CompletionDate = inhListsProphylaxes[0].CompletionDate.ToString();
                }

                if (vaccinations.Count > 0)
                {
                    foreach (var itemVaccination in vaccinations)
                    {
                        if (itemVaccination.VaccineStage == "FluVaccine")
                        {
                            oneTimeEventsDetails.FluVaccine = "1";
                        }

                        if (itemVaccination.VaccineStage == "HBV")
                        {
                            oneTimeEventsDetails.HBV = "1";
                        }

                        if (itemVaccination.Vaccine == 0)
                        {
                            oneTimeEventsDetails.OtherVaccination = itemVaccination.VaccineStage;
                        }
                    }
                }

                Msg = JsonConvert.SerializeObject(oneTimeEventsDetails);
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
    }
}
