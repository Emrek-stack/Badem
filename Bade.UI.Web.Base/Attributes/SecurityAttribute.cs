using System.Web;
using System.Web.Mvc;
using Bade.Constants.Structs;
using Bade.UI.Web.Base.Infrastructure;
using Bade.UI.Web.Base.Manager;

namespace Bade.UI.Web.Base.Attributes
{
    public class SecurityAttribute : AuthorizeAttribute
    {
        public string Key { get; private set; }
        public string ReturnUrl { get; private set; }

        public bool IsAuthorize { get; private set; }

        public SecurityAttribute()
        {
            Key = Keys.Security.CommonScreens;
        }
        public SecurityAttribute(string key, string returnUrl = "")
        {
            Key = key;
            ReturnUrl = returnUrl;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IsAuthorize = base.AuthorizeCore(httpContext);
            if (Current.IsAccountLogin)
            {
                IsAuthorize = Current.Account.HasPermissionFor(Key);
            }
            return IsAuthorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var cont = filterContext.HttpContext;
            var contextReturnUrl = "";
            if (cont.Request.Url != null)
            {
                contextReturnUrl = cont.Request.Url.ToString();
            }
            var retUrl = string.IsNullOrWhiteSpace(ReturnUrl) ? contextReturnUrl : ReturnUrl.Replace("~/", "/");
            SessionManager.Set(Keys.Session.AfterLoginReturnUrl,retUrl);
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult(Keys.PageUrl.AccountLogin);
        }
    }
}