#region

using System;
using Bade.Admin.Model.Validation;
using Bade.Lib.Model;
using FluentValidation.Attributes;

#endregion

namespace Bade.Admin.Model.Model
{
    [Validator(typeof (MemberInputValidator))]
    public class MemberRequest : ModelBase
    {
        public MemberRequest()
        {
            CreateDate = DateTime.Now;
        }

        public int MemberId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public DateTime CreateDate { get; set; }
    }
}