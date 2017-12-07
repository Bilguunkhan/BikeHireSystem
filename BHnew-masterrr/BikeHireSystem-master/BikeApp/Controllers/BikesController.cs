using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BikeApp.Models;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace BikeApp.Controllers
{
    public class BikesController : Controller
    {
        private BikeHireDatabaseEntities db = new BikeHireDatabaseEntities();
        private BikeHireDatabaseEntities2 hdb = new BikeHireDatabaseEntities2();
        private BikeHireDatabaseEntities3 db2 = new BikeHireDatabaseEntities3();

        // GET: Bikes
        public ActionResult Index(string searchString)
        {
            var Bikes = from m in db.Bikes
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Bikes = Bikes.Where(s => s.Bike_Model.Contains(searchString));
                return View(Bikes.ToList());
            }

            else { return View(db.Bikes.ToList()); }
        }

        public ActionResult EditBikes()
        {
            return View( "Index");
        }

        public ActionResult AvailableBikes(string searchString)
        {
            
           var Bikes = from m in db.Bikes
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Bikes = Bikes.Where(s => s.Bike_Model.Contains(searchString));
            }

            return View(Bikes.ToList());
        }

        //GET: Hire
        public ActionResult Hire(int? id)
        {
    
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (User.Identity.IsAuthenticated == true)
            {

                return View("HiringInfo");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //POST: Hire
        [HttpPost, ActionName("Hire")]
        [ValidateAntiForgeryToken]
        public ActionResult HireConfirmed([Bind(Include = "fullName, sDate, eDate, CardInfo, FbikeId")] HiringInfo hBike)
        {
            try
            {
                Bike bike = db.Bikes.Find(hBike.FbikeId);
                bike.Status = "H";
                db.SaveChanges();

                UsersAndBike unb = new UsersAndBike();
                unb.Name = hBike.fullName;
                unb.BikeId = bike.Bike_Id;
                unb.BikeModel = bike.Bike_Model;
                unb.StardDate = Convert.ToDateTime(hBike.sDate);
                unb.EndDate = Convert.ToDateTime(hBike.eDate);
                
                db2.UsersAndBikes.Add(unb);
                db2.SaveChanges();


                if (User.IsInRole("Admin") || User.IsInRole("Receptionist"))
                {
                    hdb.HiringInfoes.Add(hBike);
                    hdb.SaveChanges();
                    return RedirectToAction("Hire", "Bikes", hBike.FbikeId);
                }

                return RedirectToAction("AvailableBikes");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        // GET: Bikes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }

        // GET: Bikes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Bike_Id,Bike_Model,Description,Cost,Status")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                db.Bikes.Add(bike);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bike);
        }

        // GET: Bikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }

        // POST: Bikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Bike_Id,Bike_Model,Description,Cost,Status")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bike);
        }

        // GET: Bikes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bike bike = db.Bikes.Find(id);
            db.Bikes.Remove(bike);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PrintReport(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HiringInfo hBike = new HiringInfo();
            hBike = hdb.HiringInfoes.Find(id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(hBike);
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
