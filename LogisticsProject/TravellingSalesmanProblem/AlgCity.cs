using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    public class AlgCity
    {
        public int x { get; set; }
        public int y { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Weight { get; set; }
        public int Truck { get; set; }
        public AlgCity ()
        {
            Random rnd = new Random();
            this.x = rnd.Next(0, 201);
            this.y = rnd.Next(0, 201);
        }

        public AlgCity(LogisticsProject.Models.City city, double weight)
        {
            this.CityId = city.CityId;
            this.CityName = city.CityName;
            this.Latitude = city.Latitude;
            this.Longitude = city.Longitude;
            this.Weight = weight;
        }

        public AlgCity(AlgCity algCity)
        {
            this.CityId = algCity.CityId;
            this.CityName = algCity.CityName;
            this.Latitude = algCity.Latitude;
            this.Longitude = algCity.Longitude;
            this.Weight = algCity.Weight;
        }

        public double DistanceTo(AlgCity city)
        {
            //int xDistance = Math.Abs(this.x - city.x);
            //int yDistance = Math.Abs(this.y - city.y);
            //return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
            if (!TourManager.IsRoutesListSet())
            {
                using (LogisticsProject.Models.ApplicationDbContext db = new LogisticsProject.Models.ApplicationDbContext())
                {
                    List<LogisticsProject.Models.Route> routes = (from r in db.Routes.Include("cityFrom").Include("cityTo")
                                                                  select r).ToList();
                    TourManager.SetRoutesList(routes);
                    LogisticsProject.Models.Route route = (from r in db.Routes
                                                           where (r.cityFrom.CityId == this.CityId
                                                           && r.cityTo.CityId == city.CityId)
                                                           select r).First();
                    return route.distance;
                }
            }
            else
            {
                List<LogisticsProject.Models.Route> routes = TourManager.GetRoutesList();
                LogisticsProject.Models.Route route = (from r in routes
                                                       where (r.cityFrom.CityId == this.CityId
                                                       && r.cityTo.CityId == city.CityId)
                                                       select r).First();
                return route.distance;
            }
        
        }

        public override string ToString()
        {
            return this.CityName + " [" + this.Longitude + ", " + this.Latitude + "] ";
        }
    }
}
