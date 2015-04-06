using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class Member
    {
        public Member()
        {
            this.MemberDetails = new List<MemberDetail>();
        }

        public int Id { get; set; }
        public string UniqueName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public byte Gender { get; set; }
        public byte Status { get; set; }
        public virtual ICollection<MemberDetail> MemberDetails { get; set; }
    }
}
