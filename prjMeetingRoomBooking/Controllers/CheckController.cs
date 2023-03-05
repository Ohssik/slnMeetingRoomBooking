using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using prjMeetingRoomBooking.Models;
using System.Globalization;

namespace prjMeetingRoomBooking.Controllers
{
    public class CheckController : Controller
    {
        private readonly testContext _db;
        public CheckController(testContext db)
        {
            _db = db;
        }
        public IActionResult WeeklyView(string? date)
        {
            DateTime getDate = DateTime.Today;
            if (!string.IsNullOrEmpty(date))
                getDate=Convert.ToDateTime(date);

            DateTime firstDay = getDate.AddDays(-((int)getDate.DayOfWeek));
            ViewBag.Sun=firstDay.ToString("yyyy-MM-dd");
            ViewBag.Mon=firstDay.AddDays(1).ToString("yyyy-MM-dd");
            ViewBag.Tue=firstDay.AddDays(2).ToString("yyyy-MM-dd");
            ViewBag.Wed=firstDay.AddDays(3).ToString("yyyy-MM-dd");
            ViewBag.Thur=firstDay.AddDays(4).ToString("yyyy-MM-dd");
            ViewBag.Fri=firstDay.AddDays(5).ToString("yyyy-MM-dd");
            ViewBag.Sat=firstDay.AddDays(6).ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult DailyView()
        {
            return View();
        }
        public IActionResult MonthlyView()
        {
            return View();
        }
        public IActionResult getThisWeekRecords(string data)
        {
            DateTime getFirstDay = DateTime.Today;
            if (!string.IsNullOrEmpty(data))
                getFirstDay=Convert.ToDateTime(data);
            
            DateTime lastDay = getFirstDay.AddDays(6);

            IEnumerable<TMeeingBooking> records = _db.TMeeingBookings.Where(r =>
                r.StartTime>=getFirstDay && r.EndTime<lastDay
            ).Select(r => r).OrderBy(r => r.RoomId);

            return Json(records);
        }
        private int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
