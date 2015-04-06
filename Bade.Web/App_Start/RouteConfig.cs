using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Bade.Entity.Domain;
using Route = Bade.Entity.Domain.Route;


namespace Bade.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //IRepository<Data.EF.Route> routeRepository = new Repository<Data.EF.Route>(new EmlakMilliyetEntities());
            IList<Route> routeEntityList = new List<Route>(); //routeRepository.GetAll().Where(w => w.Enabled == true).ToList();
            routeEntityList = routeEntityList.OrderBy(o => o.TableOrder).ToList();
            IList<Route> routeList = routeEntityList.Select(item => new Route
            {
                Id = item.Id, RouteName = item.RouteName, RouteDataToken = item.RouteDataToken.Select(s => new RouteDataToken
                {
                    RouteDataTokenId = s.RouteDataTokenId, Name = s.Name, Value = s.Value
                }).ToList(),
                RouteDefault = item.RouteDefault.Select(s => new RouteDefault
                {
                    RouteDefaultId = s.RouteDefaultId, Name = s.Name, Value = s.Value
                }).ToList(),
                RouteConstraint = item.RouteConstraint.Select(s => new RouteConstraint
                {
                    RouteConstraintId = s.RouteConstraintId, Name = s.Name, Value = s.Value
                }).ToList(),
                CheckPhysicalUrlAccess = item.CheckPhysicalUrlAccess, PhysicalFile = item.PhysicalFile, RouteType = new RouteType
                {
                    RouteTypeId = item.RouteType.RouteTypeId, Name = item.RouteType.Name, ImplementationType = item.RouteType.ImplementationType
                },
                RouteUrl = item.RouteUrl, TableOrder = item.TableOrder
            }).ToList();


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
