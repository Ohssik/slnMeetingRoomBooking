using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public IActionResult Index(TUser login)
        {
            TUser result = _db.TUsers.FirstOrDefault(u => u.UserId==login.UserId && u.UserPwd == login.UserPwd);
            if (result==null)
                return View();
            string json = JsonConvert.SerializeObject(result);
            HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, json);
            return RedirectToAction("CreateNewBooking");
            
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))            
                HttpContext.Session.Remove(CDictionary.SK_LOGINED_USER);
            
            return RedirectToAction("Index");
        }
        public IActionResult checkUserPwd(string data)
        {
            if(string.IsNullOrEmpty(data))
                return Json("error: No Data");
            try
            {
                TUser user = JsonConvert.DeserializeObject<TUser>(data);
                TUser result = _db.TUsers.FirstOrDefault(u => u.UserId==user.UserId && u.UserPwd == user.UserPwd);
                if (result==null)
                    return Json("f");
                
                return Json("t");
            }
            catch
            {
                return Json("error: Invalid Data");
            }                       
            
        }
        public IActionResult CreateNewBooking()
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
                return RedirectToAction("Index");
            string data = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (string.IsNullOrEmpty(data))
                return RedirectToAction("Index");
            TUser user = JsonConvert.DeserializeObject<TUser>(data);
            ViewBag.UserId=user.UserId;

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
                return RedirectToAction("DailyView", "Check", new {date=$"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}"});
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
        public IActionResult checkLoginIsBookingUserId(string data)
        {
            if(string.IsNullOrEmpty(data))
                return Json($"error: No Data!");
            if(!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
                return RedirectToAction("Index");
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            TUser login = JsonConvert.DeserializeObject<TUser>(json);
            if (data != login.UserId)
                return Json("f");
            return Json("t");

        }
        public IActionResult UpdateBooking(int? id)
        {
            if(id == null)
                return RedirectToAction("DailyView", "Check");
            TMeeingBooking booking = _db.TMeeingBookings.FirstOrDefault(b => b.Id==(int)id);
            if(booking == null)
                return RedirectToAction("DailyView", "Check");
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
                return RedirectToAction("DailyView", "Check", new { date = $"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}" });

            room.RoomId=booking.RoomId;
            room.Subject=booking.Subject;
            //room.BookingUserId=booking.BookingUserId;            
            string et = $"{booking.endDate}T{booking.endT}:00";
            room.StartTime=Convert.ToDateTime(st);
            room.EndTime=Convert.ToDateTime(et);
            
            try
            {
                _db.SaveChanges();
                return RedirectToAction("DailyView", "Check", new { date = $"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}" });
            }
            catch (Exception err)
            {
                return View();
            }
        }
        public IActionResult DeleteBooking(int? id)
        {
            DateTime st = DateTime.Today;
            if (id != null)
            {
                TMeeingBooking booking = _db.TMeeingBookings.FirstOrDefault(b => b.Id==(int)id);
                if (booking == null)
                    return RedirectToAction("DailyView", "Check");
                st=(DateTime)booking.StartTime;
                _db.TMeeingBookings.Remove(booking);
                _db.SaveChanges();
            }
            return RedirectToAction("DailyView", "Check", new { date = $"{st.ToString("yyyy-MM-dd")}" });
        }
        public IActionResult checkBookingTime(string data)
        {
            //檢查是否時間已被預約
            CPeriod period = JsonConvert.DeserializeObject<CPeriod>(data);
            if (period == null)
                return Json($"error: No Data!");
            try
            {
                string targetDate = ((DateTime)period.FirstDay).ToString("yyyy-MM-dd");
                int target_st = (int)period.STime;
                int target_et = (int)period.ETime;
                IEnumerable<ViewTmeeingBooking> records = _db.ViewTmeeingBookings.Where(r => r.StartDate==targetDate);
                if(period.Id != null)
                    records = _db.ViewTmeeingBookings.Where(r => r.StartDate==targetDate && r.Id != (int)period.Id);
                foreach (ViewTmeeingBooking record in records)
                {
                    int st = Convert.ToInt32(record.StartTime);
                    int et = Convert.ToInt32(record.EndTime);
                    if ((target_st>=st && target_st<et) || (target_et>st && target_et<=et) || (target_st<=st && target_et>=et))
                    {
                        return Json("f");
                    }                    
                }
                return Json("t");
            }
            catch(Exception ex)
            {
                return Json($"error: {ex.Message}");
            }            
            
        }
        private int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
