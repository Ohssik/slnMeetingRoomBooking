using Microsoft.AspNetCore.Mvc;
using MeetingRoomBooking.Web.Parameters;
using System.Globalization;
using MeetingRoomBooking.Service.Interfaces;
using MapsterMapper;
using MeetingRoomBooking.Service.ParameterDtos;

namespace MeetingRoomBooking.Web.Controllers
{
    public class CheckController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public CheckController(IMapper mapper,
            IBookingService bookingService)
        {
            _mapper = mapper;
            _bookingService = bookingService;
        }

        public IActionResult WeeklyView(string? date)
        {
            var getDate = DateTime.Today;

            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    getDate = Convert.ToDateTime(date);
                }
                catch
                {
                    getDate = DateTime.Today;
                }
            }

            var firstDay = getDate.AddDays(-((int)getDate.DayOfWeek));
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
            var getDate = DateTime.Today;

            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    getDate = Convert.ToDateTime(date);
                }
                catch
                {
                    getDate = DateTime.Today;
                }
            }

            ViewBag.txtDate = getDate.ToString("yyyy-MM-dd");
            ViewBag.txtWeekDay = getDate.ToString("ddd");
            return View();
        }

        public IActionResult MonthlyView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetBookingRecords([FromBody] GetBookingParameter period)
        {
            try
            {
                if (period == null)
                {
                    return Json("");
                }

                period.LastDay = period.LastDay.GetValueOrDefault().AddDays(1);
                var records = await _bookingService.GetBookingListAsync(_mapper.Map<GetBookingParameterDto>(period));

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
