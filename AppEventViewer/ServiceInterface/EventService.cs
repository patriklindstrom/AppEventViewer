using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web;
using AppEventViewer.Models;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace AppEventViewer.ServiceInterface
{
    //Define Request and Response DTOs

    // TODO: fix check that from is less then to
    /// <summary>
    /// The Request DTO for the Get EventRecordList DTO
    /// </summary>

    [Api("Service Description")]
    [Route("/Events/", "GET", Summary = @"A sorted list from all server nodes with events of from last hour ", Notes = "What server nodes and the filter of events are hardcoded")]
    [Route("/Events/{from}" ,"GET", Summary = @"A sorted list from all server nodes with events of ", Notes = "What server nodes and the filter of events are hardcoded")]
    public class Events : IReturn<EventRecordListResponse>
    {
            [ApiMember(Name = "from", Description = "Date and time from where the logs should be taken. The string must in yyyyMMddTHHmmss eg 20130618T102955 format to  be parsed as a DateTime ",
               ParameterType = "path", DataType = "string", IsRequired = false)]
        public string from { get; set; }
       //     [ApiMember(Name = "to", Description = "Date and time to what where has to be bigger then the from value and The string must be parsed as a DateTime. if null then defaults to now",
       //ParameterType = "path", DataType = "string", IsRequired = false)]
        //    public DateTime? to { get; set; }
        //public string SourceNameFilter { get; set; }
        //public DateTime FromDate { get; set; }
        //public DateTime ToDate { get; set; }
        //public int MaxRows { get; set; }

    }

    /// <summary>
    /// The Request DTO for the Get EventRecordList DTO
    /// </summary>
    public class EventRecordListResponse
    {
        public List<EventRecord> EventRecords { get; set; }
    }

    //Implementation
    public class EventService : Service
    {
        public EventRepository Repository { get; set; } //hopefully injected by IOC
        public object Get(Events request)
        {
            string format = "yyyyMMddTHHmmss";
            //DateTime dt = new DateTime(2013, 06, 17, 13, 29, 55);
            //var fum=    String.Format("{0:" + format +"}", dt);
            //string foo = DateTime.Now.AddHours(-1).ToString(format);
            string fromTime = request.from ?? DateTime.Now.AddHours(-1).ToString(format);
            DateTime outFromTime = DateTime.ParseExact(fromTime, format, System.Globalization.CultureInfo.InvariantCulture);
            return Repository.GetByTimeFilter(outFromTime); }        
        }
        }

    public class EventRepository
    {
        public List<EventRecord> GetByTimeFilter(DateTime fromTime  )
        {
           // DateTime fromTime = DateTime.Now.AddHours(-1*lag);
            string strFromTime = String.Format("{0:yyyyMMddHHmmss}", fromTime) + ".000000+000";
          //  string strToTime = String.Format("{0:yyyyMMddHHmmss}", toTime) + ".000000+000";
            string wmiQuery =
                String.Format("SELECT * FROM Win32_NTLogEvent WHERE Logfile = 'Application' AND TimeGenerated > '{0}'",strFromTime);
            var mos = new ManagementObjectSearcher(wmiQuery);
            object o;
            var eventRecordList = new List<EventRecord>();
            foreach (ManagementObject mo in mos.Get())
            {
                eventRecordList.Add(new EventRecord(mo));
            }
            return eventRecordList;
        }
    }
