using Bade.Constants.Enum;

namespace Bade.Entity.Domain
{
    public class Wall
    {
        public int WallId { get; set; }

        public int MemberId { get; set; }

        public string Content { get; set; }

        public WallStatus Status { get; set; }

        //Reference
        public  Member Member { get; set; }
    }
}