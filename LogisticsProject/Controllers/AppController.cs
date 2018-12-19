using LogisticsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogisticsProject.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult AppForm()
        {
            List<SelectListItem> citiesFrom = new List<SelectListItem>();
            List<SelectListItem> citiesTo = new List<SelectListItem>();
            FormModel formModel;
            List<City> citiesDb;
            // Create and add our cities from database 
            using (var db = new ApplicationDbContext())
            {
                citiesDb = (from c in db.Cities
                    select c).ToList();
                foreach (City city in citiesDb)
                {
                    citiesFrom.Add(city);
                    citiesTo.Add(city);
                }


            }
            //citiesFrom = citiesDb;
            //citiesTo = citiesDb;
            formModel = new FormModel
            {
                citiesFrom = new SelectList(citiesFrom, "CityName", "CityName").AsQueryable(),
                citiesTo = new SelectList(citiesTo, "CityName", "CityName").AsQueryable(),
                cities = new List<City> { citiesDb.First() }
            };
            return View(formModel);
        }

        [HttpPost]
        public ActionResult AppForm(FormCollection collection)
        {
            return RedirectToAction("AppForm");
        }
    }
}