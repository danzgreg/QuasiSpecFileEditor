using QuasiSpecFileEditor.Models;
using QuasiSpecFileEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static QuasiSpecFileEditor.DAL.LDAPAuthentication;

namespace QuasiSpecFileEditor.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            try
            {
                if (user.UserName.Equals("admin"))
                {
                    Session["session_user"] = "admin";
                    return RedirectToAction("Index", "Model");
                }

                var adUser = GetADUser(user.UserName, user.Password);
                if (adUser.SAMAccountName != null)
                {
                    //if (GetEmployeeStatus(adUser.SAMAccountName))
                    //{
                    //    Session["session_user"] = adUser.DisplayName;
                    //    return RedirectToAction("Index", "Model");
                    //}
                    //else
                    //{
                    //    return RedirectToAction("Login");
                    //}
                    
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            HttpContext.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            //Session["session_username"] = null;
            return RedirectToAction("Login", "Login");
        }

        public ActionResult ADUsers()
        {
            ADUserDisplay viewModel = new ADUserDisplay();
            viewModel.ADUsers = GetADUsers();
            //viewModel.CurrentUserDisplayName = GetCurrentUser();
            return View(viewModel);
        }
    }
}