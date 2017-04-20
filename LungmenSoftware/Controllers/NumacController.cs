using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models.Service;
using Newtonsoft.Json;

namespace LungmenSoftware.Controllers
{
    public class NumacController : Controller
    {
        private NumacDataService dataService=new NumacDataService();
        private ChangeRequestService crSerice = new ChangeRequestService();

        // GET: Chassis
        public ActionResult Index()
        {

            
            return View();
        }

        //For AngularJS
        public JsonResult GetNumacData()
        {
            var data = dataService.GetFirmwareList();
           
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // For AngularJS
        public JsonResult GetSystemPanelList()
        {
            var data = dataService.GetSystemPanelList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // For AngularJS
        public JsonResult GetSubSystemById(string systemId)
        {
            var data = dataService.GetSubSystemById(new Guid(systemId));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetModulesById(string chassisId)
        {
            var data = dataService.GetModulesById(new Guid(chassisId));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNumacChangeRequestRecordById(string ChassisBoardId)
        {
            var data = crSerice.GetNumacChangeRequestRecordById(ChassisBoardId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NumacChangeRequestHistory(string moduleId)
        {
            var data = crSerice.GetNumacChangeRequestRecordById(moduleId);

            return View(data);
        }

        // GET: Chassis/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Chassis chassis = db.Chassis.Find(id);
        //    if (chassis == null)
        //    {
        //        return HttpNotFound();
                     
        //    }
        //    return View(chassis);
        //}

        // GET: Chassis/Create
        //public ActionResult CreateChassis()
        //{
        //    return View();
        //}

        // POST: Chassis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateChassis([Bind(Include = "ChassisId,ChassisName,ChasisSerialNumber,Equipment,Panel")] Chassis chassis)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var lastModifiedBy = "IP"+Request.UserHostAddress+", 主機"+Request.UserHostName;
        //        var lastModifiedDate = DateTime.Now.ToString();
        //        chassis.ChassisId = Guid.NewGuid();
        //        chassis.LastModifiedBy = lastModifiedBy;
        //        chassis.LastModifiedDate = lastModifiedDate;
        //        db.Chassis.Add(chassis);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(chassis);
        //}

        //public ActionResult CreateChassisBoard()
        //{
        //    ViewBag.Chassis = GetChassisList(); 
        //    return View();
        //}

        //private IEnumerable<SelectListItem> GetChassisList()
        //{
        //    var list = dataService.GetChassis().Select(c=>new SelectListItem()
        //    {
        //        Value = c.ChassisId.ToString(),
        //        Text = c.ChassisName
        //    });
        //    return list;
        //}
        // POST: Chassis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateChassisBoard([Bind(Include = "ChassBoardName")] ChassisBoard chassisBoard, params string[] Chassis)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var lastModifiedBy = "IP" + Request.UserHostAddress + ", 主機" + Request.UserHostName;
        //        var lastModifiedDate = DateTime.Now.ToString();
        //        chassisBoard.ChassisBoardId = Guid.NewGuid();
        //        chassisBoard.ChassisId = new Guid(Chassis[0]);
        //        var chassis = dataService.GetChassisById(chassisBoard.ChassisId);
        //        chassisBoard.ChassisName = chassis.ChassisName;
        //        chassisBoard.LastModifiedBy = lastModifiedBy;
        //        chassisBoard.LastModifiedDate = lastModifiedDate;
        //        db.ChassisBoards.Add(chassisBoard);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Chassis = GetChassisList();
        //    return View(chassisBoard);
        //}

        //public ActionResult CreateEPROM()
        //{
        //    ViewBag.ChassisBoards = GetChassisBoardList();
        //    return View();
        //}

        //private IEnumerable<SelectListItem> GetChassisBoardList()
        //{
        //    var list = dataService.GetChassisBoards().Select(b => new SelectListItem()
        //    {
        //        Value = b.ChassisBoardId.ToString(),
        //        Text = b.ChassBoardName+"-"+b.ChassisName
        //    });
        //    return list;
        //}

        //// POST: Chassis/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateEPROM([Bind(Include = "SocketLocation,EPROMAssembly,EPROMAssemblyRev,PartsListRev,EPROMProgram,EPROMProgramRev,EPROMSerialNumber")] EPROM eprom, params string[] ChassisBoard)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var lastModifiedBy = "IP" + Request.UserHostAddress + ", 主機" + Request.UserHostName;
        //        var lastModifiedDate = DateTime.Now.ToString();
        //        eprom.EPROMId = Guid.NewGuid();
        //        eprom.ChassisBoardId=new Guid(ChassisBoard[0]);
        //        ChassisBoard chassisBoard = dataService.GetChassisBoardById(eprom.ChassisBoardId);
        //        eprom.ChassisBoardName = chassisBoard.ChassBoardName;
        //        eprom.ChassisName = chassisBoard.ChassisName;
        //        eprom.LastModifiedBy = lastModifiedBy;
        //        eprom.LastModifiedDate = lastModifiedDate;
        //        db.EPROMs.Add(eprom);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ChassisBoards = GetChassisBoardList();
        //    return View(eprom);
        //}

        //// GET: Chassis/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Chassis chassis = db.Chassis.Find(id);
        //    if (chassis == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(chassis);
        //}

        //// POST: Chassis/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ChassisId,ChassisName,ChasisSerialNumber,Equipment,Panel")] Chassis chassis)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(chassis).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(chassis);
        //}

        //// GET: Chassis/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Chassis chassis = db.Chassis.Find(id);
        //    if (chassis == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(chassis);
        //}

        //// POST: Chassis/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Chassis chassis = db.Chassis.Find(id);
        //    db.Chassis.Remove(chassis);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
