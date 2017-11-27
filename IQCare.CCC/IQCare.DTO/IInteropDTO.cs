namespace IQCare.DTO
{
    public interface IInteropDTO <T> where T : class
    {
        string Save(T t);
        T Get(int entityId);

        string Update(T t);
    }
}
