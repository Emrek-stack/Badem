#region

using System.Collections.Generic;
using Bade.Infrastructure;

#endregion

namespace Bade.Entity.Domain
{
    public class SocialMedia
    {
        public int Id { get; set; }

        public string Description { get; set; }

        //reference
        public virtual IEnumerable<Member> Member { get; set; }
    }
}