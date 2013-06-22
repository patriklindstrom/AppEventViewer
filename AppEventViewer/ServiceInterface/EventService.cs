using System;
using System.Collections.Generic;
using System.Web;
using AppEventViewer.App_Start;
using AppEventViewer.Models;
using AppEventViewer.Repository;
using AppEventViewer.ServiceInterface;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace AppEventViewer.ServiceInterface
{
  
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
        /// <summary>
        /// Date and time from where the logs should be taken
        /// </summary>
        /// <value>The string must in yyyyMMddHHmmss eg 20130618102955 format to  be parsed as a DateTime. If null then defaults to - 1 hour</value>
        [ApiMember(Name = "from",
            Description =
                "Date and time from where the logs should be taken. The string must in yyyyMMddHHmmss eg 20130618102955 format to  be parsed as a DateTime. If null then defaults to - 1 hour"
            ,
            ParameterType = "path", DataType = "string", IsRequired = false)]
        public string From { get; set; }
        /// <summary>
        /// Date and time To in a pair of from where the logs should be taken
        /// </summary>
        /// <value>Has to be bigger then the from value and The string must be The string must in yyyyMMddHHmmss eg 20130618102955 parsed as a DateTime. if null then defaults to now </value>
        [ApiMember(Name = "to",
            Description =
                "Date and time to what where has to be bigger then the from value and The string must be The string must in yyyyMMddHHmmss eg 20130618102955 parsed as a DateTime. if null then defaults to now"
            ,
            ParameterType = "path", DataType = "string", IsRequired = false)]
        public string To { get; set; }
    }

    /// <summary>
    /// The Respons DTO for the Get EventRecordList DTO
    /// </summary>
    public class EventRecordListResponse
    {
        public List<IEventRecord> EventRecords { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    //Implementation
    /// <summary>
    /// This server get an optional pair of from and to datetimes strings. It then returns a List of filtered Events from logfile. It gets the data from a repository. This repository is injected 
    /// outside of this class. It only implements the Get request. 
    /// </summary>
    /// <seealso cref="Repository"/>
    public class EventService : Service
    {
        /// <summary>
        /// It injected by Funq IOC in AppHost config or the Testprojects similar.
        /// </summary>
        public IEventRepository Repository { get; set; } //                   
        public object Get(Events request)
        {
            //throw new NotImplementedException("This is a test");
            string fromTime = request.From ?? DateTime.Now.AddHours(-3).ToString(Global_Const.DATE_FORMAT);
            DateTime outFromTime = DateTime.ParseExact(fromTime, Global_Const.DATE_FORMAT,
                                                       System.Globalization.CultureInfo.InvariantCulture);
            string toTime = request.To ?? DateTime.Now.ToString(Global_Const.DATE_FORMAT);
            DateTime outToTime = DateTime.ParseExact(toTime, Global_Const.DATE_FORMAT,
                                                     System.Globalization.CultureInfo.InvariantCulture);
            return Repository.GetByTimeFilter(outFromTime, outToTime, Global_Const.MAXGETROWS, Global_Const.TIMEOUT_S);
        }
    }
}

