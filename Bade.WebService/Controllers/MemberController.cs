#region

using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bade.Admin.Model.Model;
using Bade.Manager.Interface;
using WebApi.OutputCache.V2;

#endregion

namespace Bade.WebService.Controllers
{
    public class MemberController : BaseApiController
    {
        private readonly IMemberManager _memberManager;
        private readonly IApplicationManager _applicationManager;

        public MemberController(IMemberManager memberService, IApplicationManager applicationManager)
        {
            _memberManager = memberService;
            _applicationManager = applicationManager;
        }

        [HttpPost]
        public HttpResponseMessage Post(MemberRequest memberRequest)
        {
            MemberResponse member = _memberManager.Add(memberRequest);
            var response = Request.CreateResponse(HttpStatusCode.Created, new {Member = member});
            return response;
        }

        [HttpGet]
        public HttpResponseMessage IsEmailAvaliable(string email)
        {
            bool result = _memberManager.IsEmailAvaliable(email);
            return Request.CreateResponse(HttpStatusCode.Created, new { IsEmailAvaliable = result });
        }

        [HttpGet]
        public HttpResponseMessage IsUsernameAvaliable(string username)
        {
            bool result = _memberManager.IsUsernameAvaliable(username);
            return Request.CreateResponse(HttpStatusCode.Created, new { IsUsernameAvaliable = result });
        }

        [HttpPut]
        public HttpResponseMessage Put(MemberRequest memberRequest)
        {
            MemberResponse member = _memberManager.Update(memberRequest);
            var response = Request.CreateResponse(HttpStatusCode.Created, new {Member = member});
            return response;
        }

        [CacheOutput(ServerTimeSpan = 30*60)]
        [HttpGet]
        public HttpResponseMessage List()
        {
            var aa = _applicationManager.GetKey(1, "a");
            var response = Request.CreateResponse(HttpStatusCode.Created, new {Members = "test"});
            return response;
        }
    }
}