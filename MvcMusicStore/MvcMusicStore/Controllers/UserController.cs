using MvcMusicStore.Utilities.ClassModels;
using MvcMusicStore.Utilities.DatabaseUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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
                if(IsValid(ref user))
                {
                    string userData = string.Join("|",user.id, user.Email);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                      1,                                     
                      user.Email,                            
                      DateTime.Now,                          
                      DateTime.Now.AddMinutes(10),           
                      false,                          
                      userData,                              
                      FormsAuthentication.FormsCookiePath);  
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    //Response.Redirect(FormsAuthentication.GetRedirectUrl(user.Email, false));
                    

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
            return RedirectToAction("Index","Home");
        }

        private bool IsValid(ref UserModel userModel)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            
            var isValid = false;

            var user = UserDbUtilities.GetUser(userModel.Email);

            if (user != null)
            {
                if(user.Parola == crypto.Compute(userModel.Parola, user.CheieParola))
                {
                    isValid = true;
                    userModel.id = user.Id;
                }
            }

            return isValid;

        }
    }
}