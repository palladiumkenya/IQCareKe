using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Linq;
namespace IQCare.CustomConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class PaymentConfigSection : ConfigurationSection
    {
        //PaymentHandlerElement element;
        //public PaymentConfigSection()
        //{
        //    element = new PaymentHandlerElement();
        //}
        /// <summary>
        /// Gets or sets the payment handlers.
        /// </summary>
        /// <value>
        /// The payment handlers.
        /// </value>
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true, IsKey = false)]
        [ConfigurationCollection(typeof(PaymentHandlerCollection), AddItemName = "paymenthandler",
            ClearItemsName = "clear", RemoveItemName = "remove",
            CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
        public PaymentHandlerCollection PaymentHandlers
        {
            get
            {
                return (PaymentHandlerCollection)base[""];
            }
            set
            {
                base[""] = value;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only; otherwise, false.
        /// </returns>
        public override bool IsReadOnly()
        {
            return false; ;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class PaymentHandlerElement : ConfigurationElement
    {

        /// <summary>
        /// Gets or sets the name of the payment method. The same name should be registered in features/roles 
        /// </summary>
        /// <value>
        /// The name of the method.
        /// </value>
        [ConfigurationProperty("handlername", IsKey = true, IsRequired = true)]
        public string HandlerName
        {
            get { return (string)this["handlername"]; }
            set { this["handlername"] = value; }
        }
        /// <summary>
        /// Gets or sets the name user control which handles this payment method
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationProperty("controlname", IsRequired = true)]
        public string ControlName
        {
            get { return (string)this["controlname"]; }
            set { this["controlname"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the connection.
        /// </summary>
        /// <value>
        /// The name of the connection.
        /// </value>
        [ConfigurationProperty("description", IsRequired = false)]
        public string Description
        {
            get { return (string)this["description"]; }
            set { this["description"] = value; }
        }
        /// <summary>
        /// Gets the handler pay methods.
        /// </summary>
        /// <value>
        /// The handler pay methods.
        /// </value>
        [ConfigurationProperty("paymethods", IsRequired = true, IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(PayMethodCollection), AddItemName = "paymethod",
            ClearItemsName = "clear", RemoveItemName = "remove",
            CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
        public PayMethodCollection HandlerPayMethods
        {
            get
            {
                return (PayMethodCollection)this["paymethods"];
            }

        }

    }
    /// <summary>
    /// 
    /// </summary>
    public class PayMethodElement : ConfigurationElement
    {

        /// <summary>
        /// Gets or sets the name of the payment method. The same name should be registered in features/roles 
        /// </summary>
        /// <value>
        /// The name of the method.
        /// </value>
        [ConfigurationProperty("methodname", IsKey = true, IsRequired = true)]
        public string MethodName
        {
            get { return (string)this["methodname"]; }
            set { this["methodname"] = value; }
        }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [ConfigurationProperty("code", IsKey = false, IsRequired = false)]
        public string Code
        {
            get { return (string)this["code"]; }
            set { this["code"] = value; }
        }
        /// <summary>
        /// Gets or sets the name of the connection.
        /// </summary>
        /// <value>
        /// The name of the connection.
        /// </value>
        [ConfigurationProperty("description", IsRequired = false)]
        public string Description
        {
            get { return (string)this["description"]; }
            set { this["description"] = value; }
        }


    }
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationCollection(typeof(PayMethodElement), AddItemName = "paymethod", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class PayMethodCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new PayMethodElement();
        }
        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Add(PayMethodElement element)
        {
            BaseAdd(element);
        }
        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only; otherwise, false.
        /// </returns>
        public override bool IsReadOnly()
        {
            return false;
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }
        /// <summary>
        /// Removes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Remove(PayMethodElement element)
        {
            if (BaseIndexOf(element) >= 0)
            {
                BaseRemove(element.MethodName);
            }
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Gets or sets the <see cref="PayMethodElement"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="PayMethodElement"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public PayMethodElement this[int index]
        {
            get { return (PayMethodElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PayMethodElement)element).MethodName;
        }
        /// <summary>
        /// Collection type setting
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationCollection(typeof(PaymentHandlerElement), AddItemName = "paymenthandler", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class PaymentHandlerCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentHandlerCollection"/> class.
        /// </summary>
        //public PaymentHandlerCollection()
        //{
        //    PaymentHandlerElement myElement = (PaymentHandlerElement)CreateNewElement();
        //   // Add(myElement);
        //}
        /// <summary>
        /// Adds the specified custom element.
        /// </summary>
        /// <param name="customElement">The custom element.</param>
        protected void Add(PaymentHandlerElement customElement)
        {
            BaseAdd(customElement);
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only; otherwise, false.
        /// </returns>
        public override bool IsReadOnly()
        {
            return false; ;
        }
        /// <summary>
        /// Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            base.BaseAdd(element, false);
        }
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new PaymentHandlerElement();
        }
        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PaymentHandlerElement)element).HandlerName;
        }
        /// <summary>
        /// Gets the name used to identify this collection of elements in the configuration file when overridden in a derived class.
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return "paymenthandler";
            }
        }
        /// <summary>
        /// Indicates whether the specified <see cref="T:System.Configuration.ConfigurationElement" /> exists in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <param name="elementName">The name of the element to verify.</param>
        /// <returns>
        /// true if the element exists in the collection; otherwise, false. The default is false.
        /// </returns>
        //protected override bool IsElementName(string elementName)
        //{
        //    return !String.IsNullOrEmpty(elementName) && elementName == "paymenthandler";
        //}

        /// <summary>
        /// Gets the <see cref="PaymentHandlerElement"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="PaymentHandlerElement"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public PaymentHandlerElement this[int index]
        {
            get { return (PaymentHandlerElement)base.BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);

                }
                BaseAdd(index, value);
            }
        }
        /// <summary>
        /// Gets the <see cref="PaymentHandlerElement"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="PaymentHandlerElement"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        new public PaymentHandlerElement this[string name]
        {
            get { return (PaymentHandlerElement)base.BaseGet(name); }
        }
        /// <summary>
        /// Collection type setting
        /// </summary>
        //public override ConfigurationElementCollectionType CollectionType
        //{
        //    get
        //    {
        //        return ConfigurationElementCollectionType.AddRemoveClearMap;
        //    }
        //}
        /// <summary>
        /// Indexofs the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public int indexof(PaymentHandlerElement element)
        {
            return BaseIndexOf(element);
        }

        /// <summary>
        /// Removes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Remove(PaymentHandlerElement element)
        {
            if (BaseIndexOf(element) >= 0)
                BaseRemove(element.HandlerName);
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }
        /// <summary>
        /// Removes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class PaymentConfigHelper
    {
        /// <summary>
        /// The _instances
        /// </summary>
        protected static Dictionary<string, PaymentHandlerElement> _handlers;
        protected static Dictionary<string, PayMethodElement> _methods;
        /// <summary>
        /// The _pay methods
        /// </summary>
        // protected static Dictionary<string, PayMethodElement> _payMethods;

         static PaymentConfigSection configSection;//= ConfigurationManager.GetSection("paymentSection") as PaymentConfigSection;
         static System.Configuration.Configuration webConfig = WebConfigurationManager.OpenWebConfiguration("~");
        /// <summary>
        /// Initializes the <see cref="ReportConfig"/> class.
        /// </summary>
        static PaymentConfigHelper()
        {
            Refresh();
        }
        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        static void Refresh()
        {
            webConfig = WebConfigurationManager.OpenWebConfiguration("~");
            _handlers = new Dictionary<string, PaymentHandlerElement>();
            _methods = new Dictionary<string, PayMethodElement>();
            configSection = webConfig.GetSection("paymentSection") as PaymentConfigSection;
            foreach (PaymentHandlerElement i in configSection.PaymentHandlers)
            {
                _handlers.Add(i.HandlerName, i);
                foreach (PayMethodElement e in i.HandlerPayMethods)
                {
                    _methods.Add(e.MethodName, e);
                }
            }
        }
        /// <summary>
        /// Sets the configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void SetConfig(Configuration config)
        {
            if (webConfig == null)
            {
                webConfig = config;
            }
        }
        /// <summary>
        /// Gets the report configuration element.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        static PaymentHandlerElement GetPayHandlerConfigElement(string name)
        {
            return _handlers[name];
        }
        
        /// <summary>
        /// Gets the pay method available.
        /// </summary>
        /// <value>
        /// The pay method available.
        /// </value>
        static List<PaymentHandlerElement> HandlersAvailable
        {
            get
            {
                return _handlers.Values.ToList();
            }
        }
        /// <summary>
        /// Gets the method available.
        /// </summary>
        /// <value>
        /// The method available.
        /// </value>
        static List<PayMethodElement> MethodAvailable
        {
            get
            {
                return _methods.Values.ToList();
            }
        }
        /// <summary>
        /// Pays the element description.
        /// </summary>
        /// <param name="paymethodName">Name of the paymethod.</param>
        /// <returns></returns>
        public static string PayElementDescription(string paymethodName)
        {
            try
            {
                return _methods[paymethodName].Description;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Pays the element code.
        /// </summary>
        /// <param name="paymethodName">Name of the paymethod.</param>
        /// <returns></returns>
        public static string PayElementCode(string paymethodName)
        {
           try
            { return _methods[paymethodName].Code;
            }
           catch
           {
               return "";
           }
        }
        /// <summary>
        /// Handlers the description.
        /// </summary>
        /// <param name="handlerName">Name of the handler.</param>
        /// <returns></returns>
        public static string HandlerDescription(string handlerName)
        {
           return _handlers[handlerName].Description;
            
        }
        /// <summary>
        /// Handlers the name of the control.
        /// </summary>
        /// <param name="handlerName">Name of the handler.</param>
        /// <returns></returns>
        public static string HandlerControlName(string handlerName)
        {
            PaymentHandlerElement handler = _handlers[handlerName];
            return handler.ControlName;
        }
        /// <summary>
        /// Handlerses this instance.
        /// </summary>
        /// <returns></returns>
        public static List<string> Handlers()
        {
            List<string> handlers = new List<string>();
            foreach (PaymentHandlerElement h in HandlersAvailable)
            {
                handlers.Add(h.HandlerName);
            }
            return handlers;
        }
        /// <summary>
        /// Gets the default handler element.
        /// </summary>
        /// <value>
        /// The default handler element.
        /// </value>
        static PaymentHandlerElement DefaultHandlerElement
        {
            get
            {
                return GetPayHandlerConfigElement("CORPORATE");
            }
        }
        /// <summary>
        /// Gets the pay handler by pay method.
        /// </summary>
        /// <param name="payMethod">The pay method.</param>
        /// <returns></returns>
         static PaymentHandlerElement GetHandlerByPayMethod(string payMethod)
        {
            PaymentHandlerElement returnValue  = null; //= DefaultHandlerElement;
            foreach (PaymentHandlerElement h in _handlers.Values.ToList())
            {
                if (HandlersPayMethod(h.HandlerName).Exists(m => m.MethodName == payMethod))
                {
                    returnValue = h;
                    break;
                }

            }
            return returnValue;

        }

        /// <summary>
        /// Gets the handler name by pay method.
        /// </summary>
        /// <param name="payMethod">The pay method.</param>
        /// <returns></returns>
        public static string GetHandlerNameByPayMethod(string payMethod)
        {
            PaymentHandlerElement returnValue = GetHandlerByPayMethod(payMethod);
           if (returnValue==null)
               return null;
            return returnValue.HandlerName;

        }
       
        /// <summary>
        /// Determine whether the handler contains the pay method identified by the name
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="paymentName">Name of the payment.</param>
        /// <returns></returns>
        //static bool HandlerHasPaymethod(PaymentHandlerElement handler, string paymentName)
        //{
        //    return HandlersPayMethod(handler.HandlerName).Exists(m => m.MethodName == paymentName);
        //}
        /// <summary>
        /// Handlers the has paymethod.
        /// </summary>
        /// <param name="handlerName">Name of the handler.</param>
        /// <param name="paymentName">Name of the payment.</param>
        /// <returns></returns>
        public static bool HandlerHasPaymethod(string handlerName, string paymentName)
        {

            return HandlersPayMethod(_handlers[handlerName].HandlerName).Exists(m => m.MethodName == paymentName);
        }
        /// <summary>
        /// Paymethods the exists.
        /// </summary>
        /// <param name="paymentName">Name of the payment.</param>
        /// <returns></returns>
        public static bool PaymethodExists(string paymentName)
        {
            bool exists = false;
           // return _methods.ContainsKey(paymentName);
            exists = GetHandlerByPayMethod(paymentName) != null;
            return exists;
        }
        /// <summary>
        /// Instanceses the specified instance name.
        /// </summary>
        /// <param name="instanceName">Name of the instance.</param>
        /// <returns></returns>
        PaymentHandlerElement Handler(string handlerName)
        {
            return _handlers[handlerName];
        }
        /// <summary>
        /// Returns the paymethods for a handler identified by the handlername parameter
        /// </summary>
        /// <param name="handlerName">Name of the handler.</param>
        /// <returns></returns>
        static List<PayMethodElement> HandlersPayMethod(string handlerName)
        {
            Dictionary<string, PayMethodElement> _payMethods = new Dictionary<string, PayMethodElement>();
           
                foreach (PayMethodElement i in _handlers[handlerName].HandlerPayMethods)
                {
                    _payMethods.Add(i.MethodName, i);
                }
            
            return _payMethods.Values.ToList();
        }

        /// <summary>
        /// Handlerses the pay methods.
        /// </summary>
        /// <param name="handlerName">Name of the handler.</param>
        /// <returns></returns>
        public static List<string> HandlersPayMethods(string handlerName)
        {
            List<string> payMethods = new List<string>();
            foreach (PayMethodElement i in _handlers[handlerName].HandlerPayMethods)
            {
                payMethods.Add(i.MethodName);
            }
            return payMethods;
            //return HandlersPayMethod(handlerName).Select(h => h.MethodName).ToList<string>();
        }
        /// <summary>
        /// Pays the methods.
        /// </summary>
        /// <returns></returns>
        public static List<string> PayMethods()
        {
            List<string> payMethods = new List<string>();
            List<string> handlerPaymethods = new List<string>();
            foreach (PaymentHandlerElement h in HandlersAvailable)
            {
                handlerPaymethods = PayMethods(h.HandlerName);
                //foreach (string hp in handlerPaymethods)
                //{
                    payMethods.AddRange(handlerPaymethods);
                //}
               // payMethods.Concat(PayMethods(h.HandlerName));
            }
            return payMethods;
        }
        /// <summary>
        /// Pays the methods.
        /// </summary>
        /// <param name="handlerName">Name of the handler.</param>
        /// <returns></returns>
        public static List<string> PayMethods(string handlerName)
        {
            List<string> payMethods = new List<string>();
            foreach (PayMethodElement i in _handlers[handlerName].HandlerPayMethods)
            {
                payMethods.Add(i.MethodName);
            }
            return payMethods;
        }

        /// <summary>
        /// Handlerses the with pay method.
        /// </summary>
        /// <returns></returns>
        static Dictionary<string, List<PayMethodElement>> HandlersWithPayMethod()
        {
            Dictionary<string, List<PayMethodElement>> handlerAndMethod = new Dictionary<string, List<PayMethodElement>>();
            foreach (PaymentHandlerElement h in HandlersAvailable)
            {

                handlerAndMethod.Add(h.ControlName, HandlersPayMethod(h.HandlerName));
            }

            return handlerAndMethod;
        }

     
        /// <summary>
        /// Adds the pay element.
        /// </summary>
        /// <param name="handlerName">Name of the handler.</param>
        /// <param name="element">The element.</param>
        public static void RemovePayElement(string handlerName, string elementName)
        {
            PaymentHandlerElement handler = GetPayHandlerConfigElement(handlerName);
            PayMethodElement element = HandlersPayMethod(handler.HandlerName).DefaultIfEmpty(null).FirstOrDefault(h => h.MethodName == elementName);
            if (element != null)
            {
                RemovePayElement(handler, element);
            }
        }
        /// <summary>
        /// Removes the pay element.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="element">The element.</param>
        static void RemovePayElement(PaymentHandlerElement handler, PayMethodElement element)
        {
            handler.HandlerPayMethods.Remove(element);

        }
        /// <summary>
        /// Adds the pay element.
        /// </summary>
        /// <param name="handlerName">Name of the handler.</param>
        /// <param name="element">The element.</param>
        static void AddPayElement(string handlerName, PayMethodElement element)
        {
            PaymentHandlerElement handler = GetPayHandlerConfigElement(handlerName);
            RemovePayElement(handler, element);
            handler.HandlerPayMethods.Add(element);
        }
        /// <summary>
        /// Modifies the pay element.
        /// </summary>
        /// <param name="currentHandlerName">Name of the current handler.</param>
        /// <param name="currentMethodName">Name of the current method.</param>
        /// <param name="newHandlerName">New name of the handler.</param>
        /// <param name="newMethodName">New name of the method.</param>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        public static void ModifyPayElement(string currentHandlerName, string currentMethodName, string newHandlerName, string newMethodName, string code, string description)
        {
            PaymentHandlerElement currentHandler = null;

            bool changeMethodName = newMethodName != currentMethodName;
            bool changeHandler = currentHandlerName != newHandlerName;

            if (_handlers.ContainsKey(currentHandlerName))
            {
                currentHandler = _handlers[currentHandlerName];
            }
            if (currentHandler == null)
            {
                currentHandler = _handlers[newHandlerName];
                changeHandler = true;
            }
            PaymentHandlerElement newHandler = _handlers[newHandlerName];           

            if (changeHandler)
            {// chaging the handler
                PayMethodElement element = HandlersPayMethod(currentHandler.HandlerName).DefaultIfEmpty(null).FirstOrDefault(h => h.MethodName == currentMethodName);
                if(element !=null) RemovePayElement(currentHandler, element);
                AddPayElement(newHandlerName, newMethodName, code, description);               
            }
            else 
            {
                //we are not changing the handler
                PayMethodElement element = HandlersPayMethod(newHandler.HandlerName).DefaultIfEmpty(null).FirstOrDefault(h => h.MethodName == currentMethodName);
                if (element != null)
                {
                    //current paymethod exists for the handler
                    if (changeMethodName) element.MethodName = newMethodName;
                    element.Code = code;
                    element.Description = description;
                }
            }
        }
        /// <summary>
        /// Adds the pay element.
        /// </summary>
        /// <param name="handlerName">Name of the handler.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="code">The code.</param>
        /// <param name="description">The descrption.</param>
        public static void AddPayElement(string handlerName, string methodName, string code, string description )
        {
            PaymentHandlerElement handler = GetPayHandlerConfigElement(handlerName);
            if (!PaymethodExists(methodName))
            {
                PayMethodElement element = new PayMethodElement() { MethodName = methodName, Code = code, Description = description };
                AddPayElement(handlerName, element);
            }
        }
        /// <summary>
        /// Refreshes the section.
        /// </summary>
        public static void RefreshSection()
        {
            webConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("paymentSection");
            Refresh();
        }
    }

}
