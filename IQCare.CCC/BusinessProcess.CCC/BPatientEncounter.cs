using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.CCC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
                    ClsUtility.AddParameters("@TBScreening", SqlDbType.Int, TBScreening.ToString());
                    ClsUtility.AddParameters("@NutritionalStatus", SqlDbType.Int, NutritionalStatus.ToString());
                    ClsUtility.AddParameters("@lmp", SqlDbType.VarChar, lmp);
                    ClsUtility.AddParameters("@PregStatus", SqlDbType.VarChar, PregStatus);
                    ClsUtility.AddParameters("@edd", SqlDbType.VarChar, edd);
                    ClsUtility.AddParameters("@ANC", SqlDbType.VarChar, ANC);
                    ClsUtility.AddParameters("@OnFP", SqlDbType.Int, OnFP.ToString());
                    ClsUtility.AddParameters("@fpMethod", SqlDbType.Int, fpMethod.ToString());
                    ClsUtility.AddParameters("@CaCx", SqlDbType.VarChar, CaCx);
                    ClsUtility.AddParameters("@STIScreening", SqlDbType.VarChar, STIScreening);
                    ClsUtility.AddParameters("@STIPartnerNotification", SqlDbType.VarChar, STIPartnerNotification);

                    DataRow dr = (DataRow)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterPresentingComplaints", ClsUtility.ObjectEnum.DataRow);
                    int masterVisitID = Int32.Parse(dr[0].ToString());

                    if(adverseEvents.Count > 0)
                    {
                        ClsObject delAadvEvents = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                        int i = (int)delAadvEvents.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterAdverseEvents", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    foreach (var advEvnts in adverseEvents)
                    {
                        ClsObject advEvents = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                        ClsUtility.AddParameters("@adverseEvent", SqlDbType.Int, advEvnts.adverseEvent);
                        ClsUtility.AddParameters("@medicineCausingAE", SqlDbType.Int, advEvnts.medicineCausingAE);
                        ClsUtility.AddParameters("@adverseSeverity", SqlDbType.Int, advEvnts.adverseSeverity);
                        ClsUtility.AddParameters("@adverseAction", SqlDbType.Int, advEvnts.adverseAction);

                        int i = (int)advEvents.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterAdverseEvents", ClsUtility.ObjectEnum.ExecuteNonQuery);
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
                    foreach (var chrIll in chronicIllness)
                    {
                        ClsObject chrIllness = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);
                        ClsUtility.AddParameters("@chronicIllness", SqlDbType.Int, chrIll.chronicIllness);
                        ClsUtility.AddParameters("@treatment", SqlDbType.VarChar, chrIll.treatment);
                        ClsUtility.AddParameters("@dose", SqlDbType.VarChar, chrIll.dose);
                        ClsUtility.AddParameters("@duration", SqlDbType.Int, chrIll.duration);

                        int i = (int)chrIllness.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterChronicIllness", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }


                    foreach (var vacc in Vaccines)
                    {
                        ClsObject vaccine = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);
                        ClsUtility.AddParameters("@vaccine", SqlDbType.Int, vacc.vaccine);
                        ClsUtility.AddParameters("@vaccineStage", SqlDbType.Int, vacc.vaccineStage);
                        ClsUtility.AddParameters("@vaccineDate", SqlDbType.VarChar, vacc.vaccinationDate);

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
                    foreach (var pe in physicalExam)
                    {
                        ClsObject PEObj = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, masterVisitID.ToString());
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID);
                        ClsUtility.AddParameters("@examType", SqlDbType.Int, pe.examType);
                        ClsUtility.AddParameters("@exam", SqlDbType.VarChar, pe.exam);
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
                    ClsUtility.AddParameters("@ARVAdherence", SqlDbType.Int, ARVAdherence);
                    ClsUtility.AddParameters("@CTXAdherence", SqlDbType.VarChar, CTXAdherence);
                    ClsUtility.AddParameters("@nextAppointment", SqlDbType.VarChar, nextAppointment);
                    ClsUtility.AddParameters("@appointmentType", SqlDbType.VarChar, appointmentType);
                    //ClsUtility.AddParameters("@ANC", SqlDbType.VarChar, phdp);

                    int a = (int)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterPatientManagement", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //int masterVisitID = Int32.Parse(dr[0].ToString());

                    for(int i = 0; i < phdp.Count; i++)
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
                    //    ClsObject delAadvEvents = new ClsObject();
                    //    ClsUtility.Init_Hashtable();
                    //    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                    //    int i = (int)delAadvEvents.ReturnObject(ClsUtility.theParams, "sp_deletePatientEncounterAdverseEvents", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    //}

                    foreach (var diag in diagnosis)
                    {
                        ClsObject advEvents = new ClsObject();
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@masterVisitID", SqlDbType.Int, PatientMasterVisitID);
                        ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);
                        ClsUtility.AddParameters("@diagnosis", SqlDbType.VarChar, diag.diagnosis);
                        ClsUtility.AddParameters("@treatment", SqlDbType.VarChar, diag.treatment);
                        
                        int i = (int)advEvents.ReturnObject(ClsUtility.theParams, "sp_savePatientEncounterDiagnosis", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    }

                    return 0;
                }
            }
            catch //Exception ex)
            {
                return 0;
            }
        }


        public DataSet getPatientEncounter(string PatientMasterVisitID, string PatientID)
        {
            lock (this)
            {
                ClsObject PatientEncounter = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PatientMasterVisitID", SqlDbType.Int, PatientMasterVisitID);
                ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID);

                return (DataSet)PatientEncounter.ReturnObject(ClsUtility.theParams, "sp_getPatientEncounter", ClsUtility.ObjectEnum.DataSet);
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
    }
}
