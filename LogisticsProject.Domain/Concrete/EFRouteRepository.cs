using LogisticsProject.Domain.Abstract;
using LogisticsProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsProject.Domain.Concrete
{
    public class EFRouteRepository : IRouteRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Route> Routes
        {
            get { return context.Routes; }
        }
    }
}
