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
                citiesFrom = new SelectList(citiesFrom, "CityName", "CityName").AsQueryable(),
                citiesTo = new SelectList(citiesTo, "CityName", "CityName").AsQueryable(),
            };
            return View(formModel);
        }

        [HttpPost]
        public string AppForm(Orders orders)
        {
            List<AlgCity> cities = new List<AlgCity>();
            using (var db = new ApplicationDbContext())
            {
                int cityInId = 3;
                int cityOutId = 4;
                //int cityInId = int.Parse(collection["citiesTo"]);
                //int cityOutId = int.Parse(collection["citiesFrom"]);

                for(int i = 0; i < orders.OrdersList.Count(); i++)
                {
                    if(i == 0)
                    {
                        string cityOutName = orders.OrdersList.First().Odjazd;
                        int masaOdjazd = 5;
                        cities.Add(new AlgCity((from c in db.Cities
                                   where c.CityName == cityOutName
                                   select c).First(), masaOdjazd));
                    }
                    string cityInName = orders.OrdersList[i].Przyjazd;
                    double masaPrzyjazd = orders.OrdersList[i].Masa;
                    cities.Add(new AlgCity((from c in db.Cities
                                where c.CityName == cityInName
                                select c).First(),masaPrzyjazd));
                }
                
            }

            foreach(var city in cities)
            {
                TourManager.AddCity(city);
            }

            TourManager.SetMaxFuelConsump(20);
            TourManager.TruckLoad = 35;


            Population population = new Population(80, true);
            Debug.WriteLine("Initial total distance : " + population.GetFittest().GetTotalDistance());

            population = GeneticAlgorithm.EvolvePopulation(population);
            for (int i = 0; i < 200; i++)
            {
                population = GeneticAlgorithm.EvolvePopulation(population);
            }

           
            Debug.WriteLine("Done");
            Debug.WriteLine("Computed total distance : " + population.GetFittest().GetTotalDistance());
            Debug.Write("Solution : \n" + population.GetFittest());

            List<SelectListItem> citiesFrom = new List<SelectListItem>();
            List<SelectListItem> citiesTo = new List<SelectListItem>();
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

            FormModel formModel = new FormModel
            {
                bestTour = population.GetFittest(),
                citiesFrom = new SelectList(citiesFrom, "CityId", "CityName").AsQueryable(),
                citiesTo = new SelectList(citiesTo, "CityId", "CityName").AsQueryable()
            };

            TourManager.ResetDestination();

            return population.GetFittest().ToString();
        }
    }
}