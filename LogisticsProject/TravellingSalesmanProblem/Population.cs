using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    public class Population
    {
        //Population of candidate tours
        Tour[] tours;

        public Population(int populationSize, Boolean initialise)
        {
            tours = new Tour[populationSize];

            if (initialise)
            {
                //Loop and create random individuals
                for(int i = 0; i < populationSize; i++)
                {
                    Tour newTour = new Tour();
                    newTour.GenerateIndividual(TourManager.GetMaxFuelConsump());
                    SaveTour(i, newTour);
                }
            }
        }

        //Saves a tour
        public void SaveTour(int index, Tour tour)
        {
            tour.SetMaxFuel(TourManager.GetMaxFuelConsump());
            tour.SetTruckLoad(TourManager.TruckLoad);
            tours[index] = tour;
        }

        //Gets a tour from population
        public Tour GetTour(int index)
        {
            return tours[index];
        }

        //Gets the best tour in population
        public Tour GetFittest()
        {
            Tour fittest = tours[0];
            for(int i = 1; i < PopulationSize(); i++)
            {
                if(fittest.GetFitness() <= GetTour(i).GetFitness())
                {
                    fittest = GetTour(i);
                }
            }
            return fittest;
        }

        public int PopulationSize()
        {
            return tours.Length;
        }
    }
}
