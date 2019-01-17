using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravellingSalesmanProblem;

namespace LogisticsProject.Models
{
    public class FormModel
    {
        public IQueryable<SelectListItem> citiesFrom { get; set; }
        public IQueryable<SelectListItem> citiesTo { get; set; }
        public List<City> cities { get; set; }
        public Tour bestTour { get; set; }
    }
}