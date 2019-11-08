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
    public class PartController : Controller
    {
        private SpecFileDataContext db = new SpecFileDataContext();

        // GET: Part
        public ActionResult Index()
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var parts = db.Parts.Include(p => p.Group).Include(p => p.Model);
            return View(parts.ToList());
        }

        // GET: Part/Details/5
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
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // GET: Part/Create
        public ActionResult Create()
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            //ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "CodeName");
            return View();
        }

        // POST: Part/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartID,PN,ModelID,GroupID")] Part part)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(part);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", part.GroupID);
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "CodeName", part.ModelID);
            return View(part);
        }

        // GET: Part/Edit/5
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
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            //ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", part.GroupID);
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "CodeName", part.ModelID);
            return View(part);
        }

        // POST: Part/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartID,PN,ModelID,GroupID")] Part part)
        {
            if (ModelState.IsValid)
            {
                db.Entry(part).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", part.GroupID);
            ViewBag.ModelID = new SelectList(db.Models, "ModelID", "CodeName", part.ModelID);
            return View(part);
        }

        // GET: Part/Delete/5
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
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // POST: Part/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Part part = db.Parts.Find(id);
            db.Parts.Remove(part);
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
