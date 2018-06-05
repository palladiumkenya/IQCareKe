using Application.Presentation;
using Entities.CCC.Tb;
using Interface.CCC.Tb;
using System.Collections.Generic;
using IQCare.Web.UILogic;

namespace IQCare.CCC.UILogic.Tb
{
    public class PatientIcfActionManager
    {
        private IPatientIcfAction _patientIcfAction =
            (IPatientIcfAction)
            ObjectFactory.CreateInstance("BusinessProcess.CCC.Tb.BPatientIcfAction, BusinessProcess.CCC");

        public int AddPatientIcfAction(PatientIcfAction p)
        {
            PatientIcfAction patientIcfAction = new PatientIcfAction()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                ChestXray = p.ChestXray,
                EvaluatedForIpt = p.EvaluatedForIpt,
                InvitationOfContacts = p.InvitationOfContacts,
                SputumSmear = p.SputumSmear,
                StartAntiTb = p.StartAntiTb,
                GeneXpert = p.GeneXpert,
                CreatedBy = SessionManager.UserId
            };
            return _patientIcfAction.AddPatientIcfAction(patientIcfAction);
        }

        public PatientIcfAction GetPatientIcfAction(int id)
        {
            var patientIcfAction = _patientIcfAction.GetPatientIcfAction(id);
            return patientIcfAction;
        }

        public void DeletePatientIcfAction(int id)
        {
            _patientIcfAction.DeletePatientIcfAction(id);
        }

        public int UpdatePatientIcfAction(PatientIcfAction p)
        {
            PatientIcfAction patientIcfAction = new PatientIcfAction()
            {
                Id = p.Id,
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                ChestXray = p.ChestXray,
                EvaluatedForIpt = p.EvaluatedForIpt,
                InvitationOfContacts = p.InvitationOfContacts,
                SputumSmear = p.SputumSmear,
                StartAntiTb = p.StartAntiTb,
                GeneXpert = p.GeneXpert,
                CreatedBy = SessionManager.UserId
            };
            return _patientIcfAction.UpdatePatientIcfAction(patientIcfAction);
        }

        public List<PatientIcfAction> GetByPatientId(int patientId)
        {
            var patientIcfAction = _patientIcfAction.GetByPatientId(patientId);
            return patientIcfAction;
        }
    }
}