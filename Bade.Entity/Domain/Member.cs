#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Bade.Constants.Enum;

#endregion

namespace Bade.Entity.Domain
{
    [Table("Membership.Member")]
    public class Member
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Firtname { get; set; }

        public string Surname { get; set; }

        public DateTime? BirthDate { get; set; }

        public Gender Gender { get; set; }

        public MemberStatus Status { get; set; }

        public DateTime CreateDate { get; set; }


        //Reference
        public IEnumerable<MemberDetail> MemberDetail { get; set; }

        public IEnumerable<Content> Content { get; set; }

        public IEnumerable<SocialMedia> SocialMedia { get; set; }
    }
}