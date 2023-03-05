using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjMeetingRoomBooking.Models;
using prjMeetingRoomBooking.ViewModels;
using System.Globalization;

namespace prjMeetingRoomBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly testContext _db;
        public BookingController(testContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {           
            return View();
        }
        [HttpPost]
        public IActionResult Index(CBooking booking)
        {
            return View();
        }
        public IActionResult CreateNewBooking()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewBooking(CBooking booking)
        {
            if (booking == null)
                return View();

            TMeeingBooking room = new TMeeingBooking();
            room.RoomId=booking.RoomId;
            room.Subject=booking.Subject;
            room.BookingUserId=booking.BookingUserId;
            string st = $"{booking.startDate}T{booking.startT}:00";
            string et = $"{booking.endDate}T{booking.endT}:00";
            room.StartTime=Convert.ToDateTime(st);
            room.EndTime=Convert.ToDateTime(et);
            _db.TMeeingBookings.Add(room);
            try
            {
                _db.SaveChanges();
                return RedirectToAction("WeeklyView", "Check", new {date=$"{DateTime.Today.ToString("yyyy-MM-dd")}"});
            }
            catch (Exception err)
            {
                return View();
            }
        }

        public IActionResult getAllMeetingRoomsOp()
        {
            try
            {
                var rooms = _db.TMeetingRooms.Select(r => new { r.RoomName, r.RoomId });
                return Json(rooms);
            }
            catch(Exception err)
            {
                return Json($"error:{err.Message}");
            }
            
        }
        public IActionResult getAllRoomManagers()
        {                        
            try
            {
                var managers = _db.ViewManager2Rooms.Select(m => m);
                return Json(managers);
            }
            catch (Exception err)
            {
                return Json($"error:{err.Message}");
            }
        }
        public IActionResult UpdateBooking(int? id)
        {
            if(id == null)
                return RedirectToAction("WeeklyView","Check");
            TMeeingBooking booking = _db.TMeeingBookings.FirstOrDefault(b => b.Id==(int)id);
            if(booking == null)
                return RedirectToAction("WeeklyView", "Check");
            CBooking cb = new CBooking();
            cb.MeeingBooking = booking;
            cb.startDate=((DateTime)booking.StartTime).ToString("yyyy-MM-dd");
            cb.startT=((DateTime)booking.StartTime).ToString("HH:mm");
            cb.endDate=((DateTime)booking.EndTime).ToString("yyyy-MM-dd");
            cb.endT=((DateTime)booking.EndTime).ToString("HH:mm");
            return View(cb);
        }
        [HttpPost]
        public IActionResult UpdateBooking(CBooking booking)
        {
            if (booking == null)
                return View();

            TMeeingBooking room = _db.TMeeingBookings.FirstOrDefault(b=>b.Id==booking.Id);
            string st = $"{booking.startDate}T{booking.startT}:00";
            if (room == null)
                return RedirectToAction("WeeklyView", "Check", new { date = $"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}" });

            room.RoomId=booking.RoomId;
            room.Subject=booking.Subject;
            //room.BookingUserId=booking.BookingUserId;            
            string et = $"{booking.endDate}T{booking.endT}:00";
            room.StartTime=Convert.ToDateTime(st);
            room.EndTime=Convert.ToDateTime(et);
            
            try
            {
                _db.SaveChanges();
                return RedirectToAction("WeeklyView", "Check", new { date = $"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}" });
            }
            catch (Exception err)
            {
                return View();
            }
        }
        public IActionResult DeleteBooking(int? id)
        {
            if (id != null)
            {
                TMeeingBooking booking = _db.TMeeingBookings.FirstOrDefault(b => b.Id==(int)id);
                if (booking == null)
                    return RedirectToAction("WeeklyView", "Check");
                _db.TMeeingBookings.Remove(booking);
                _db.SaveChanges();
            }
            return RedirectToAction("WeeklyView", "Check");
        }
        private int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
