using System.Web.Mvc;
using Bade.Admin.Model.Model.Output;
using Bade.Lib.Configuration;
using Bade.Manager.Interface;

namespace Bade.Admin.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IConfigReader _configReader;
        private readonly IApplicationManager _applicationManager;

        public SettingsController(IConfigReader configReader, IApplicationManager applicationManager)
        {
            _configReader = configReader;
            _applicationManager = applicationManager;
        }

        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetApplicationConfig()
        {
            ApplicationConfigOutput applicationConfigOutput = _applicationManager.ApplicationConfigListById(_configReader.ApplicationId);
            return View(applicationConfigOutput);
        }
    }
}