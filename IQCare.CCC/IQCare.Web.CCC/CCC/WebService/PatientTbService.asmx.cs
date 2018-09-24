using Entities.CCC.Tb;
using IQCare.CCC.UILogic.Tb;
using System;
using System.Linq;
using System.Web.Script.Serialization;
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
        public string AddPatientIcf(int patientId, int patientMasterVisitId, string cough, string fever, string nightSweats, string weightLoss, bool onAntiTbDrugs, bool onIpt, bool everBeenOnIpt)
        {
            bool? _cough = null;
            if (cough.Trim().ToLower() == "true") { _cough = true; } else if(cough.Trim().ToLower() == "false") { _cough = false; }
            bool? _fever = null;
            if (fever.Trim().ToLower() == "true") { _fever = true; } else if (fever.Trim().ToLower() == "false") { _fever = false; }

            bool? _nightSweat = null;
            if (nightSweats.Trim().ToLower() == "true") { _nightSweat = true; } else if (nightSweats.Trim().ToLower() == "false") { _nightSweat = false; }

            bool? _WeightLoss = null;
            if (weightLoss.Trim().ToLower() == "true") { _WeightLoss = true; } else if (weightLoss.Trim().ToLower() == "false") { _WeightLoss = false; }
            PatientIcf patientIcf = new PatientIcf()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                Cough = _cough,
                Fever = _fever,
                NightSweats = _nightSweat,
                WeightLoss = _WeightLoss,
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
        public string AddIpt(int patientId, int patientMasterVisitId, decimal weight, DateTime iptDueDate, DateTime iptDateCollected, bool hepatotoxicity, bool peripheralneoropathy, bool rash, int adheranceMeasurement, string hepatotoxicityAction, string peripheralneoropathyAction, string rashAction, string adheranceMeasurementAction)
        {
            PatientIpt patientIpt = new PatientIpt()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                Weight = Convert.ToInt32(weight),
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
                ChestXray = (IcfRadiologyOptions)Convert.ToInt32(chestXray),
                EvaluatedForIpt = evaluatedForIpt,
                InvitationOfContacts = invitationOfContacts,
                SputumSmear = (IcfTestOptions)Convert.ToInt32(sputumSmear),
                StartAntiTb = startAntiTb,
                GeneXpert = (IcfTestOptions)Convert.ToInt32(geneXpert)
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
        public string AddPatientIptWorkup(int patientId, int patientMasterVisitId, bool abdominalTenderness, bool numbness, bool yellowColouredUrine, bool yellownessOfEyes, string liverFunctionTests, bool startIpt, DateTime ? iptStartDate)
        {
            PatientIptWorkup patientIptWorkup = new PatientIptWorkup()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                AbdominalTenderness = abdominalTenderness,
                LiverFunctionTests = liverFunctionTests,
                Numbness = numbness,
                YellowColouredUrine = yellowColouredUrine,
                YellownessOfEyes = yellownessOfEyes,
                IptStartDate = iptStartDate,
                StartIpt = startIpt
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

        [WebMethod(EnableSession = true)]
        public string GetPatientIptOutcome(int patientId)
        {
            try
            {
                var iptOutcome = new PatientIptOutcomeManager();
                var x = iptOutcome.GetByPatientId(patientId).FirstOrDefault();
                if (x != null)
                {
                    PatientIptOutcome patientIptOutcome = new PatientIptOutcome()
                    {
                        PatientId = x.PatientId,
                        PatientMasterVisitId = x.PatientMasterVisitId,
                        IptEvent = x.IptEvent,
                        ReasonForDiscontinuation = x.ReasonForDiscontinuation,
                        Id = x.Id
                    };
                    JavaScriptSerializer parser = new JavaScriptSerializer();

                    Msg = parser.Serialize(patientIptOutcome);
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