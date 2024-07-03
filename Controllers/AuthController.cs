using LeBarbier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LeBarbier.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "FirstName, LastName, Email, PhoneNo, Password")] User user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DBContext())
                {
                    bool emailExists = db.Users.Any(u => u.Email == user.Email);
                    bool phoneNo = db.Users.Any(u => u.PhoneNo == user.PhoneNo);
                    bool passwordExists = db.Users.Any(u => u.Password == user.Password);

                    if (emailExists || phoneNo || passwordExists)
                    {
                        if (emailExists)
                        {
                            ModelState.AddModelError("Email", "L'indirizzo email è già stato registrato.");
                        }
                        if (phoneNo)
                        {
                            ModelState.AddModelError("PhoneNo", "Il numero di telefono è già stato registrato.");
                        }
                        if (passwordExists)
                        {
                            ModelState.AddModelError("Password", "La password è già utilizzata.");
                        }
                        return View(user);
                    }

                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Login", "Auth");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DBContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.IdUser.ToString(), true);
                        switch (user.Id_Role)
                        {
                            case 1:
                                return RedirectToAction("Index", "Admin");

                            case 2:
                                return RedirectToAction("Index", "Reservations");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Email o password non validi.");
                    }
                    return RedirectToAction("Index", "Reservations");
                }

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("WelcomePage", "Home");
        }
    }
}