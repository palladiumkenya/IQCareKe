using IQCare.DTO;
using IQCare.DTO.ObservationResult;

namespace IQCare.CCC.UILogic.Interoperability.Observation
{
    public class ProcessObservationResultMessage : IInteropDTO<ObservationResultDTO>
    {
        public ObservationResultDTO Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public ObservationResultDTO GetObservation(int entityId, int observationType)
        {
            switch (observationType)
            {
                case 0:
                    return ProcessObservation.GetWHOStage(entityId);
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
