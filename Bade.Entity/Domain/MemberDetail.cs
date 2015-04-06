#region

using System;
using Bade.Constants.Enum;
using Bade.Infrastructure;
using Bade.Entity.Domain;

#endregion

namespace Bade.Entity.Domain
{
    public class MemberDetail 
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string About { get; set; }

        public string Motto { get; set; }

        public MemberRegistrationSource RegistrationSource { get; set; }

        public string SocialMediaId { get; set; }

        public DateTime? LastLogin { get; set; }

        //Refence
        public  Member Member { get; set; }
    }
}