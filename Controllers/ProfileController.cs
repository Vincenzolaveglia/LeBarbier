using LeBarbier.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace LeBarbier.Controllers
{
    public class ProfileController : Controller
    {
        private DBContext db = new DBContext();

        [HttpGet]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            var userId = int.Parse(User.Identity.Name);
            var userProfile = db.Users.Include(u => u.Reservations).FirstOrDefault(u => u.IdUser == userId);

            if (userProfile == null)
            {
                return HttpNotFound();
            }

            return View(userProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReservation(int id)
        {
            var userId = int.Parse(User.Identity.Name);

            var reservation = db.Reservations
                                .Include(r => r.ReservationServices)
                                .FirstOrDefault(r => r.IdReservation == id && r.Id_User == userId);

            if (reservation == null)
            {
                return HttpNotFound();
            }

            db.ReservationServices.RemoveRange(reservation.ReservationServices);

            db.Reservations.Remove(reservation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }
    }
}

