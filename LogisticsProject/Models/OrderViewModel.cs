﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LogisticsProject.Models
{
    public class OrderViewModel
    {
        private string odjazd;

        public string Odjazd
        {
            get { return odjazd; }
            set { odjazd = value; }
        }

        private string przyjazd;

        public string Przyjazd
        {
            get { return przyjazd; }
            set { przyjazd = value; }
        }

        [Required]
        private double masa;

        public double Masa
        {
            get { return masa; }
            set { masa = value; }
        }


    }
}