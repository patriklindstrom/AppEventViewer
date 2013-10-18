using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppEventViewer.Models;

namespace AppEventViewer.Repository
{
    public class ISql1Repository
    {
        public IAppConfig Config { get; set; } //injected hopefully buy IOC
        /// <summary>
        /// It takes filtered event data from several windows servers. The servers name are inserted from Config class via DIP 
        /// It takes the content string filter from the Config class most likely webconfig. It then returns all event record log data between two datetimes.
        /// It uses VMI to query the servers
        /// </summary>
        /// <param name="fromTime">DataTime from where to start the search span. The time is included in the search</param>
        /// <param name="toTime">DateTime to is bigger - more in future time then from. Marks end of time span filter uses the time is included in search </param>
        /// <param name="maxRows">Not implemented yet</param>
        /// <param name="timeOutSec">Not implemented yet</param>
        /// <returns></returns>
        public List<ISql1Record> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec)
        {


            List<IEventRecord> eventRecMergedList = new List<IEventRecord>();
            if (Config == null)
            {
                throw new NullReferenceException(
                    "There is no configuration object and therefore no list of servers. Config object  is null. It is the Func IOC that should have set this in the code. Something trivial is missing.");
            }
            if (Config.ServersToQuery == null)
            {
                throw new NullReferenceException("The list of servers from Config is null. That is bad.");
            }
            List<IEventRecord> eventRecordList = new List<IEventRecord>();

            //TODO break out this logic so it can be tested.
            // Here comes three nested list. Its for every server, for every event record check every searchTerm if ok then add it.
            var searchTermList = Config.FilterTerm;
            int sTCount = searchTermList.Count;
            bool multiSearch = (searchTermList.Count() > 1);
            foreach (var serv in Config.ServersToQuery)
            {
                var mos = new ManagementObjectSearcher("\\\\" + serv + "\\root\\cimv2", wmiQuery);
                var mossos = mos.Get();
                foreach (var mo in mossos)
                {
                    var eventRec = new EventRecord((ManagementObject)mo);
                    //Filter out all data that contains records that we are interested in.
                    for (int index = 0; index < sTCount; index++)
                    {
                        var searchTerm = searchTermList[index];
                        if (eventRec.SourceName.Contains(searchTerm) || eventRec.Message.Contains(searchTerm) ||
                            eventRec.InsertionStrings.Contains(searchTerm))
                        {

                            eventRec.SearchTerm = "Word_" + index.ToString(CultureInfo.InvariantCulture);
                            eventRecordList.Add(eventRec);
                            break;
                        }
                    }
                }

                eventRecMergedList = (List<IEventRecord>)eventRecMergedList.Concat(eventRecordList).ToList();
                eventRecordList.Clear();
            }
            //var eventRecMergedList = EventRecordList.Concat(EventRecordList2).OrderBy(e => e.TimeGenerated);
            List<IEventRecord> returnEventList = new List<IEventRecord>(eventRecMergedList.OrderByDescending(e => e.TimeGenerated));
            return returnEventList;
        }
    }
}