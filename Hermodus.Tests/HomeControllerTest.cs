using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Hermodus.UI.Controllers;
using Hermodus.Service;
using Moq;
using System.Collections.Generic;
using Hermodus.Data;

namespace Hermodus.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<IPageRepository>();
           
            PageController controller = new PageController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNull(result.Model);
        }
        [TestMethod]
        public void CreatePageAction_NotNull()
        {
            // arrange
            string expected = "";
            var mock = new Mock<IPageRepository>();
            Page comp = new Page();
            PageController controller = new PageController(mock.Object);
            controller.ModelState.AddModelError("Name", "Название модели не установлено");
            // act
            ViewResult result = controller.AddNewPage(comp) as ViewResult;
            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewName);
        }
    }
}
