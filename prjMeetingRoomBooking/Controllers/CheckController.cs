using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using prjMeetingRoomBooking.Models;
using prjMeetingRoomBooking.ViewModels;
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
            {
                try
                {
                    getDate=Convert.ToDateTime(date);
                }
                catch
                {
                    getDate = DateTime.Today;
                }
            }

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
        public IActionResult DailyView(string? date)
        {
            DateTime getDate = DateTime.Today;
            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    getDate=Convert.ToDateTime(date);
                }
                catch
                {
                    getDate = DateTime.Today;
                }
            }                
            ViewBag.txtDate=getDate.ToString("yyyy-MM-dd");
            ViewBag.txtWeekDay=getDate.ToString("ddd");
            return View();
        }
        public IActionResult MonthlyView()
        {
            return View();
        }
        public IActionResult getBookingRecords(string data)
        {
            if (string.IsNullOrEmpty(data))
                return Json($"error: No Data!");
            try
            {
                CPeriod period = JsonConvert.DeserializeObject<CPeriod>(data);
                if (period == null)
                    return Json("");

                DateTime getFirstDay = (DateTime)period.FirstDay;
                DateTime lastDay = ((DateTime)period.LastDay).AddDays(1);

                IEnumerable<TMeeingBooking> records = _db.TMeeingBookings.Where(r =>
                    r.StartTime>=getFirstDay && r.EndTime<lastDay
                ).Select(r => r).OrderBy(r => r.RoomId);

                return Json(records);
            }
            catch(Exception ex)
            {
                return Json($"error:{ex.Message}");
            }
            
            
        }
        private int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
