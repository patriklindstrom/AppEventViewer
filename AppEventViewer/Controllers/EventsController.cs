using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppEventViewer.App_Start;
using AppEventViewer.Models;
using AppEventViewer.ServiceInterface;
using ServiceStack.Configuration;
using ServiceStack.Mvc;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;

namespace AppEventViewer.Controllers
{
    public class EventsController : ControllerBase<CustomUserSession>
    {
       // public IAppConfig Config { get; set; } = //injected hopefully buy IOC
    //  public JsonServiceClient ServiceClient = new JsonServiceClient("http://localhost:60176/api/");
      //  public JsonServiceClient ServiceClient = new JsonServiceClient(Config.AbsoluteBaseUri);
        //public IEventRepository EventRepository; //injected by Func IOC
        //
        // GET: /Events/
        /// <summary>
        /// Gets an optional from and to show the repositories list of log events. The main get methods. Shows list of all events. 
        /// EventReqModelBinder translates the parameters and httpcontext and make a good EventReq object
        /// </summary>
        /// <param name="eventReq">A class for parameters they are strings but represent from to dates for the log events</param>
        /// <seealso cref="EventReq"/>
        /// <seealso cref="EventReqModelBinder"/>
        public ActionResult Index([ModelBinder(typeof(EventReqModelBinder))]EventReq eventReq)
        {
            // Todo all this must be moved to the a config of IAppConfig and injected with Funq IOC instead
            var appSettings = new AppSettings();
            string baseApiUrl = appSettings.Get("BaseApiUrl", "http://localhost:80/api/");
             JsonServiceClient ServiceClient = new JsonServiceClient(baseApiUrl);

                //injected by Func IOC
            ViewBag.Message = "Here is a list of all filtered events from all server nodes.";
            var eventRecListViewModel = new EventRecListViewModel();
            //Events from to datetime 
            var events = new Events {From = eventReq.From, To = eventReq.To};
            try
            {
                var response = ServiceClient.Get(events);
                //string respstatus;
                //string respMsg;
                //response.ResponseStatus(out respstatus, out respMsg);
                //if (respstatus.Equals(String.Empty))
                //{

                    foreach (var ev in response.EventRecords)
                    {
                        var evViewR = new EventRec
                        {
                            Category = ev.Category,
                            Server = ev.ComputerName,
                            EventCode = ev.EventCode,
                            EventType = ev.EventType,
                            InsMessage = ev.InsertionStrings,
                            Logfile = ev.Logfile,
                            Msg = ev.Message,
                            RecordNr = ev.RecordNumber,
                            Source = ev.SourceName,
                            Time = ev.TimeGenerated.Substring(0, 8) + "T" + ev.TimeGenerated.Substring(8, 6),
                            Type = ev.Type,
                            SearchTermNr = ev.SearchTerm
                        };
                        eventRecListViewModel.EventList.Add(evViewR);
                    } 
                //}
                //else
                //{
                //    throw new ServiceResponseException("Problem with Event Service Responsstatus:" + responsStatus);
                //}
               
            }
            catch (WebServiceException exception)
            {
                throw;
            }

            return View(eventRecListViewModel);
        }
        public ActionResult Lasthours(int hours)
        {
            // Todo all this must be moved to the a config of IAppConfig and injected with Funq IOC instead
            var appSettings = new AppSettings();
           var dynamicEventReq = new EventReq() { To = DateTime.Now, From = DateTime.Now.AddHours(-1 * hours) };
            string baseApiUrl = appSettings.Get("BaseApiUrl", "http://localhost:80/api/");
            JsonServiceClient ServiceClient = new JsonServiceClient(baseApiUrl);

            //injected by Func IOC
            ViewBag.Message = "Here is a list of all filtered events from all server nodes.";
            var eventRecListViewModel = new EventRecListViewModel();
            var events = new Events { From = dynamicEventReq.From, To = dynamicEventReq.To };
            try
            {
                var response = ServiceClient.Get(events);
                //string respstatus;
                //string respMsg;
                //response.ResponseStatus(out respstatus, out respMsg);
                //if (respstatus.Equals(String.Empty))
                //{
                var foo = "fum";
                foreach (var ev in response.EventRecords)
                {
                    var evViewR = new EventRec
                    {
                        Category = ev.Category,
                        Server = ev.ComputerName,
                        EventCode = ev.EventCode,
                        EventType = ev.EventType,
                        InsMessage = ev.InsertionStrings,
                        Logfile = ev.Logfile,
                        Msg = ev.Message,
                        RecordNr = ev.RecordNumber,
                        Source = ev.SourceName,
                        Time = ev.TimeGenerated.Substring(0, 8) + "T" + ev.TimeGenerated.Substring(8, 6),
                        Type = ev.Type,
                        SearchTermNr = ev.SearchTerm
                    };
                    eventRecListViewModel.EventList.Add(evViewR);
                }
                //}
                //else
                //{
                //    throw new ServiceResponseException("Problem with Event Service Responsstatus:" + responsStatus);
                //}

            }
            catch (WebServiceException exception)
            {
                throw;
            }

            return View("Index",eventRecListViewModel);
        }
        //
        // GET: /Events/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Events/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Events/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Events/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Events/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Events/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Events/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
