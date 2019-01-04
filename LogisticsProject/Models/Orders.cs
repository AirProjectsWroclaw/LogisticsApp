using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogisticsProject.Models
{
    public class Orders
    {
        private List<Order> ordersList;

        public List<Order> OrdersList
        {
            get { return ordersList; }
            set { ordersList = value; }
        }

    }
}