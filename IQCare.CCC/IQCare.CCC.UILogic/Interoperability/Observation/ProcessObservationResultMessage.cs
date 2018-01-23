using IQCare.DTO;
using IQCare.DTO.ObservationResult;
using IQCare.Events;

namespace IQCare.CCC.UILogic.Interoperability.Observation
{
    public class ProcessObservationResultMessage : IInteropDTO<ObservationResultDTO>
    {
        public ObservationResultDTO Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public ObservationResultDTO GetObservation(MessageEventArgs messageEvent)
        {
            switch (messageEvent.ObservationType)
            {
                case ObservationType.WhoStage:
                    return ProcessObservation.GetWHOStage(messageEvent.EntityId);
                case ObservationType.Vitals:
                    return ProcessObservation.GetVitals(messageEvent.PatientId, messageEvent.PatientMasterVisitId);
                    default:
                        return ProcessObservation.GetWHOStage(entityId);
            }
        }

        public string Save(ObservationResultDTO t)
        {
            throw new System.NotImplementedException();
        }

        public string Update(ObservationResultDTO t)
        {
            throw new System.NotImplementedException();
        }
    }
}
