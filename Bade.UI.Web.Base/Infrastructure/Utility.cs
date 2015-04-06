using System;
using System.Web;

namespace Bade.UI.Web.Base.Infrastructure
{
    public static class Utility
    {
        #region URL Processing

        public static string IsLocalUrl(string returnUrl, HttpContextBase context, string defaultUrl)
        {
            return IsLocalUrl(returnUrl, context) ? returnUrl : defaultUrl;
        }

        public static bool IsLocalUrl(string returnUrl, HttpContextBase context)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
                return false;
            Uri absoluteUri;

            if (!Uri.TryCreate(returnUrl, UriKind.RelativeOrAbsolute, out absoluteUri))
                return false;

            var url = context.Request.Url;
            return !absoluteUri.IsAbsoluteUri || string.Equals(url.Host, absoluteUri.Host, StringComparison.OrdinalIgnoreCase);
        }

        public static string Redir(string redir, string defaultRedir = "/")
        {
            if (string.IsNullOrWhiteSpace(redir))
            {
                redir = defaultRedir;
            }
            return redir;
        }
        #endregion
    }
}