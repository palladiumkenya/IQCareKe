using Entities.CCC.Tb;
using IQCare.CCC.UILogic.Tb;
using System;
using System.Linq;
using System.Web.Services;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientTbService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class PatientTbService : System.Web.Services.WebService
    {
        private string Msg { get; set; }
        private int Result { get; set; }

        [WebMethod(EnableSession = true)]
        public string AddPatientIcf(int patientId, int patientMasterVisitId, bool cough, bool fever, bool nightSweats, bool weightLoss, bool onAntiTbDrugs, bool onIpt, bool everBeenOnIpt)
        {
            PatientIcf patientIcf = new PatientIcf()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                Cough = cough,
                Fever = fever,
                NightSweats = nightSweats,
                WeightLoss = weightLoss,
                OnIpt = onIpt,
                OnAntiTbDrugs = onAntiTbDrugs,
                EverBeenOnIpt = everBeenOnIpt
            };
            try
            {
                var icf = new PatientIcfManager();
                var x = icf.GetByPatientId(patientId).FirstOrDefault(m => m.PatientMasterVisitId == patientMasterVisitId);
                if (x == null)
                {
                    Result = icf.AddPatientIcf(patientIcf);
                }
                else
                {
                    patientIcf.Id = x.Id;
                    Result = icf.UpdatePatientIcf(patientIcf);
                }
                if (Result > 0)
                {
                    Msg = "Patient ICF saved successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddIpt(int patientId, int patientMasterVisitId, int weight, DateTime iptDueDate, DateTime iptDateCollected, bool hepatotoxicity, bool peripheralneoropathy, bool rash, int adheranceMeasurement, string hepatotoxicityAction, string peripheralneoropathyAction, string rashAction, string adheranceMeasurementAction)
        {
            PatientIpt patientIpt = new PatientIpt()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                Weight = weight,
                AdheranceMeasurement = adheranceMeasurement,
                Hepatotoxicity = hepatotoxicity,
                IptDateCollected = iptDateCollected,
                IptDueDate = iptDueDate,
                Peripheralneoropathy = peripheralneoropathy,
                Rash = rash,
                HepatotoxicityAction = hepatotoxicityAction,
                PeripheralneoropathyAction = peripheralneoropathyAction,
                RashAction = rashAction,
                AdheranceMeasurementAction = adheranceMeasurementAction,
            };
            try
            {
                var ipt = new PatientIptManager();
                var x = ipt.GetByPatientId(patientId).FirstOrDefault(m => m.PatientMasterVisitId == patientMasterVisitId);
                if (x == null)
                {
                    Result = ipt.AddPatientIpt(patientIpt);
                }
                else
                {
                    patientIpt.Id = x.Id;
                    Result = ipt.UpdatePatientIpt(patientIpt);
                }
                if (Result > 0)
                {
                    Msg = "Patient IPT saved successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientIcfAction(int patientId, int patientMasterVisitId, string chestXray, bool evaluatedForIpt, bool invitationOfContacts, string sputumSmear, bool startAntiTb, string geneXpert)
        {
            PatientIcfAction patientIcfAction = new PatientIcfAction()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                ChestXray = Convert.ToInt32(chestXray),
                EvaluatedForIpt = evaluatedForIpt,
                InvitationOfContacts = invitationOfContacts,
                SputumSmear = Convert.ToInt32(sputumSmear),
                StartAntiTb = startAntiTb,
                GeneXpert = Convert.ToInt32(geneXpert)
            };
            try
            {
                var icfAction = new PatientIcfActionManager();
                var x = icfAction.GetByPatientId(patientId).FirstOrDefault(n => n.PatientMasterVisitId == patientMasterVisitId);
                if (x == null)
                {
                    Result = icfAction.AddPatientIcfAction(patientIcfAction);
                }
                else
                {
                    patientIcfAction.Id = x.Id;
                    Result = icfAction.UpdatePatientIcfAction(patientIcfAction);
                }
                if (Result > 0)
                {
                    Msg = "Patient ICF Action saved successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientIptWorkup(int patientId, int patientMasterVisitId, bool abdominalTenderness, bool numbness, bool yellowColouredUrine, bool yellownessOfEyes, string liverFunctionTests)
        {
            PatientIptWorkup patientIptWorkup = new PatientIptWorkup()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                AbdominalTenderness = abdominalTenderness,
                LiverFunctionTests = liverFunctionTests,
                Numbness = numbness,
                YellowColouredUrine = yellowColouredUrine,
                YellownessOfEyes = yellownessOfEyes
            };
            try
            {
                var iptWorkup = new PatientIptWorkupManager();
                var x = iptWorkup.GetByPatientId(patientId).FirstOrDefault(n => n.PatientMasterVisitId == patientMasterVisitId);
                if (x == null)
                {
                    Result = iptWorkup.AddPatientIptWorkup(patientIptWorkup);
                }
                else
                {
                    patientIptWorkup.Id = x.Id;
                    Result = iptWorkup.UpdatePatientIptWorkup(patientIptWorkup);
                }
                if (Result > 0)
                {
                    Msg = "Patient IPT Workup saved successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientIptOutcome(int patientId, int patientMasterVisitId, int iptEvent, string reasonForDiscontinuation)
        {
            PatientIptOutcome patientIptOutcome = new PatientIptOutcome()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                IptEvent = iptEvent,
                ReasonForDiscontinuation = reasonForDiscontinuation
            };
            try
            {
                var iptOutcome = new PatientIptOutcomeManager();
                var x = iptOutcome.GetByPatientId(patientId).FirstOrDefault(n => n.PatientMasterVisitId == patientMasterVisitId);
                if (x == null)
                {
                    Result = iptOutcome.AddPatientIptOutcome(patientIptOutcome);
                }
                else
                {
                    patientIptOutcome.Id = x.Id;
                    Result = iptOutcome.UpdatePatientIptOutcome(patientIptOutcome);
                }
                if (Result > 0)
                {
                    Msg = "Patient IPT Outcome saved successfully!";
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