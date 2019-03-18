using LogisticsProject.Domain;
using LogisticsProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class Application
    {
        public static void Main(string[] args)
        {
            List<City> citiesDb;
            // Create and add our cities from database 
            using (var db = new EFDbContext())
            {
                citiesDb = (from c in db.Cities
                          select c).ToList();
            }
            List<AlgCity> citiesSalesman = new List<AlgCity>(citiesDb.Count());

            //for (int i = 6; i < 12; i++)
            //{
            //    citiesSalesman.Add(new City(citiesDb[i], i*10 + 10));
            //    TourManager.AddCity(citiesSalesman[i-6]);
            //}

            TourManager.AddCity(new AlgCity(citiesDb[0], 5));
            TourManager.AddCity(new AlgCity(citiesDb[3], 5));
            TourManager.AddCity(new AlgCity(citiesDb[7], 5));
            TourManager.AddCity(new AlgCity(citiesDb[2], 6));
            TourManager.AddCity(new AlgCity(citiesDb[9], 50));
            TourManager.SetMaxFuelConsump(20);
            TourManager.TruckLoad = 80;


            Population population = new Population(50, true);
            Console.WriteLine("Initial distance : " + population.GetFittest().GetDistance());

            population = GeneticAlgorithm.EvolvePopulation(population);
            for(int i = 0; i<120; i++)
            {
                population = GeneticAlgorithm.EvolvePopulation(population);
            }

            Console.WriteLine("Done");
            Console.WriteLine("Computed distance : " + population.GetFittest().GetDistance());
            Console.Write("Solution : \n" + population.GetFittest());
            Console.Read();
        }
    }
}
