using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;

namespace Bade.Common.Logging.EntLib
{
    public class EntLibXmlFormatter : LogFormatter
    {
        private NameValueCollection _attributes = null;

        public EntLibXmlFormatter(NameValueCollection attributes)
        {
            _attributes = attributes;

        }

        public override string Format(LogEntry log)
        {
            const string prefix = "log"; //this.Attributes["prefix"];
            const string ns = "GarantiFilo"; //this.Attributes["namespace"];

            using (StringWriter s = new StringWriter())
            {
                XmlTextWriter w = new XmlTextWriter(s) {Formatting = Formatting.Indented, Indentation = 2};
                w.WriteStartDocument(true);
                w.WriteStartElement(prefix, "logEntry", ns);
                w.WriteAttributeString("Priority", ns, log.Priority.ToString(CultureInfo.InvariantCulture));
                w.WriteElementString("Timestamp", ns, log.TimeStampString);
                w.WriteElementString("Message", ns, log.Message);
                w.WriteElementString("EventId", ns, log.EventId.ToString(CultureInfo.InvariantCulture));
                w.WriteElementString("Severity", ns, log.Severity.ToString());
                w.WriteElementString("Title", ns, log.Title);
                w.WriteElementString("Machine", ns, log.MachineName);
                w.WriteElementString("AppDomain", ns, log.AppDomainName);
                w.WriteElementString("ProcessId", ns, log.ProcessId);
                w.WriteElementString("ProcessName", ns, log.ProcessName);
                w.WriteElementString("Win32ThreadId", ns, log.Win32ThreadId);
                w.WriteElementString("ThreadName", ns, log.ManagedThreadName);

                w.WriteStartElement("ext", "ExtendedProperties");
                foreach (var item in log.ExtendedProperties)
                {
                    w.WriteElementString(item.Key, ns, item.Value.ToString());
                }
                w.WriteEndElement();

                w.WriteEndElement();
                w.WriteEndDocument();

                return s.ToString();
            }
        }

    }
}