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
using QuasiSpecFileEditor.Repository;
using QuasiSpecFileEditor.UnitOfWork;

namespace QuasiSpecFileEditor.Controllers
{
    public class ParameterController : Controller
    {
        private SpecFileDataContext db = new SpecFileDataContext();

        private UnitOfWork<SpecFileDataContext> unitOfWork = new UnitOfWork<SpecFileDataContext>();
        private GenericRepository<Parameter> repository;

        public ParameterController()
        {
            repository = new GenericRepository<Parameter>(unitOfWork);
        }

        public ActionResult Index()
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View(repository.GetAll());
        }

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
            Parameter parameter = repository.GetById(id);
            if (parameter == null)
            {
                return HttpNotFound();
            }
            return View(parameter);
        }

        public ActionResult Create()
        {
            if (Session["session_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParameterID,FieldName,UpperLimit,UpperLimitDefect,LowerLimit,LowerLimitDefect,NegFactor,NegFactorDefect,OpReqUpload,ParamTest,CSVHeaderName")] Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                if (!CheckIfExist(parameter)) {
                    repository.Insert(parameter);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("FieldName", "This field name already exist");
                    return View(parameter);
                }
            }

            return View(parameter);
        }

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
            Parameter parameter = repository.GetById(id);
            if (parameter == null)
            {
                return HttpNotFound();
            }
            return View(parameter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParameterID,FieldName,UpperLimit,UpperLimitDefect,LowerLimit,LowerLimitDefect,NegFactor,NegFactorDefect,OpReqUpload,ParamTest,CSVHeaderName")] Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                repository.SetEntryModified(parameter);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(parameter);
        }

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
            Parameter parameter = repository.GetById(id);
            if (parameter == null)
            {
                return HttpNotFound();
            }
            return View(parameter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parameter parameter = repository.GetById(id);
            repository.Delete(parameter);
            unitOfWork.Save();
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

        private bool CheckIfExist(Parameter parameter)
        {
            if (repository.GetAll().Select(c => c.FieldName).Contains(parameter.FieldName))
            {
                return true;
            }
            return false;
        }
    }
}
