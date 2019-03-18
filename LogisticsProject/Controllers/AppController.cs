using LogisticsProject.Domain;
using LogisticsProject.Domain.Abstract;
using LogisticsProject.Domain.Entities;
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
        private ICityRepository cityRepository;
        private IRouteRepository routeRepository;

        public AppController(ICityRepository cityRepository, IRouteRepository routeRepository)
        {
            this.cityRepository = cityRepository;
            this.routeRepository = routeRepository;
        }

        public PartialViewResult CreatePartialView()
        {
            return PartialView("MyPartialView");
        }

        public ActionResult AddClient()
        {
            List<SelectListItem> citiesFrom = new List<SelectListItem>();
            List<City> citiesDb;
            FormViewModel formModel;

        
            // add cities from db
          
            citiesDb = cityRepository.Cities.ToList();
            foreach (City city in citiesDb)
            {
                citiesFrom.Add(city);

            }

            formModel = new FormViewModel
            {
                CitiesFrom = new SelectList(citiesFrom, "CityName", "CityName").AsQueryable()
            }; 
                return View(formModel);
        }
        

        //            return View("AddClient");

        // GET: App
        public ActionResult AppForm()
        {
            List<SelectListItem> citiesFrom = new List<SelectListItem>();
            List<SelectListItem> citiesTo = new List<SelectListItem>();
            FormViewModel formModel;
            List<City> citiesDb;
            // Create and add our cities from database 
      
                 citiesDb = cityRepository.Cities.ToList();
                foreach (City city in citiesDb)
                {
                    citiesFrom.Add(city);
                    citiesTo.Add(city);
                }
                
                
            
            //citiesFrom = citiesDb;
            //citiesTo = citiesDb;
            formModel = new FormViewModel
            {
                CitiesFrom = new SelectList(citiesFrom, "CityName", "CityName").AsQueryable(),
                CitiesTo = new SelectList(citiesTo, "CityName", "CityName").AsQueryable(),
            };
            return View(formModel);
        }

        [HttpPost]
        public ActionResult AppForm(List<OrderViewModel> orders)
        {
            if(orders == null)
            {
                return RedirectToAction("AppForm");
            }
            List<AlgCity> cities = new List<AlgCity>();
        
                int cityInId = 3;
                int cityOutId = 4;
                //int cityInId = int.Parse(collection["citiesTo"]);
                //int cityOutId = int.Parse(collection["citiesFrom"]);

                for(int i = 0; i < orders.Count(); i++)
                {
                    if(i == 0)
                    {
                        string cityOutName = orders.First().Odjazd;
                        int masaOdjazd = 5;
                        cities.Add(new AlgCity((from c in cityRepository.Cities
                                   where c.CityName == cityOutName
                                   select c).First(), masaOdjazd));
                    }
                    string cityInName = orders[i].Przyjazd;
                    double masaPrzyjazd = orders[i].Masa;
                    cities.Add(new AlgCity((from c in cityRepository.Cities
                                where c.CityName == cityInName
                                select c).First(),masaPrzyjazd));
                }
                
            

            foreach(var city in cities)
            {
                TourManager.AddCity(city);
            }

            TourManager.SetMaxFuelConsump(20);
            TourManager.TruckLoad = 20;


            Population population = new Population(200, true);
            Debug.WriteLine("Initial total distance : " + population.GetFittest().GetTotalDistance());

            population = GeneticAlgorithm.EvolvePopulation(population);
            for (int i = 0; i < 1000; i++)
            {
                population = GeneticAlgorithm.EvolvePopulation(population);
                Debug.WriteLine(population.GetFittest().GetTotalDistance());
            }

           
            Debug.WriteLine("Done");
            Debug.WriteLine("Computed total distance : " + population.GetFittest().GetTotalDistance());
            Debug.Write("Solution : \n" + population.GetFittest());

            List<SelectListItem> citiesFrom = new List<SelectListItem>();
            List<SelectListItem> citiesTo = new List<SelectListItem>();
            List<City> citiesDb;

            // Create and add our cities from database 
       
                citiesDb = (from c in cityRepository.Cities
                            select c).ToList();
                foreach (City city in citiesDb)
                {
                    citiesFrom.Add(city);
                    citiesTo.Add(city);
                }

                

            FormViewModel formModel = new FormViewModel
            {
                BestTour = population.GetFittest(),
                CitiesFrom = new SelectList(citiesFrom, "CityId", "CityName").AsQueryable(),
                CitiesTo = new SelectList(citiesTo, "CityId", "CityName").AsQueryable()
            };

            TourManager.ResetDestination();

            return new JsonResult { Data = population.GetFittest().ToString() };
        }
    }
}