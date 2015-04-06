using System;
using Bade.Entity.Domain;

namespace Bade.Entity.Domain
{
    public class Media
    {
        public int MediaId { get; set; }

        public int MemberId { get; set; }

        public string OriginalFilename { get; set; }
       
        public DateTime CreateDate { get; set; }

        //Reference
        public Member Member { get; set; }
    }
}