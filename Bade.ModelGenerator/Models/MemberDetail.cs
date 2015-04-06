using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class MemberDetail
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string About { get; set; }
        public string Motto { get; set; }
        public byte RegistrationSource { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public virtual Member Member { get; set; }
    }
}
