using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppEventViewer.ServiceInterface;

namespace AppEventViewer.Controllers
{
    public class EventsController : Controller
    {
        //
        // GET: /Events/
        /// <summary>
        /// The main get methods. Shows list of all events
        /// </summary>
        /// <param name="eventReq">A class for parameters they are strings but represent from to dates for the log events</param>
        /// <seealso cref="EventReq"/>
        public ActionResult Index(EventReq eventReq)
        {
            return View();
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
        public string   From { get; set; }
        /// <summary>
        /// Get the to in a from-to date and time for log events 
        /// </summary>
        /// <value>The value should be able to parse as a date time</value>
        public string To { get; set; }
    }
}
