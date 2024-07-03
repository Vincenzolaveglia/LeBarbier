using LeBarbier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; 

namespace LeBarbier.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private DBContext db = new DBContext();
        public ActionResult Index()
        {

            var placedReservations = db.Reservations.Include(r => r.Users).ToList();

            return View(placedReservations);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null) 
            {
                return HttpNotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new DBContext())
            {
                var reservation = db.Reservations.Include(r => r.ReservationServices)
                                                 .SingleOrDefault(r => r.IdReservation == id);

                if (reservation != null)
                {
                    db.ReservationServices.RemoveRange(reservation.ReservationServices);

                    db.Reservations.Remove(reservation);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

    }
}
