using AutoMapper;
using MeetingRoomBooking.Service.DTOs;
using MeetingRoomBooking.Service.Interfaces;
using MeetingRoomBooking.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using prjMeetingRoomBooking.Utility;
using prjMeetingRoomBooking.ViewModels;
using System.Globalization;
using IResult = Evertrust.Core.Common.AspNetCore.Misc.IResult;

namespace prjMeetingRoomBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;
        private IMapper _mapper;
        public BookingController(
            IBookingService bookingService,
            IMapper mapper,
            IUserService userService,
            IRoomService roomService)
        {
            _bookingService = bookingService;
            _mapper = mapper;
            _userService = userService;
            _roomService = roomService;
        }
        public IActionResult Index()
        {           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CUserViewModel login)
        {
            
            UserDto result = await _userService.GetByIdAsync(login.UserId);
            if (result == null)
            {
                return View();
            }
               
            string json = JsonConvert.SerializeObject(result);
            HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, json);
            
            return RedirectToAction("CreateNewBooking");
            
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                HttpContext.Session.Remove(CDictionary.SK_LOGINED_USER);
            }                       
            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> checkUserPwd(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return Json("error: No Data");
            }
                
            try
            {                
                UserDto user = JsonConvert.DeserializeObject<UserDto>(data);
                if ( await _userService.IsExist(user) )
                    return Json("t");

                return Json("f");
            }
            catch
            {
                return Json("error: Invalid Data");
            }                       
            
        }
        public IActionResult CreateNewBooking()
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                return RedirectToAction("Index");
            }
                
            string data = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (string.IsNullOrEmpty(data))
            {
                return RedirectToAction("Index");
            }
                
            CUserViewModel user = JsonConvert.DeserializeObject<CUserViewModel>(data);
            ViewBag.UserId=user.UserId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBooking(BookingViewModel booking)
        {
            if (booking == null)
            {
                return View();
            }                            

            BookingDto room = new BookingDto();
            room=_mapper.Map<BookingDto>(booking);

            Evertrust.Core.Common.AspNetCore.Misc.IResult result = await _bookingService.CreateAsync(room);

            if (result.Success)
            {
                string st = $"{booking.StartDate}T{booking.StartTime}:00";
                return RedirectToAction("DailyView", "Check", new { date = $"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}" });
            }

            return View();

        }
        
        public async Task<IActionResult> getAllMeetingRoomsOp()
        {
            try
            {
                var rooms= await _roomService.GetAllRoomsAsync();
                var result= rooms.Select(r => new { r.RoomName, r.RoomId });
                return Json(result);
            }
            catch(Exception err)
            {
                return Json($"error:{err.Message}");
            }
            
        }
       
        public async Task<IActionResult> getAllRoomManagers()
        {                        
            try
            {                
                var managers = await _roomService.GetAllManagersAsync();
                return Json(managers);
            }
            catch (Exception err)
            {
                return Json($"error:{err.Message}");
            }
        }
       
        public IActionResult checkLoginIsBookingUserId(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return Json($"error: No Data!");
            }

            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                return RedirectToAction("Index");
            }
                
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            CUserViewModel login = JsonConvert.DeserializeObject<CUserViewModel>(json);

            if (data != login.UserId)
            {
                return Json("f");
            }
                
            return Json("t");

        }
        public async Task<IActionResult> UpdateBooking(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("DailyView", "Check");
            }
                
            BookingDto booking = await _bookingService.GetByIdAsync((int)id);
            if (booking == null)
            {
                return RedirectToAction("DailyView", "Check");
            }
                
            BookingViewModel cb = new BookingViewModel();
            cb = _mapper.Map<BookingViewModel>(booking);

            return View(cb);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBooking(BookingViewModel booking)
        {
            if (booking == null)
            {
                return View();
            }
                
            BookingDto room = await _bookingService.GetByIdAsync(booking.Id);
            string st = $"{booking.StartDate}T{booking.StartTime}:00";
            if (room == null)
            {                
                return RedirectToAction("DailyView", "Check", new { date = $"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}" });
            }
            
            room=_mapper.Map<BookingDto>(booking);
            Evertrust.Core.Common.AspNetCore.Misc.IResult result = await _bookingService.UpdateAsync(room);

            if (result.Success)
            {
                return RedirectToAction("DailyView", "Check", new { date = $"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}" });

            }

            return View();

        }

        public async Task<IActionResult> DeleteBooking(int? id)
        {
            DateTime st = DateTime.Today;
            if (id != null)
            {
                BookingDto booking = await _bookingService.GetByIdAsync((int)id);
                if (booking == null)
                {
                    return RedirectToAction("DailyView", "Check");
                }
                    
                st = (DateTime)booking.StartTime;

                IResult result = await _bookingService.DeleteAsync((int)id);

            }

            return RedirectToAction("DailyView", "Check", new { date = $"{st.ToString("yyyy-MM-dd")}" });
        }
       
        public async Task<IActionResult> checkBookingTime(string data)
        {
            //檢查是否時間已被預約
            CheckPeriodDto period = JsonConvert.DeserializeObject<CheckPeriodDto>(data);
            if (period == null)
            {
                return Json($"error: No Data!");
            }
                
            try
            {                
                if (await _bookingService.IsBooked(period))
                {
                    return Json("Y");
                }
                else
                {
                    return Json("N");
                }
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
