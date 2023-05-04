using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Assignment_02.Models;

namespace Assignment_02.Controllers
{
    public class HomeController : Controller
    {
        ASSIGNMENT02Entities db = new ASSIGNMENT02Entities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About us";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "contact us";

            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Services/Departments/Facility";

            return View();
        }
        public ActionResult Registration()
        {
            //ViewBag.Message = "Registration Page";

            return View();
        }


        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "AccountID,FullName,Email,AccountPassword,reAccountPassword,PersonAddress,PersonPhoneNo,Gender,BloodType,AccountRole,DOB")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Account accountDB)
        {
            if (ModelState.IsValid)
            {
                if (db.Accounts.Any(x => x.Email == accountDB.Email))
                {
                    ViewBag.SameEmailMsg = ("<script>alert('Email has been use before!.')</script>");
                    return View();
                }
                else
                {
                    db.Accounts.Add(accountDB);
                    db.SaveChanges();

                    Session["AccountIDSS"] = accountDB.AccountID.ToString();
                    Session["EmailSS"] = accountDB.Email.ToString();

                    ViewBag.CreateAccountMsg = ("<script>alert('Account Create Successfully!.')</script>");

                    return RedirectToAction("Index", "Home");
                }     
            }
            return View(accountDB);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
 }
