using Microsoft.AspNetCore.Mvc;
using prjMeetingRoomBooking.Models;

namespace prjMeetingRoomBooking.Controllers
{
    public class CheckController : Controller
    {
        private readonly testContext _db;
        public CheckController(testContext db)
        {
            _db = db;
        }
        public IActionResult WeeklyView()
        {
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
    }
}
