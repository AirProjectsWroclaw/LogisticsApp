using LogisticsProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsProject.Domain.Abstract
{
    public interface IRouteRepository
    {
        IQueryable<Route> Routes { get; }
    }
}
