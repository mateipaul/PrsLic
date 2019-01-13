using MvcMusicStore.Utilities.ClassModels;
using MvcMusicStore.Utilities.DatabaseUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcMusicStore.Controllers
{
    public class UserController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if(IsValid(user.Email, user.Parola))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    return RedirectToAction("Index", "Home");
                }
                else{
                    ModelState.AddModelError("", "Datale introduse nu sunt corecte");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                UserDbUtilities.AddUserToDb(user);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string email,string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            
            var isValid = false;

            var user = UserDbUtilities.GetUser(email);

            if (user != null)
            {
                if(user.Parola == crypto.Compute(password, user.CheieParola))
                {
                    isValid = true;
                }
            }

            return isValid;

        }
    }
}