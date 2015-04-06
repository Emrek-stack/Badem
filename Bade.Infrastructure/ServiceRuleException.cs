

using System;

namespace Bade.Infrastructure
{
    public class ServiceRuleException : Exception
    {
        private string _message;
        public ServiceRuleException() { }

        public ServiceRuleException(string message)
            : base(message)
        {
            _message = message;
        }
        public ServiceRuleException(string messageDis, string messageIc)
            : base(messageIc)
        {
            _message = messageIc;
        }
        
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
