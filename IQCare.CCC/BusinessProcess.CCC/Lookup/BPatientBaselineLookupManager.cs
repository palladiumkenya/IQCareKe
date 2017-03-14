using System.Collections.Generic;
using DataAccess.Base;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using System.Linq;

namespace BusinessProcess.CCC.Lookup
{
    public class BPatientBaselineLookupManager : ProcessBase, IPatientBaselineLookupManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());

        public List<PatientBaselineLookup> GetAllPatientBaseline(int patientId)
        {
            return _unitOfWork.PatientBaselineLookupRepository.FindBy(x => x.patientId == patientId & x.Id > 0).ToList();
        }

        public List<PatientBaselineLookup> GetPatientBaselineAssessment(int patientId)
        {
            return
                _unitOfWork.PatientBaselineLookupRepository.FindBy(x => x.patientId == patientId)
                    .Select(x => new PatientBaselineLookup()
                    {
                        patientId = x.patientId,
                        TransferInDate = x.TransferInDate,
                        TreatmentStartDate = x.TreatmentStartDate,
                        CurrentTreatment = x.CurrentTreatment,
                        FacilityFrom = x.FacilityFrom,
                        mflcode = x.mflcode,
                        CountyFrom = x.CountyFrom,
                        TransferInNotes = x.TransferInNotes
                    }).ToList();
        }


        public List<PatientBaselineLookup> GetPatientHIVDiagnosis(int patientId)
        {
            return
                _unitOfWork.PatientBaselineLookupRepository.FindBy(x => x.patientId == patientId)
                    .Select(x => new PatientBaselineLookup()
                    {
                        patientId = x.patientId,
                        HivDiagnosisDate = x.HivDiagnosisDate,
                        EnrollmentDate = x.EnrollmentDate,
                        EnrollmentWHOStage = x.EnrollmentWHOStage,
                        ARTInitiationDate = x.ARTInitiationDate
                    }).ToList();
        }

        public List<PatientBaselineLookup> GetPatientTransferinStatus(int patientId)
        {
            return
                _unitOfWork.PatientBaselineLookupRepository.FindBy(x => x.patientId == patientId)
                    .Select(x => new PatientBaselineLookup()
                    {
                        patientId = x.patientId,
                        HBVInfected = x.HBVInfected,
                        Pregnant = x.Pregnant,
                        TBinfected = x.TBinfected,
                        WHOStage = x.WHOStage,
                        BreastFeeding = x.BreastFeeding,
                        CD4Count = x.CD4Count,
                        MUAC = x.MUAC,
                        Weight = x.Weight,
                            Height = x.Height,
                            BMI = x.BMI
                    }).ToList();
        }

        public List<PatientBaselineLookup> GetPatientTreatmentInitiation(int patientId)
        {
            return
                _unitOfWork.PatientBaselineLookupRepository.FindBy(x => x.patientId == patientId)
                    .Select(x => new PatientBaselineLookup()
                    {
                        patientId = x.patientId,
                        DateStartedOnFirstline = x.DateStartedOnFirstline,
                        Cohort = x.Cohort,
                        Regimen = x.Regimen,
                        BaselineViralLoad = x.BaselineViralLoad,
                        BaselineViralLoadDate = x.BaselineViralLoadDate

                    }).ToList();
        }
    }
}
