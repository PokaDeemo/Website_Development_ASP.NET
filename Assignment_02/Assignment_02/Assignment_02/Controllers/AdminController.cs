using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Assignment_02.Models;

namespace Assignment_02.Controllers
{
    public class AdminController : Controller
    {
        ASSIGNMENT02Entities db = new ASSIGNMENT02Entities();

        public ActionResult getUserList()
        {
            var users = db.Accounts.ToList();
            return View(users);
        }

        // GET: Admin
        public ActionResult Index()
        {
           if (Session["Login"] != null)
           {
               Models.Account obj = (Models.Account)Session["Login"];

               if (obj.AccountRole != "Admin")
               {
                  return RedirectToAction("Login", "Login");
               }
           }
          else
          {
              return RedirectToAction("Login", "Login");
          }

        return View(db.Accounts.ToList());
        }

        /*public ActionResult Index(Account accountDB)
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
                else if (accountDB.AccountRole == "Doctor")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (accountDB.AccountRole == "Patient")
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
            return View(db.Accounts.ToList());
        }*/

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Admin")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

      
        // GET: Admin/Create
        public ActionResult Create()
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Admin")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account accountDB)
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Admin")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

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

                    return RedirectToAction("Index", "Admin");
                }
            }
            return View(accountDB);
        }

    
        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Admin")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            var edit = db.Accounts.Where(model => model.AccountID == id).FirstOrDefault();

            return View(edit);
        }

        /*
        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,FullName,Email,AccountPassword,PersonAddress,PersonPhoneNo,Gender,BloodType,AccountRole,DOB")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }*/

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Account accountDB)
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Admin")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
          

            if (ModelState.IsValid)
            {
                if (db.Accounts.Any(x => x.Email == accountDB.Email))
                {
                    ViewBag.SameEmailMsg = ("<script>alert('Email has been use before!.')</script>");
                    return View();
                }
                else
                {
                    db.Entry(accountDB).State = EntityState.Modified;
                    int edit = db.SaveChanges();

                    Session["AccountIDSS"] = accountDB.AccountID.ToString();
                    Session["EmailSS"] = accountDB.Email.ToString();

                    if (edit > 0)
                    {
                        ViewBag.EditMsg2 = ("<script>alert('Edit Account Successfully!.')</script>");

                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewBag.EditMsg = ("<script>alert('Something is wrong with the input...')</script>");
                    }
                }
            }
            return View(accountDB);
        }

   
        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Admin")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

      
        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Admin")
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
