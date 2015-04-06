namespace Bade.Entity.Domain
{
    public class RouteDataToken
    {
        public int RouteDataTokenId { get; set; }

        public int RouteId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        //Reference
        public  Route Route { get; set; }
    }
}