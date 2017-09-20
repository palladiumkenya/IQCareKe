
using IQCare.Web.ApiLogic.MessageHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Events
{
    public class Publisher
    {
       public Publisher()
        {
           
        }
        public void RaiseEvent(object sender, MessageEventArgs e)
        {

            OutgoingMessageService ms = new OutgoingMessageService();
            ms.Handle(e);

        }
        public delegate void m_eventHandler(object sender, MessageEventArgs args);

        public event m_eventHandler DataExchangeEvent;

        protected void OnDataExchange(object sender, MessageEventArgs args)
        {
            DataExchangeEvent?.Invoke(this,args);
        }
        //public void NotifyListeners()
        //{
        //    m_event?.Invoke(this);
        //}

        //public void RegisterListener(IDataExchangeListener listener)
        //{
        //    m_event += new m_eventHandler(listener.OnNotification);
        //}

        //public void UnregisterListener(IDataExchangeListener listener)
        //{
        //    m_event -= new m_eventHandler(listener.OnNotification);
        //}
    }
}
