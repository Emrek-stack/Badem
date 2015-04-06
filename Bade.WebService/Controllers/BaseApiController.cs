using System.Web.Http;
using System.Web.Http.Cors;

namespace Bade.WebService.Controllers
{
     [EnableCors(origins: "http://localhost:1114", headers: "*", methods: "*")]
    public class BaseApiController : ApiController
    {
         
    }
}