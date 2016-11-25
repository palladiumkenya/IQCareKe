using System;
using System.Reflection;
using Application.Interface;

namespace Application.BusinessProcess
{
    public class BusinessServerFactory : MarshalByRefObject, IBusinessServerFactory
    {
        public Object CreateInstance(string type)
        {
            object remserver = Activator.CreateInstance(Type.GetType(type));
            //return (IRemServer)Convert.ChangeType(remserver,typeof(IRemServer));
            return remserver; 
        }
    }
}
