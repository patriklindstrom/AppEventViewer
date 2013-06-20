using System;
using AppEventViewer.ServiceInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Management;
using AppEventViewer.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppEventViewer.Tests
{
    [TestClass]
    public class EventLogCrud
    {
        public const string SOURCE = "EventTestWriter";
        public const string LOGNAME = "application";
        public const string TESTENTRY = "The " + SOURCE + " was initilized. Go DTD. Do not fail me";


        [TestMethod]
        public void Write_And_Read_Event_From_Log()
        {
            //Arrange
            //   EventLog.CreateEventSource(SOURCE, LOGNAME);
            var testEventLog = new EventLog { Source = LOGNAME, Log = LOGNAME };
            var entryMessageToCheck = String.Empty;
            //Act
            testEventLog.WriteEntry(TESTENTRY, EventLogEntryType.Information);
            var testEventRows = testEventLog.Entries;
            var stopdt = DateTime.Now.AddMilliseconds(-500);
            var maxIter = 1000;
            int i = 0;
            foreach (EventLogEntry entry in testEventRows)
            {
                i++;
                if (i>maxIter)
                {
                    break;                    
                }
                if (entry.TimeWritten > stopdt) break; //only 

                    if (entry.ReplacementStrings[0] == null) continue;
                    if (entry.ReplacementStrings[0].Equals(TESTENTRY))
                    {
                        entryMessageToCheck = entry.ReplacementStrings[0];
                        break;
                    }           
            }

            //Assert

            Assert.IsTrue(entryMessageToCheck.Equals(TESTENTRY));
        }

        [TestMethod]
        public void Read_Event_From_Log_With_WMI()
        {
            //Arrange
           Debug.WriteLine("Testing Wmi Method Write_And_Read_Event_From_Log_With_WMI(), yeah.");
           DateTime FromTime = DateTime.Now.AddDays(-1); 
           // string SomeDateTime = "20130526000000.000000+000";
           string strFromTime = String.Format(Global_Const.DATE_FORMAT_STR, FromTime) + ".000000+000";
            string wmiQuery =
                  String.Format("SELECT * FROM Win32_NTLogEvent WHERE Logfile = '{0}' AND TimeGenerated > '{1}'", Global_Const.SOURCE, strFromTime);
            //Act
            var mos = new ManagementObjectSearcher(wmiQuery);
           // mos.
            //EventLogEntryCollection eventLogEntryCollection = (EventLogEntryCollection)mos.Container; 
            object o;
            Debug.WriteLine("***** Start writing Properties *****");
            List<EventRecord> EventRecordList = new List<EventRecord>();
            foreach (ManagementObject mo in mos.Get())
            {
               EventRecordList.Add (new EventRecord(mo));
                Debug.WriteLine("***** New Managment Object from Eventstore *****");
                Debug.WriteLine(String.Format("{0}: {1}","Message", EventRecordList[EventRecordList.Count - 1].Message));
               // EventLogEntry eventLogRow = (EventLogEntry)mo;
                //foreach (PropertyData pd in mo.Properties)
                //{
                //    o = mo[pd.Name];
                //    if (o != null)
                //    {
                //        Debug.WriteLine(String.Format("{0}: {1}", pd.Name, mo[pd.Name].ToString()));
                //    }
                //}
            }
            //Assert
            Assert.IsTrue(EventRecordList.Count > 0, "There should be some events last day");
        }

        [TestMethod]
        public void Read_Event_From_2_Computer_Log_With_WMI()
        { // more info http://msdn.microsoft.com/en-us/library/ms257337(v=VS.80).aspx
            //Arrange
            Debug.WriteLine("Testing Wmi Method Write_And_Read_Event_From_2_Log_With_WMI(), scary.");
            DateTime FromTime = DateTime.Now.AddDays(-1);

             ManagementScope scope = 
            new ManagementScope(
                "\\\\WL2006228\\root\\cimv2");
        scope.Connect();
            // string SomeDateTime = "20130526000000.000000+000";
        string strFromTime = String.Format(Global_Const.DATE_FORMAT_STR, FromTime) + ".000000+000";
            string wmiQuery =
                  String.Format("SELECT * FROM Win32_NTLogEvent WHERE Logfile = '{0}' AND TimeGenerated > '{1}'", Global_Const.SOURCE, strFromTime);
            //Act
            var mos = new ManagementObjectSearcher("\\\\.\\root\\cimv2",wmiQuery);
            //Act
            var mos2 = new ManagementObjectSearcher("\\\\WL2006228\\root\\cimv2", wmiQuery);

            object o;
            Debug.WriteLine("***** Start writing Properties *****");
            List<EventRecord> EventRecordList = new List<EventRecord>();
            List<EventRecord> EventRecordList2 = new List<EventRecord>();
            
            foreach (ManagementObject mo in mos.Get())
            {
                EventRecordList.Add(new EventRecord(mo));
                Debug.WriteLine("***** New Managment Object from Eventstore *****");
                Debug.WriteLine(String.Format("{0}: {1}", "Message", EventRecordList[EventRecordList.Count - 1].Message));
            }

            foreach (ManagementObject mo2 in mos2.Get())
            {
                EventRecordList2.Add(new EventRecord(mo2));
                Debug.WriteLine("***** New Managment Object from Eventstore *****");
                Debug.WriteLine(String.Format("{0}: {1}", "Message", EventRecordList[EventRecordList.Count - 1].Message));
            }

            var eventRecMergedList = EventRecordList.Concat(EventRecordList2).OrderBy(e => e.TimeGenerated);
            foreach (EventRecord ev in eventRecMergedList)
            {
                Debug.WriteLine(ev.TimeGenerated);
                Debug.WriteLine(ev.RecordNumber);
            }

            //Assert
            Assert.IsTrue(EventRecordList.Count > 0, "There should be some events last day");
        }
    }
}
