using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace Bade.Common.Logging.EntLib
{
    public class EntLibRepositoryTraceListener : CustomTraceListener
    {
#warning if we need it, we gotta use logging repositories here

        //private readonly LoggingRepository _loggingRepository;
        //public EntLibRepositoryTraceListener(LoggingRepository loggingRepository)
        //{
        //    _loggingRepository = loggingRepository;
        //}

        public override void TraceData(System.Diagnostics.TraceEventCache eventCache, string source, System.Diagnostics.TraceEventType eventType, int id, object data)
        {
            if (data is LogEntry && Formatter != null)
            {
                WriteLine(Formatter.Format(data as LogEntry));
            }
            else
            {
                WriteLine(data.ToString());
            }
        }

        public override void Write(string message)
        {
            //throw new NotImplementedException();
        }

        public override void WriteLine(string message)
        {
            //throw new NotImplementedException();
        }
    }
}