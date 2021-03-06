﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class TourManager
    {
        private static List<City> destinationCities = new List<City>();
        private static double maxFuelConsumption;
        private static double truckLoad;

        public static double TruckLoad { get => truckLoad; set => truckLoad = value; }

        public static void AddCity(City city)
        {
            destinationCities.Add(city);
        }

        public static City GetCity(int index)
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
