using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuasiSpecFileEditor.DAL;
using QuasiSpecFileEditor.Models;

namespace QuasiSpecFileEditor.Controllers
{
    public class AlarmDescriptionController : Controller
    {
        private SpecFileDataContext db = new SpecFileDataContext();

        // GET: AlarmDescription
        public ActionResult Index()
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var alarmDescriptions = db.AlarmDescriptions.Include(a => a.Alarm).Include(a => a.Model);
            return View(alarmDescriptions.ToList());
        }

        // GET: AlarmDescription/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlarmDescription alarmDescription = db.AlarmDescriptions.Find(id);
            if (alarmDescription == null)
            {
                return HttpNotFound();
            }
            return View(alarmDescription);
        }

        // GET: AlarmDescription/Create
        public ActionResult Create()
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.AlarmID = new SelectList(db.Alarms, "AlarmID", "AlarmName");
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "CodeName");
            return View();
        }

        // POST: AlarmDescription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlarmDescriptionID,ModelID,AlarmID,Priority,Criteria,Hold,ParameterName,TargetLimit")] AlarmDescription alarmDescription)
        {
            if (ModelState.IsValid)
            {
                db.AlarmDescriptions.Add(alarmDescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlarmID = new SelectList(db.Alarms, "AlarmID", "AlarmName", alarmDescription.AlarmID);
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "CodeName", alarmDescription.ModelID);
            return View(alarmDescription);
        }

        // GET: AlarmDescription/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlarmDescription alarmDescription = db.AlarmDescriptions.Find(id);
            if (alarmDescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlarmID = new SelectList(db.Alarms, "AlarmID", "AlarmName", alarmDescription.AlarmID);
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "CodeName", alarmDescription.ModelID);
            return View(alarmDescription);
        }

        // POST: AlarmDescription/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlarmDescriptionID,ModelID,AlarmID,Priority,Criteria,Hold,ParameterName,TargetLimit")] AlarmDescription alarmDescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alarmDescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlarmID = new SelectList(db.Alarms, "AlarmID", "AlarmName", alarmDescription.AlarmID);
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "CodeName", alarmDescription.ModelID);
            return View(alarmDescription);
        }

        // GET: AlarmDescription/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlarmDescription alarmDescription = db.AlarmDescriptions.Find(id);
            if (alarmDescription == null)
            {
                return HttpNotFound();
            }
            return View(alarmDescription);
        }

        // POST: AlarmDescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlarmDescription alarmDescription = db.AlarmDescriptions.Find(id);
            db.AlarmDescriptions.Remove(alarmDescription);
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
