    using System.Web.Mvc;
    using Bade.Constants.Helper;
    using Bade.Web.Controllers;

namespace Bade.Web.Areas.Member.Controllers
{
    public class MemberController : BaseController
    {
        private readonly IBadeClient _badeClient;
        public MemberController(IBadeClient badeClient)
        {
            _badeClient = badeClient;
        }

        public ActionResult Register()
        {
            


        //    _badeClient.RequestUrl = "Member/Post";
          //  _badeClient.Execute(new MemberRequest{MemberId = 1});
            return View();
        }

        
        //public ActionResult Register(MemberRequest memberRequest)
        //{
            
            

        //    _badeClient.RequestUrl = "POST";
        //    _badeClient.Execute(memberRequest);
            

        //    return View();
        //}
    }
}