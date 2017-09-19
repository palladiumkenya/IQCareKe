using System;
using System.Collections.Generic;
using System.Text;

namespace IQ.ApiLogic.MessageHandler
{
    public interface IIncomingMessageService
    {
        void Handle(string messageType, string message);
    }
}
