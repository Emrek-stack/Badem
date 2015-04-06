using System;

namespace Bade.Entity.Domain
{
    public class LabelDetail
    {
        public int Id { get; set; }

        public int LabelId { get; set; }

        public string Content { get; set; }

        public int MemberId { get; set; }

        public DateTime CreateDate { get; set; }

        //Reference
        public Member Member { get; set; }

    }
}