namespace IQCare.Events
{
   public interface IDataExchange
    {
        event InteropEventHandler OnDataExchage;
        void RegisterListener(IDataExchangeListener listener);
        void UnregisterListener(IDataExchangeListener listener);
        void NotifyListeners();
    }
    public delegate void InteropEventHandler(MessageEventArgs e);
}
