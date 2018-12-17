using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    public class Tour
    {
        // Holds tour of cities
        private List<City> tour = new List<City>();
        //Cache
        private double fitness = 0;
        private int distance = 0;
        private double busLoad = 400;
        private double tourWeight = 0;
        private double maxFuel = 30;

        //Constructor for blank tour
        public Tour()
        {
            for(int i = 0; i < TourManager.NumberOfCities(); i++)
            {
                tour.Add(null);
            }
        }

        public Tour(List<City> tour)
        {
            this.tour = tour;
        }

        // Creates random tour
        public void GenerateIndividual()
        {
            for(int cityIndex = 0; cityIndex < TourManager.NumberOfCities(); cityIndex++)
            {
                SetCity(cityIndex, TourManager.GetCity(cityIndex));
            }
        }

        public City GetCity(int cityIndex)
        {
            return tour[cityIndex];
        }

        public void SetCity(int cityIndex, City city)
        {
            tour[cityIndex] = city;
            fitness = 0;
            distance = 0;
        }

        public double GetFitness()
        {
            if(fitness == 0)
            {
                fitness = 1 / (double)GetDistance();
            }
            return fitness;
        }

        public double GetWeight(int cityIndex)
        {
            return tour[cityIndex].Weight;
        }

        public int GetDistance()
        {
            if(distance == 0)
            {
                double tourDistance = 0;

                for(int cityIndex = 0; cityIndex < TourSize(); cityIndex++)
                {
                    City fromCity = GetCity(cityIndex);

                    City destinationCity;

                    if(cityIndex+1 < TourSize())
                    {
                        destinationCity = GetCity(cityIndex + 1);
                    }
                    else
                    {
                        destinationCity = GetCity(0);
                    }

                    int actDist = (int)fromCity.DistanceTo(destinationCity);
                    double prevWeight = GetAllPreviousWeight(cityIndex);


                    tourDistance += (actDist/100) * GetMaxFuel() * (GetTourWeight()-prevWeight)/GetBusLoad() * (1/ GetWeight(cityIndex));
                }
                distance = (int)tourDistance;
            }
            return distance;
        }

        public int TourSize()
        {
            return tour.Count;
        }

        //Check if tour constains a city
        public Boolean ContainsCity(City city)
        {
            return tour.Contains(city);
        }

        //Get completed transports weight
        public double GetAllPreviousWeight(int cityIndex)
        {
            double weightSum = 0;
            for(int i = 0; i < cityIndex; i++)
            {
                weightSum += GetWeight(i);
            }
            return weightSum;
        }

        public double GetBusLoad()
        {
            return this.busLoad;
        }

        public void SetBudLoad(double load)
        {
            this.busLoad = load;
        }

        public double GetTourWeight()
        {
            double weightSum = 0;
            for (int i = 0; i < TourSize(); i++)
            {
                weightSum += GetWeight(i);
            }
            return weightSum;
        }

        public void SetTourWeight(double weight)
        {
            this.tourWeight = weight;
        }

        public double GetMaxFuel()
        {
            return this.maxFuel;
        }

        public void SetMaxFuel(double fuel)
        {
            this.maxFuel = fuel;
        }

        public override string ToString()
        {
            string GeneString = "|";
            for(int i = 0; i < TourSize(); i++)
            {
                GeneString += GetCity(i) + "|";
            }
            return GeneString;
        }
    }
}
