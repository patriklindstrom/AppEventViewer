using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AppEventViewer.App_Start;

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
            _defaultReq = new EventReq() { To = DateTime.Now.ToString(Global_Const.DATE_FORMAT), From = DateTime.Now.AddHours(-1).ToString(Global_Const.DATE_FORMAT) };
        }
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(EventReq))
            {
                return _defaultReq;
            }

            ValueProviderResult val = bindingContext.ValueProvider.GetValue("from");
            if (val == null)
            {
                return _defaultReq;
            }
            string key = val.RawValue as string;
            if (key == null)
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.ModelName, "Wrong value type");
                return _defaultReq;
            }
           
            if (_defaultReq !=null )
            {
                bindingContext.Model = _defaultReq;
                return _defaultReq;
            }

            bindingContext.ModelState.AddModelError(
                bindingContext.ModelName, "Cannot convert value to EventReq");
            return _defaultReq;
        }
    }
}