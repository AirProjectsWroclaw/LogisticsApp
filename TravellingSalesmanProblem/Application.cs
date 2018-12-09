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
            // Create and add our cities
            City city = new City(60, 200);
            TourManager.AddCity(city);
            City city2 = new City(180, 200);
            TourManager.AddCity(city2);
            City city3 = new City(80, 180);
            TourManager.AddCity(city3);
            City city4 = new City(140, 180);
            TourManager.AddCity(city4);
            City city5 = new City(20, 160);
            TourManager.AddCity(city5);
            City city6 = new City(100, 160);
            TourManager.AddCity(city6);
            City city7 = new City(200, 160);
            TourManager.AddCity(city7);
            City city8 = new City(140, 140);
            TourManager.AddCity(city8);
            City city9 = new City(40, 120);
            TourManager.AddCity(city9);
            City city10 = new City(100, 120);
            TourManager.AddCity(city10);
            City city11 = new City(180, 100);
            TourManager.AddCity(city11);
            City city12 = new City(60, 80);
            TourManager.AddCity(city12);
            City city13 = new City(120, 80);
            TourManager.AddCity(city13);
            City city14 = new City(180, 60);
            TourManager.AddCity(city14);
            City city15 = new City(20, 40);
            TourManager.AddCity(city15);
            City city16 = new City(100, 40);
            TourManager.AddCity(city16);
            City city17 = new City(200, 40);
            TourManager.AddCity(city17);
            City city18 = new City(20, 20);
            TourManager.AddCity(city18);
            City city19 = new City(60, 20);
            TourManager.AddCity(city19);
            City city20 = new City(160, 20);
            TourManager.AddCity(city20);

            Population population = new Population(50, true);
            Console.WriteLine("Initial distance : " + population.GetFittest().GetDistance());

            population = GeneticAlgorithm.EvolvePopulation(population);
            for(int i = 0; i<100; i++)
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
