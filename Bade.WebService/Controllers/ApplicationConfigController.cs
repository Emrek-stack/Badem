using System.Web.Http;
using Bade.Admin.Model.Model.Output;
using Bade.Manager.Interface;

namespace Bade.WebService.Controllers
{
    public class ApplicationConfigController : BaseApiController
    {
        private readonly IApplicationManager _applicationManager;

        public ApplicationConfigController(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        [HttpGet]
        public IHttpActionResult GetListByApplicationId(int id)
        {
            ApplicationConfigOutput applicationConfig = _applicationManager.ApplicationConfigListById(id);
            return Ok(applicationConfig);
        }
    }
}   