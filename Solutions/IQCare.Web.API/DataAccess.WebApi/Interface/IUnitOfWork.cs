using System;

namespace DataAccess.WebApi.Interface
{
    public interface IUnitOfWork :IDisposable
    {
        int Complete();

        IApiInboxRepository ApiInboxRepository { get; }
        IApiOutboxRepository ApiOutboxRepository { get; }
        IApiInteropSystemsRepository ApiInteropSystemsRepository { get; }
    }
}
