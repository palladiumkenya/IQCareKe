using System.Data;
using Entities.Queue;

namespace Interface.Clinical
{
   public interface IPatientQueue
    {
       int QueuePatient(int patientId, int queueId, QueueStatus queueStatus, QueuePriority priority, int moduleId, int userId);

       void ChangeQueueStatus(int patientQueueId, QueueStatus queueStatus, int userId);

       DataTable GetPatientPendingQueue(int patientId);

       DataTable GetQueuedPatient(int queueId, int moduleId);
    }
}
