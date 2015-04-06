using System;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;

namespace Generator.CodeGenerators.InfrastructureGenerator
{
    public class WebAccess
    {
        private StreamReader GetSource(string WebAddress)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(WebAddress);
            httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0b; Windows NT 5.1)";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 1000000;
            httpWebRequest.Proxy = (IWebProxy)WebProxy.GetDefaultProxy();
            httpWebRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
            return new StreamReader(httpWebRequest.GetResponse().GetResponseStream());
        }

        public string GetTextSource(string WebAddress)
        {
            try
            {
                return this.GetSource(WebAddress).ReadToEnd();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public DataSet GetXmlSource(string WebAddress)
        {
            try
            {
                DataSet dataSet = new DataSet();
                XmlTextReader xmlTextReader = new XmlTextReader((TextReader)this.GetSource(WebAddress));
                int num = (int)dataSet.ReadXml((XmlReader)xmlTextReader);
                return dataSet;
            }
            catch (Exception ex)
            {
                return (DataSet)null;
            }
        }
    }
}
