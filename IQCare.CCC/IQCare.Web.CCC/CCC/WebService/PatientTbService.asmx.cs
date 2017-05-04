using Entities.CCC.Tb;
using IQCare.CCC.UILogic.Tb;
using System;
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
        public string AddPatientIcf(int patientId, int patientMasterVisitId, bool cough, bool fever, bool nightSweats, bool weightLoss, bool onAntiTbDrugs, bool onIpt)
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
                OnAntiTbDrugs = onAntiTbDrugs
            };
            try
            {
                var icf = new PatientIcfManager();
                Result = icf.AddPatientIcf(patientIcf);
                if (Result > 0)
                {
                    Msg = "Patient ICF added successfully!";
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
                Result = ipt.AddPatientIpt(patientIpt);
                if (Result > 0)
                {
                    Msg = "Patient IPT added successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientIcfAction(int patientId, int patientMasterVisitId, bool chestXray, bool evaluatedForIpt, bool invitationOfContacts, bool sputumSmear, bool startAntiTb)
        {
            PatientIcfAction patientIcfAction = new PatientIcfAction()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                ChestXray = chestXray,
                EvaluatedForIpt = evaluatedForIpt,
                InvitationOfContacts = invitationOfContacts,
                SputumSmear = sputumSmear,
                StartAntiTb = startAntiTb,
            };
            try
            {
                var icfAction = new PatientIcfActionManager();
                Result = icfAction.AddPatientIcfAction(patientIcfAction);
                if (Result > 0)
                {
                    Msg = "Patient ICF Action added successfully!";
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
                Result = iptWorkup.AddPatientIptWorkup(patientIptWorkup);
                if (Result > 0)
                {
                    Msg = "Patient IPT Workup added successfully!";
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
                Result = iptOutcome.AddPatientIptOutcome(patientIptOutcome);
                if (Result > 0)
                {
                    Msg = "Patient IPT Outcome added successfully!";
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