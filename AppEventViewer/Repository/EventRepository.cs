using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using AppEventViewer;
using AppEventViewer.App_Start;
using AppEventViewer.Models;

namespace AppEventViewer.Repository
{
    public interface IEventRepository
    {
        IAppConfig Config { get; set; }
        List<IEventRecord> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec);
    }
    public class EventRepository : IEventRepository
    {
        public IAppConfig Config { get; set; } //injected hopefully buy IOC
        public List<IEventRecord> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec)
        {
        
            // DateTime fromTime = DateTime.Now.AddHours(-1*lag);
            string strFromTime = String.Format(Global_Const.DATE_FORMAT_STR, fromTime) + ".000000+000";
            string strToTime = String.Format(Global_Const.DATE_FORMAT_STR, toTime) + ".000000+000";
            string wmiQuery =
                String.Format(
                    "SELECT * FROM Win32_NTLogEvent WHERE Logfile = '{0}' AND TimeGenerated >= '{1}' AND TimeGenerated <= '{2}' ",
                    Global_Const.SOURCE, strFromTime, strToTime);
            List<IEventRecord> eventRecMergedList = new List<IEventRecord>();
            if (Config == null)
            {
                throw new NullReferenceException("There is no configuration object and therefore no list of servers. Config object  is null. It is the Func IOC that should have set this in the code. Something trivial is missing.");
            }
            if (Config.ServersToQuery==null)
            {
                throw new NullReferenceException("The list of servers from Config is null. That is bad.");
            }
            foreach (var serv in Config.ServersToQuery)
            {
                var mos = new ManagementObjectSearcher("\\\\"+ serv +"\\root\\cimv2", wmiQuery);

                List<IEventRecord> eventRecordList = (from ManagementObject mo in mos.Get() select new EventRecord(mo)).Cast<IEventRecord>().ToList();
                eventRecMergedList = (List<IEventRecord>)eventRecMergedList.Concat(eventRecordList).ToList();
            }
            //var eventRecMergedList = EventRecordList.Concat(EventRecordList2).OrderBy(e => e.TimeGenerated);
            List<IEventRecord> returnEventList =  new List<IEventRecord>(eventRecMergedList.OrderBy(e => e.TimeGenerated));
            return returnEventList;
        }


    }
}