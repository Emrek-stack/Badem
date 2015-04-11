using System.Web.Mvc;

namespace Bade.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["test"] = "euheuehueh";
            return View();
        }
    }
}