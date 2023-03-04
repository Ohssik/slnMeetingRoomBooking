using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjMeetingRoomBooking.Models;
using prjMeetingRoomBooking.ViewModels;

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
                return RedirectToAction();
            }
            catch(Exception err)
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
    }
}
