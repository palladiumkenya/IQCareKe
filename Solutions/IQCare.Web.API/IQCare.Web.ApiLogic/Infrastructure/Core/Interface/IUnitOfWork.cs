using System;

namespace IQCare.Web.ApiLogic.Infrastructure.Core.Interface
{
    public interface IUnitOfWork :IDisposable
    {
        int Complete();

        IApiInboxRepository ApiInboxRepository { get; }
        IApiOutboxRepository ApiOutboxRepository { get; }
        IApiInteropSystemsRepository ApiInteropSystemsRepository { get; }
    }
}
