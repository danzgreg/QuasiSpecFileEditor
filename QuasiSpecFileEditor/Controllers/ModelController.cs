using QuasiSpecFileEditor.DAL;
using QuasiSpecFileEditor.Models;
using QuasiSpecFileEditor.Repository;
using QuasiSpecFileEditor.UnitOfWork;
using QuasiSpecFileEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;

namespace QuasiSpecFileEditor.Controllers
{
    public class ModelController : Controller
    {
        private SpecFileDataContext db = new SpecFileDataContext();

        private UnitOfWork<SpecFileDataContext> unitOfWork = new UnitOfWork<SpecFileDataContext>();
        private GenericRepository<Model> repository;
        private IGroupRepository groupRepository;
        private IAlarmDescriptionRepository alarmDescriptionRepository;
        private IPartRepository partRepository;
        private IParameterRepository parameterRepository;
        private IAlarmRepository alarmRepository;

        public ModelController()
        {
            //If you want to use Generic Repository with Unit of work
            repository = new GenericRepository<Model>(unitOfWork);

            //If you want to use Specific Repository with Unit of work
            groupRepository = new GroupRepository(unitOfWork);
            alarmDescriptionRepository = new AlarmDescriptionRepository(unitOfWork);
            partRepository = new PartRepository(unitOfWork);
            parameterRepository = new ParameterRepository(unitOfWork);
            alarmRepository = new AlarmRepository(unitOfWork);
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

            ViewBag.GroupTBD = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = repository.GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        
        #region Create Methods
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModelID,CodeName")] Model model)
        {
            try
            {
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    repository.Insert(model);
                    unitOfWork.Save();
                    unitOfWork.Commit();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                unitOfWork.Rollback();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateGroup(Group group, int[] SelectedParts, int[] SelectedParameters)
        {
            try
            {
                unitOfWork.CreateTransaction();

                if (ModelState.IsValid)
                {
                    groupRepository.Insert(group);
                    unitOfWork.Save();

                    groupRepository.CreateGroup(group, SelectedParts, SelectedParameters);
                    unitOfWork.Save();
                    unitOfWork.Commit();
                    return RedirectToAction("Details", new { id = group.ModelID });
                }
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateAlarm(AlarmDescription alarmDescription)
        {
            if (ModelState.IsValid)
            {
                if (alarmDescription.AlarmDescriptionID == 0)
                {
                    alarmDescriptionRepository.CreateAlarmDescription(alarmDescription);
                    unitOfWork.Save();
                }
                return RedirectToAction("Details", new { id = alarmDescription.ModelID });
            }
            return View();
        }
        #endregion

        #region Edit Methods
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = repository.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModelID,CodeName")] Model model)
        {
            if (ModelState.IsValid)
            {
                repository.Update(model);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult EditGroup(Group group, int[] SelectedParts, int[] SelectedParameters)
        {
            try
            {
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    groupRepository.EditGroup(group, SelectedParts, SelectedParameters);
                    unitOfWork.Save();
                    unitOfWork.Commit();
                    return RedirectToAction("Details", new { id = group.ModelID });
                }
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
            return View();
        }

        public ActionResult EditAlarm(AlarmDescription alarmDescription)
        {
            if (ModelState.IsValid)
            {
                alarmDescriptionRepository.UpdateAlarmDescription(alarmDescription);
                unitOfWork.Save();

                return RedirectToAction("Details", new { id = alarmDescription.ModelID });

            }
            return View();
        }
        #endregion

        #region Delete Methods
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = repository.GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Model model = repository.GetById(id);
            repository.Delete(model);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public JsonResult DeleteGroup(int groupID)
        {
            var model = groupRepository.GetById(groupID);
            var parts = partRepository.GetAll().Where(g => g.GroupID == groupID).ToList();
            unitOfWork.CreateTransaction();

            try
            {
                if (parts.Count != 0 || parts != null)
                {
                    foreach (var item in parts)
                    {
                        item.GroupID = null;
                    }
                }
                groupRepository.Delete(model);

                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAlarmDescription(int alarmDescriptionID)
        {
            AlarmDescription model = alarmDescriptionRepository.GetById(alarmDescriptionID);
            alarmDescriptionRepository.Delete(model);
            unitOfWork.Save();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Form Display Methods
        public ActionResult AddGroupForm(int modelID)
        {
            AddModelGroupDisplay modelGroupDisplay = new AddModelGroupDisplay();
            modelGroupDisplay.Group = new Group();
            //modelGroupDisplay.Parts = db.Parts.Where(c => c.GroupID == null).ToList();
            modelGroupDisplay.Parts = partRepository.GetAll().Where(c => c.GroupID == null && c.ModelID == modelID).ToList();
            //modelGroupDisplay.Parameters = db.Parameters.ToList();
            modelGroupDisplay.Parameters = parameterRepository.GetAll().ToList();

            modelGroupDisplay.Group.ModelID = modelID;

            return PartialView(modelGroupDisplay);
        }

        public ActionResult EditGroupForm(int groupID)
        {
            AddModelGroupDisplay modelGroupDisplay = new AddModelGroupDisplay();
            //modelGroupDisplay.Group = db.Groups.Find(groupID);
            modelGroupDisplay.Group = groupRepository.GetById(groupID);
            modelGroupDisplay.Parts = partRepository.GetAll().Where(c => c.GroupID == null && c.ModelID == modelGroupDisplay.Group.ModelID).ToList();
            //modelGroupDisplay.Parameters = db.Parameters.ToList();
            modelGroupDisplay.Parameters = parameterRepository.GetAll().ToList();

            return PartialView(modelGroupDisplay);
        }

        public ActionResult EditAlarmForm(int alarmDescriptionID)
        {
            AlarmDescriptionDisplay viewModel = new AlarmDescriptionDisplay();
            //viewModel.AlarmDescription = db.AlarmDescriptions.Find(alarmDescriptionID);
            viewModel.AlarmDescription = alarmDescriptionRepository.GetById(alarmDescriptionID);

            return PartialView(viewModel);
        }

        public ActionResult GroupParameterForm(int groupID)
        {
            GroupParameterDisplay viewModel = new GroupParameterDisplay();
            //viewModel.Parameters = db.Groups.Where(c => c.GroupID == groupID).Single().Parameters.ToList();
            viewModel.Parameters = groupRepository.GetAll().Where(c => c.GroupID == groupID).Single().Parameters.ToList();
            return PartialView(viewModel);
        }

        public ActionResult AddAlarmForm(int modelID)
        {
            AlarmDescriptionDisplay viewModel = new AlarmDescriptionDisplay();
            //viewModel.Alarms = db.Alarms.ToList();
            viewModel.Alarms = alarmRepository.GetAll().ToList();
            //viewModel.MyAlarms = db.AlarmDescriptions.Where(c => c.ModelID == modelID).Select(a => a.Alarm).ToList();
            viewModel.MyAlarms = alarmDescriptionRepository.GetAll().Where(c => c.ModelID == modelID).Select(a => a.Alarm).ToList();
            viewModel.AlarmDescription = new AlarmDescription();
            viewModel.AlarmDescription.ModelID = modelID;

            return PartialView(viewModel);
        }
        #endregion

        #region SpecFile Printer Methods
        public JsonResult PrintSpecFile(int modelID)
        {
            GenerateSpecFileXML(modelID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public void GenerateSpecFileXML(int modelID)
        {
            var specModel = db.Models.Where(c => c.ModelID == modelID)
                .Include(i => i.Groups)
                .Include(i => i.AlarmDescriptions)
                .SingleOrDefault();

            string specFileName = $"{specModel.CodeName}_SpecFile.xml";
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", null)
            );

            XElement root = new XElement("SpecLimits");
            root.Add(new XElement("FileVersion", "2.0.0"),
                new XElement("CodeName", specModel.CodeName));


            foreach (var group in specModel.Groups)
            {
                List<String> PNs = new List<String>();

                foreach (var part in group.Parts)
                {
                    PNs.Add(part.PN);
                }

                XElement model = new XElement("Model",
                    new XElement("PN", string.Join(",", PNs)));
                foreach (var parameter in group.Parameters)
                {
                    model.Add(new XElement("Parameters",
                        new XElement("FieldName", parameter.FieldName),
                        new XElement("UpperLImit", parameter.UpperLimit),
                        new XElement("UpperLimitDefect", parameter.UpperLimitDefect),
                        new XElement("LowerLimit", parameter.LowerLimit),
                        new XElement("LowerLimitDefect", parameter.LowerLimitDefect),
                        new XElement("NegFactor", parameter.NegFactor),
                        new XElement("NegFactorDefect", parameter.NegFactorDefect),
                        new XElement("OpReqUpload", parameter.OpReqUpload),
                        new XElement("ParamTest", parameter.ParamTest),
                        new XElement("CSVHeaderName", parameter.CSVHeaderName)
                    ));
                }
                root.Add(model);
            }

            XElement alarms = new XElement("AllAlarms");

            foreach (var alarm in specModel.AlarmDescriptions)
            {
                alarms.Add(new XElement("Alarm",
                    new XElement("AlarmName", alarm.Alarm.AlarmName),
                    new XElement("Priority", alarm.Priority),
                    new XElement("Criteria", alarm.Criteria),
                    new XElement("Comment", alarm.Alarm.Comment),
                    new XElement("Hold", alarm.Hold),
                    new XElement("ParameterName", alarm.ParameterName),
                    new XElement("TargetLimit", alarm.TargetLimit)));
            }
            root.Add(alarms);
            doc.Add(root);
            ForceTags(doc);
            doc.Save(Path.Combine(ConfigurationManager.AppSettings.Get("OUTPUT_DIR_PATH"), specFileName));

        }

        private void ForceTags(XDocument document)
        {
            foreach (XElement childElement in
                from x in document.DescendantNodes().OfType<XElement>()
                where x.IsEmpty
                select x)
            {
                childElement.Value = string.Empty;
            }
        }
        #endregion

        #region Helper Methods
        public ActionResult CheckGroupName(Group group)
        {
            if (groupRepository.GetAll().Where(c => c.ModelID == group.ModelID).Select(c => c.GroupName).Contains(group.GroupName))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

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
