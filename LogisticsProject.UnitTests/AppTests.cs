using LogisticsProject.Domain.Abstract;
using LogisticsProject.Domain.Entities;
using LogisticsProject.Models;
using LogisticsProject.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LogisticsProject.UnitTests
{
    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void Is_App_Form_Model_Valid()
        {
            //preparation - creating imitation of repos
            Mock<ICityRepository> mockCity = new Mock<ICityRepository>();
            Mock<IRouteRepository> mockRoute = new Mock<IRouteRepository>();
            mockCity.Setup(m => m.Cities).Returns(new City[]
            {
                new City {CityId = 1, CityName = "City1"},
                new City {CityId = 2, CityName = "City2"}
            }.AsQueryable());

            //preparation - creating controller
            AppController controller = new AppController(mockCity.Object, mockRoute.Object);

            ViewResult result = (ViewResult)controller.AppForm();

            Assert.AreEqual(2, ((FormViewModel)result.Model).CitiesFrom.Count());
            
        }
    }
}
