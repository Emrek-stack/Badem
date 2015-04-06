using System.Web.Mvc;

namespace Bade.UI.Web.Base.Attributes
{
    public class AjaxAndHttpPostAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest() || filterContext.HttpContext.Request.HttpMethod != "POST")
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}