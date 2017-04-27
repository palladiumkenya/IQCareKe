using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.CCC;
using System;
using System.Collections.Generic;
using System.Data;
using static Entities.CCC.Encounter.PatientEncounter;

namespace BusinessProcess.CCC
{
    public class BPatientEncounter : ProcessBase, IPatientEncounter
    {
        public int savePresentingComplaints(string PatientMasterVisitID, string PatientID, string ServiceID, string VisitDate, string VisitScheduled, string VisitBy, string anyComplaints, string Complaints, int TBScreening, int NutritionalStatus, int userId, List<AdverseEvents> adverseEvents, List<PresentingComplaints> presentingComplaints)
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
                    ClsUtility.AddParameters("@anyPresentingComplaints", SqlDbType.VarChar, anyComplaints);
                    ClsUtility.AddParameters("@ComplaintsNotes", SqlDbType.VarChar, Complaints);
                    ClsUtility.AddParameters("@TBScreening", SqlDbType.VarChar, TBScreening.ToString());
                    ClsUtility.AddParameters("@NutritionalStatus", SqlDbType.VarChar, NutritionalStatus.ToString());
                    ClsUtility.AddParameters("@userID", SqlDbType.VarChar, userId.ToString());

                    DataRow dr = (DataRow)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterPresentingComplaints", ClsUtility.ObjectEnum.DataRow);
                    int masterVisitID = Int32.Parse(dr[0].ToString());

                    ClsObject delAadvEvents = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                    int a = (int)delAadvEvents.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterAdverseEvents", ClsUtility.ObjectEnum.ExecuteNonQuery);


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
                            ClsUtility.AddParameters("@userID", SqlDbType.VarChar, userId.ToString());

                            int i = (int)advEvents.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterAdverseEvents", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }


                    ///
                    ClsObject delPComplaints = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                    int k = (int)delPComplaints.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterComplaints", ClsUtility.ObjectEnum.ExecuteNonQuery);


