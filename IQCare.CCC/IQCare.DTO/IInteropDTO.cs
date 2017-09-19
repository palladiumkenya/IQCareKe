namespace IQCare.DTO
{
    public interface IInteropDTO <T> where T : class
    {
        string Save(T t);
        T Retrive(int entityId);

        string Update(T t);
    }
}
