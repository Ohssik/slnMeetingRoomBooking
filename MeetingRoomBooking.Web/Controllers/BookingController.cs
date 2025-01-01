using MapsterMapper;
using MeetingRoomBooking.Service.Interfaces;
using MeetingRoomBooking.Service.ParameterDtos;
using MeetingRoomBooking.Service.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using MeetingRoomBooking.Web.Parameters;
using MeetingRoomBooking.Web.ViewModels;
using MeetingRoomBooking.Web.Models;
using System.Globalization;

namespace MeetingRoomBooking.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public BookingController(IUserService userService, IMapper mapper, 
            IBookingService bookingService, IRoomService roomService)
        {
            _mapper = mapper;
            _userService = userService;
            _bookingService = bookingService;
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(GetUserParameter parameter)
        {
            var parameterDto = _mapper.Map<GetUserParameterDto>(parameter);
            var result = await _userService.GetUserAsync(parameterDto);

            if (string.IsNullOrEmpty(result.UserId))
            {
                return View();
            }

            string json = JsonConvert.SerializeObject(result);
            HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, json);
            return RedirectToAction("CreateNewBooking");
        }

        public async Task<IActionResult> Logout()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))            
                HttpContext.Session.Remove(CDictionary.SK_LOGINED_USER);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CheckUserPwd([FromBody] GetUserParameter parameter)
        {
            try
            {
                var parameterDto = _mapper.Map<GetUserParameterDto>(parameter);
                var result = await _userService.GetUserAsync(parameterDto);

                if (string.IsNullOrEmpty(result.UserId))
                {
                    return Json("f");
                }

                return Json("t");
            }
            catch
            {
                return Json("error: Invalid Data");
            }                       
            
        }

        public async Task<IActionResult> CreateNewBooking()
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

            var user = JsonConvert.DeserializeObject<UserDto>(data);
            ViewBag.UserId = user?.UserId ?? string.Empty;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBooking(CBooking booking)
        {
            if (booking == null)
            {
                return View();
            }

            var parameter = new AddBookingParameterDto
            {
                RoomId = booking.RoomId,
                Subject = booking.Subject,
                BookingUserId = booking.BookingUserId,
                StartTime = DateTime.TryParse($"{booking.startDate}T{booking.startT}:00", out var st) ? st : null,
                EndTime = DateTime.TryParse($"{booking.endDate}T{booking.endT}:00", out var et) ? et : null,
            };

            try
            {
                await _bookingService.AddBookingAsync(parameter);
                return RedirectToAction("DailyView", "Check", new {date=$"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}"});
            }
            catch (Exception err)
            {
                return View();
            }
        }

        public async Task<IActionResult> GetAllMeetingRoomsOp()
        {
            try
            {
                var rooms = await _roomService.GetAllAsync();
                return Json(rooms.Select(r => new { r.RoomName, r.RoomId }));
            }
            catch(Exception err)
            {
                return Json($"error:{err.Message}");
            }
            
        }

        public async Task<IActionResult> GetAllRoomManagers()
        {                        
            try
            {
                var managers = await _roomService.GetAllRoomInfoAsync();
                return Json(managers);
            }
            catch (Exception err)
            {
                return Json($"error:{err.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckLoginIsBookingUserId([FromBody] GetUserParameter user)
        {
            if (string.IsNullOrEmpty(user.UserId))
            {
                return Json($"error: No Data!");
            }

            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
            {
                return RedirectToAction("Index");
            }

            var json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) ?? string.Empty;
            var login = JsonConvert.DeserializeObject<UserViewModel>(json);

            if (user.UserId != login?.UserId)
            {
                return Json("f");
            }

            return Json("t");

        }

        public async Task<IActionResult> UpdateBooking(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("DailyView", "Check");
            }

            var booking = await _bookingService.GetBookingAsync(new GetBookingParameterDto
            {
                Id = id.Value
            });

            if(booking?.Id == 0)
            {
                return RedirectToAction("DailyView", "Check");
            }
            
            return View(new CBooking
            {
                MeeingBooking = booking,
                startDate = booking.StartTime?.ToString("yyyy-MM-dd"),
                startT = booking.StartTime?.ToString("HH:mm"),
                endDate = booking.EndTime?.ToString("yyyy-MM-dd"),
                endT = booking.EndTime?.ToString("HH:mm"),
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBooking(CBooking booking)
        {
            if (booking == null)
            {
                return View();
            }

            try
            {
                string st = $"{booking.startDate}T{booking.startT}:00";
                string et = $"{booking.endDate}T{booking.endT}:00";

                await _bookingService.ModifyBookingAsync(new ModifyBookingParameterDto
                {
                    RoomId = booking.RoomId ?? string.Empty,
                    Subject = booking.Subject ?? string.Empty,
                    StartTime = DateTime.TryParse(st, out var startTime)? startTime : null,
                    EndTime = DateTime.TryParse(et, out var endTime) ? endTime : null,
                    Id = booking.Id,
                });

                return RedirectToAction("DailyView", "Check", new { date = $"{Convert.ToDateTime(st).ToString("yyyy-MM-dd")}" });
            }
            catch (Exception err)
            {
                return View();
            }
        }

        public async Task<IActionResult> DeleteBooking(int? id)
        {
            var st = DateTime.Today;

            try
            {
                if (id.HasValue)
                {
                    await _bookingService.RemoveBookingAsync(id.Value);
                }

                return RedirectToAction("DailyView", "Check", new { date = $"{st:yyyy-MM-dd}" });
            }
            catch (Exception)
            {
                return RedirectToAction("DailyView", "Check", new { date = $"{st:yyyy-MM-dd}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckBookingTime([FromBody] GetBookingParameter period)
        {
            //檢查是否時間已被預約
            if (period == null)
            {
                return Json($"error: No Data!");
            }

            try
            {
                if (await _bookingService.IsBooked(_mapper.Map<GetBookingParameterDto>(period)))
                {
                    return Json("Y");
                }
                else
                {
                    return Json("N");
                }
            }
            catch (Exception ex)
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
