using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DataContracts;

namespace TravellingSalesmanProblem
{
    public class GeneticAlgorithm
    {
        private const double mutationRate = 0;
        private const int tournamentSize = 15;
        private const bool elitism = true;
        static Random rand = new Random();


        public static Population EvolvePopulation(Population pop)
        {
            Population newPopulation = new Population(pop.PopulationSize(), false);
            double sum=0;
            double sumtruck=0;
            int elitismOffset = 0;
            if (elitism)
            {
                newPopulation.SaveTour(0, pop.GetFittest());
                elitismOffset = 1;
            }

            for (int i = elitismOffset; i < newPopulation.PopulationSize(); i++)
            {
                Tour parent1 = TournamentSelection(pop);
                Tour parent2 = TournamentSelection(pop);
                //------------------------------------
                var child = Crossover(parent1, parent2);

                while (child == null)
                {
                    child = Crossover(parent1, parent2);
                }
                /*for (int j = 1; j < 4; j++)
                {
                    sumtruck = sumtruck + child.trucksLoad[j];
                }
                for (int j = 0; j < child.TourSize(); j++)
                {
                    sum = sum + child.GetCity(j).Weight;
                }

                if (sum!=60-sumtruck)
                {
                    Console.WriteLine("brak wszystkich maist");
                    
                }*/
                
                
                    newPopulation.SaveTour(i, child);

            }

            for(int i = elitismOffset; i < newPopulation.PopulationSize(); i++)
            {
                Mutate(newPopulation.GetTour(i));
            }

            return newPopulation;
        }

        private static void Mutate(Tour tour)
        {
            for(int tourPos1 = 1; tourPos1 < tour.TourSize(); tourPos1++)
            {
                Boolean choosed = false;
                //Apply mutation rate
                if(rand.Next(0,10)/10.0 < mutationRate)
                {
                    int tourPos2 = (int)((tour.TourSize()-1) * rand.Next(0,10)/10.0 +1);

                    AlgCity city1 = new AlgCity(tour.GetCity(tourPos1));
                    AlgCity city2 = new AlgCity(tour.GetCity(tourPos2));
                    
                    tour.trucksLoad[city1.Truck] += city1.Weight;
                    tour.trucksLoad[city2.Truck] += city2.Weight;

                    if (city1.Weight < city2.Weight)
                    {
                        AlgCity citySwitch = city1;
                        city1 = city2;
                        city2 = citySwitch;
                    }
                    //city1.Truck = rand.Next(1, 4);
                    //city2.Truck = rand.Next(1, 4);
                    while (!choosed)
                    {
                        city1.Truck = rand.Next(1, 4);
                        if (tour.trucksLoad[city1.Truck] >= city1.Weight)
                        {
                            tour.trucksLoad[city1.Truck] -= city1.Weight;
                            tour.SetCity(tourPos2, city1);
                            choosed = true;
                        }
                    }

                    choosed = false;

                    while (!choosed)
                    {
                        city2.Truck = rand.Next(1, 4);
                        if (tour.trucksLoad[city2.Truck] >= city2.Weight)
                        {
                            tour.trucksLoad[city2.Truck] -= city2.Weight;
                            tour.SetCity(tourPos1, city2);
                            choosed = true;
                        }
                    }

                    //tour.SetCity(tourPos2, city1);
                    //tour.SetCity(tourPos1, city2);
                }
            }
        }

        private static Tour Crossover(Tour parent1, Tour parent2)
        {
            Tour child = new Tour();
            Random random = new Random();

            double startPos = random.Next(0,11)/10.0 * parent1.TourSize();
            double endPos = random.Next(0,11)/10.0 * parent1.TourSize();

            int[] tab = {0,0,0,0};

            for (int i = 0; i < child.TourSize(); i++)
            {
                if (startPos < endPos && i > startPos && i < endPos)
                {
                    if(i!=0) child.trucksLoad[parent1.GetCity(i).Truck] -= parent1.GetCity(i).Weight;

                    //
                    child.SetCity(i, parent1.GetCity(i));
                }
                else if (startPos > endPos)
                {
                    if (!(i < startPos && i > endPos))
                    {
                        if (i != 0) child.trucksLoad[parent1.GetCity(i).Truck] -= parent1.GetCity(i).Weight;
                        //
                        child.SetCity(i, parent1.GetCity(i));
                    };
                }
            }

            //Loop throught parent2's city tour
            for(int i = 0; i < parent2.TourSize(); i++)
            {
                if (!child.ContainsCity(parent2.GetCity(i)))
                {
                    for(int j = 0; j < child.TourSize(); j++)
                    {
                        if (child.GetCity(j) == null)
                        {
                            AlgCity city = new AlgCity(parent2.GetCity(i));
                            Boolean choosed = false;
                            while (!choosed)
                            {

                                city.Truck = rand.Next(1, 4);
                                
                                if (child.trucksLoad[city.Truck] >= city.Weight)
                                {
                                    child.trucksLoad[city.Truck] -= city.Weight;
                                    child.SetCity(j, city);

                                    choosed = true;
                                }
                                else
                                {
                                    tab[city.Truck]++;
                                    if (tab[1] != 0 && tab[2] != 0 && tab[3] != 0)
                                        return null;
                                }
                            }
                            break;

                        }
                    }
                }
            }
            
            return child;
           
           

        }

        private static Tour TournamentSelection(Population pop)
        {
            Population tournament = new Population(tournamentSize, false);

            for(int i = 0; i < tournamentSize; i++)
            {
                int randomId = (int)(rand.Next(0, 10)/10.0 * pop.PopulationSize());
                tournament.SaveTour(i, pop.GetTour(randomId));
            }
            Tour fittest = new Tour();
            fittest = tournament.GetFittest();
            return fittest;
        }
    }
}
