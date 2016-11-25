using System;
using System.Configuration;
using System.Collections.Specialized;
using Application.Common;
using Application.Interface;

    namespace Application.Presentation
    {
    public abstract class ObjectFactory
    {
        public static Object CreateInstance(string type)
        {
            string theServerLocation = ConfigurationManager.AppSettings.Get("ServerLocation");
            IBusinessServerFactory theFactory = (IBusinessServerFactory)Activator.GetObject(Type.GetType("Application.Interface.IBusinessServerFactory, Application.Interface"), theServerLocation);
             return theFactory.CreateInstance(type);
        }
    }
    }
