using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Assignment_02.Models;

namespace Assignment_02.Controllers
{
    public class LoginController : Controller
    {
        ASSIGNMENT02Entities db = new ASSIGNMENT02Entities();

        // GET: Login

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account accountDB)
        {
            var checklogin = db.Accounts.Where(i => i.Email.Equals(accountDB.Email) && i.AccountPassword.Equals(accountDB.AccountPassword) && i.AccountRole.Equals(accountDB.AccountRole)).FirstOrDefault();

            if (checklogin != null)
            {
                Session["AccountIDSS"] = accountDB.AccountID.ToString();
                Session["EmailSS"] = accountDB.Email.ToString();
                Session["AccountSS"] = accountDB.AccountRole.ToString();

                if (accountDB.AccountRole == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if(accountDB.AccountRole == "Doctor")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if(accountDB.AccountRole == "Patient")
                {
                    return RedirectToAction("Index", "Patients");
                }
                else
                {
                    ViewBag.LoginMsg = ("<script>alert('Invalid Role, Please try again!.')</script>");
                }
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMsg = ("<script>alert('Wrong Email or Password or Account Role!.')</script>");
                //ViewBag.Notification = "Wrong Email or Password or Account Role, Please Try Again!";
            }   
            return View();
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account accountDB)
        {
            var checklogin = db.Accounts.Where(i => i.Email.Equals(accountDB.Email) && i.AccountPassword.Equals(accountDB.AccountPassword) && i.AccountRole.Equals(accountDB.AccountRole)).FirstOrDefault();

            if (checklogin != null)
            {
                Session["Login"] = accountDB;
                Session["EmailSS"] = accountDB.Email;
                Session["AccountSS"] = accountDB.AccountRole;

                switch (accountDB.AccountRole)
                {
                    case "Admin":
                        return RedirectToAction("Index", "Admin");

                    case "Patient":
                        return RedirectToAction("Index", "Patient");

                    case "Doctor":
                        return RedirectToAction("Index", "Doctor");

                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.LoginMsg = ("<script>alert('Wrong Email or Password or Account Role!.')</script>");
                //ViewBag.Notification = "Wrong Email or Password or Account Role, Please Try Again!";
            }
            return View();
        }
    }
}
