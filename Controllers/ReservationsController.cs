using LeBarbier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeBarbier.Controllers
{
    public class ReservationsController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            var userId = int.Parse(User.Identity.Name);
            var reservations = db.Reservations.Where(r => r.Id_User == userId).ToList();
            var user = db.Users.Find(userId);
            ViewBag.Name = user.FirstName;
            return View(reservations);
        }

        [HttpGet]
        public ActionResult Create(DateTime selectedDate)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            DateTime currentDate = selectedDate;

            ViewBag.Services = db.OfferedServices.ToList();
            ViewBag.SelectedDate = currentDate.ToString("yyyy-MM-dd");

            var allTimes = new List<TimeSpan>
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(12, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(16, 0, 0),
                new TimeSpan(17, 0, 0),
                new TimeSpan(18, 0, 0)
            };

            var bookedTimes = db.Reservations
                                .Where(r => r.ReservationDate == currentDate)
                                .Select(r => r.ReservationTime)
                                .ToList();

            var availableTimes = allTimes.Except(bookedTimes).ToList();
            ViewBag.AvailableTimes = availableTimes;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int serviceId, DateTime reservationDate, string reservationTime)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            var userId = int.Parse(User.Identity.Name);
            var timeSpan = TimeSpan.Parse(reservationTime);

            var reservation = new Reservation
            {
                Id_User = userId,
                ReservationDate = reservationDate,
                ReservationTime = timeSpan
            };

            db.Reservations.Add(reservation);
            db.SaveChanges();

            var reservationService = new ReservationService
            {
                Id_Reservation = reservation.IdReservation,
                Id_OfferedServices = serviceId
            };

            db.ReservationServices.Add(reservationService);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            var reservation = db.Reservations.Find(id);
            if (reservation != null)
            {
                db.Reservations.Remove(reservation);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult GetReservations()
        {

            var reservations = db.Reservations
                .Select(r => new
                {
                    IdService = db.ReservationServices
                                  .Where(rs => rs.Id_Reservation == r.IdReservation)
                                  .Select(rs => rs.Id_OfferedServices)
                                  .FirstOrDefault(),
                    r.ReservationDate,
                    r.ReservationTime
                })
                .ToList()
                .Select(r => new
                {
                    title = db.OfferedServices.FirstOrDefault(s => s.IdOfferedServices == r.IdService)?.ServiceName ?? "Unknown Service",
                    start = r.ReservationDate.Add(r.ReservationTime),
                }).ToList();

            return Json(reservations, JsonRequestBehavior.AllowGet);
        }


    }
}
