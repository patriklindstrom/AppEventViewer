using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using AppEventViewer.App_Start;
using AppEventViewer.Models;

namespace AppEventViewer.Repository
{
    public class Sql1Repository : ISqlRepository
    {
        public IAppConfig Config { get; set; } //injected hopefully buy IOC

        /// <summary>
        ///     It takes filtered event data from several windows servers. The servers name are inserted from Config class via DIP
        ///     It takes the content string filter from the Config class most likely webconfig. It then returns all event record
        ///     log data between two datetimes.
        ///     It uses VMI to query the servers
        /// </summary>
        /// <param name="fromTime">DataTime from where to start the search span. The time is included in the search</param>
        /// <param name="toTime">
        ///     DateTime to is bigger - more in future time then from. Marks end of time span filter uses the time
        ///     is included in search
        /// </param>
        /// <param name="maxRows">Not implemented yet</param>
        /// <param name="timeOutSec">Not implemented yet</param>
        /// <returns></returns>
        public List<ISql1Record> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec)
        {
            var sqlRecMergedList = new List<ISql1Record>();
            if (Config == null)
            {
                throw new NullReferenceException(
                    "There is no configuration object and therefore no list of servers. Config object  is null. It is the Func IOC that should have set this in the code. Something trivial is missing.");
            }
            if (Config.SQL1ServersToQuery == null)
            {
                throw new NullReferenceException("The list of servers from Config is null. That is bad.");
            }
            var sqlRecordList = new List<ISql1Record>();

            //TODO break out this logic so it can be tested.
            // Here comes three nested list. Its for every server, for every event record check every searchTerm if ok then add it.
            var searchTermList = Config.SQL1FilterTerm;
            int sTCount = searchTermList.Count;
            bool multiSearch = (searchTermList.Count() > 1);
            foreach (var serv in Config.SQL1ServersToQuery)
            {
                // create a new SqlConnection object with the appropriate connection string 
                var sqlConn = new SqlConnection(serv.connectionString);
                sqlConn.Open();
                var rs = rs.Get(); // get data from 
                foreach (var mo in rs)
                {
                    var sqlRec = new Sql1Record();
                    //Filter out all data that contains records that we are interested in.
                    for (int index = 0; index < sTCount; index++)
                    {
                        var searchTerm = searchTermList[index];
                        if (sqlRec.JobName.StartsWith(searchTerm))
                        {
                            sqlRec.SearchTerm = "Word_" + index.ToString(CultureInfo.InvariantCulture);
                            sqlRecordList.Add(sqlRec);
                            break;
                        }
                    }
                }

                sqlRecMergedList = (List<ISql1Record>)sqlRecordList.Concat(sqlRecordList).ToList();
                sqlRecordList.Clear();
                sqlConn.Close();
            }
            //var eventRecMergedList = EventRecordList.Concat(EventRecordList2).OrderBy(e => e.TimeGenerated);
            var returnEventList = new List<ISql1Record>(sqlRecMergedList.OrderByDescending(e => e.TimeGenerated));
            return returnEventList;
        }
    }

    public interface ISqlRepository
    {
        IAppConfig Config { get; set; }
        List<ISql1Record> GetByTimeFilter(DateTime fromTime, DateTime toTime, int maxRows, int timeOutSec);
    }
}