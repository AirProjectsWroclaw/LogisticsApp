﻿using LogisticsProject.Domain.Abstract;
using LogisticsProject.Domain.Concrete;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace LogisticsProject.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, 
            Type controllerType)
        {
            return controllerType == null
                ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<ICityRepository>().To<EFCityRepository>();
            ninjectKernel.Bind<IRouteRepository>().To<EFRouteRepository>();
        }
    }
}