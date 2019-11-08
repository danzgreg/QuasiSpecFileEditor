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
    public class AlarmController : Controller
    {
        private SpecFileDataContext db = new SpecFileDataContext();

        // GET: Alarm
        public ActionResult Index()
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(db.Alarms.ToList());
        }

        // GET: Alarm/Details/5
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
            Alarm alarm = db.Alarms.Find(id);
            if (alarm == null)
            {
                return HttpNotFound();
            }
            return View(alarm);
        }

        // GET: Alarm/Create
        public ActionResult Create()
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        // POST: Alarm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlarmID,AlarmName,Comment")] Alarm alarm)
        {
            if (ModelState.IsValid)
            {
                db.Alarms.Add(alarm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alarm);
        }

        // GET: Alarm/Edit/5
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
            Alarm alarm = db.Alarms.Find(id);
            if (alarm == null)
            {
                return HttpNotFound();
            }
            return View(alarm);
        }

        // POST: Alarm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlarmID,AlarmName,Comment")] Alarm alarm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alarm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alarm);
        }

        // GET: Alarm/Delete/5
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
            Alarm alarm = db.Alarms.Find(id);
            if (alarm == null)
            {
                return HttpNotFound();
            }
            return View(alarm);
        }

        // POST: Alarm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alarm alarm = db.Alarms.Find(id);
            db.Alarms.Remove(alarm);
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
