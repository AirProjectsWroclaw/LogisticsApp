using LogisticsProject.Domain;
using LogisticsProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravellingSalesmanProblem;

namespace LogisticsProject.Models
{
    public class FormViewModel
    {
        [Required]
        public IQueryable<SelectListItem> CitiesFrom { get; set; }

        [Required]
        public IQueryable<SelectListItem> CitiesTo { get; set; }

        [Required]
        public List<City> Cities { get; set; }


        public Tour BestTour { get; set; }
    }
}