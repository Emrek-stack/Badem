#region

using System.Collections.Generic;

#endregion

namespace Bade.Entity.Domain
{
    public class Route 
    {
        public int Id { get; set; }

        public string RouteName { get; set; }

        public string RouteUrl { get; set; }

        public string PhysicalFile { get; set; }

        public bool? CheckPhysicalUrlAccess { get; set; }

        public int TableOrder { get; set; }

        public int RouteTypeId { get; set; }

        public bool Enabled { get; set; }

        //Reference
        public IEnumerable<RouteConstraint> RouteConstraint { get; set; }

        public IEnumerable<RouteDataToken> RouteDataToken { get; set; }

        public IEnumerable<RouteDefault> RouteDefault { get; set; }

        public RouteType RouteType { get; set; }
    }
}