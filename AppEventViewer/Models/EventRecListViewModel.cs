namespace AppEventViewer.Models
{
    public interface IEventRecListViewModel
    {
        string Category { get; set; }
        string Server { get; set; }
        string EventCode { get; set; }
        string EventType { get; set; }
        string InsMessage { get; set; }
        string Logfile { get; set; }
        string Msg { get; set; }
        string RecordNr { get; set; }
        string Source { get; set; }
        string Time { get; set; }
        string Type { get; set; }
    }

    public class EventRecListViewModel : IEventRecListViewModel
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