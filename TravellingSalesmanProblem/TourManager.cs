using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class TourManager
    {
        private static List<AlgCity> destinationCities = new List<AlgCity>();
        private static double maxFuelConsumption;
        private static double truckLoad;

        public static double TruckLoad { get => truckLoad; set => truckLoad = value; }

        public static void AddCity(AlgCity city)
        {
            destinationCities.Add(city);
        }

        public static AlgCity GetCity(int index)
        {
            return destinationCities[index];
        }

        public static int NumberOfCities()
        {
            return destinationCities.Count;
        }

        public static void SetMaxFuelConsump(double fuel)
        {
            maxFuelConsumption = fuel;
        }

        public static double GetMaxFuelConsump()
        {
            return maxFuelConsumption;
        }
    }
}
