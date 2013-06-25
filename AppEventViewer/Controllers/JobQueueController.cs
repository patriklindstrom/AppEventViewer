using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEventViewer.Controllers
{
    public class JobQueueController : Controller
    {
        //
        // GET: /JobQueue/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /JobQueue/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /JobQueue/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /JobQueue/Create

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
        // GET: /JobQueue/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /JobQueue/Edit/5

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
        // GET: /JobQueue/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /JobQueue/Delete/5

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
