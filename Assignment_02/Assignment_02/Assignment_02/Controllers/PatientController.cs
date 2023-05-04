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
    public class PatientController : Controller
    {
        ASSIGNMENT02Entities db = new ASSIGNMENT02Entities();

        // GET: Patient
        public ActionResult Index()
        {
                var bookings = db.Bookings.Include(b => b.Account);

                if (Session["Login"] != null)
                {
                    Models.Account obj = (Models.Account)Session["Login"];

                    if (obj.AccountRole != "Patient" && obj.AccountRole != "Admin")
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

        // GET: Patient/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Patient" && obj.AccountRole != "Admin")
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

        // GET: Patient/Create
        public ActionResult Create()
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Patient" && obj.AccountRole != "Admin")
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

        // POST: Patient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                if (db.Bookings.Any(x => x.PatientEmail == booking.PatientEmail))
                {
                    ViewBag.SameEmailBookingMsg = ("<script>alert('Email has been use before!.')</script>");
                    return View();
                }
                else
                {
                    db.Bookings.Add(booking);
                    db.SaveChanges();

                    Session["AccountIDSS"] = booking.AccountID.ToString();
                    Session["EmailSS"] = booking.PatientEmail.ToString();

                    ViewBag.CreateAppointmentMsg = ("<script>alert('Appointment Create Successfully!.')</script>");

                    return RedirectToAction("Index", "Patient");
                }
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "FullName", booking.AccountID);
            return View(booking);
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Patient" && obj.AccountRole != "Admin")
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

        // POST: Patient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,BookingDate,TimeSlot,Department,DoctorName,DoctorEmail,AccountID,PatientName,PatientEmail,BookingStatus")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                if (db.Bookings.Any(x => x.PatientEmail == booking.PatientEmail))
                {
                    ViewBag.SameEmailMsg3 = ("<script>alert('Email has been use before!.')</script>");
                    return View();
                }
                else
                {
                    db.Entry(booking).State = EntityState.Modified;
                    int edit = db.SaveChanges();
                    Session["AccountIDSS"] = booking.AccountID.ToString();
                    Session["EmailSS"] = booking.PatientEmail.ToString();

                    if (edit > 0)
                    {
                        ViewBag.PatientEditMsg2 = ("<script>alert('Edit Appointment Successfully!.')</script>");

                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
                        ViewBag.PatientEditMsg = ("<script>alert('Something is wrong with the input...')</script>");
                    }
                }
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "FullName", booking.AccountID);
            return View(booking);
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Patient" && obj.AccountRole != "Admin")
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

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Login"] != null)
            {
                Models.Account obj = (Models.Account)Session["Login"];

                if (obj.AccountRole != "Patient" && obj.AccountRole != "Admin")
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
