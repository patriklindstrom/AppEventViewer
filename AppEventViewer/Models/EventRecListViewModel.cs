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
        public string From { get; set; }
        /// <summary>
        /// Get the to in a from-to date and time for log events 
        /// </summary>
        /// <value>The value should be able to parse as a date time</value>
        public string To { get; set; }
    }

    public class EventReqModelBinder : IModelBinder
    {
        private EventReq _defaultReq ;
        private EventReq _eventReq;
        public EventReqModelBinder()
        {
            _defaultReq = new EventReq() { To = DateTime.Now.ToString(Global_Const.DATE_FORMAT), From = DateTime.Now.AddHours(-6).ToString(Global_Const.DATE_FORMAT) };
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
            var valueFrom = valFrom.AttemptedValue ?? _defaultReq.From;
            ValueProviderResult valTo = bindingContext.ValueProvider.GetValue("to");
            string valueTo = String.Empty;
            if (valTo == null)

            {
                valueTo = _defaultReq.To;
            }
            else
            {
                valueTo = valTo.AttemptedValue;
            }        
            
            // if from tom Time is given in sortable format with T in middle remove it. 
            var valueReq = new EventReq() { To = valueTo.Replace("T", String.Empty), From = valueFrom.Replace("T", String.Empty) };
            return valueReq;
        }
    }
}