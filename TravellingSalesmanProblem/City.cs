using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    public class City
    {
        public int x { get; set; }
        public int y { get; set; }

        public City ()
        {
            Random rnd = new Random();
            this.x = rnd.Next(0, 201);
            this.y = rnd.Next(0, 201);
        }

        public City(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public double DistanceTo(City city)
        {
            int xDistance = Math.Abs(this.x - city.x);
            int yDistance = Math.Abs(this.y - city.y);
            return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
        }

        public override string ToString()
        {
            return this.x + ", " + this.y;
        }
    }
}
