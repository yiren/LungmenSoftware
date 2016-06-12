using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models.NUMACFirmware;
using LungmenSoftware.Models.Service;

namespace LungmenSoftware.Controllers
{
    public class ChassisController : Controller
    {
        private NumacFirewareDbContext db = new NumacFirewareDbContext();
        private NumacDataService dataService=new NumacDataService();

        // GET: Chassis
        public ActionResult Index()
        {

            var data=dataService.GetFirmwareList();
            return View(data);
        }

        // GET: Chassis/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chassis chassis = db.Chassis.Find(id);
            if (chassis == null)
            {
                return HttpNotFound();
                     
            }
            return View(chassis);
        }

        // GET: Chassis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chassis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChassisId,ChassisName,ChasisSerialNumber,Equipment,Panel")] Chassis chassis)
        {
            if (ModelState.IsValid)
            {
                chassis.ChassisId = Guid.NewGuid();
                db.Chassis.Add(chassis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chassis);
        }

        // GET: Chassis/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chassis chassis = db.Chassis.Find(id);
            if (chassis == null)
            {
                return HttpNotFound();
            }
            return View(chassis);
        }

        // POST: Chassis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChassisId,ChassisName,ChasisSerialNumber,Equipment,Panel")] Chassis chassis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chassis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chassis);
        }

        // GET: Chassis/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chassis chassis = db.Chassis.Find(id);
            if (chassis == null)
            {
                return HttpNotFound();
            }
            return View(chassis);
        }

        // POST: Chassis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Chassis chassis = db.Chassis.Find(id);
            db.Chassis.Remove(chassis);
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
