using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    public class GeneticAlgorithm
    {
        private const double mutationRate = 0.015;
        private const int tournamentSize = 5;
        private const bool elitism = true;
        static Random rand = new Random();


        public static Population EvolvePopulation(Population pop)
        {
            Population newPopulation = new Population(pop.PopulationSize(), false);

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

                Tour child = Crossover(parent1, parent2);

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
                //Apply mutation rate
                if(rand.Next(0,10)/10.0 < mutationRate)
                {
                    int tourPos2 = (int)((tour.TourSize()-1) * rand.Next(0,10)/10.0 +1);

                    City city1 = tour.GetCity(tourPos1);
                    City city2 = tour.GetCity(tourPos2);

                    tour.SetCity(tourPos2, city1);
                    tour.SetCity(tourPos1, city2);
                }
            }
        }

        private static Tour Crossover(Tour parent1, Tour parent2)
        {
            Tour child = new Tour();
            Random random = new Random();

            double startPos = random.Next(0,11)/10.0 * parent1.TourSize();
            double endPos = random.Next(0,11)/10.0 * parent1.TourSize();

            for (int i = 0; i < child.TourSize(); i++)
            {
                if (startPos < endPos && i > startPos && i < endPos)
                {
                    child.SetCity(i, parent1.GetCity(i));
                }
                else if (startPos > endPos)
                {
                    if (!(i < startPos && i > endPos))
                    {
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
                            child.SetCity(j, parent2.GetCity(i));
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

            Tour fittest = tournament.GetFittest();
            return fittest;
        }
    }
}
