using System;
using System.Collections.Generic;
using System.Management;
using AppEventViewer.Models;
using FakeItEasy;

namespace AppEventViewer.Tests
{
    public class EventRepository_Mock : IEventRepository
    {


        //injected hopefully buy IOC
       public  IAppConfig Config { get; set; }


        public List<IEventRecord> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec)
        {
            var eventRecordList = new List<IEventRecord>();
            ManagementObject vmi = null;
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


    }


}