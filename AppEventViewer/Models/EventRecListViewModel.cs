using System.Collections.Generic;

namespace AppEventViewer.Models
{
    public interface IEventRecListViewModel
    {
        List<EventRec> EventList { get; }
    }

    public class EventRecListViewModel : IEventRecListViewModel
    {
        private List<EventRec> _eventList;

        public EventRecListViewModel()
        {
            _eventList = new List<EventRec>();
        }

        public List<EventRec> EventList
        {
            get { return _eventList; }
        }
    }

    public class EventRec 
    {
        public string Category { get; set; }
        public string Server { get; set; }
        public string EventCode { get; set; }
        public string EventType { get; set; }
        public string InsMessage { get; set; }
        public string Logfile { get; set; }
        public string Msg { get; set; }
        public string RecordNr { get; set; }
        public string Source { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }
        public string SearchTermNr { get; set; }
    }
    /// <summary>
    /// A DTO class that stores the request parameters for the datetime from and to
    /// similar to the Events class
    /// </summary>
    /// <seealso cref="AppEventViewer.ServiceInterface.Events"/>
    public class EventReq
    {
        /// <summary>
        /// Get the from date and time for log events 
        /// </summary>
        /// <value>The value should be able to parse as a date time</value>
        public string From { get; set; }
        /// <summary>
        /// Get the to in a from-to date and time for log events 
        /// </summary>
        /// <value>The value should be able to parse as a date time</value>
        public string To { get; set; }
    }
}