using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AppEventViewer.Controllers;
using AppEventViewer.Models;
using Funq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppEventViewer.Tests.Controllers
{
    [TestClass]
    public class EventsControllerTest
    {
        private static Funq.Container TestContainer = new Container();
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            //Arrange
            //Set up Funq IOS for Dependency injection
            TestContainer.Register<IEventRepository>(new EventRepository_Mock());
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            EventsController controller = new EventsController();
            var eventReq = new EventReq {From = "20130613154515", To = "20130614163022"};
            // Act
            ViewResult result = controller.Index(eventReq) as ViewResult;
            IEventRecListViewModel viewResult = (IEventRecListViewModel) result;
            // Assert
 
            Assert.AreEqual("Here is a list of all filtered events from all server nodes.", result.ViewBag.Message);
            Assert.IsNotNull(result, "Response from Eventservice is null");
            Assert.IsInstanceOfType(result, typeof(IEventRecListViewModel), "Returns the wrong ViewModel");
            IEventRecListViewModel eventRList = (IEventRecListViewModel)result.Model;

            int evRlCount = eventRList.Count;
            Assert.IsTrue(evRlCount == 20, "There should be 20 item in the event test list");
        }
    }
}
