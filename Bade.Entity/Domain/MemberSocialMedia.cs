namespace Bade.Entity.Domain
{
    public class MemberSocialMedia
    {

        public int MemberId { get; set; }

        public int SocialMediaId { get; set; }

        //Reference
        public Member Member { get; set; }

        public SocialMedia SocialMedia { get; set; }
    }
}