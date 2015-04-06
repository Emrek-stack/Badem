#region

using System.Collections.Generic;

#endregion

namespace Bade.Entity.Domain
{
    public class RouteType
    {
        public int RouteTypeId { get; set; }

        public string Name { get; set; }

        public string ImplementationType { get; set; }

        //Reference
        public IEnumerable<Route> Route { get; set; }
    }
}