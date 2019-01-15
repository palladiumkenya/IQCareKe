using System;
using IQCare.CCC.UILogic;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Encounter;
using Entities.CCC.Enrollment;
using Interface.CCC.Visit;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.CCC.UILogic.Triage;
using AutoMapper;
using IQCare.Events;
using Entities.CCC.Lookup;
using System.Linq;
using IQCare.CCC.UILogic.Screening;
using Entities.CCC.Screening;
using IQCare.CCC.UILogic.Visit;
using Interface.CCC.Lookup;
using Entities.CCC.Appointment;


//using static Entities.CCC.Encounter.PatientEncounter;

namespace IQCare.Web.CCC.WebService
{
    public class ArtDistributionDeTails : PatientArtDistribution
    {
        public string DateReferedToClinic { get; set; }
    }
    /// <summary>
    /// Summary description for PatientEncounterService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]


    public class PatientEncounterService : System.Web.Services.WebService
    {

        public class OIData
        {
            public int OI { get; set; }
            public bool Checked { get; set; }

            public DateTime? Current { get; set; }

            public bool DeleteFlag { get; set; }
        }

        public class SexualHistoryOutcome
        {
            public List<SexualHistory> list { get; set; }

            public string sexuallyactive { get; set; }

            public string numberofpartners { get; set; }

            public List<LookupItemView> sexualorient { get; set; }
            public List<LookupItemView> gender { get; set; }

            public List<LookupItemView> hivstatus { get; set; }

        }
        public class Output
        {
            public List<SexualHistory> list { get; set; }
            public string msg { get; set; }
        }

        public class PreviousHistoryOutcome
        {
            public List<HistoryOutcome> Orientation { get; set; }

            public List<HistoryOutcome> Gender { get; set; }

            public List<HistoryOutcome> HivStatus { get; set; }
            public int noofpartners { get; set; }

            public DateTime? VisitDate { get; set; }
        }
        public class HistoryOutcome
        {

            public int MasterId { get; set; }
            public string MasterName { get; set; }
            public string ItemValue { get; set; }

            public int value { get; set; }

        }
        public class HighRisk
        {
            public int Id { get; set; }
            public string value { get; set; }
        }


        public class SexualHistory
        {
            public string uniqueid { get; set; }
            public string id { get; set; }
            public string PartnerStatus { get; set; }
            public string Gender { get; set; }

            public bool DeleteFlag { get; set; }
            public string SexualOrientation { get; set; }
            public List<HighRisk> Highrisk { get; set; }


        }
        private string Msg { get; set; }
        private int Result { get; set; }
        public string numberofpartners;
        public int count = 1;
        public int DosageFrequency;

        public string ItemDisplayName;
        public string sexuallyactive;
        private readonly IPatientMasterVisitManager _visitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");
        [WebMethod(EnableSession = true)]
        public int savePatientEncounterPresentingComplaints(string VisitDate, string VisitScheduled, string VisitBy, string anyComplaints, string Complaints, int TBScreening, int NutritionalStatus, string adverseEvent, string presentingComplaints)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            int val = patientEncounter.savePatientEncounterPresentingComplaints(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), "203", VisitDate, VisitScheduled, VisitBy, anyComplaints, Complaints, TBScreening, NutritionalStatus, Convert.ToInt32(Session["AppUserId"].ToString()), adverseEvent, presentingComplaints);


