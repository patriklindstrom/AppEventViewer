using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using AppEventViewer.App_Start;
using AppEventViewer.Models;
using AppEventViewer.Repository;
using Funq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppEventViewer.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        static Funq.Container TestContainer = new Container();
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            //Arrange
            //Set up Funq IOS for Dependency injection
            TestContainer.Register<IEventRepository>(new EventRepository());// We want to test the real Repository


        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_that_GetByTimeFilter_Returns_a_sorted_list_when_Several_servers()
        {
            //Arrange
            TestContainer.Register<IAppConfig>(new AppConfig_Mock()); // We are using test config for this tests.
            IEventRepository testRep = TestContainer.Resolve<IEventRepository>();
            testRep.Config = TestContainer.Resolve<IAppConfig>();            
            DateTime fromTime = DateTime.Now.AddDays(-1);
            DateTime toTime = DateTime.Now;
            //Act
            List<IEventRecord> eventList = testRep.GetByTimeFilter(fromTime, toTime, Global_Const.MAXGETROWS, Global_Const.TIMEOUT_S);
            //Check that it is sorted
            DateTime prevEv =DateTime.UtcNow;
            bool isBigger =false;
            foreach (EventRecord ev in eventList)
            {
                isBigger = (ev.TimeGenerated !=prevEv);  // String.Compare(ev.TimeGenerated, prevEv, System.StringComparison.Ordinal);
                prevEv = ev.TimeGenerated;
                if (isBigger )
                {
                    break;
                }
            }
            //Assert
            Assert.IsTrue(eventList.Any(), "There should be some events last day");
            Assert.IsTrue(isBigger, "Sorting sucks");
        }
        [TestMethod]
        public void Test_that_GetByTimeFilter_Returns_a_sorted_list_when_No_server()
        {
            //Arrange
            TestContainer.Register<IAppConfig>(new AppConfig_ZeroServer_Mock()); // We are using test config for this tests.
            IEventRepository testRep = TestContainer.Resolve<IEventRepository>();
            testRep.Config = TestContainer.Resolve<IAppConfig>();
            DateTime fromTime = DateTime.Now.AddDays(-1);
            DateTime toTime = DateTime.Now;
            //Act
            List<IEventRecord> eventList = testRep.GetByTimeFilter(fromTime, toTime, Global_Const.MAXGETROWS, Global_Const.TIMEOUT_S);          
            //Assert List should be empty
            Assert.IsFalse(eventList.Any(), "If no servers then no list");

        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException),"The list of servers from Config is null. That is bad.")]
        public void Test_that_GetByTimeFilter_Returns_a_sorted_list_when_Null_serverlist()
        {
            //Arrange
            TestContainer.Register<IAppConfig>(new AppConfig_Null_Mock());
            IEventRepository testRep = TestContainer.Resolve<IEventRepository>();
            testRep.Config = TestContainer.Resolve<IAppConfig>();
            DateTime fromTime = DateTime.Now.AddDays(-1);
            DateTime toTime = DateTime.Now;
            //Act
            List<IEventRecord> eventList = testRep.GetByTimeFilter(fromTime, toTime, Global_Const.MAXGETROWS, Global_Const.TIMEOUT_S);
            // See  [ExpectedException at top
        }
    }
}
