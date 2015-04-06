using System.Web.Mvc;

namespace Bade.UI.Web.Base.Attributes
{
    public class AjaxAndChildActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest() && !filterContext.IsChildAction)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}