            return val;

        }

        [WebMethod(EnableSession = true)]
        public int savePatientEncounterTS(string VisitDate, string VisitScheduled, string VisitBy)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            int val = patientEncounter.savePatientEncounterTS(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), "203", VisitDate, VisitScheduled, VisitBy, Convert.ToInt32(Session["AppUserId"].ToString()));
            return val;
        }


        [WebMethod(EnableSession = true)]
        public void savePatientEncounterChronicIllness(string chronicIllness, string vaccines, string allergies)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientEncounterChronicIllness(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), Session["AppUserId"].ToString(), chronicIllness, vaccines, allergies);
        }

        [WebMethod(EnableSession = true)]
        public void savePatientPhysicalExam(string physicalExam, string generalExam)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientEncounterPhysicalExam(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), Session["AppUserId"].ToString(), physicalExam, generalExam);
        }





        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetPatientOIs()
        {
            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            PatientOIManager patientoiManager = new PatientOIManager();
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            List<PatientOI> list = new List<PatientOI>();
            list = patientoiManager.GetPatientsOI(patientId, patientMasterVisitId);
            ArrayList arrayList = new ArrayList(list);
            return arrayList;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetPreviousSexualHistory()
        {
            PatientHighRiskManager hr = new PatientHighRiskManager();
            PatientSexualHistoryManager psh = new PatientSexualHistoryManager();
            PatientPartnersManager partman = new PatientPartnersManager();
            PatientMasterVisitManager pmv = new PatientMasterVisitManager();
            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());

            int PreviousMasterVisitId;
            DateTime? patientvisitdate;
            int userId = Convert.ToInt32(Session["AppUserId"]);
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            List<Entities.CCC.Visit.PatientMasterVisit> pmastervisit = pmv.GetPatientVisits(patientId);
            Entities.CCC.Visit.PatientMasterVisit pmlist = pmastervisit.FindAll(x => x.Id != patientMasterVisitId && x.Id < patientMasterVisitId).OrderByDescending(x => x.Id).FirstOrDefault();
            if (pmlist != null)
            {
                PreviousMasterVisitId = pmlist.Id;
                patientvisitdate = pmlist.VisitDate;
            }
            else
            {
                PreviousMasterVisitId = 0;
                patientvisitdate = new DateTime();
            }

            PatientPartner pat = partman.GetPatientPartner(patientId, PreviousMasterVisitId);

            List<PatientSexualHistory> patienthistory = psh.GetPatientSexualHistoryList(patientId, PreviousMasterVisitId);

            if (pat != null)
            {
                numberofpartners = pat.NoofPartners.ToString();
            }
            List<HistoryOutcome> Orient = new List<HistoryOutcome>();
            List<HistoryOutcome> Gen = new List<HistoryOutcome>();
            List<HistoryOutcome> hst = new List<HistoryOutcome>();
            List<LookupItemView> lSexualOrientation = LookupLogic.GetLookItemByGroup("SexualOrientation");
            List<LookupItemView> lGender = LookupLogic.GetLookItemByGroup("Gender");
            List<LookupItemView> lHivStatus = LookupLogic.GetLookItemByGroup("HivStatus");
            if (patienthistory.Count > 0)
            {
                foreach (LookupItemView lt in lSexualOrientation)
                {
                    List<PatientSexualHistory> list = patienthistory.Where(x => x.PatientSexualOrientation == lt.ItemId).ToList();
                    if (list != null && list.Count > 0)
                    {
                        int value = list.Count;
                        string itemDisplay = lt.ItemName;
                        HistoryOutcome ho = new HistoryOutcome();
                        ho.ItemValue = itemDisplay;
                        ho.value = value;
                        ho.MasterId = lt.MasterId;
                        ho.MasterName = lt.MasterName;
                        Orient.Add(ho);

                    }

                    else
                    {
                        Orient = null;
                    }

                }

                foreach (LookupItemView lt in lGender)
                {
                    List<PatientSexualHistory> list = patienthistory.Where(x => x.PartnerGender == lt.ItemId).ToList();
                    if (list != null && list.Count > 0)
                    {
                        int value = list.Count;
                        string itemDisplay = lt.ItemName;
                        HistoryOutcome ho = new HistoryOutcome();
                        ho.ItemValue = itemDisplay;
                        ho.value = value;
                        ho.MasterId = lt.MasterId;
                        ho.MasterName = lt.MasterName;
                        Gen.Add(ho);

                    }

                    else
                    {
                        Gen = null;
                    }

                }

                foreach (LookupItemView lt in lHivStatus)
                {
                    List<PatientSexualHistory> list = patienthistory.Where(x => x.PartnerHivStatus == lt.ItemId).ToList();
                    if (list != null && list.Count > 0)
                    {
                        int value = list.Count;
                        string itemDisplay = lt.ItemName;
                        HistoryOutcome ho = new HistoryOutcome();
                        ho.ItemValue = itemDisplay;
                        ho.value = value;
                        hst.Add(ho);

                    }
                    else
                    {
                        hst = null;
                    }


                }
            }

            PreviousHistoryOutcome pho = new PreviousHistoryOutcome();

            if (numberofpartners == null)
            {
                if (patienthistory != null && patienthistory.Count > 0)
                {
                    numberofpartners = patienthistory.Count.ToString();
                }
            }
            else
            {

                numberofpartners = "0";
            }


            pho.noofpartners = Convert.ToInt32(numberofpartners);
            pho.Gender = Gen;
            pho.Orientation = Orient;
            pho.HivStatus = hst;
            pho.VisitDate = patientvisitdate;
            return new JavaScriptSerializer().Serialize(pho);


        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetSexualHistory()
        {
            PatientHighRiskManager hr = new PatientHighRiskManager();
            PatientSexualHistoryManager psh = new PatientSexualHistoryManager();
            PatientPartnersManager partman = new PatientPartnersManager();



            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());


            int userId = Convert.ToInt32(Session["AppUserId"]);
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());



            PatientScreeningManager pscreen = new PatientScreeningManager();
            List<SexualHistory> sexuallist = new List<SexualHistory>();
            PatientScreening psc = pscreen.GetCurrentPatientScreening(patientId, patientMasterVisitId);
            List<PatientSexualHistory> patienthistory = psh.GetPatientSexualHistoryList(patientId, patientMasterVisitId);



            PatientPartner pat = partman.GetPatientPartner(patientId, patientMasterVisitId);
            if (pat != null)
            {
                numberofpartners = pat.NoofPartners.ToString();
            }
            if (psc != null)
            {
                int? screeningTypeId = psc.ScreeningTypeId;
            }
            List<LookupItemView> lSexualOrientation = LookupLogic.GetLookItemByGroup("SexualOrientation");
            List<LookupItemView> lGender = LookupLogic.GetLookItemByGroup("Gender");
            List<LookupItemView> lHivStatus = LookupLogic.GetLookItemByGroup("HivStatus");
            List<LookupItemView> lsexual = LookupLogic.GetLookItemByGroup("SexualScreening");
            if (psc != null)
            {
                LookupItemView lookupview = lsexual.Find(x => x.ItemId == psc.ScreeningValueId);
                if (lookupview != null)
                {
                    string sexualactive = lookupview.ItemName;

                    if (sexualactive.ToString().ToLower() == "yes")
                    {
                        sexuallyactive = "yes";

                    }
                    else if (sexualactive.ToString().ToLower() == "no")
                    {
                        sexuallyactive = "no";
                    }
                }
            }
            SexualHistoryOutcome shc = new SexualHistoryOutcome();
            if (patienthistory != null)
            {

                foreach (PatientSexualHistory psexual in patienthistory)
                {
                    SexualHistory sh = new SexualHistory();

                    sh.id = psexual.Id.ToString();



                    sh.uniqueid = count.ToString();
                    sh.SexualOrientation = psexual.PatientSexualOrientation.ToString();
                    sh.PartnerStatus = psexual.PartnerHivStatus.ToString();
                    sh.Gender = psexual.PartnerGender.ToString();
                    sh.DeleteFlag = psexual.DeleteFlag;
                    List<HighRisk> htr = new List<HighRisk>();
                    List<PatientHighRisk> hrs = hr.GetPatientHighRiskList(patientId, patientMasterVisitId, psexual.Id);
                    foreach (PatientHighRisk hhr in hrs)
                    {
                        HighRisk highr = new HighRisk();
                        if (hhr.HighRisk > 0)
                        {
                            highr.Id = hhr.HighRisk;
                        }
                        else
                        {
                            highr.Id = 0;
                        }
                        List<LookupItemView> lv = LookupLogic.GetLookItemByGroup("HighRisk");

                        LookupItemView ltv = lv.Find(x => x.ItemId == hhr.HighRisk);
                        if (ltv != null)
                        {
                            highr.value = ltv.ItemDisplayName;
                        }
                        else
                        {
                            highr.value = "null";
                        }

                        htr.Add(highr);
                        sh.Highrisk = htr;

                    }
                    sexuallist.Add(sh);
                    count = ++count;



                }


            }
            shc.list = sexuallist;
            shc.sexualorient = lSexualOrientation;
            shc.hivstatus = lHivStatus;
            shc.gender = lGender;
            shc.numberofpartners = numberofpartners;
            shc.sexuallyactive = sexuallyactive;
            return new JavaScriptSerializer().Serialize(shc);


        }

        [WebMethod(EnableSession = true)]
        public string SaveSexualHistory(string data, string sexuallyactive, string partnersno)
        {
            string Msg = "";
            PatientHighRiskManager hr = new PatientHighRiskManager();
            PatientSexualHistoryManager psh = new PatientSexualHistoryManager();
            PatientPartnersManager partman = new PatientPartnersManager();
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<SexualHistory> sexualhist = json.Deserialize<List<SexualHistory>>(data);
            string SexActive = sexuallyactive;
            string numberofpartners = partnersno.ToString();

            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            int userId = Convert.ToInt32(Session["AppUserId"]);
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            int partnerid;


            if (SexActive != "undefined")
            {
                if (SexActive.ToLower() == "yes")
                {
                    ItemDisplayName = "yes";
                }
                else if (SexActive.ToLower() == "no")
                {
                    ItemDisplayName = "no";
                }
            }


            List<LookupItemView> lv = LookupLogic.GetLookItemByGroup("SexualScreening");
            LookupItemView itemview = new LookupItemView();
            if (!String.IsNullOrEmpty(ItemDisplayName))
            {
                itemview = lv.Find(x => x.DisplayName.ToLower() == ItemDisplayName.ToLower());
                var MasterId = itemview.MasterId;
                var ItemId = itemview.ItemId;

                PatientScreeningManager pmscreen = new PatientScreeningManager();
                PatientScreening psc = pmscreen.GetCurrentPatientScreening(patientId, patientMasterVisitId);
                if (psc != null)
                {
                    psc.ScreeningValueId = ItemId;
                    psc.ScreeningDate = DateTime.Now;
                    psc.VisitDate = DateTime.Now;
                    psc.ScreeningCategoryId = MasterId;
                    psc.ScreeningTypeId = MasterId;

                    var updatescreen = pmscreen.UpdateCurrentPatientScreening(psc);
                    if (updatescreen > 0)
                    {
                        Msg += "The sexually active is updated successfully";
                    }
                }
                else
                {

                    PatientScreening pscadd = new PatientScreening();
                    pscadd.PatientId = patientId;
                    pscadd.PatientMasterVisitId = patientMasterVisitId;
                    pscadd.ScreeningCategoryId = MasterId;
                    pscadd.ScreeningTypeId = MasterId;
                    pscadd.ScreeningValueId = ItemId;
                    pscadd.ScreeningDone = true;
                    pscadd.ScreeningDate = DateTime.Now;
                    pscadd.VisitDate = DateTime.Now;
                    int result = pmscreen.AddPatientScreening(pscadd.PatientId, pscadd.PatientMasterVisitId,
                    Convert.ToDateTime(pscadd.VisitDate), Convert.ToInt32(pscadd.ScreeningTypeId)
                    , pscadd.ScreeningDone, Convert.ToDateTime(pscadd.ScreeningDate),
                    Convert.ToInt32(pscadd.ScreeningCategoryId)
                    , pscadd.ScreeningValueId, pscadd.Comment, userId);
                    if (result > 0)
                    {
                        Msg += "The sexually active is added successfully";
                    }
                }
            }



            if (!String.IsNullOrEmpty(numberofpartners))
            {
                int partners = Convert.ToInt32(numberofpartners);
                if (partners > 0)
                {
                    PatientPartner pat = new PatientPartner();
                    pat = partman.GetPatientPartner(patientId, patientMasterVisitId);
                    if (pat != null)
                    {
                        pat.NoofPartners = Convert.ToInt32(numberofpartners);
                        pat.UpdateDate = Convert.ToDateTime(DateTime.Now);
                        pat.CreatedBy = userId;

                        PatientPartner part = partman.UpdatePatientPartner(pat);
                        if (part != null)
                        {
                            Msg += "Number of partners  in last  6 months has been updated";
                        }
                    }
                    else
                    {

                        PatientPartner padd = new PatientPartner();
                        padd.PatientId = patientId;
                        padd.PatientMasterVisitId = patientMasterVisitId;
                        padd.NoofPartners = Convert.ToInt32(numberofpartners);
                        padd.CreateDate = Convert.ToDateTime(DateTime.Now);
                        padd.CreatedBy = userId;
                        PatientPartner padded = partman.addPatientPartner(padd);
                        if (padded != null)
                        {
                            Msg += "Number of partners  in last  6 months has been added";
                        }

                    }
                }
                else
                {
                    PatientPartner pat = new PatientPartner();
                    pat = partman.GetPatientPartner(patientId, patientMasterVisitId);
                    if (pat != null)
                    {

                        pat.UpdateDate = Convert.ToDateTime(DateTime.Now);
                        pat.CreatedBy = userId;
                        pat.DeleteFlag = true;
                        PatientPartner patupd = partman.UpdatePatientPartner(pat);
                        if (patupd != null)
                        {
                            Msg += "Number of partners  in last  6 months has been updated";
                        }
                    }
                }

            }
            else
            {
                PatientPartner pat = new PatientPartner();
                pat = partman.GetPatientPartner(patientId, patientMasterVisitId);
                if (pat != null)
                {

                    pat.UpdateDate = Convert.ToDateTime(DateTime.Now);
                    pat.CreatedBy = userId;
                    pat.DeleteFlag = true;
                    PatientPartner patupd = partman.UpdatePatientPartner(pat);
                    if (patupd != null)
                    {
                        Msg += "Number of partners  in last  6 months has been updated";
                    }
                }


            }
            if (sexualhist != null)
            {
                foreach (SexualHistory sx in sexualhist)
                {
                    if (Convert.ToInt32(sx.id) > 0)
                    {
                        PatientSexualHistory psexual = new PatientSexualHistory();
                        psexual = psh.GetPatientSexualHistory(patientId, patientMasterVisitId, Convert.ToInt32(sx.id));
                        if (psexual != null)
                        {
                            psexual.PartnerGender = Convert.ToInt32(sx.Gender);
                            psexual.PartnerHivStatus = Convert.ToInt32(sx.PartnerStatus);
                            psexual.PatientSexualOrientation = Convert.ToInt32(sx.SexualOrientation);
                            psexual.UpdateDate = Convert.ToDateTime(DateTime.Today);
                            psexual.CreatedBy = userId;
                            psexual.DeleteFlag = sx.DeleteFlag;
                            PatientSexualHistory updatedsexual = new PatientSexualHistory();
                            updatedsexual = psh.UpdatePatientSexualHistory(psexual);
                            if (updatedsexual != null)
                            {
                                partnerid = updatedsexual.Id;

                                Msg += "Sexual History of Patient Successfully updated";

                                if (partnerid > 0)
                                {
                                    PatientHighRisk phr = new PatientHighRisk();

                                    foreach (HighRisk highr in sx.Highrisk)
                                    {
                                        if (highr.value != null || highr.value == "null")
                                        {
                                            phr = hr.GetPatientHighRisks(patientId, patientMasterVisitId, partnerid, highr.Id);

                                            if (phr != null)
                                            {
                                                phr.HighRisk = Convert.ToInt32(highr.Id);
                                                phr.PartnerId = partnerid;
                                                phr.PatientMasterVisitId = patientMasterVisitId;
                                                phr.PatientId = patientId;
                                                phr.CreatedBy = userId;
                                                phr.DeleteFlag = sx.DeleteFlag;
                                                PatientHighRisk pupdaterisk = new PatientHighRisk();
                                                pupdaterisk = hr.UpdatePatientHighRisk(phr);
                                                if (pupdaterisk != null)
                                                {
                                                    Msg += "Patient Risk Behaviour is updated";
                                                }
                                            }

                                            else
                                            {
                                                PatientHighRisk paddh = new PatientHighRisk();
                                                paddh.HighRisk = Convert.ToInt32(highr.Id);
                                                paddh.PartnerId = partnerid;
                                                paddh.PatientMasterVisitId = patientMasterVisitId;
                                                paddh.PatientId = patientId;
                                                paddh.CreatedBy = userId;
                                                paddh.CreateDate = Convert.ToDateTime(DateTime.Today);


                                                PatientHighRisk paddrisk = new PatientHighRisk();

                                                paddrisk = hr.addPatientHighRisk(paddh);

                                                if (paddrisk != null)
                                                {
                                                    Msg += "Patient Risk Behaviour is added";
                                                }
                                            }


                                        }
                                    }
                                }
                            }
                        }
                    }



                    else
                    {
                        PatientSexualHistory psex = new PatientSexualHistory();
                        psex.PartnerGender = Convert.ToInt32(sx.Gender);
                        psex.PartnerHivStatus = Convert.ToInt32(sx.PartnerStatus);
                        psex.PatientSexualOrientation = Convert.ToInt32(sx.SexualOrientation);
                        psex.CreateDate = Convert.ToDateTime(DateTime.Today);
                        psex.CreatedBy = userId;
                        psex.PatientId = patientId;
                        psex.PatientMasterVisitId = patientMasterVisitId;
                        PatientSexualHistory psexualadd = new PatientSexualHistory();
                        psexualadd = psh.AddPatientSexualHistory(psex);
                        if (psexualadd != null)
                        {
                            partnerid = psexualadd.Id;
                            sx.id = Convert.ToString(partnerid);
                            Msg += "Sexual History of Patient Successfully added";
                            if (psexualadd.Id > 0)
                            {
                                PatientHighRisk phr = new PatientHighRisk();

                                foreach (HighRisk highr in sx.Highrisk)
                                {
                                    if (highr.value != null || highr.value == "null")
                                    {
                                        phr = hr.GetPatientHighRisks(patientId, patientMasterVisitId, psexualadd.Id, highr.Id);

                                        if (phr != null)
                                        {
                                            phr.HighRisk = Convert.ToInt32(highr.Id);
                                            phr.PartnerId = psexualadd.Id;
                                            phr.PatientMasterVisitId = patientMasterVisitId;
                                            phr.PatientId = patientId;
                                            phr.CreatedBy = userId;

                                            PatientHighRisk pupdaterisk = new PatientHighRisk();
                                            pupdaterisk = hr.UpdatePatientHighRisk(phr);
                                            if (pupdaterisk != null)
                                            {
                                                Msg += "Patient Risk Behaviour is updated";
                                            }
                                        }

                                        else
                                        {
                                            PatientHighRisk paddh = new PatientHighRisk();
                                            paddh.HighRisk = Convert.ToInt32(highr.Id);
                                            paddh.PartnerId = psexualadd.Id;
                                            paddh.PatientMasterVisitId = patientMasterVisitId;
                                            paddh.PatientId = patientId;
                                            paddh.CreatedBy = userId;
                                            paddh.CreateDate = Convert.ToDateTime(DateTime.Today);


                                            PatientHighRisk paddrisk = new PatientHighRisk();


                                            paddrisk = hr.addPatientHighRisk(paddh);
                                            if (paddrisk != null)
                                            {
                                                Msg += "Patient Risk Behaviour is added";
                                            }
                                        }


                                    }
                                }
                            }
                        }


                    }




                }


            }

            Output res = new Output();
            res.list = sexualhist;
            res.msg = Msg;
            return new JavaScriptSerializer().Serialize(res);



        }

        [WebMethod(EnableSession = true)]
        public void SavePatientOI(string data)
        {
            int userId = Convert.ToInt32(Session["AppUserId"]);
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<OIData> oidata = json.Deserialize<List<OIData>>(data);
            PatientOIManager patientoiManager = new PatientOIManager();
            foreach (OIData oi in oidata)
            {
                int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
                int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());

                if (oi != null)
                {

                    PatientOI patientoi = new PatientOI();
                    patientoi = patientoiManager.GetPatientOI(patientId, patientMasterVisitId, oi.OI);
                    if (patientoi != null)
                    {
                        patientoi.OIId = oi.OI;
                        patientoi.UpdateDate = DateTime.Now;
                        patientoi.Current = oi.Current;
                        patientoi.CreatedBy = userId;
                        patientoi.PatientId = patientId;
                        patientoi.PatientMasterVisitId = patientMasterVisitId;
                        patientoi.DeleteFlag = oi.DeleteFlag;
                        patientoiManager.UpdatePatientOI(patientoi);
                    }
                    else
                    {
                        PatientOI patient = new PatientOI();
                        patient = patientoiManager.addPatientOI(patientId, patientMasterVisitId, oi.OI, userId, oi.Current);
                        if (patient != null)
                        {

                            int facilityId = Convert.ToInt32(Session["AppPosID"]);
                            MessageEventArgs args = new MessageEventArgs()
                            {
                                PatientId = patientId,
                                EntityId = patient.Id,
                                MessageType = MessageType.ObservationResult,
                                EventOccurred = "Patient Observation Result",
                                FacilityId = facilityId,
                                ObservationType = ObservationType.PatientOI
                            };

                            Publisher.RaiseEventAsync(this, args).ConfigureAwait(false);
                        }



                    }

                }
            }
        }



        [WebMethod(EnableSession = true)]
        public void savePatientWhoStage(int whoStage)
        {
            PatientWhoStageManager whoStageManager = new PatientWhoStageManager();

            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());

            PatientWhoStage PatientWhoStage = whoStageManager.GetPatientWhoStage(patientId, patientMasterVisitId);
            if (PatientWhoStage != null)
            {
                PatientWhoStage.WHOStage = whoStage;
                whoStageManager.UpdatePatientWhoStage(PatientWhoStage);
            }
            else
            {
                int whoResult = whoStageManager.addPatientWhoStage(patientId, patientMasterVisitId, whoStage);
                if (whoResult > 0)
                {
                    int facilityId = Convert.ToInt32(Session["AppPosID"]);

                    MessageEventArgs args = new MessageEventArgs()
                    {
                        PatientId = patientId,
                        EntityId = whoResult,
                        MessageType = MessageType.ObservationResult,
                        EventOccurred = "Patient Observation Result",
                        FacilityId = facilityId,
                        ObservationType = ObservationType.WhoStage
                    };

                    Publisher.RaiseEventAsync(this, args).ConfigureAwait(false);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public void savePatientManagement(string workplan, string phdp, string ARVAdherence, string CTXAdherence, string diagnosis)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            patientEncounter.savePatientManagement(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(), Session["AppUserId"].ToString(), workplan, ARVAdherence, CTXAdherence, phdp, diagnosis);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetAdverseEvents()
        {
            int adverseEventId = 0;
            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            var outcomeString = "";

            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            PatientAdverseEventOutcomeManager patientAdverseEventOutcome = new PatientAdverseEventOutcomeManager();

            LookupLogic lookupLogic = new LookupLogic();


            DataTable theDT = patientEncounter.loadPatientEncounterAdverseEvents(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string eventoutcome = "";
                DateTime outcomeDate = DateTime.Today;

                //get the adverse Event form the db
                var items = lookupLogic.GetItemIdByGroupAndItemName("AdverseEvents", row["EventName"].ToString());
                foreach (var item in items)
                {
                    adverseEventId = item.ItemId;
                }

                // get the outcome for the adverse event
                // var outcome =patientAdverseEventOutcome.GetAdverseEventOutcome(adverseEventId, patientMasterVisitId, patientId);

                var adverseEventOutcomes = patientAdverseEventOutcome.GetAdverseEventOutcome(adverseEventId, patientMasterVisitId, patientId);



                if (adverseEventOutcomes.Count > 0)
                {
                    foreach (var adverseEventOutcome in adverseEventOutcomes)
                    {
                        eventoutcome = lookupLogic.GetLookupItemNameById(adverseEventOutcome.OutcomeId);
                        outcomeDate = Convert.ToDateTime(adverseEventOutcome.OutcomeDate);
                    }
                    if (string.IsNullOrEmpty(eventoutcome))
                    {
                        string[] i = new string[7]
                        {
                            row["SeverityID"].ToString(),row["AdverseEventId"].ToString(), row["EventName"].ToString(), row["EventCause"].ToString(),
                            row["Severity"].ToString(), row["Action"].ToString(),
                            "<button type='button' class='btnAddAdverseEventOutcome btn btn-info fa fa-plus-circle btn-fill' onclick='AdverseEventOutcome();'> Specify Outcome</button> <button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                        };
                        rows.Add(i);
                    }
                    else
                    {
                        if (eventoutcome == "Died")
                        {
                            outcomeString = "<span class='text-danger'><strong>" + eventoutcome +
                                            "</strong></span> | <span class='text-info'><strong>" + outcomeDate.ToString("dd-MMM-yyy") + "</strong></span>";
                        }
                        else {
                            outcomeString = "<span class='text-primary'><strong>" + eventoutcome +
                                            "</strong></span> | <span class='text-info'><strong>" + outcomeDate.ToString("dd-MMM-yyy") + "</strong></span>";
                        }
                        string[] i = new string[6]
                        {
                            row["SeverityID"].ToString(),
                            row["EventName"].ToString(),
                            row["EventCause"].ToString(),
                            row["Severity"].ToString(),
                            row["Action"].ToString(),
                            outcomeString
                            //"<span class='text-info'>outcome:</span>"+eventoutcome+ "<span class='text-info'>outcome Date:</span>"+ outcomeDate
                        };
                        rows.Add(i);
                    }
                }
                else
                {

                    string[] i = new string[7]
                    {
                        row["SeverityID"].ToString(),row["AdverseEventId"].ToString(), row["EventName"].ToString(), row["EventCause"].ToString(),
                        row["Severity"].ToString(), row["Action"].ToString(),
                        "<button type='button' class='btnAddAdverseEventOutcome btn btn-info fa fa-plus-circle btn-fill' onclick='AdverseEventOutcome();'> Specify Outcome</button> <button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>"
                    };
                    rows.Add(i);
                }


            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList LoadComplaints()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterComplaints(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[4] { row["presentingComplaintsId"].ToString(), row["complaint"].ToString(), row["onsetDate"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList LoadWorkPlan()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPatientWorkPlan(Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[2] { row["visitDate"].ToString(), row["clinicalNotes"].ToString() };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetChronicIllness()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterChronicIllness(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string active = "";
                string dose = "";
                if (row["active"].ToString() == "1")
                    active = "checked";
                else
                    active = "";
                if (row["dose"].ToString() == "0")
                {
                    dose = "";
                }
                else
                {
                    dose = row["dose"].ToString();
                }



                string[] i = new string[7] { row["chronicIllnessID"].ToString(), row["chronicIllnessName"].ToString(), row["Treatment"].ToString(), dose, row["OnsetDate"].ToString(),
                    "<input type='checkbox' id='chkChronic" + row["chronicIllnessID"].ToString() + "' " + active + " >",
                    "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetAllergies()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterAllergies(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[8] { row["allergyId"].ToString(), row["reactionId"].ToString(), row["severityId"].ToString(),
                    row["allergy"].ToString(), row["reaction"].ToString(), row["severity"].ToString(),
                    row["onsetDate"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetVaccines()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            LookupLogic ll = new LookupLogic();
            DataTable theDT = patientEncounter.loadPatientEncounterVaccines(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                List<LookupItemView> lookupList = ll.GetItemIdByGroupAndItemName("VaccinationStages", LookupLogic.GetLookupNameById(Convert.ToInt32(row["vaccineStageID"])).ToString());
                if (lookupList.Any())
                {
                    string[] i = new string[6] { row["vaccineID"].ToString(), row["vaccineStageID"].ToString(), row["VaccineName"].ToString(), row["VaccineStageName"].ToString(), row["VaccineDate"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                    rows.Add(i);
                }

            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetPhysicalExam()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterPhysicalExam(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[7] { row["examTypeID"].ToString(), row["examID"].ToString(), row["findingID"].ToString(), row["examType"].ToString(), row["exam"].ToString(), row["findings"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDiagnosis()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterDiagnosis(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[4] { row["Diagnosis"].ToString(), row["DisplayName"].ToString(), row["ManagementPlan"].ToString(), "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>" };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetPharmacyPrescriptionDetails()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientPharmacyPrescription(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            ArrayList rows = new ArrayList();
            string remove = "";

            if (Session["DosageFrequency"] != null)
            {
                DosageFrequency = Convert.ToInt32(Session["DosageFrequency"]);
            }
            foreach (DataRow row in theDT.Rows)
            {
                if (row["DispensedQuantity"].ToString() == "")
                {
                    remove = "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>";
                }
                else
                {
                    if (Convert.ToDecimal(row["DispensedQuantity"].ToString()) == 0)
                    {
                        remove = "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>";
                    }
                    else
                    {
                        remove = "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' disabled> Remove</button>";
                    }
                }

                if (DosageFrequency == 1)
                {
                    string[] i = new string[13] { row["Drug_Pk"].ToString(), row["batchId"].ToString(),
                    row["FrequencyID"].ToString(),row["abbr"].ToString(),row["DrugName"].ToString(),
                    row["batchName"].ToString(),row["dose"].ToString(),row["freq"].ToString(),
                    row["duration"].ToString(),row["OrderedQuantity"].ToString(),row["DispensedQuantity"].ToString(),
                    row["prophylaxis"].ToString(), remove
                     };
                    rows.Add(i);

                }
                else
                {
                    string[] i = new string[14] { row["Drug_Pk"].ToString(), row["batchId"].ToString(),
                    //row["FrequencyID"].ToString(),
                    row["abbr"].ToString(),row["DrugName"].ToString(),
                    row["batchName"].ToString(),row["MorningDose"].ToString(),row["MiddayDose"].ToString(),
                    row["EveningDose"].ToString(), row["NightDose"].ToString(),
                    row["duration"].ToString(),row["OrderedQuantity"].ToString(),row["DispensedQuantity"].ToString(),
                    row["prophylaxis"].ToString(), remove
                     };
                    rows.Add(i);
                }

            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetLatestPharmacyPrescriptionDetails()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientLatestPharmacyPrescription(Session["PatientPK"].ToString(), Session["AppLocationId"].ToString());
            ArrayList rows = new ArrayList();
            string remove = "<button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button>";
            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[13] { row["Drug_Pk"].ToString(), row["batchId"].ToString(),
                    row["FrequencyID"].ToString(),row["abbr"].ToString(),row["DrugName"].ToString(),
                    row["batchName"].ToString(),row["dose"].ToString(),row["freq"].ToString(),
                    row["duration"].ToString(),row["OrderedQuantity"].ToString(),row["DispensedQuantity"].ToString(),
                    row["prophylaxis"].ToString(), remove
                     };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetPharmacyPendingPrescriptions()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPharmacyPendingPrescriptions(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[7] { row["PatientMasterVisitID"].ToString(), row["Ptn_pk"].ToString(),
                    row["identifiervalue"].ToString(),row["FirstName"].ToString(),row["MidName"].ToString(),
                    row["LastName"].ToString(),row["prescribedBy"].ToString()};
                rows.Add(i);
            }
            return rows;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDrugList(string PMSCM, string treatmentPlan)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPharmacyDrugList(PMSCM, treatmentPlan);
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[2] { row["val"].ToString(), row["DrugName"].ToString() };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetPresentingComplaints()
        {
            //PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            var result = LookupLogic.GetLookUpItemViewByMasterName("PresentingComplaints");

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var presentingComplaints = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < presentingComplaints.Count; i++)
            {
                string[] j = new string[2] { presentingComplaints[i].ItemId + "~" + presentingComplaints[i].DisplayName, presentingComplaints[i].DisplayName };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList loadAllergies()
        {
            var result = LookupLogic.GetLookUpItemViewByMasterName("Allergies");

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var allergies = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < allergies.Count; i++)
            {
                string[] j = new string[2] { allergies[i].ItemId + "~" + allergies[i].DisplayName, allergies[i].DisplayName };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList loadAllergyReactions()
        {
            var result = LookupLogic.GetLookUpItemViewByMasterName("AllergyReactions");

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var allergyReactions = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < allergyReactions.Count; i++)
            {
                string[] j = new string[2] { allergyReactions[i].ItemId + "~" + allergyReactions[i].DisplayName, allergyReactions[i].DisplayName };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList loadDiagnosis()
        {
            LookupICDCodesListManager liclm = new LookupICDCodesListManager();
            var result = LookupLogic.GetLookUpItemViewByMasterName("ICD10");
            var list = liclm.GetICDCodeList();
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var diagnosis = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

            ArrayList rows = new ArrayList();

            list.ToList().ForEach(x => rows.Add(new string[]
            {
                x.Id +"~"+"mstICD" +"~"+  x.Name,
                x.Name

            }));


            //var data=list.Select(x => new string[]
            //     {
            //    x.Id +"~"+ x.Name,
            //    x.Code+ "" + x.Name
            //     }));
            int number = rows.Count;
            for (int i = 0; i < diagnosis.Count; i++)
            {
                string[] j = new string[2] { diagnosis[i].ItemId + "~" + "lookupitem" + "~" + diagnosis[i].DisplayName, diagnosis[i].DisplayName };
                rows.Add(j);
            }



            //for (int i=0;i<list.Count;i++)
            //{
            //    string[] licd = new string[2] { list[i].Id + "~" + list[i].Name,list[i].Code+ "" + list[i].Name};
            //    rows.Add(licd);
            //}
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetCurrentRegimen()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            List<Entities.CCC.Encounter.PatientEncounter.PharmacyFields> lst = new List<Entities.CCC.Encounter.PatientEncounter.PharmacyFields>();
            lst = patientEncounter.getPharmacyCurrentRegimen(Session["PatientPK"].ToString());

            ArrayList rows = new ArrayList();

            if (lst.Count > 0)
            {
                string[] i = new string[2] { lst[0].RegimenLine, lst[0].Regimen };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDrugBatches(string DrugPk)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            List<Entities.CCC.Encounter.PatientEncounter.DrugBatch> lst = patientEncounter.getPharmacyDrugBatch(DrugPk);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < lst.Count; i++)
            {
                string[] j = new string[2] { lst[i].id, lst[i].batch };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetDrugSwitchReasons(string TreatmentPlan)
        {
            //PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            //DataTable theDT = patientEncounter.getPharmacyDrugSwitchInterruptionReason(TreatmentPlan);
            //ArrayList rows = new ArrayList();

            //foreach (DataRow row in theDT.Rows)
            //{
            //    string[] i = new string[2] { row["ItemId"].ToString(), row["DisplayName"].ToString() };
            //    rows.Add(i);
            //}
            //return rows;

            /////////////////////
            var result = LookupLogic.GetLookUpItemViewByMasterName(TreatmentPlan);

            JavaScriptSerializer parser = new JavaScriptSerializer();
            var regimen = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);

            ArrayList rows = new ArrayList();

            for (int i = 0; i < regimen.Count; i++)
            {
                string[] j = new string[2] { regimen[i].ItemId, regimen[i].DisplayName };
                rows.Add(j);
            }
            return rows;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetRegimensBasedOnRegimenLine(string RegimenLine)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.getPharmacyRegimens(RegimenLine);
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[2] { row["LookupItemId"].ToString(), row["DisplayName"].ToString() };
                rows.Add(i);
            }
            return rows;

            /////////////////////
            //var result = LookupLogic.GetLookUpItemViewByMasterName(RegimenLine);

            //JavaScriptSerializer parser = new JavaScriptSerializer();
            //var regimen = parser.Deserialize<List<KeyValue>>(result);

            //ArrayList rows = new ArrayList();

            //for (int i = 0; i < regimen.Count; i++)
            //{
            //    string[] j = new string[2] { regimen[i].ItemId, regimen[i].DisplayName };
            //    rows.Add(j);
            //}
            //return rows;

        }

        [WebMethod(EnableSession = true)]
        public int savePatientPharmacy(string TreatmentProgram, string PeriodTaken, string TreatmentPlan,
            string TreatmentPlanReason, string RegimenLine, string Regimen, string pmscm, string PrescriptionDate,
            string DispensedDate, string drugPrescription, string regimenText)
        {

            try
            {
                PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

                int val = patientEncounter.saveUpdatePharmacy(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString(),
                    Session["AppLocationId"].ToString(), Session["AppUserId"].ToString(), Session["AppUserId"].ToString(),
                    Session["AppUserId"].ToString(), RegimenLine, Session["ModuleId"].ToString(), pmscm, drugPrescription,
                    TreatmentProgram, PeriodTaken, TreatmentPlan, TreatmentPlanReason, Regimen, regimenText, PrescriptionDate,
                    DispensedDate);
                return val;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        [WebMethod(EnableSession = true)]
        public string getDrugFrequencyMultiplier(string freqID)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            return patientEncounter.getPharmacyDrugMultiplier(freqID);
        }

        [WebMethod(EnableSession = true)]
        public ArrayList DifferentiatedCareParameters()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            PatientEncounter.PatientCategorizationParameters categorizationParameters = new PatientEncounter.PatientCategorizationParameters();
            categorizationParameters = patientEncounter.getPatientDSDParameters(Session["PatientPK"].ToString());

            ArrayList rows = new ArrayList();

            double[] i = new double[6] { categorizationParameters.SameRegimen12Months, categorizationParameters.ActiveOIs, categorizationParameters.VL, categorizationParameters.Completed6MonthsIPT, categorizationParameters.BMI, categorizationParameters.age };
            rows.Add(i);


            return rows;
        }

        [WebMethod(EnableSession = true)]
        public ArrayList getZScoreValues(string height, string weight)
        {
            ArrayList result = new ArrayList();
            string weightForAgeResult = "", weightForHeight = "", BMIz = "";
            if (height != "" && weight != "")
            {
                PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
                Entities.CCC.Encounter.PatientEncounter.ZScores zsValues = new Entities.CCC.Encounter.PatientEncounter.ZScores();
                zsValues = patientEncounter.getZScores(Session["PatientPK"].ToString(), Convert.ToDouble(Session["Age"].ToString()), Session["Gender"].ToString(), Convert.ToDouble(height), Convert.ToDouble(weight));

                if (zsValues != null)
                {

                    //weight for age
                    if (zsValues.weightForAge >= 4)
                    {
                        weightForAgeResult = "4 (Overweight)";
                    }
                    else if (zsValues.weightForAge >= 3 && zsValues.weightForAge < 4)
                    {
                        weightForAgeResult = "3 (Overweight)";
                    }
                    else if (zsValues.weightForAge >= 2 && zsValues.weightForAge < 3)
                    {
                        weightForAgeResult = "2 (Overweight)";
                    }
                    else if (zsValues.weightForAge >= 1 && zsValues.weightForAge < 2)
                    {
                        weightForAgeResult = "1 (Overweight)";
                    }
                    else if (zsValues.weightForAge > -1 && zsValues.weightForAge < 1)
                    {
                        weightForAgeResult = "0 (Normal)";
                    }
                    else if (zsValues.weightForAge <= -1 && zsValues.weightForAge > -2)
                    {
                        weightForAgeResult = "-1 (Mild)";
                    }
                    else if (zsValues.weightForAge <= -2 && zsValues.weightForAge > -3)
                    {
                        weightForAgeResult = "-2 (Moderate)";
                    }
                    else if (zsValues.weightForAge <= -3 && zsValues.weightForAge > -4)
                    {
                        weightForAgeResult = "-3 (Severe)";
                    }
                    else if (zsValues.weightForAge <= -4)
                    {
                        weightForAgeResult = "-4 (Severe)";
                    }
                    else
                    {
                        weightForAgeResult = "Out of Range";
                    }

                    //weight for height
                    if (zsValues.weightForHeight >= 4)
                    {
                        weightForHeight = "4 (Overweight)";

                    }
                    else if (zsValues.weightForHeight >= 3 && zsValues.weightForHeight < 4)
                    {
                        weightForHeight = "3 (Overweight)";

                    }
                    else if (zsValues.weightForHeight >= 2 && zsValues.weightForHeight < 3)
                    {
                        weightForHeight = "2 (Overweight)";

                    }
                    else if (zsValues.weightForHeight >= 1 && zsValues.weightForHeight < 2)
                    {
                        weightForHeight = "1 (Overweight)";

                    }
                    else if (zsValues.weightForHeight > -1 && zsValues.weightForHeight < 1)
                    {
                        weightForHeight = "0 (Normal)";

                    }
                    else if (zsValues.weightForHeight <= -1 && zsValues.weightForHeight > -2)
                    {
                        weightForHeight = "-1 (Mild)";

                    }
                    else if (zsValues.weightForHeight <= -2 && zsValues.weightForHeight > -3)
                    {
                        weightForHeight = "-2 (Moderate)";

                    }
                    else if (zsValues.weightForHeight <= -3 && zsValues.weightForHeight > -4)
                    {
                        weightForHeight = "-3 (Severe)";

                    }
                    else if (zsValues.weightForHeight <= -4)
                    {
                        weightForHeight = "-4 (Severe)";

                    }
                    else
                    {
                        weightForHeight = "Out of Range";
                    }

                    //BMIz
                    if (zsValues.BMIz >= 4)
                    {
                        BMIz = "4 (Overweight)";

                    }
                    else if (zsValues.BMIz >= 3 && zsValues.BMIz < 4)
                    {
                        BMIz = "3 (Overweight)";

                    }
                    else if (zsValues.BMIz >= 2 && zsValues.BMIz < 3)
                    {
                        BMIz = "2 (Overweight)";

                    }
                    else if (zsValues.BMIz >= 1 && zsValues.BMIz < 2)
                    {
                        BMIz = "1 (Overweight)";

                    }
                    else if (zsValues.BMIz > -1 && zsValues.BMIz < 1)
                    {
                        BMIz = "0 (Normal)";

                    }
                    else if (zsValues.BMIz <= -1 && zsValues.BMIz > -2)
                    {
                        BMIz = "-1 (Mild)";

                    }
                    else if (zsValues.BMIz <= -2 && zsValues.BMIz > -3)
                    {
                        BMIz = "-2 (Moderate)";

                    }
                    else if (zsValues.BMIz <= -3 && zsValues.BMIz > -4)
                    {
                        BMIz = "-3 (Severe)";

                    }
                    else if (zsValues.BMIz <= -4)
                    {
                        BMIz = "-4 (Severe)";

                    }
                    else
                    {
                        BMIz = "Out of Range";
                    }



                    string[] i = new string[3] { weightForAgeResult, weightForHeight, BMIz };
                    result.Add(i);
                }
            }
            return result;

        }

        [WebMethod(EnableSession = true)]
        public int saveNeonatalMilestones(string milestoneAssessed, string milestoneOnsetDate, string milestoneAchieved, string milestoneStatus, string milestoneComment)
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            int userId = Convert.ToInt32(Session["AppUserId"].ToString());
            return patientEncounter.saveNeonatalMilestone(patientMasterVisitId, patientId, userId, milestoneAssessed, milestoneOnsetDate, milestoneAchieved, milestoneStatus, milestoneComment);
        }

        //[WebMethod(EnableSession = true)]
        //public int addNeonatalMilestone(int patientId, int patientVisitId, string milestoneAssessed, string milestoneOnsetDate, string milestoneAchieved, string milestoneStatus, string milestoneComment)
        //{
        //    try
        //    {

        //        PatientVital patientVital = new PatientVital()
        //        {
        //        }
        //    }
        //}


        [WebMethod(EnableSession = true)]
        public string SavePatientAdherenceAssessment(string feelBetter, string carelessAboutMedicine, string feelWorse, string forgetMedicine, string takeMedicine, string stopMedicine, string underPressure, string difficultyRemembering)
        {
            PatientAdherenceAssessmentManager patientAdherenceAssessment = new PatientAdherenceAssessmentManager();
            int adherenceScore = 0;
            string adherenceRating = null;
            decimal mmas8Score;

            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            int patientMasterVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            int createdBy = Convert.ToInt32(Session["AppUserId"].ToString());
            bool feel_Better = Convert.ToBoolean(Convert.ToInt32(feelBetter));
            bool careless_Medicine = Convert.ToBoolean(Convert.ToInt32(carelessAboutMedicine));
            bool feel_Worse = Convert.ToBoolean(Convert.ToInt32(feelWorse));
            bool forget_Medicine = Convert.ToBoolean(Convert.ToInt32(forgetMedicine));
            bool take_medicine = Convert.ToBoolean(Convert.ToInt32(takeMedicine));
            bool stop_Medicine = Convert.ToBoolean(Convert.ToInt32(stopMedicine));
            bool under_Pressure = Convert.ToBoolean(Convert.ToInt32(underPressure));
            decimal difficulty_Remembering = Convert.ToDecimal(difficultyRemembering);

            adherenceScore = Convert.ToInt32(feelBetter) + Convert.ToInt32(carelessAboutMedicine) +
                             Convert.ToInt32(feelWorse) + Convert.ToInt32(forgetMedicine);

            if (adherenceScore == 0)
            {
                adherenceRating = "Good";
            } else if (adherenceScore >= 1 && adherenceScore <= 2)
            {
                adherenceRating = "Fair";
            } else if (adherenceScore >= 3 && adherenceScore <= 4)
            {
                adherenceRating = "Poor";
            }

            if (adherenceScore > 0)
            {
                mmas8Score = Convert.ToDecimal(adherenceScore) + Convert.ToDecimal(take_medicine) +
                             Convert.ToDecimal(stop_Medicine) + Convert.ToDecimal(under_Pressure) + Convert.ToDecimal(difficulty_Remembering);

                if (mmas8Score >= 1 && mmas8Score <= 2)
                {
                    adherenceRating = "Inadequate";
                }
                else if (mmas8Score >= 3 && mmas8Score <= 8)
                {
                    adherenceRating = "Poor";
                }
            }


            var history = patientAdherenceAssessment.GetActiveAdherenceAssessment(patientId);

            if (history.Count > 0)
            {
                history[0].DeleteFlag = true;
                patientAdherenceAssessment.UpdateAdherenceAssessment(history[0]);
            }

            int result;

            if (adherenceScore > 0)
            {
                result = patientAdherenceAssessment.AddPatientAdherenceAssessment(patientId, patientMasterVisitId,
                    createdBy, feel_Better, careless_Medicine, feel_Worse, forget_Medicine, take_medicine, stop_Medicine, under_Pressure, difficulty_Remembering);
            }
            else
            {
                result = patientAdherenceAssessment.AddPatientAdherenceAssessment(patientId, patientMasterVisitId,
                    createdBy, feel_Better, careless_Medicine, feel_Worse, forget_Medicine);
            }
            if (result > 0)
            {
                var lookUpLogic = new LookupLogic();
                var adherence = lookUpLogic.GetItemIdByGroupAndItemName("ARVAdherence", adherenceRating);
                var itemId = 0;
                var msg = "Successfully Saved Adherence Assessment";
                if (adherence.Count > 0)
                {
                    itemId = adherence[0].ItemId;
                }
                string[] arr1 = new string[] { msg, itemId.ToString() };
                return new JavaScriptSerializer().Serialize(arr1);
            }
            else
                return "An error occured";
        }

        [WebMethod(EnableSession = true)]
        public string AddArtDistribution(int patientId, int patientMasterVisitId, string artRefillModel, bool missedArvDoses,
            int missedDosesCount, bool fatigue, bool fever, bool nausea, bool diarrhea, bool cough, bool rash,
            bool genitalSore, string otherSymptom, bool newMedication, string newMedicineText, bool familyPlanning,
            string fpmethod, bool referredToClinic, DateTime? appointmentDate, int pregnancyStatus, int IsPatientArtDistributionDone)
        {
            try
            {
                ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
                string FastTrackReason = mgr.GetLookupItemId("ARTFastTrackReferral").ToString();
                string serviceAreaId = mgr.GetLookupItemId("MoH 257 GREENCARD").ToString();
                string AppointmentStatus = mgr.GetLookupItemId("Pending").ToString();
                string DifferentiatedCare = mgr.GetLookupItemId("Express Care").ToString();
                // int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
                var patientAppointment = new PatientAppointmentManager();
                if (patientId > 0)
                {
                    if (appointmentDate != null && appointmentDate != DateTime.MinValue && appointmentDate != DateTime.MaxValue)
                    {
                        var appointment = patientAppointment.GetByPatientId(patientId).FirstOrDefault(n => n.AppointmentDate.Date == appointmentDate.Value.Date && n.ServiceAreaId == Convert.ToInt32(serviceAreaId) && n.ReasonId == Convert.ToInt32(FastTrackReason));
                        if (appointment != null)
                        {
                            Msg += "Appointment  exists and is Scheduled";
                        }
                        else
                        {
                            if (appointmentDate != DateTime.MinValue || appointmentDate != null)
                            {

                                PatientAppointment patientNewAppointment = new PatientAppointment()
                                {
                                    PatientId = patientId,
                                    PatientMasterVisitId = patientMasterVisitId,
                                    AppointmentDate = Convert.ToDateTime(appointmentDate),
                                    DifferentiatedCareId = Convert.ToInt32(DifferentiatedCare),
                                    ReasonId = Convert.ToInt32(FastTrackReason),
                                    ServiceAreaId = Convert.ToInt32(serviceAreaId),
                                    StatusId = Convert.ToInt32(AppointmentStatus),
                                    Description = "",
                                    CreatedBy = Convert.ToInt32(Session["AppUserId"]),
                                    CreateDate = DateTime.Now
                                };

                                var Newappointment = new PatientAppointmentManager();
                                Result = Newappointment.AddPatientAppointments(patientNewAppointment);
                                if (Result > 0)
                                {
                                    Msg += "Patient appointment Added Successfully!";
                                }
                            }
                            else
                            {
                                Msg = "Patient appointment not Saved Successfully";
                            }
                        }

                    }

                }
                var artDistribution = new PatientArtDistributionManager();

                if (IsPatientArtDistributionDone == 1)
                {
                    PatientArtDistribution patientArtDistribution = artDistribution.GetPatientArtDistributionByPatientIdAndVisitId(patientId, patientMasterVisitId);

                    if (patientArtDistribution != null)
                    {
                        patientArtDistribution.ArtRefillModel = artRefillModel;
                        patientArtDistribution.Cough = cough;
                        patientArtDistribution.Diarrhea = diarrhea;
                        patientArtDistribution.FamilyPlanning = familyPlanning;
                        patientArtDistribution.FamilyPlanningMethod = fpmethod;
                        patientArtDistribution.Fatigue = fatigue;
                        patientArtDistribution.Fever = fever;
                        patientArtDistribution.MissedArvDoses = missedArvDoses;
                        patientArtDistribution.GenitalSore = genitalSore;
                        patientArtDistribution.MissedArvDosesCount = missedDosesCount;
                        patientArtDistribution.Nausea = nausea;
                        patientArtDistribution.NewMedication = newMedication;
                        patientArtDistribution.NewMedicationText = newMedicineText;
                        patientArtDistribution.OtherSymptom = otherSymptom;
                        patientArtDistribution.PregnancyStatus = pregnancyStatus;
                        patientArtDistribution.Rash = rash;
                        patientArtDistribution.ReferedToClinic = referredToClinic;
                        patientArtDistribution.ReferedToClinicDate = appointmentDate;

                        Result = artDistribution.UpdatePatientArtDistribution(patientArtDistribution);
                    }

                    else
                    {

                        var patientArvDist = new PatientArtDistribution()
                        {
                            PatientId = patientId,
                            PatientMasterVisitId = patientMasterVisitId,
                            ArtRefillModel = artRefillModel,
                            Cough = cough,
                            Diarrhea = diarrhea,
                            FamilyPlanning = familyPlanning,
                            FamilyPlanningMethod = fpmethod,
                            Fatigue = fatigue,
                            Fever = fever,
                            MissedArvDoses = missedArvDoses,
                            GenitalSore = genitalSore,
                            MissedArvDosesCount = missedDosesCount,
                            Nausea = nausea,
                            NewMedication = newMedication,
                            NewMedicationText = newMedicineText,
                            OtherSymptom = otherSymptom,
                            PregnancyStatus = pregnancyStatus,
                            Rash = rash,
                            ReferedToClinic = referredToClinic,
                            ReferedToClinicDate = appointmentDate,
                        };

                        Result = artDistribution.AddPatientArtDistribution(patientArvDist);
                    }
                }
                else
                {

                    var patientArvDistribution = new PatientArtDistribution()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        ArtRefillModel = artRefillModel,
                        Cough = cough,
                        Diarrhea = diarrhea,
                        FamilyPlanning = familyPlanning,
                        FamilyPlanningMethod = fpmethod,
                        Fatigue = fatigue,
                        Fever = fever,
                        MissedArvDoses = missedArvDoses,
                        GenitalSore = genitalSore,
                        MissedArvDosesCount = missedDosesCount,
                        Nausea = nausea,
                        NewMedication = newMedication,
                        NewMedicationText = newMedicineText,
                        OtherSymptom = otherSymptom,
                        PregnancyStatus = pregnancyStatus,
                        Rash = rash,
                        ReferedToClinic = referredToClinic,
                        ReferedToClinicDate = appointmentDate,
                    };

                    Result = artDistribution.AddPatientArtDistribution(patientArvDistribution);
                }

                if (Result > 0)
                {
                    Msg = "Patient ART Distribution Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string GetArtDistributionForVisitDate(int pmvisitid)
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PatientArtDistribution, ArtDistributionDeTails>();
            });

            int patientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            int patientMasterVisitId = pmvisitid;

            PatientArtDistributionManager artDistributionManager = new PatientArtDistributionManager();

            PatientArtDistribution artDistribution = artDistributionManager.GetPatientArtDistributionByPatientIdAndVisitId(patientId, patientMasterVisitId);
            var results = Mapper.Map<ArtDistributionDeTails>(artDistribution);
            if (results != null)
            {
                results.DateReferedToClinic = String.Format("{0:dd-MMM-yyyy}", results.ReferedToClinicDate);

            }
            return new JavaScriptSerializer().Serialize(results);
        }


        [WebMethod(EnableSession = true)]
        public string GetArtDistributionForVisit()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PatientArtDistribution, ArtDistributionDeTails>();
            });

            int patientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            int patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);

            PatientArtDistributionManager artDistributionManager = new PatientArtDistributionManager();

            PatientArtDistribution artDistribution = artDistributionManager.GetPatientArtDistributionByPatientIdAndVisitId(patientId, patientMasterVisitId);
            var results = Mapper.Map<ArtDistributionDeTails>(artDistribution);
            if (results != null)
            {
                results.DateReferedToClinic = String.Format("{0:dd-MMM-yyyy}", results.ReferedToClinicDate);

            }
            return new JavaScriptSerializer().Serialize(results);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList LoadVitalSigns()
        {
            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
            DataTable theDT = patientEncounter.loadPatientVitalSigns(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            //DataTable theDT = patientEncounter.loadPatientEncounterComplaints(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            ArrayList rows = new ArrayList();

            foreach (DataRow row in theDT.Rows)
            {
                string[] i = new string[10] { row["VisitDate"].ToString(), row["Height"].ToString(), row["Weight"].ToString(), row["Muac"].ToString(),
                row["BPSystolic"].ToString(),row["BPDiastolic"].ToString(),row["Temperature"].ToString(),row["HeartRate"].ToString(),row["RespiratoryRate"].ToString(),
                row["SpO2"].ToString()};
                rows.Add(i);
            }
            return rows;
        }
        [WebMethod(EnableSession = true)]
        public ArrayList LoadPatientWHOStageList()
        {
            int PatientId = Convert.ToInt32(Session["PatientPk"].ToString());
            PatientWhoStageManager pms = new PatientWhoStageManager();
            ArrayList arrWholist = new ArrayList();
            PatientMasterVisitManager pmv = new PatientMasterVisitManager();
            Entities.CCC.Visit.PatientMasterVisit pmastv = new Entities.CCC.Visit.PatientMasterVisit();
            List<PatientWhoStage> pws = pms.WhoStagelistByPatientId(PatientId);
            LookupLogic ll = new LookupLogic();
            ArrayList row = new ArrayList();
            if (pws != null)
            {
                if (pws.Count > 0)
                {
                    pws.ForEach(x =>
                    {
                        int whostage = x.WHOStage;

                        string Condition = ll.GetLookupItemNameById(x.WHOStage);
                        pmastv = pmv.GetVisitById(x.PatientMasterVisitId);
                        string VisitDate = pmastv.VisitDate.ToString();

                        row.Add(new String[] { VisitDate, Condition });
                    }
                    );


                }


            }
            return row;
        }
        [WebMethod(EnableSession = true)]
        public ArrayList GetPatientAllLabs()
        {
            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());

            PatientLookupLabManager plm = new PatientLookupLabManager();
            List<PatientLab> pls = new List<PatientLab>();
                pls= plm.GetPatientLabs(patientId);
            ArrayList row = new ArrayList();
            pls.ForEach(x => { row.Add(new String[] {x.OrderedbyDate.ToString() ,x.TestName ,x.TestResult   }); });
            return row;
        }

    

    [WebMethod(EnableSession = true)]
        public ArrayList LoadPatientOIList()
        {
            int PatientId = Convert.ToInt32(Session["PatientPk"].ToString());
            ArrayList row = new ArrayList();
            PatientOIManager poi = new PatientOIManager();
            LookupLogic plm = new LookupLogic();
            string oi;
            PatientMasterVisitManager pmv = new PatientMasterVisitManager();
            Entities.CCC.Visit.PatientMasterVisit pmastv = new Entities.CCC.Visit.PatientMasterVisit();
           List<PatientOI> listoi = poi.GetPatientOIByPatient(PatientId);
            if (listoi != null)
            {
                if (listoi.Count > 0)
                {
                    listoi.ToList().ForEach(i =>
                    {
                         oi = plm.GetLookupItemNameById(i.OIId).ToString();
                        pmastv = pmv.GetVisitById(i.PatientMasterVisitId);
                          row.Add(
                            new string[] { pmastv.VisitDate.ToString(), oi });
                    });
                }


            }

            return row;
        }


        [WebMethod(EnableSession =true)]
        [ScriptMethod(UseHttpGet =false,ResponseFormat =ResponseFormat.Json)]
        public ArrayList LoadCurrentRegimen()
        {
            IPatientTreatmentTrackerManager patientTreatmentTrackerManager = (IPatientTreatmentTrackerManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPatientTreatmentTrackerManager, BusinessProcess.CCC");
            int patientId = Convert.ToInt32(Session["PatientPK"].ToString());
            var currentRegimen = patientTreatmentTrackerManager.GetCurrentPatientRegimen(patientId);
            ArrayList rows = new ArrayList();
            if (currentRegimen != null)
            {
                

                string[] i = new string[3]
                {currentRegimen.RegimenStartDate.ToString(),currentRegimen.Regimen,currentRegimen.TreatmentStatus };
                rows.Add(i);
            }
            return rows;
        }

        [WebMethod]
        public string updateScreeningYesNo(int patientId, int patientMasterVisitId, int screeningType, int screeningCategory, int screeningValue, int userId)
        {
            try
            {
                var NM = new PatientEncounterLogic();
                Result = NM.updateScreeningYesNo(patientId, patientMasterVisitId, screeningType, screeningCategory, screeningValue, userId);
                if (Result > 0)
                {
                    Msg = "Saved";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod(EnableSession = true)]
        public string savePatientEncounter(string EncounterType, int ServiceAreaId, int UserId)
        {
            try
            {
                PatientEncounterLogic patientEncounter = new PatientEncounterLogic();
                Result = patientEncounter.savePatientEncounter(Convert.ToInt32(Session["PatientPK"]), Convert.ToInt32(Session["PatientMasterVisitId"]), EncounterType,ServiceAreaId, Convert.ToInt32(Session["AppUserId"]));
                if (Result > 0)
                {
                    Msg = "Saved";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
    }
}
