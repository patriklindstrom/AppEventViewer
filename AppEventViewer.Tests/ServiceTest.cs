using System;
using System.Collections.Generic;
using System.Management;
using AppEventViewer.Models;
using AppEventViewer.ServiceInterface;
using FakeItEasy;
using Funq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AppEventViewer.Tests
{
    public class TestEventRepository : IEventRepository
    {
        
        public List<IEventRecord> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec)
        {
            var eventRecordList = new List<IEventRecord>();
            ManagementObject vmi=null;
           // ManagementObject vmi = A.Fake<ManagementObject>();
            var er1 = new EventRecord(vmi)
                {
                    Category = "0",
                    ComputerName = "Herkules",
                    EventCode = "2264",
                    EventType = "2",
                    InsertionStrings =
                        @"C:\Users\Patrik\AppData\Local\Temp\iisexpress\IIS Temporary Compressed Files\Clr4IntegratedAppPoo",
                    Logfile = "Application",
                    Message =
                        @"The directory specified for caching compressed content C:\Users\Patrik\AppData\Local\Temp\iisexpress\IIS Temporary Compressed Files\Clr4IntegratedAppPool is invalid. Static compression is being disabled.",
                    RecordNumber = "460172",
                    SourceName = "IIS Express",
                    TimeGenerated = "20130619182551.000000-000",
                    TimeWritten = "20130619182551.000000-000",
                    Type = "Varning"
                };

            eventRecordList.Add(er1);
            ManagementObject vmi2 = null;
            var er2 = new EventRecord(vmi2)
                {
                    Category = "0",
                    ComputerName = "Herkules",
                    EventCode = "0",
                    EventType = "3",
                    InsertionStrings = @"The EventTestWriter was initilized. Go DTD. Do not fail me",
                    Logfile = "Application",
                    Message = String.Empty,
                    RecordNumber = "460171",
                    SourceName = "application",
                    TimeGenerated = "20130619182521.000000-000",
                    TimeWritten = "20130619182521.000000-000",
                    Type = "Information"
                };
            eventRecordList.Add(er2);
            // https://github.com/FakeItEasy/FakeItEasy
            ManagementObject vmi3 = null;
           // var er3 = A.Fake<EventRecord>(() =>  new EventRecord(vmi3));
           // var foo = A.Fake<Foo>(() => new Foo("string passed to constructor"));
            IEventRecord er3 = A.Fake<IEventRecord>();
           // A.CallTo(er3).WithReturnType<string>().Returns("hello world");
            eventRecordList.Add(er3);
            for (int i = 4; i <= 20; i++)
            {
                IEventRecord erN = A.Fake<IEventRecord>();
                A.CallTo(erN).WithReturnType<string>().Returns("hello world nr: " + i.ToString());
                eventRecordList.Add(erN);
            }

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
                Assert.IsInstanceOfType(response,typeof(List<IEventRecord>), "Returns the wrong list type");
                List<IEventRecord> eventRList = (List<IEventRecord>)response;
                int evRlCount = eventRList.Count;
                Assert.IsTrue(evRlCount == 20, "There should be 20 item in the event test list");
            }
        }
    }
}
