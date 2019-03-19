using Entities.CCC.Tb;
using IQCare.CCC.UILogic.Tb;
using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using IQCare.CCC.UILogic;
using Entities.CCC.Lookup;
using System.Collections.Generic;
using Interface.CCC.Lookup;
using Application.Presentation;
using IQCare.CCC.UILogic.Visit;
using Entities.CCC.Visit;
using System.Collections;

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
        public string AddPatientIcf(int patientId, int patientMasterVisitId, int cough, int fever, int nightSweats, int weightLoss, int onAntiTbDrugs, int onIpt, int everBeenOnIpt)
        {
            LookupLogic lookUp = new LookupLogic();
            //cough
            bool? _cough = null;
            if (lookUp.GetLookupItemNameById(cough) == "Yes") { _cough = true; } else if(lookUp.GetLookupItemNameById(cough) == "No") { _cough = false; }
            //fever
            bool? _fever = null;
            if (lookUp.GetLookupItemNameById(fever) == "Yes") { _fever = true; } else if (lookUp.GetLookupItemNameById(fever) == "No") { _fever = false; }
            //night sweat
            bool? _nightSweat = null;
            if (lookUp.GetLookupItemNameById(nightSweats) == "Yes") { _nightSweat = true; } else if (lookUp.GetLookupItemNameById(nightSweats) == "No") { _nightSweat = false; }
            //weight loss
            bool? _WeightLoss = null;
            if (lookUp.GetLookupItemNameById(weightLoss) == "Yes") { _WeightLoss = true; } else if (lookUp.GetLookupItemNameById(weightLoss) == "No") { _WeightLoss = false; }
            //onAntiTBdrugs
            bool _onAntiTbDrugs = false;
            string passedValue = lookUp.GetLookupItemNameById(onAntiTbDrugs);
            if (passedValue == "Yes")
            {
                _onAntiTbDrugs = true;
            }
            else if (lookUp.GetLookupItemNameById(onAntiTbDrugs) == "No")
            {
                _onAntiTbDrugs = false;
            }
            //onIPT
            bool _onIpt = false;
            if (lookUp.GetLookupItemNameById(onIpt) == "Yes") { _onIpt = true; } else if (lookUp.GetLookupItemNameById(onIpt) == "No") { _onIpt = false; }
            //everBeenOnIpt
            bool? _everBeenOnIpt = null;
            if (lookUp.GetLookupItemNameById(everBeenOnIpt) == "Yes") { _everBeenOnIpt = true; } else if (lookUp.GetLookupItemNameById(everBeenOnIpt) == "No") { _everBeenOnIpt = false; }
            PatientIcf patientIcf = new PatientIcf()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                Cough = _cough,
                Fever = _fever,
                NightSweats = _nightSweat,
                WeightLoss = _WeightLoss,
                OnIpt = _onIpt,
                OnAntiTbDrugs = _onAntiTbDrugs,
                EverBeenOnIpt = _everBeenOnIpt
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
        public string AddPatientIcfAction(int patientId, int patientMasterVisitId, string chestXray, int evaluatedForIpt, int invitationOfContacts, string sputumSmear, int startAntiTb, string geneXpert)
        {
            LookupLogic lookUp = new LookupLogic();
            bool _evaluatedForIpt = false;
            if (lookUp.GetLookupItemNameById(evaluatedForIpt) == "yes") { _evaluatedForIpt = true; } else if (lookUp.GetLookupItemNameById(evaluatedForIpt) == "No") { _evaluatedForIpt = false; }
            bool _invitationOfContacts = false;
            if (lookUp.GetLookupItemNameById(invitationOfContacts) == "yes") { _invitationOfContacts = true; } else if (lookUp.GetLookupItemNameById(invitationOfContacts) == "No") { _invitationOfContacts = false; }
            bool _startAntiTb = false;
            if (lookUp.GetLookupItemNameById(startAntiTb) == "yes") { _startAntiTb = true; } else if (lookUp.GetLookupItemNameById(startAntiTb) == "No") { _startAntiTb = false; }
            PatientIcfAction patientIcfAction = new PatientIcfAction()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                ChestXray = (IcfRadiologyOptions)Convert.ToInt32(chestXray),
                EvaluatedForIpt = _evaluatedForIpt,
                InvitationOfContacts = _invitationOfContacts,
                SputumSmear = (IcfTestOptions)Convert.ToInt32(sputumSmear),
                StartAntiTb = _startAntiTb,
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
        public string AddPatientIptOutcome(int patientId,DateTime? IPTDate,int patientMasterVisitId, int iptEvent, string reasonForDiscontinuation)
        {
           
     
            PatientIptOutcome patientIptOutcome = new PatientIptOutcome()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                IptEvent = iptEvent,
                ReasonForDiscontinuation = reasonForDiscontinuation,
                IPTOutComeDate=IPTDate
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

        [WebMethod(EnableSession =true)]
        public ArrayList GetPatientIPTHistory()
        {
            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            var iptWorkup = new PatientIptWorkupManager();
            var iptworkup = iptWorkup.GetByPatientId(patientId).GroupBy(x => x.IptStartDate).Select(x => x.OrderByDescending(t => t.Id).First()).ToList();
            ArrayList rows = new ArrayList();
            DateTime? IPTDate;
            if (iptworkup.Count > 0)
            {
                foreach(var l in iptworkup)
                {
                    
                 
                   var startdateipt = l.IptStartDate;
                    
                        List<IPTOutcome> loutcome = new List<IPTOutcome>();
                        var iptOutcome = new PatientIptOutcomeManager();
                        var x = iptOutcome.GetByPatientId(patientId);
                        IPTDate = l.IptStartDate;
                        if (x.Count > 0)
                        {
                            foreach (var patient in x)
                            {
                                IPTOutcome ip = new IPTOutcome();
                                if (patient.IptEvent.ToString() == "1")
                                {
                                    ip.IPT = "Currently on IPT:Yes";

                                }
                                else if (patient.IptEvent.ToString() == "0")
                                {
                                    ip.IPT = "Currently on IPT:No";
                                }
                                else
                                {
                                    ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
                                    string outcome = "IptOutcome";
                                    var lm = mgr.GetLookupItemNameByMasterNameItemId(patient.IptEvent, outcome);
                                    ip.IPT = lm.ToString();
                                }
                                if (patient.IPTOutComeDate != null)
                                {
                                    ip.IPTOutComeDate = patient.IPTOutComeDate;
                                }
                                else
                                {
                                    ip.IPTOutComeDate = null;


                                }
                                if (ip.IPTOutComeDate >= startdateipt)
                                {
                                    string[] i = new string[3] { startdateipt.ToString(), ip.IPT, ip.IPTOutComeDate.ToString() };
                                    rows.Add(i);
                                }
                                else
                                {
                                    string[] i = new string[3] { startdateipt.ToString(), ip.IPT, "" };
                                    rows.Add(i);
                                }

                                loutcome.Add(ip);

                            }


                        }

                        else
                        {
                            string[] i = new string[3] { startdateipt.ToString(), "", "" };
                            rows.Add(i);
                        }
                    }
                }
            
         
            
            return rows;
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
                        Id = x.Id,
                        IPTOutComeDate=x.IPTOutComeDate
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
        [WebMethod(EnableSession = true)]
        public string AddPatienTBRx(int patientId, int patientMasterVisitId,DateTime TBRxStartDate, DateTime TBRxEndDate, int TBRxRegimen)
        {
            PatientTBRx patientTBRX = new PatientTBRx()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                TBRxStartDate = TBRxStartDate,
                TBRxEndDate = TBRxEndDate,
                RegimenId = TBRxRegimen
            };
            try
            {
                var TBRx = new PatientTBRxManager();
                var x = TBRx.GetByPatientId(patientId).FirstOrDefault(n => n.PatientMasterVisitId == patientMasterVisitId);
                if (x == null)
                {
                    Result = TBRx.AddPatientTBRx(patientTBRX);
                }
                else
                {
                    patientTBRX.Id = x.Id;
                    Result = TBRx.UpdatePatientTBRx(patientTBRX);
                }
                if (Result > 0)
                {
                    Msg = "Patient TBRx saved successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
    }

   public class IPTOutcome
    {
        public string IPT { get; set; }
       
        public DateTime? IPTOutComeDate { get; set; }
    }
}