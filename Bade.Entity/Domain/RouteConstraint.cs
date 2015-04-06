using Bade.Entity.Domain;

namespace Bade.Entity.Domain
{
    public class RouteConstraint
    {
        public int RouteConstraintId { get; set; }

        public int RouteId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        //Reference
        public  Route Route { get; set; }
    }
}