using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;

namespace HairSalon.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_ReturnRightView_True()
        {
            //Arrange
            HomeController newController = new HomeController();

            //Act
            ActionResult indexView = newController.Index();

            //Assert
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
    }
}