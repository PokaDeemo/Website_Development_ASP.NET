using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment_02.Models;

namespace Assignment_02.Controllers
{
    public class AdminTwoController : Controller
    {
        private ASSIGNMENT02Entities db = new ASSIGNMENT02Entities();

        // GET: AdminTwo
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Account);

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

            return View(bookings.ToList());
        }

        // GET: AdminTwo/Details/5
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
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: AdminTwo/Create
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

            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "FullName");
            return View();
        }

        // POST: AdminTwo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,BookingDate,TimeSlot,Department,DoctorName,DoctorEmail,AccountID,PatientName,PatientEmail,BookingStatus")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "FullName", booking.AccountID);
            return View(booking);
        }

        // GET: AdminTwo/Edit/5
        public ActionResult Edit(int? id)
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
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "FullName", booking.AccountID);
            return View(booking);
        }

        // POST: AdminTwo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,BookingDate,TimeSlot,Department,DoctorName,DoctorEmail,AccountID,PatientName,PatientEmail,BookingStatus")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "FullName", booking.AccountID);
            return View(booking);
        }

        // GET: AdminTwo/Delete/5
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
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: AdminTwo/Delete/5
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

            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
