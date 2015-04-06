using System;
using System.Linq;
using RestSharp;

namespace Bade.Constants.Helper
{
    public class BadeApi : IBadeClient
    {
        private readonly string _baseUrl;

        readonly string _accountSid;
        readonly string _secretKey;

        public BadeApi()
        {
            _accountSid = string.Empty;
            _secretKey = string.Empty;
            _baseUrl = "http://localhost:21991";
        }

        public string RequestUrl { get; set; }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient {BaseUrl = new Uri(_baseUrl), Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey)};
            request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request
            var response = client.Execute<T>(request);

            if (response.ErrorException == null) return response.Data;
            const string message = "Error retrieving response.  Check inner details for more info.";
            var badeException = new ApplicationException(message, response.ErrorException);
            //ElmahWrapper wrapper = new ElmahWrapper();
            //wrapper.Log(badeException);
            throw badeException;
        }

        public T Execute<T>(T obj) where T : new()
        {
            var client = new RestClient { BaseUrl = new Uri(_baseUrl), Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey) };
            var request = new RestRequest(Method.POST) {Resource = RequestUrl, RootElement = "Calls"};

            
            foreach (var prop in obj.GetType().GetProperties())
            {
                object value =prop.GetValue(obj, null);
                if (value != null)
                {
                    request.AddParameter(prop.Name, prop.GetValue(obj, null));    
                }                
            }
            var response = client.Execute<T>(request);

            if (response.ErrorException == null) return response.Data;
            const string message = "Error retrieving response.  Check inner details for more info.";
            var badeException = new ApplicationException(message, response.ErrorException);
            //ElmahWrapper wrapper = new ElmahWrapper();
            //wrapper.Log(badeException);
            throw badeException;
        }

        public TR Execute<T, TR>(T obj) where TR : new()
        {
            var client = new RestClient { BaseUrl = new Uri(_baseUrl), Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey) };
            var request = new RestRequest(Method.POST) { Resource = RequestUrl, RootElement = "Calls" };

            var t = typeof(T);
            foreach (var prop in t.GetProperties().Where(prop => prop.GetValue(prop, null) != null))
            {
                request.AddParameter(prop.Name, prop.GetValue(prop, null));
            }
            var response = client.Execute<TR>(request);

            if (response.ErrorException == null) return response.Data;
            const string message = "Error retrieving response.  Check inner details for more info.";
            var badeException = new ApplicationException(message, response.ErrorException);
            //ElmahWrapper wrapper = new ElmahWrapper();
            //wrapper.Log(badeException);
            throw badeException;
        }
    }

    public interface IBadeClient
    {
        string RequestUrl { get; set; }
        T Execute<T>(RestRequest request) where T : new();
        T Execute<T>(T obj) where T : new();
        TR Execute<T, TR>(T obj) where TR : new();
    }
}