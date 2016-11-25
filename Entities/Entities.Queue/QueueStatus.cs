
namespace Entities.Queue
{
    public enum QueueStatus
    {
        Pending = 0,
        Served = 1,
        SystemRemove = 2,
        Deleted = 3
    }

    public enum QueuePriority
    {
        Normal = 1,
        Medium = 2,
        High = 3,
        Emergency = 4
    }
}