                    foreach (var PC in presentingComplaints)
                    {
                        if (PC.presentingComplaintID != "")
                        {
                            ClsObject advEvents = new ClsObject();
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                            ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                            ClsUtility.AddParameters("@presentingComplaintID", SqlDbType.VarChar, PC.presentingComplaintID);
                            ClsUtility.AddParameters("@onsetDate", SqlDbType.VarChar, PC.onsetDate);
                            ClsUtility.AddParameters("@userID", SqlDbType.VarChar, userId.ToString());

                            int i = (int)advEvents.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterComplaints", ClsUtility.ObjectEnum.ExecuteNonQuery);
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

        public int savePresentingComplaintsTS(string PatientMasterVisitID, string PatientID, string ServiceID, string VisitDate, string VisitScheduled, string VisitBy, int userId)
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
                    ClsUtility.AddParameters("@userID", SqlDbType.VarChar, userId.ToString());

                    DataRow dr = (DataRow)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterTS", ClsUtility.ObjectEnum.DataRow);
                    int masterVisitID = Int32.Parse(dr[0].ToString());

                    return masterVisitID;
                }
            }
            catch //Exception ex)
            {

                return 0;
            }
        }
        public int saveChronicIllness(string masterVisitID, string patientID, string userID, List<ChronicIlness> chronicIllness, List<Vaccines> Vaccines, List<Allergies> allergies)
        {
            try
            {
                lock (this)
                {
                    ClsObject obj = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, masterVisitID);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);

                    int a = (int)obj.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterChronicIllness", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    foreach (var chrIll in chronicIllness)
                    {
                        ClsObject chrIllness = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);
                        ClsUtility.AddParameters("@chronicIllness", SqlDbType.VarChar, chrIll.chronicIllnessID);
                        ClsUtility.AddParameters("@treatment", SqlDbType.VarChar, chrIll.treatment);
                        ClsUtility.AddParameters("@dose", SqlDbType.VarChar, chrIll.dose);
                        ClsUtility.AddParameters("@onsetDate", SqlDbType.VarChar, chrIll.OnsetDate);
                        ClsUtility.AddParameters("@active", SqlDbType.VarChar, chrIll.Active);
                        ClsUtility.AddParameters("@userID", SqlDbType.VarChar, userID);

                        int i = (int)chrIllness.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterChronicIllness", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    ClsObject objj = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, masterVisitID);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);

                    int b = (int)objj.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterVaccines", ClsUtility.ObjectEnum.ExecuteNonQuery);

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

                    //allergies
                    ClsObject objAllergy = new ClsObject();
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, masterVisitID);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);

                    int q = (int)objAllergy.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterAllergies", ClsUtility.ObjectEnum.ExecuteNonQuery);

                    foreach (var all in allergies)
                    {
                        ClsObject allerg = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);
                        ClsUtility.AddParameters("@allergy", SqlDbType.VarChar, all.allergyID);
                        ClsUtility.AddParameters("@allergyReaction", SqlDbType.VarChar, all.reactionID);
                        ClsUtility.AddParameters("@allergySeverity", SqlDbType.VarChar, all.severityID);
                        ClsUtility.AddParameters("@allergyOnsetDate", SqlDbType.VarChar, all.onsetDate);
                        ClsUtility.AddParameters("@userID", SqlDbType.VarChar, userID);

                        int i = (int)allerg.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterAllergies", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.VarChar, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.VarChar, PatientID);

                DataSet theDS = (DataSet)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounter", ClsUtility.ObjectEnum.DataSet);

                PresentingComplaintsEntity pce = new PresentingComplaintsEntity();

                if (theDS.Tables[0].Rows.Count > 0)
                {
                    pce.visitDate = ((DateTime)theDS.Tables[0].Rows[0]["visitDate"]).ToString("dd-MMM-yyyy");
                    pce.visitScheduled = theDS.Tables[0].Rows[0]["visitScheduled"].ToString();
                    pce.visitBy = theDS.Tables[0].Rows[0]["visitBy"].ToString();
                }

                if (theDS.Tables[1].Rows.Count > 0)
                {
                    pce.anyComplaint = theDS.Tables[1].Rows[0]["anyComplaint"].ToString();
                    pce.complaints = theDS.Tables[1].Rows[0]["PresentingComplaint"].ToString();
                }

                //if (theDS.Tables[2].Rows.Count > 0)
                //{
                //    string lmp = theDS.Tables[2].Rows[0]["FemaleLMP"].ToString();
                //    string edd = theDS.Tables[2].Rows[0]["ExpectedDateOfChild"].ToString();
                //    if (lmp != "")
                //    {
                //        DateTime dtLmp = DateTime.Parse(lmp);
                //        pce.lmp = dtLmp.ToString("dd-MMM-yyyy");
                //    }

                //    if(edd != "")
                //    {
                //        DateTime dtEdd = DateTime.Parse(edd);
                //        pce.edd = dtEdd.ToString("dd-MMM-yyyy");
                //    }

                //    pce.pregStatus = theDS.Tables[2].Rows[0]["PregnancyStatus"].ToString();
                //    //pce.edd = theDS.Tables[2].Rows[0]["ExpectedDateOfChild"].ToString();
                //    pce.STIPartnerNotification = theDS.Tables[2].Rows[0]["STIPartnerNotification"].ToString();
                //    pce.ancProfile = theDS.Tables[2].Rows[0]["ANCPNCProfile"].ToString();
                //}

                if (theDS.Tables[3].Rows.Count > 0)
                {
                    pce.tbScreening = theDS.Tables[3].Rows[0]["ScreeningValueId"].ToString();
                }

                if (theDS.Tables[4].Rows.Count > 0)
                {
                    pce.nutritionStatus = theDS.Tables[4].Rows[0]["ScreeningValueId"].ToString();
                }

                //if (theDS.Tables[5].Rows.Count > 0)
                //{
                //    pce.CaCX = theDS.Tables[5].Rows[0]["ScreeningValueId"].ToString();
                //}

                //if (theDS.Tables[6].Rows.Count > 0)
                //{
                //    pce.STIScreening = theDS.Tables[6].Rows[0]["ScreeningValueId"].ToString();
                //}

                //if (theDS.Tables[7].Rows.Count > 0)
                //{
                //    pce.onFP = theDS.Tables[7].Rows[0]["FPStatusId"].ToString();
                //    pce.reasonNotOnFP = theDS.Tables[7].Rows[0]["ReasonNotOnFPId"].ToString();
                //}

                //pce.fpMethod = new string[theDS.Tables[8].Rows.Count];
                //for (int k = 0; k < theDS.Tables[8].Rows.Count; k++)
                //{
                //    pce.fpMethod[k] = theDS.Tables[8].Rows[k]["FPMethodId"].ToString();
                //}

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

        public DataTable getPatientEncounterComplaints(string PatientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounterComplaints", ClsUtility.ObjectEnum.DataTable);

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

        public DataTable getPatientEncounterAllergies(string PatientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataTable)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounterAllergies", ClsUtility.ObjectEnum.DataTable);
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

        public ZScoresParameters GetZScoreValues(string PatientID, string gender, string height)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                ClsUtility.AddParameters("@sex", SqlDbType.VarChar, gender);
                ClsUtility.AddParameters("@height", SqlDbType.VarChar, height);

                DataSet ZScoreDS = (DataSet)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getZScores", ClsUtility.ObjectEnum.DataSet);

                ZScoresParameters zs = new ZScoresParameters();

                if(ZScoreDS.Tables[0].Rows.Count > 0)
                {
                    zs.L_WA = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["L"].ToString());
                    zs.M_WA = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["M"].ToString());
                    zs.S_WA = Convert.ToDouble(ZScoreDS.Tables[0].Rows[0]["S"].ToString());
                }

                if (ZScoreDS.Tables[1].Rows.Count > 0)
                {
                    zs.L_WH = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["L"].ToString());
                    zs.M_WH = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["M"].ToString());
                    zs.S_WH = Convert.ToDouble(ZScoreDS.Tables[1].Rows[0]["S"].ToString());
                }

                if (ZScoreDS.Tables[2].Rows.Count > 0)
                {
                    zs.L_BMIz = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["L"].ToString());
                    zs.M_BMIz = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["M"].ToString());
                    zs.S_BMIz = Convert.ToDouble(ZScoreDS.Tables[2].Rows[0]["S"].ToString());
                }

                return zs;
            }
        }


    }
}