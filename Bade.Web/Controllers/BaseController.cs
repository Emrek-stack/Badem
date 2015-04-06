using System.Net.Http;
using System.Web.Mvc;

namespace Bade.Web.Controllers
{
    public abstract class BaseController : Controller
    {


        protected ActionResult ErrorView(HttpResponseMessage response)
        {
            ModelState.AddModelError("Model", response.ReasonPhrase);
            return View("Error");
        }
    }
}