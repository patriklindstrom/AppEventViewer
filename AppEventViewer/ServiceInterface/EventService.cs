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
        public const string SOURCE = "application";
        public const string DATE_FORMAT = "yyyyMMddHHmmss";
        public const string DATE_FORMAT_STR = "{0:yyyyMMddHHmmss}";
        public const int MAXGETROWS = 5000;
        public const int TIMEOUT_S = 2;
    }

    //Define Request and Response DTOs

    // TODO: fix check that from is less then to
    /// <summary>
    /// The Request DTO for the Get EventRecordList DTO
    /// </summary>
    [Api("Service Description")]
    [Route("/Events/", "GET", Summary = @"A sorted list from all server nodes with events of from last hour ",
        Notes = "What server nodes and the filter of events are hardcoded")]
    [Route("/Events/{from}", "GET", Summary = @"A sorted list from all server nodes with events of ",
        Notes = "What server nodes and the filter of events are hardcoded")]
    [Route("/Events/{from}/{to}", "GET", Summary = @"A sorted list from all server nodes with events of ",
    Notes = "What server nodes and the filter of events are hardcoded")]
    public class Events : IReturn<EventRecordListResponse>
    {
        [ApiMember(Name = "from",
            Description =
                "Date and time from where the logs should be taken. The string must in yyyyMMddHHmmss eg 20130618102955 format to  be parsed as a DateTime. If null then defaults to - 1 hour"
            ,
            ParameterType = "path", DataType = "string", IsRequired = false)]
        public string From { get; set; }

        [ApiMember(Name = "to",
            Description =
                "Date and time to what where has to be bigger then the from value and The string must be The string must in yyyyMMddHHmmss eg 20130618102955 parsed as a DateTime. if null then defaults to now"
            ,
            ParameterType = "path", DataType = "string", IsRequired = false)]
        public string To { get; set; }

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
        public List<IEventRecord> EventRecords { get; set; }
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
            string fromTime = request.From ?? DateTime.Now.AddHours(-1).ToString(GlobalVar.DATE_FORMAT);
            DateTime outFromTime = DateTime.ParseExact(fromTime, GlobalVar.DATE_FORMAT,
                                                       System.Globalization.CultureInfo.InvariantCulture);
            string toTime = request.To ?? DateTime.Now.ToString(GlobalVar.DATE_FORMAT);
            DateTime outToTime = DateTime.ParseExact(toTime, GlobalVar.DATE_FORMAT,
                                                     System.Globalization.CultureInfo.InvariantCulture);
            return Repository.GetByTimeFilter(outFromTime, outToTime, GlobalVar.MAXGETROWS, GlobalVar.TIMEOUT_S);
        }
    }
}

public interface IEventRepository
{
    List<IEventRecord> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec);
}

public class EventRepository : IEventRepository
{
    public List<IEventRecord> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec)
    {
        // DateTime fromTime = DateTime.Now.AddHours(-1*lag);
        string strFromTime = String.Format(GlobalVar.DATE_FORMAT_STR, fromTime) + ".000000+000";
        string strToTime = String.Format("{0:yyyyMMddHHmmss}", toTime) + ".000000+000";
        string wmiQuery =
            String.Format(
                "SELECT * FROM Win32_NTLogEvent WHERE Logfile = '{0}' AND TimeGenerated >= '{1}' AND TimeGenerated <= '{2}' ",
                GlobalVar.SOURCE, strFromTime, strToTime);
        var mos = new ManagementObjectSearcher(wmiQuery);
        object o;
        return new List<IEventRecord>((from ManagementObject mo in mos.Get() select new EventRecord(mo)).ToList());
    }
}
