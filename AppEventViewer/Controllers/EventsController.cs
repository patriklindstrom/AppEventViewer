using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppEventViewer.Models;
using AppEventViewer.ServiceInterface;

namespace AppEventViewer.Controllers
{
    public class EventsController : Controller
    {
        //
        // GET: /Events/
        /// <summary>
        /// Gets an optional from and to show the repositories list of log events. The main get methods. Shows list of all events
        /// </summary>
        /// <param name="eventReq">A class for parameters they are strings but represent from to dates for the log events</param>
        /// <seealso cref="EventReq"/>
        public ActionResult Index(EventReq eventReq)
        {
            var eventRecListViewModel = new EventRecListViewModel();
            return View(eventRecListViewModel);
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
