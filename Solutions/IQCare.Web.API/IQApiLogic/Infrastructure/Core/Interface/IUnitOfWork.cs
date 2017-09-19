using System;
using System.Collections.Generic;
using System.Text;

namespace IQ.ApiLogic.Infrastructure.Core.Interface
{
    public interface IUnitOfWork :IDisposable
    {
        int Complete();

        IApiInboxRepository ApiInboxRepository { get; }
        IApiOutboxRepository ApiOutboxRepository { get; }
        IApiInteropSystemsRepository ApiInteropSystemsRepository { get; }
    }
}
