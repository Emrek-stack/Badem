#region

using System.Collections.Generic;
using Bade.Infrastructure;

#endregion

namespace Bade.Entity.Domain
{
    public class Application  : IAggregateRoot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //Reference
        public IEnumerable<ApplicationConfig> ApplicationSetting { get; set; }
    }
}