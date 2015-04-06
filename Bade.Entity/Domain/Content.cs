#region

using System;
using Bade.Constants.Enum;
using Bade.Infrastructure;
using Bade.Entity.Domain;
using ContentType = System.Net.Mime.ContentType;

#endregion

namespace Bade.Entity.Domain
{
    public class Content
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ItemContent { get; set; }

        public ContentType ContentType { get; set; }

        public ContentStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        //Reference
        public  Member Member { get; set; }
    }
}