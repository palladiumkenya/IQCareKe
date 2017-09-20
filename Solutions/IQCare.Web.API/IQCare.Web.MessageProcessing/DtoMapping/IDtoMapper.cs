using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.DTO;
using IQCare.Web.MessageProcessing.JsonMappingEntities;

namespace IQCare.Web.MessageProcessing.DtoMapping
{
    public interface IDtoMapper
    {
        Registration PatientRegistrationMapping(PatientRegistrationEntity entity);
        void PatientTransferIn();
        void UpdatedClientInformation();
        void PatientTransferOut();
        void RegimenChange();
        void StopDrugs();
        void DrugPrescriptionRaised();
        void DrugOrderCancel();
        void DrugOrderFulfilment();
        void AppointmentScheduling();
        void AppointmentUpdated();
        void AppointmentRescheduling();
        void AppointmentCanceled();
        void AppointmentHonored();
        void UniquePatientIdentification();
        void ViralLoadLabOrder();
        ViralLoadDto ViralLoadResults(ViralLoadResultEntity entity);
    }
}
