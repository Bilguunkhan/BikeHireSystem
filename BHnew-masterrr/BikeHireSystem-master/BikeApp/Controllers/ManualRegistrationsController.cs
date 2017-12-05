using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BikeApp.Models;

namespace BikeApp.Controllers
{
    public class ManualRegistrationsController : Controller
    {
        private BikeHireDatabaseEntities1 db = new BikeHireDatabaseEntities1();
        [Authorize(Roles = "Receptionist, Admin")]
        // GET: ManualRegistrations
        public ActionResult Index(string searchString)
        {
            var Customers = from m in db.ManualRegistrations
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Customers = Customers.Where(s => s.Name_.Contains(searchString));
                return View(Customers.ToList());
            }

            else { return View(db.ManualRegistrations.ToList()); };
        }

        // GET: ManualRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManualRegistration manualRegistration = db.ManualRegistrations.Find(id);
            if (manualRegistration == null)
            {
                return HttpNotFound();
            }
            return View(manualRegistration);
        }

        // GET: ManualRegistrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManualRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name_,Address,Phone")] ManualRegistration manualRegistration)
        {
            if (ModelState.IsValid)
            {
                db.ManualRegistrations.Add(manualRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manualRegistration);
        }

        // GET: ManualRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManualRegistration manualRegistration = db.ManualRegistrations.Find(id);
            if (manualRegistration == null)
            {
                return HttpNotFound();
            }
            return View(manualRegistration);
        }

        // POST: ManualRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name_,Address,Phone")] ManualRegistration manualRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manualRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manualRegistration);
        }

        // GET: ManualRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManualRegistration manualRegistration = db.ManualRegistrations.Find(id);
            if (manualRegistration == null)
            {
                return HttpNotFound();
            }
            return View(manualRegistration);
        }

        // POST: ManualRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManualRegistration manualRegistration = db.ManualRegistrations.Find(id);
            db.ManualRegistrations.Remove(manualRegistration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
