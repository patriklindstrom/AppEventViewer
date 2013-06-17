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


    /// <summary>
    /// The Request DTO for the Get EventRecordList DTO
    /// lag is in hours
    /// </summary>

    [Api("Service Description")]
    [Route("/Events/{lag}" ,"GET", Summary = @"A sorted list from all server nodes with events of ", Notes = "What server nodes and the filter of events are hardcoded")]
    public class Events : IReturn<EventRecordListResponse>
    {
            [ApiMember(Name = "Lag", Description = "Filter events on Minus hour from now",
               ParameterType = "path", DataType = "integer", IsRequired = true)]
        public int Lag { get; set; }
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
            return Repository.GetByTimeFilter(request.Lag);
        }

        }

    public class EventRepository
    {
        public List<EventRecord> GetByTimeFilter(int lag)
        {
            DateTime fromTime = DateTime.Now.AddHours(-1*lag);
            string strFromTime = String.Format("{0:yyyyMMddHHmmss}", fromTime) + ".000000+000";
            string wmiQuery =
                String.Format("SELECT * FROM Win32_NTLogEvent WHERE Logfile = 'Application' AND TimeGenerated > '{0}'",
                              strFromTime);
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

}