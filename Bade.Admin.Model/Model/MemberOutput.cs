#region

using System;
using Bade.Lib.Model;

#endregion

namespace Bade.Admin.Model.Model
{
    public class MemberResponse : ModelBase
    {
        public int MemberId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate { get; set; }
    }
}