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
    public class UsersAndBikesController : Controller
    {
        private BikeHireDatabaseEntities3 db = new BikeHireDatabaseEntities3();

        // GET: UsersAndBikes
        public ActionResult Index(string searchString)
        {
            
            var Bikes = from m in db.UsersAndBikes
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Bikes = Bikes.Where(j => j.BikeModel.Contains(searchString) || j.Name.Contains(searchString));
                return View(Bikes.ToList());
            }

            else { return View(db.UsersAndBikes.ToList()); }
        }

        // GET: UsersAndBikes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersAndBike usersAndBike = db.UsersAndBikes.Find(id);
            if (usersAndBike == null)
            {
                return HttpNotFound();
            }
            return View(usersAndBike);
        }

        // GET: UsersAndBikes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersAndBikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,BikeModel,BikeId,StardDate,EndDate")] UsersAndBike usersAndBike)
        {
            if (ModelState.IsValid)
            {
                db.UsersAndBikes.Add(usersAndBike);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usersAndBike);
        }

        // GET: UsersAndBikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersAndBike usersAndBike = db.UsersAndBikes.Find(id);
            if (usersAndBike == null)
            {
                return HttpNotFound();
            }
            return View(usersAndBike);
        }

        // POST: UsersAndBikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,BikeModel,BikeId,StardDate,EndDate")] UsersAndBike usersAndBike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersAndBike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usersAndBike);
        }

        // GET: UsersAndBikes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersAndBike usersAndBike = db.UsersAndBikes.Find(id);
            if (usersAndBike == null)
            {
                return HttpNotFound();
            }
            return View(usersAndBike);
        }

        // POST: UsersAndBikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersAndBike usersAndBike = db.UsersAndBikes.Find(id);
            db.UsersAndBikes.Remove(usersAndBike);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult filterUser(string searchString)
        {

            var users = from m in db.UsersAndBikes
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(w => w.Name.Contains(searchString));
            }

            return View(users.ToList());
        }

        public ActionResult filterBike(string searchString)
        {

            var bikes = from m in db.UsersAndBikes
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                bikes = bikes.Where(g => g.BikeModel.Contains(searchString));
            }

            return View(bikes.ToList());
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
