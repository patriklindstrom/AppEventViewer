using System;
using AppEventViewer.ServiceInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Management;
using AppEventViewer.Models;
using System.Collections.Generic;

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
           string strFromTime = String.Format(GlobalVar.DATE_FORMAT_STR, FromTime) + ".000000+000";
            string wmiQuery =
                  String.Format("SELECT * FROM Win32_NTLogEvent WHERE Logfile = '{0}' AND TimeGenerated > '{1}'", GlobalVar.SOURCE, strFromTime);
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
    }
}
