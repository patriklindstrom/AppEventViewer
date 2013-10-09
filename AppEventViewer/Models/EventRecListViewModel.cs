using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using AppEventViewer.App_Start;
using ServiceStack.Text;

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
        public DateTime From { get; set; }
        /// <summary>
        /// Get the to in a from-to date and time for log events 
        /// </summary>
        /// <value>The value should be able to parse as a date time</value>
        public DateTime To { get; set; }
    }

    public class EventReqModelBinder : IModelBinder
    {
        private EventReq _defaultReq ;
        private EventReq _eventReq;
        public EventReqModelBinder()
        {
            _defaultReq = new EventReq() { To = DateTime.Now, From = DateTime.Now.AddHours(-6) };
        }
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(EventReq))
            {
                return _defaultReq;
            }
            ValueProviderResult valFrom = bindingContext.ValueProvider.GetValue("from");
            if (valFrom == null)
            {
                return _defaultReq;
            }
            string sValFrom = (string) valFrom.AttemptedValue;
            DateTime dValFrom;
            DateTime dValueFrom;
            dValueFrom = DateTime.TryParse(sValFrom, out dValFrom) ? dValFrom : _defaultReq.From;
          
            ValueProviderResult valTo = bindingContext.ValueProvider.GetValue("to");
            string sValTo = (string)valFrom.AttemptedValue;
            DateTime dValTo;
            DateTime dValueTo;
            dValueTo = DateTime.TryParse(sValTo, out dValTo) ? dValTo : _defaultReq.From;
                 
            // if from tom Time is given in sortable format with T in middle remove it. 
            var valueReq = new EventReq() { To = dValueTo, From = dValueFrom };
            return valueReq;
        }
    }
}