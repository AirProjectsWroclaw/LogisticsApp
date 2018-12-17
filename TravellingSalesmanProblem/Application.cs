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
            List<LogisticsProject.Models.City> citiesDb;
            // Create and add our cities from database 
            using (var db = new LogisticsProject.Models.ApplicationDbContext())
            {
                citiesDb = (from c in db.Cities
                          select c).ToList();
            }
            List<City> citiesSalesman = new List<City>(citiesDb.Count());

            for (int i = 6; i < 12; i++)
            {
                citiesSalesman.Add(new City(citiesDb[i], i*10 + 10));
                TourManager.AddCity(citiesSalesman[i-6]);
            }

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
