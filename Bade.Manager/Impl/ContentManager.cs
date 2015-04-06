#region

using Bade.Data.Contract;
using Bade.Entity.Domain;
using Bade.Manager.Interface;

#endregion

namespace Bade.Manager.Impl
{
    public class ContentManager : Manager, IContentManager
    {
        private readonly IContentRepository _contentRepository;

        public ContentManager(IContentRepository contentRepository)
            
        {
            _contentRepository = contentRepository;
        }
    }
}