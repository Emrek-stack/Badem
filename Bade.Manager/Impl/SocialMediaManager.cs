#region

using Bade.Data.Contract;
using Bade.Entity.Domain;
using Bade.Manager.Interface;

#endregion

namespace Bade.Manager.Impl
{
    public class SocialMediaManager : Manager, ISocialMediaService
    {
        private readonly ISocialMediaRepository _socialMediaRepository;

        public SocialMediaManager(ISocialMediaRepository socialMediaRepository) 
        {
            _socialMediaRepository = socialMediaRepository;
        }
    }
}