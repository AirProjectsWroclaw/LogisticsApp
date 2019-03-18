using HttpClientLib;
using LogisticsProject.Domain;
using LogisticsProject.Domain.Entities;
using LogisticsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LogisticsProject.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            List<City> cities = null;

            using(var db = new EFDbContext())
            {
                var query = from city in db.Cities
                            orderby city.CityName
                            select city;
                cities = query.ToList();
            }

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://maps.googleapis.com/");
            //HttpResponseMessage response = client.GetAsync(path);

            return View(cities);
        }

        // GET: City/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: City/Create
        public ActionResult Create()
        {
            using(var db = new EFDbContext())
            {
                var city = new City { CityName = "Gostyń", Latitude = 51.8786, Longitude = 17.01215};
                db.Cities.Add(city);
                db.SaveChanges();
            }
            return Index();
        }

        // POST: City/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: City/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: City/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
