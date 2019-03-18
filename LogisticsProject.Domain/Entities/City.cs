using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LogisticsProject.Domain.Entities
{
    public class City : SelectListItem
    {
        [Required]
        public int CityId { get; set; }

        [Required]
        public string CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}