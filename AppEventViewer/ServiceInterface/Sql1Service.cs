using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppEventViewer.App_Start;
using AppEventViewer.Models;
using AppEventViewer.Repository;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace AppEventViewer.ServiceInterface
{
   
    /// <summary>
    /// The Request DTO for the Get SQL1RecordList DTO
    /// </summary>
    [Api("Service Description")]
    [Route("/Sql1/{from}/{to}", "GET", Summary = @"Resultset from SQL statement. Showing sqlagent jobhistory or other sql related logs ",
        Notes = "The Sql statement is in the webconfig")]
    public class Sql1 : IReturn<EventRecordListResponse>
    {       
            /// <summary>
            /// Date and time from where the sqlrows should be taken
            /// </summary>
            /// <value>The string must in yyyyMMddHHmmss eg 20130618102955 format to  be parsed as a DateTime.r</value>
            [ApiMember(Name = "from",
                Description =
                    "Date and time from where the logs should be taken. The string must in yyyyMMddHHmmss eg 20130618102955 format to  be parsed as a DateTime."
                ,
                ParameterType = "path", DataType = "dateTime", IsRequired = true)]
            public string From { get; set; }
            /// <summary>
            /// Date and time To in a pair of from where the logs should be taken
            /// </summary>
            /// <value>Has to be bigger then the from value and The string must be The string must in yyyyMMddHHmmss eg 20130618102955 parsed as a DateTime. </value>
            [ApiMember(Name = "to",
                Description =
                    "Date and time to what where has to be bigger then the from value and The string must be The string must in yyyyMMddHHmmss eg 20130618102955 parsed as a DateTime."
                ,
                ParameterType = "path", DataType = "dateTime", IsRequired = true)]
            public string To { get; set; }
    }
    /// <summary>
    /// The Respons DTO for the Get EventRecordList DTO
    /// </summary>
    public class Sql1RecordListResponse
    {
        public List<ISql1Record> IsSql1Records { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
    //Implementation
    /// <summary>
    /// This server get a pair of from and to datetimes strings. It then returns a List of filtered Events from logfile. It gets the data from a repository. This repository is injected 
    /// outside of this class. It only implements the Get request. 
    /// </summary>
    /// <seealso cref="Repository"/>
    public class Sql1Service : Service
    {
        /// <summary>
        /// It injected by Funq IOC in AppHost config or the Testprojects similar.
        /// </summary>
        public ISql1Repository Repository { get; set; } //                   
        public object Get(Events request)
        {
            //throw new NotImplementedException("This is a test");
            string fromTime = request.From ?? DateTime.Now.AddHours(-1).ToString(Global_Const.DATE_FORMAT);
            DateTime outFromTime = DateTime.ParseExact(fromTime, Global_Const.DATE_FORMAT,
                                                       System.Globalization.CultureInfo.InvariantCulture);
            string toTime = request.To ?? DateTime.Now.ToString(Global_Const.DATE_FORMAT);
            DateTime outToTime = DateTime.ParseExact(toTime, Global_Const.DATE_FORMAT,
                                                     System.Globalization.CultureInfo.InvariantCulture);
            var returnList = Repository.GetByTimeFilter(outFromTime, outToTime, Global_Const.MAXGETROWS, Global_Const.TIMEOUT_S);
            var eventRecListResp = new EventRecordListResponse();
            eventRecListResp.EventRecords = returnList;
            return eventRecListResp;
        }
    }
}