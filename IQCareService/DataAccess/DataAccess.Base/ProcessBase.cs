using System;

namespace DataAccess.Base
{
    public class ProcessBase : MarshalByRefObject
    {
        #region "Constructor"
        public ProcessBase()
        {
            _Connection = null;
            _Transaction = null;
        }
        #endregion

        #region "Custom Functions"
        protected object _Connection;
        protected object _Transaction;

        public object Connection
        {
            get { return _Connection; }
            set { _Connection = value; }
        }

        public object Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }

        #endregion
    }
}
