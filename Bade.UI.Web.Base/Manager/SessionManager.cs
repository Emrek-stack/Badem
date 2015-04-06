using System.Web;

namespace Bade.UI.Web.Base.Manager
{
    public class SessionManager
    {
        public static object Get(string sessionName)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
                return null;
            return HttpContext.Current.Session[sessionName];
        }

        public static T Get<T>(string sessionName)
        {
            var obj = Get(sessionName);
            if (obj != null)
                return (T)obj;

            return default(T);
        }


        //public static T Get<T>(string sessionName) where T : class
        //{
        //    var obj = Get(sessionName);
        //    return (T)obj;
        //}

        public static T? Get<T>(string sessionName, bool isValue) where T : struct
        {
            var obj = Get(sessionName);
            if (obj is T?)
                return (T?)obj;
            if (obj != null)
                return (T?)obj;
            return new T?();
        }

        public static bool Check(string sessionName)
        {
            return Get(sessionName) != null;
        }

        public static void Set(string sessionName, object obj)
        {
            HttpContext.Current.Session[sessionName] = obj;
        }

        public static void Remove(string sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName);
        }
    }
}