using System;
using System.Collections.Generic;
using AppEventViewer.App_Start;
using AppEventViewer.Models;
using AppEventViewer.Repository;
using AppEventViewer.ServiceInterface;
using Funq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AppEventViewer.Tests
{
    [TestClass]
        public class ServiceTest
        {
            static Funq.Container TestContainer= new Container();

            // Use ClassInitialize to run code before running the first test in the class
            [ClassInitialize()]
            public static void MyClassInitialize(TestContext testContext) {
                //Arrange
                //Set up Funq IOS for Dependency injection
                TestContainer.Register<IEventRepository>(new EventRepository_Mock());               
            }
            [TestMethod]
            public void Test_Get_EventService_Returns_Response()
            {
                //Arrange
                //Set up the Service I want to Test
                var eventService = new EventService { Repository = TestContainer.Resolve<IEventRepository>() };
                string testFromDate = DateTime.Now.AddHours(-1).ToString(Global_Const.DATE_FORMAT);
                var testEvents = new Events {From = testFromDate};
                //Act
                // Call on the Service
                EventRecordListResponse response = (EventRecordListResponse) eventService.Get(testEvents);
                var eventRecords = response.EventRecords;
                //Assert
                Assert.IsNotNull(response, "Response from Eventservice is null");

                Assert.IsInstanceOfType(response, typeof(EventRecordListResponse), "Returns the wrong list type");
                List<IEventRecord> eventRList = (List<IEventRecord>)response.EventRecords;
                int evRlCount = eventRList.Count;
                Assert.IsTrue(evRlCount == 20, "There should be 20 item in the event test list");
            }
        }
    }

