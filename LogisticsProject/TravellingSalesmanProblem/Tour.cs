using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace TravellingSalesmanProblem
{
    public class Tour
    {
        // Holds tour of cities
        private List<AlgCity> tour = new List<AlgCity>();
        //Cache
        private double fitness = 0;
        private int distance = 0;
        private double truckLoad = 0;
        private double tourWeight = 0;
        private double maxFuel = 0;
        public double[] trucksLoad;

        //Constructor for blank tour
        public Tour()
        {
            for(int i = 0; i < TourManager.NumberOfCities(); i++)
            {
                tour.Add(null);
            }
            this.trucksLoad = new double[TourManager.trucksLoad.Count()];
            for(int index = 0; index < TourManager.trucksLoad.Count(); index++)
            {
                this.trucksLoad[index] = TourManager.trucksLoad[index];
            }
        }

        public Tour(List<AlgCity> tour)
        {
            this.tour = tour;
        }

        // Creates random tour
        public void GenerateIndividual(double fuel, Random random)
        {
            //bardziej deterministycznie - wpisywanie dla posortowanych
            /*List<AlgCity> listaMiast = new List<AlgCity>();
            for (int i = 0; i < TourManager.NumberOfCities(); i++)
            {
                listaMiast.Add(new AlgCity(TourManager.GetCity(i)));
            }
            for (int i = 0; i < listaMiast.Count; i++)
            {
                listaMiast[i].Weight
            }
            listaMiast[i].;*/

            //odwrotnie dla truckow
            /*for (int i = 1; i < 4; i++)
            {
                AlgCity algCity = new AlgCity(TourManager.GetCity(cityIndex));

            }*/


            for (int cityIndex = 0; cityIndex < TourManager.NumberOfCities(); cityIndex++)
            {
                Boolean choosed = false;
                AlgCity algCity = new AlgCity(TourManager.GetCity(cityIndex));
                //algCity.Truck = random.Next(1, 4);






                while (!choosed)
                {
                    
                    algCity.Truck = random.Next(1, 4);



                    if (this.trucksLoad[algCity.Truck] >= algCity.Weight)
                    {
                        this.trucksLoad[algCity.Truck] -= algCity.Weight;
                        SetCity(cityIndex, algCity);
                        choosed = true;
                    }
                }
            }
            SetMaxFuel(fuel);
        }

        public AlgCity GetCity(int cityIndex)
        {
            return tour[cityIndex];
        }

        public void SetCity(int cityIndex, AlgCity city)
        {
            tour[cityIndex] = city;
            fitness = 0;
            distance = 0;
        }

        public double GetFitness()
        {
            if(fitness == 0)
            {
                fitness = 1 / (double)GetTotalDistance();
            }
            return fitness;
        }

        public double GetWeight(int cityIndex)
        {
            return tour[cityIndex].Weight;
        }

        public int GetTruckDistance(int truckId)
        {
            double truckTourDistance = 0;
            AlgCity fromCity = null;
            AlgCity destinationCity = null;
            for (int cityIndex = 0; cityIndex < TourSize(); cityIndex++)
            {
                if (cityIndex == 0 || GetCity(cityIndex).Truck == truckId)
                {
                    // First origin (happen when cityIndex == 0)
                    if (fromCity == null)
                    {
                        fromCity = GetCity(cityIndex);
                    }
                    // Next origins are previous destinations
                    else
                    {
                        fromCity = destinationCity;
                    }

                    // searching next destination

                    if (cityIndex < (TourSize() - 1) && GetCity(cityIndex + 1).Truck == truckId) destinationCity = GetCity(cityIndex + 1);
                    while (cityIndex < (TourSize() - 1) && GetCity(cityIndex + 1).Truck != truckId)
                    {
                        cityIndex++;
                        if (cityIndex + 1 >= TourSize())
                        {
                            // Back to start point
                            destinationCity = GetCity(0);
                            break;
                        }
                        else if (GetCity(cityIndex + 1).Truck == truckId)
                        {
                            // Next destination for this truck
                            destinationCity = GetCity(cityIndex + 1);
                            break;
                        }
                    }
                    if (fromCity.CityName == destinationCity.CityName) break;

                    int actDist = (int)fromCity.DistanceTo(destinationCity);
                    double prevWeight = GetAllPreviousWeight(cityIndex);

                    //fuel consumption
                    //truckTourDistance += (actDist / 100) * GetMaxFuel() *
                    //(1 + ((GetTourWeight() - prevWeight) / GetTruckLoad())) * (1 / GetWeight(cityIndex));

                    //distance
                    truckTourDistance += actDist/1000;
                }
            }
            int singleDistance = (int)truckTourDistance;

            return singleDistance;
        }

        public int GetTotalDistance()
        {
            //if(distance == 0)
            //{
            distance = 0;
                int totalTourDistance = 0;
                for (int truckIndex = 1; truckIndex < 4; truckIndex++)
                {
                    totalTourDistance += GetTruckDistance(truckIndex);
                }
                    //// Loop over all trucks
                    //for (int truckIndex = 0; truckIndex < 5; truckIndex++)
                    //{
                    //    for (int cityIndex = 0; cityIndex < TourSize(); cityIndex++)
                    //    {
                    //        AlgCity fromCity = GetCity(cityIndex);
                    //        AlgCity destinationCity;


                    //        if (cityIndex + 1 < TourSize())
                    //        {
                    //            destinationCity = GetCity(cityIndex + 1);
                    //        }
                    //        else
                    //        {
                    //            // Back to start point
                    //            destinationCity = GetCity(0);
                    //        }

                    //        // Check if actual truck is going to actual city
                    //        if (destinationCity.Truck == truckIndex && fromCity.Truck == truckIndex )
                    //        {
                    //            int actDist = (int)fromCity.DistanceTo(destinationCity);
                    //            double prevWeight = GetAllPreviousWeight(cityIndex);


                    //            tourDistance += (actDist / 100) * GetMaxFuel() *
                    //                (1 + ((GetTourWeight() - prevWeight) / GetTruckLoad())) * (1 / GetWeight(cityIndex));
                    //        }
                    //    }
                    //}
                    distance = totalTourDistance;
            //}
            return distance;
        }

        public int TourSize()
        {
            return tour.Count;
        }

        //Check if tour constains a city
        public Boolean ContainsCity(AlgCity city)
        {
            foreach(AlgCity algCity in tour)
            {
                if (algCity != null && algCity.CityName == city.CityName) return true;
            }
            return false;
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

        public double GetTruckLoad()
        {
            return this.truckLoad;
        }

        public void SetTruckLoad(double load)
        {
            this.truckLoad = load;
        }

        public double GetTourWeight()
        {
            double weightSum = 0;
            for (int i = 0; i < TourSize(); i++)
            {
                weightSum += GetWeight(i);
            }
            SetTourWeight(weightSum);
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
            //TOJSON
            string[] tabTruck = new string[3];
            string[] tabTruckDistance = new string[3];
            string all;

            for (int j = 0; j < tabTruck.Length; j++)
            {
                for (int i = 0; i < TourSize(); i++)
                {
                    if (i==0)
                    {
                        
                    }
                    if (GetCity(i).Truck == j + 1)
                    {
                        if (tabTruck[j] == null)
                        {
                            tabTruck[j] = "\"" + GetCity(i).ToString() + "\"";
                        }
                        else
                        {
                            tabTruck[j] = tabTruck[j] + ",\"" + GetCity(i).ToString() + "\"";
                        }
                    }

                }

                tabTruckDistance[j] = GetTruckDistance(j+1).ToString();
            }

            all = "{ \"Trucks\" : [";
            bool isNotFirst = true;
            for (int j = 0; j < 3; j++)
            {
                if (tabTruckDistance[j] != "0" && isNotFirst==false)
                {
                    all = all + ",";
                    all = all + "{\"" + "Cities" + "\"" + ":[" + tabTruck[j] + "],";
                    all = all + "\"Distance\" :" + "\"" + tabTruckDistance[j] + "\"}";
                }
                else if (tabTruckDistance[j] != "0" && isNotFirst==true)
                {
                    all = all + "{\"" + "Cities" + "\"" + ":[" + tabTruck[j] + "],";
                    all = all + "\"Distance\" :" + "\"" + tabTruckDistance[j] + "\"}";
                    isNotFirst = false;
                }


            }

            all = all + "]}";
           

            return all;
        }
    }
}
