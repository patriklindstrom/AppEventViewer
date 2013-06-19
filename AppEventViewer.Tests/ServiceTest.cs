using System;
using System.Collections.Generic;
using System.Management;
using AppEventViewer.Models;
using AppEventViewer.ServiceInterface;
using Funq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AppEventViewer.Tests
{
    public class TestEventRepository : IEventRepository
    {
        public List<EventRecord> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec)
        {
            var eventRecordList = new List<EventRecord>();
            ManagementObject vmi=null;
            var er1 = new EventRecord(vmi);
            er1.Category = "3";
            er1.ComputerName = "mycomputer\\Stuff";
            er1.EventCode = "4";
            eventRecordList.Add(er1);
            var er2 = new EventRecord(vmi);
            er2.Category = "4";
            er2.ComputerName = "DiabloIIIComputer";
            er2.EventCode = "-1";
            eventRecordList.Add(er2);
            return eventRecordList;
        }


        [TestClass]
        public class ServiceTest
        {
            static Funq.Container TestContainer= new Container();

            // Use ClassInitialize to run code before running the first test in the class
            [ClassInitialize()]
            public static void MyClassInitialize(TestContext testContext) {
                //Arrange
                //Set up Funq IOS for Dependency injection
                TestContainer.Register<IEventRepository>(new TestEventRepository());               
            }
            [TestMethod]
            public void Test_Get_EventService_Returns_Response()
            {
                //Arrange
                //Set up the Service I want to Test
                var eventService = new EventService { Repository = TestContainer.Resolve<IEventRepository>() };
                string testFromDate = DateTime.Now.AddHours(-1).ToString(GlobalVar.DATE_FORMAT);
                var testEvents = new Events {From = testFromDate};
                //Act
                // Call on the Service
                var response = eventService.Get(testEvents);
                //Assert
                Assert.IsNotNull(response, "Response from Eventservice is null");
                Assert.IsInstanceOfType(response,typeof(List<EventRecord>), "Returns the wrong list type");
                List<EventRecord> eventRList = (List<EventRecord>)response;
                int evRlCount = eventRList.Count;
                Assert.IsTrue(evRlCount == 2, "There should be 2 item in the event test list");
            }
        }
    }
}
