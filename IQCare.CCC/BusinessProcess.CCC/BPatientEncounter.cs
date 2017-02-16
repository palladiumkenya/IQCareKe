using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using DataAccess.Context;
using Interface.CCC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using static Entities.CCC.Encounter.PatientEncounter;

namespace BusinessProcess.CCC
{
    public class BPatientEncounter : ProcessBase, IPatientEncounter
    {
        public int savePresentingComplaints(string PatientMasterVisitID, string PatientID, string ServiceID, string VisitDate, string VisitScheduled, string VisitBy, string Complaints, int TBScreening, int NutritionalStatus, string lmp, string PregStatus, string edd, string ANC, int OnFP, int fpMethod, string CaCx, string STIScreening, string STIPartnerNotification, List<AdverseEvents> adverseEvents)
        {
            try
            {
                lock (this)
                {
                    ClsObject PatientEncounter = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                    ClsUtility.AddParameters("@ServiceID", SqlDbType.Int, ServiceID);
                    ClsUtility.AddParameters("@VisitDate", SqlDbType.VarChar, VisitDate);
                    ClsUtility.AddParameters("@VisitScheduled", SqlDbType.VarChar, VisitScheduled);
                    ClsUtility.AddParameters("@VisitBy", SqlDbType.VarChar, VisitBy);
                    ClsUtility.AddParameters("@Complaints", SqlDbType.VarChar, Complaints);
                    ClsUtility.AddParameters("@TBScreening", SqlDbType.VarChar, TBScreening.ToString());
                    ClsUtility.AddParameters("@NutritionalStatus", SqlDbType.VarChar, NutritionalStatus.ToString());
                    ClsUtility.AddParameters("@lmp", SqlDbType.VarChar, lmp);
                    ClsUtility.AddParameters("@PregStatus", SqlDbType.VarChar, PregStatus);
                    ClsUtility.AddParameters("@edd", SqlDbType.VarChar, edd);
                    ClsUtility.AddParameters("@ANC", SqlDbType.VarChar, ANC);
                    ClsUtility.AddParameters("@OnFP", SqlDbType.VarChar, OnFP.ToString());
                    ClsUtility.AddParameters("@fpMethod", SqlDbType.VarChar, fpMethod.ToString());
                    ClsUtility.AddParameters("@CaCx", SqlDbType.VarChar, CaCx);
                    ClsUtility.AddParameters("@STIScreening", SqlDbType.VarChar, STIScreening);
                    ClsUtility.AddParameters("@STIPartnerNotification", SqlDbType.VarChar, STIPartnerNotification);

                    DataRow dr = (DataRow)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterPresentingComplaints", ClsUtility.ObjectEnum.DataRow);
                    int masterVisitID = Int32.Parse(dr[0].ToString());

                    //if(adverseEvents.Count > 0)
                    //{
                        ClsObject delAadvEvents = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                        int a = (int)delAadvEvents.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterAdverseEvents", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //}

                    foreach (var advEvnts in adverseEvents)
                    {
                        if (advEvnts.adverseEvent != "")
                        {
                            ClsObject advEvents = new ClsObject();
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                            ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                            ClsUtility.AddParameters("@adverseEvent", SqlDbType.VarChar, advEvnts.adverseEvent);
                            ClsUtility.AddParameters("@medicineCausingAE", SqlDbType.VarChar, advEvnts.medicineCausingAE);
                            ClsUtility.AddParameters("@adverseSeverity", SqlDbType.VarChar, advEvnts.adverseSeverityID);
                            ClsUtility.AddParameters("@adverseAction", SqlDbType.VarChar, advEvnts.adverseAction);

                            int i = (int)advEvents.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterAdverseEvents", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }

                    return masterVisitID;
                }
            }
            catch //Exception ex)
            {
                return 0;
            }
        }


        public int saveChronicIllness(string masterVisitID, string patientID, List<ChronicIlness> chronicIllness, List<Vaccines> Vaccines)
        {
            try
            {
                lock (this)
                {
                    //if (chronicIllness.Count > 0)
                    //{
                        ClsObject obj = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, masterVisitID);
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);

                        int a = (int)obj.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterChronicIllness", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //}

                    foreach (var chrIll in chronicIllness)
                    {
                        ClsObject chrIllness = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);
                        ClsUtility.AddParameters("@chronicIllness", SqlDbType.VarChar, chrIll.chronicIllnessID);
                        ClsUtility.AddParameters("@treatment", SqlDbType.VarChar, chrIll.treatment);
                        ClsUtility.AddParameters("@dose", SqlDbType.VarChar, chrIll.dose);
                        ClsUtility.AddParameters("@duration", SqlDbType.VarChar, chrIll.duration);

                        int i = (int)chrIllness.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterChronicIllness", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    //if (Vaccines.Count > 0)
                    //{
                        ClsObject objj = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, masterVisitID);
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);

                        int b = (int)objj.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterVaccines", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //}

                    foreach (var vacc in Vaccines)
                    {
                        ClsObject vaccine = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);
                        ClsUtility.AddParameters("@vaccine", SqlDbType.VarChar, vacc.vaccineID);
                        ClsUtility.AddParameters("@vaccineStage", SqlDbType.VarChar, vacc.vaccineStageID);
                        ClsUtility.AddParameters("@vaccineDate", SqlDbType.VarChar, vacc.vaccineDate);

                        int i = (int)vaccine.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterVaccines", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    return 0;
                }
            }
            catch //Exception ex)
            {
                return 0;
            }
        }

        public int savePhysicalEaxminations(string masterVisitID, string patientID, List<PhysicalExamination> physicalExam)
        {
            try
            {
                lock (this)
                {
                    //if (physicalExam.Count > 0)
                    //{
                        ClsObject obj = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, masterVisitID);
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);

                        int a = (int)obj.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterPhysicalExam", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //}

                    foreach (var pe in physicalExam)
                    {
                        ClsObject PEObj = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);
                        ClsUtility.AddParameters("@examType", SqlDbType.VarChar, pe.examTypeID);
                        ClsUtility.AddParameters("@exam", SqlDbType.VarChar, pe.examID);
                        ClsUtility.AddParameters("@findings", SqlDbType.VarChar, pe.findings);

                        int i = (int)PEObj.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterPhysicalExam", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    return 0;
                }
            }
            catch //Exception ex)
            {
                return 0;
            }
        }

        public int savePatientManagement(string PatientMasterVisitID, string PatientID, string ARVAdherence, string CTXAdherence, string nextAppointment, string appointmentType, List<string> phdp, List<Diagnosis> diagnosis)
        {
            try
            {
                lock (this)
                {
                    ClsObject PatientEncounter = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                    ClsUtility.AddParameters("@ARVAdherence", SqlDbType.VarChar, ARVAdherence);
                    ClsUtility.AddParameters("@CTXAdherence", SqlDbType.VarChar, CTXAdherence);
                    ClsUtility.AddParameters("@nextAppointment", SqlDbType.VarChar, nextAppointment);
                    ClsUtility.AddParameters("@appointmentType", SqlDbType.VarChar, appointmentType);
                    //ClsUtility.AddParameters("@ANC", SqlDbType.VarChar, phdp);

                    int a = (int)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterPatientManagement", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //int masterVisitID = Int32.Parse(dr[0].ToString());

                    ClsObject objj = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                    int c = (int)objj.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterPHDP", ClsUtility.ObjectEnum.ExecuteNonQuery);


                    for (int i = 0; i < phdp.Count; i++)
                    {
                        ClsObject phdpObj = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, PatientMasterVisitID);
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                        ClsUtility.AddParameters("@phdp", SqlDbType.VarChar, phdp[i].ToString());

                        int j = (int)phdpObj.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterPHDP", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }


                    //if (diagnosis.Count > 0)
                    //{
                        ClsObject obj = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                        int b = (int)obj.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterDiagnosis", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //}

                    foreach (var diag in diagnosis)
                    {
                        ClsObject advEvents = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, PatientMasterVisitID);
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                        ClsUtility.AddParameters("@diagnosis", SqlDbType.VarChar, diag.diagnosis);
                        ClsUtility.AddParameters("@treatment", SqlDbType.VarChar, diag.treatment);
                        
                        int d = (int)advEvents.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterDiagnosis", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    return 0;
                }
            }
            catch //Exception ex)
            {
                return 0;
            }
        }


        public PresentingComplaintsEntity getPatientEncounter(string PatientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                DataSet theDS = (DataSet)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounter", ClsUtility.ObjectEnum.DataSet);

                PresentingComplaintsEntity pce = new PresentingComplaintsEntity();
       
                if(theDS.Tables[0].Rows.Count > 0)
                {
                    pce.visitDate = ((DateTime)theDS.Tables[0].Rows[0]["visitDate"]).ToString("dd-MMM-yyyy");
                    pce.visitScheduled = theDS.Tables[0].Rows[0]["visitScheduled"].ToString();
                    pce.visitBy = theDS.Tables[0].Rows[0]["visitBy"].ToString();
                }

                if(theDS.Tables[1].Rows.Count > 0)
                {
                    pce.complaints = theDS.Tables[1].Rows[0]["PresentingComplaint"].ToString();
                }

                if (theDS.Tables[2].Rows.Count > 0)
                {
                    pce.lmp = theDS.Tables[2].Rows[0]["FemaleLMP"].ToString();
                    pce.pregStatus = theDS.Tables[2].Rows[0]["PregnancyStatus"].ToString();
                    pce.edd = theDS.Tables[2].Rows[0]["ExpectedDateOfChild"].ToString();
                    pce.STIPartnerNotification = theDS.Tables[2].Rows[0]["STIPartnerNotification"].ToString();
                    pce.ancProfile = theDS.Tables[2].Rows[0]["ANCPNCProfile"].ToString();
                }

                if (theDS.Tables[3].Rows.Count > 0)
                {
                    pce.tbScreening = theDS.Tables[3].Rows[0]["ScreeningValueId"].ToString();
                }

                if (theDS.Tables[4].Rows.Count > 0)
                {
                    pce.nutritionStatus = theDS.Tables[4].Rows[0]["ScreeningValueId"].ToString();
                }

                if (theDS.Tables[5].Rows.Count > 0)
                {
                    pce.CaCX = theDS.Tables[5].Rows[0]["ScreeningValueId"].ToString();
                }

                if (theDS.Tables[6].Rows.Count > 0)
                {
                    pce.STIScreening = theDS.Tables[6].Rows[0]["ScreeningValueId"].ToString();
                }

                if (theDS.Tables[7].Rows.Count > 0)
                {
                    pce.onFP = theDS.Tables[7].Rows[0]["FPStatusId"].ToString();
                }

                if (theDS.Tables[8].Rows.Count > 0)
                {
                    pce.fpMethod = theDS.Tables[8].Rows[0]["FPMethodId"].ToString();
                }

                if (theDS.Tables[9].Rows.Count > 0)
                {
                    pce.nextAppointmentDate = ((DateTime)theDS.Tables[9].Rows[0]["AppointmentDate"]).ToString("dd-MMM-yyyy");
                    pce.nextAppointmentType = theDS.Tables[9].Rows[0]["ReasonID"].ToString();
                }
                
                pce.phdp = new string[theDS.Tables[10].Rows.Count];
                for (int k = 0; k < theDS.Tables[10].Rows.Count; k++)
                {
                    pce.phdp[k] = theDS.Tables[10].Rows[k]["phdp"].ToString();
                }

                if (theDS.Tables[11].Rows.Count > 0)
                {
                    pce.ARVAdherence = theDS.Tables[11].Rows[0]["Score"].ToString();
                }

                if (theDS.Tables[12].Rows.Count > 0)
                {
                    pce.CTXAdherence = theDS.Tables[12].Rows[0]["Score"].ToString();
                }

                return pce;
            }
        }

        public DataTable getPatientEncounterHistory(string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounterHistory", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable getPatientEncounterAdverseEvents(string PatientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounterAdverseEvents", ClsUtility.ObjectEnum.DataTable);

            }
        }

        public DataTable getPatientEncounterChronicIllness(string PatientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounterChronicIllness", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable getPatientEncounterVaccines(string PatientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounterVaccines", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable getPatientEncounterPhysicalExam(string PatientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounterExam", ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable getPatientEncounterDiagnosis(string PatientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounterDiagnosis", ClsUtility.ObjectEnum.DataTable);
            }
        }
    }
}