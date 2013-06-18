using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web;
using AppEventViewer.Models;
using AppEventViewer.ServiceInterface;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace AppEventViewer.ServiceInterface
{
    public static class GlobalVar
    {

        public const string DATE_FORMAT = "yyyyMMddHHmmss";
        public const string DATE_FORMAT_STR ="{0:yyyyMMddHHmmss}";
    }
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
            [ApiMember(Name = "from", Description = "Date and time from where the logs should be taken. The string must in yyyyMMddHHmmss eg 20130618102955 format to  be parsed as a DateTime ",
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
    /// <summary>
    /// 
    /// </summary>
    public class EventService : Service
    {
        public IEventRepository Repository { get; set; } // injected by Funq IOC in AppHost config                   
        public object Get(Events request)
        {
           
            string fromTime = request.from ?? DateTime.Now.AddHours(-1).ToString(GlobalVar.DATE_FORMAT);
            DateTime outFromTime = DateTime.ParseExact(fromTime, GlobalVar.DATE_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
            return Repository.GetByTimeFilter(outFromTime); }        
        }
        }

public interface IEventRepository
{
    List<EventRecord> GetByTimeFilter(DateTime fromTime  );
}

public class EventRepository : IEventRepository
{
        public List<EventRecord> GetByTimeFilter(DateTime fromTime  )
        {
           // DateTime fromTime = DateTime.Now.AddHours(-1*lag);
            string strFromTime = String.Format(GlobalVar.DATE_FORMAT_STR, fromTime) + ".000000+000";
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
