using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogisticsProject.Domain.Entities
{
    public class Route
    {
        public int routeId { get; set; }
        public City cityFrom { get; set; }
        public City cityTo { get; set; }
        public Double distance { get; set; }
        public Double duration { get; set; }
    }
}