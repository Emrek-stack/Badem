using System.Web.Mvc;
using Bade.Manager.Interface;

namespace Bade.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IApplicationManager _applicationManager;

        public HomeController(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        public ActionResult Index()
        {
            //var aa = _applicationConfigService.GetKey(1, "a");
            return View();
        }
    }
}