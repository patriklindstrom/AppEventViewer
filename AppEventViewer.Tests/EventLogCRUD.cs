using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Management;
using AppEventViewer.Models;

namespace AppEventViewer.Tests
{
    [TestClass]
    public class EventLogCrud
    {
        public const string SOURCE = "application";
        public const string LOGNAME = "EventTest";
        public const string TESTENTRY = "The " + LOGNAME + " was initilized.";



        [TestMethod]
        public void Write_And_Read_Event_From_Log()
        {
            //Arrange
            //   EventLog.CreateEventSource(SOURCE, LOGNAME);
            var testEventLog = new EventLog {Source = SOURCE, Log = SOURCE};
            var entryMessage = String.Empty;
            //Act
            testEventLog.WriteEntry(TESTENTRY, EventLogEntryType.Information);
            var testEventRows = testEventLog.Entries;

            foreach (EventLogEntry entry in testEventRows)
            {
                if (entry.TimeWritten > DateTime.Now.AddMilliseconds(-500)) break; //only 
                if (entry.ReplacementStrings[0] == null) continue;
                if (entry.ReplacementStrings[0].Equals(TESTENTRY))
                {
                    entryMessage = entry.ReplacementStrings[0];

                    break;
                }
            }

            //Assert

            Assert.IsTrue(entryMessage.Equals(TESTENTRY));
        }

        [TestMethod]
        public void Write_And_Read_Event_From_Log_With_WMI()
        {
           Debug.WriteLine("Testing Wmi Method Write_And_Read_Event_From_Log_With_WMI(), yeah."); 
            string SomeDateTime = "20130526000000.000000+000";
            var anotherTime= DateTime.Now.AddMilliseconds(-500);
            string wmiQuery =
                String.Format("SELECT * FROM Win32_NTLogEvent WHERE Logfile = 'Application' AND TimeGenerated > '{0}'",
                              SomeDateTime);
            var mos = new ManagementObjectSearcher(wmiQuery);
           // mos.
            //EventLogEntryCollection eventLogEntryCollection = (EventLogEntryCollection)mos.Container; 

            object o;
            Debug.WriteLine("***** Start writing Properties *****"); 
            foreach (ManagementObject mo in mos.Get())
            {
                var foo = new EventRecord(mo);
                Debug.WriteLine("***** New Managment Object from Eventstore *****");
               // EventLogEntry eventLogRow = (EventLogEntry)mo;
                foreach (PropertyData pd in mo.Properties)
                {
                    o = mo[pd.Name];
                    if (o != null)
                    {
                        Debug.WriteLine(String.Format("{0}: {1}", pd.Name, mo[pd.Name].ToString()));
                    }
                }
            }
        }
    }
}
