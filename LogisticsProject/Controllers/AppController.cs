using LogisticsProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravellingSalesmanProblem;

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
                citiesFrom = new SelectList(citiesFrom, "CityId", "CityName").AsQueryable(),
                citiesTo = new SelectList(citiesTo, "CityId", "CityName").AsQueryable(),
            };
            return View(formModel);
        }

        [HttpPost]
        public ActionResult AppForm(FormCollection collection)
        {
            City cityIn, cityOut, cityThird, cityFourth;
            using (var db = new ApplicationDbContext())
            {
                int cityInId = int.Parse(collection["citiesTo"]);
                int cityOutId = int.Parse(collection["citiesFrom"]);

                cityIn = (from c in db.Cities
                          where c.CityId == cityInId
                            select c).First();
                cityOut = (from c in db.Cities
                           where c.CityId == cityOutId
                           select c).First();
                cityThird = (from c in db.Cities
                             select c).ToList()[20];
                cityFourth = (from c in db.Cities
                             select c).ToList()[21];
            }
            TourManager.AddCity(new AlgCity(cityOut, 5));
            TourManager.AddCity(new AlgCity(cityThird, 5));
            TourManager.AddCity(new AlgCity(cityIn, 20));
            TourManager.AddCity(new AlgCity(cityFourth, 5));


            TourManager.SetMaxFuelConsump(20);
            TourManager.TruckLoad = 35;


            Population population = new Population(20, true);
            Debug.WriteLine("Initial distance : " + population.GetFittest().GetDistance());

            population = GeneticAlgorithm.EvolvePopulation(population);
            for (int i = 0; i < 60; i++)
            {
                population = GeneticAlgorithm.EvolvePopulation(population);
            }

           
            Debug.WriteLine("Done");
            Debug.WriteLine("Computed distance : " + population.GetFittest().GetDistance());
            Debug.Write("Solution : \n" + population.GetFittest());

            return RedirectToAction("AppForm");
        }
    }
}