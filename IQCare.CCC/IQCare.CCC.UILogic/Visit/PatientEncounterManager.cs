using System;
using System.Collections.Generic;
using System.Web;
using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Visit;

namespace IQCare.CCC.UILogic.Visit
{
    public class PatientEncounterManager
    {
        private readonly IPatientEncounterManager _patientEncounterManager = (IPatientEncounterManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientEncounterManager, BusinessProcess.CCC");

        public int AddPatientEncounter(PatientEncounter pmt)
        {
            try
            {
                return _patientEncounterManager.AddpatientEncounter(pmt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int AddpatientEncounter(int patientId,int patientMasterVisitId,int encounterTypeId,int serviceId,int userId)
        {
            try
            {
                PatientEncounter patientVisitEncounter = new PatientEncounter()
                {
                    PatientId = patientId,
                    EncounterStartTime = DateTime.Now,
                    EncounterEndTime = DateTime.Now,
                    ServiceAreaId = serviceId,
                    EncounterTypeId = encounterTypeId,
                    PatientMasterVisitId = patientMasterVisitId,
                    CreatedBy = userId

                 };
                return _patientEncounterManager.AddpatientEncounter(patientVisitEncounter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public int AddpatientEncounterTracing(int patientId, int patientMasterVisitId, int encounterTypeId, int serviceId, int userId, DateTime encounterStartTime, DateTime encounterEndTime)
        {
            try
            {
                PatientEncounter patientVisitEncounter = new PatientEncounter()
                {
                    PatientId = patientId,
                    EncounterStartTime = encounterStartTime,
                    EncounterEndTime = encounterEndTime,
                    ServiceAreaId = serviceId,
                    EncounterTypeId = encounterTypeId,
                    PatientMasterVisitId = patientMasterVisitId,
                    CreatedBy = userId

                };
                return _patientEncounterManager.AddpatientEncounter(patientVisitEncounter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public int UpdatePatientEncounter(PatientEncounter patientEncounter)
        {
            try
            {
               return _patientEncounterManager.UpdatePatientEncounter(patientEncounter);
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message) ;
            }
        }

        public int DeletePatientEncounter(int id)
        {
            try
            {
                return _patientEncounterManager.DeletePatientEncounter(id);
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            } 
        }

        public  List<PatientEncounter> GetPatientCurrentEncounters(int patientId, DateTime visitDate)
        {
            try
            {
                return _patientEncounterManager.GetPatientCurrentEncounters(patientId, visitDate);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public List<PatientEncounter> GetPatientEncounterAll(int patientId)
        {
            try
            {
                return _patientEncounterManager.GetPatientEncounterAll(patientId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<PatientEncounter>  GetPatientEncounterByEncounterType(int patientId, string encounterName)
        {
            try
            {
                return _patientEncounterManager.GetPatientEncounterByEncounterType(patientId, encounterName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        public int GetPatientEncounterId(string masterName,string itemName)
        {
            int encounterId = 0;
            LookupLogic lookupLogic=new LookupLogic();
            var encounters = lookupLogic.GetItemIdByGroupAndItemName(masterName, itemName);
            foreach (var encounter in encounters)
            {
                encounterId = encounter.ItemId;
            }
            return encounterId;
        }

        public PatientEncounter GetEncounterIfExists(int patientId, int patientMasterVisitId, int encounterTypeId)
        {
            try
            {
                return _patientEncounterManager.GetEncounterIfExists(patientId, patientMasterVisitId, encounterTypeId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
    }
}