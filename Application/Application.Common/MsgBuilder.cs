using System;
using System.Collections;
using System.Collections.Specialized;

    namespace Application.Common
    {
    public class MsgBuilder
    {
        protected NameValueCollection _DataElements;
        protected NameValueCollection _MsgRepository;

        #region "Property"
        public NameValueCollection DataElements
        {
            get { return _DataElements; }
        }

        public NameValueCollection MsgRepository
        {
            get { return _MsgRepository; }
        }
        #endregion

        #region "Constructor"
        public MsgBuilder()
        {
            _DataElements = new NameValueCollection();
            _MsgRepository = new NameValueCollection();
        }
        #endregion

        public string BuildMessage(string MessageId)
        {
            string theMsg = _MsgRepository[MessageId.Trim()];
            ICollection theElements = _DataElements.Keys;
            foreach (string key in theElements)
            {
                theMsg = theMsg.Replace(string.Format("<DataElement>{0}</DataElement>", key), _DataElements[key]);

            }
            return theMsg;
        }
    }
    }