using Microsoft.AspNetCore.Mvc;
using MeetingRoomBooking.Common.Models;
using MeetingRoomBooking.Common.Infrastructure.Validators;
using MeetingRoomBooking.Common.Infrastructure.ActionFilters;
using MeetingRoomBooking.Repository.Models;
using MeetingRoomBooking.Service.Interfaces;
using MeetingRoomBooking.Service.DTOs;
using System.Web.Http.Description;
using Evertrust.Core.Logging.Abstractions;
using Swashbuckle.AspNetCore.Filters;



namespace MeetingRoomBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        //Controller避免Repository DI        
        private readonly IBookingService _bookingService;
        private readonly ILogHelper _logHelper;

        public BookingController(IBookingService roomService, ILogHelperFactory logHelperFactory)
        {
            _bookingService = roomService;
            _logHelper = logHelperFactory.Create<BookingController>();
        }

        /// <summary>取得全部的預約記錄</summary>
        /// <remarks>取得所有的預約記錄</remarks>
        [CoreProfiling]
        [HttpGet("GetAll")]
        [ResponseType(typeof(IEnumerable<BookingDto>))]
        public async Task<IEnumerable<BookingDto>> GetAllAsync()
        {
            return await _bookingService.GetAllAsync(new DateTime(2023, 1, 1), DateTime.Now.AddDays(1));
        }

        /// <summary>取得今天的預約記錄</summary>        
        [CoreProfiling]
        [HttpGet("GetTodayBookings")]
        public async Task<IEnumerable<BookingDto>> GetTodayBookingsAsync()
        {
            return await _bookingService.GetAllAsync(DateTime.Now, DateTime.Now.AddDays(1));
        }

        /// <summary>從User ID取得單筆預約記錄</summary>
        /// <param name="id">User帳號</param>
        [CoreProfiling]
        [HttpGet("{id}")]
        [ResponseType(typeof(BookingDto))]
        //[ProducesResponseType(typeof(BookingOutputModel), StatusCodes.Status200OK)]
        public async Task<BookingDto> GetByIdAsync(int id)
        {
            //紀錄任何動作到Exceptionless
            "from BookingController - GetByIdAsync".LogInfo(
            logHelper: this._logHelper,
            categoryName: "BookingController-GetByIdAsync",
            tags: new[] { "BookingController", "GetByIdAsync" });

            return await _bookingService.GetByIdAsync(id);
        }

        /// <summary>輸入多項條件取得預約記錄</summary>
        /// <param name="booking">查詢預約的參數。</param>
        [HttpGet("SearchAsync")]
        [Produces("application/json")]
        [BookingValidator(typeof(BookingInputParamaterValidator))]
        [SwaggerRequestExample(typeof(BookingInputParamater), typeof(BookingInputParamaterExample))]
        [ResponseType(typeof(BookingInputParamater))]
        public async Task<IActionResult> SearchAsync([FromQuery] BookingInputParamater booking)
        {                       
            PagedOutputModel<BookingDto> result = await _bookingService.GetPagedResultAsync(booking);
            return Ok(result);                           
        }

        /// <summary>新增預約記錄</summary>
        /// <param name="booking">建立預約的參數。</param>
        [HttpPost]
        [Produces("application/json", "text/json")]
        [Consumes("application/json")]
        [SwaggerRequestExample(typeof(BookingViewModel), typeof(BookingViewModelExample))]
       
        public async Task<IActionResult> Post([FromBody] MeetingRoomBooking.WebAPI.Models.BookingApiModel booking)
        {            
            var output = new BookingOutputModel();

            output.Success = true;
            output.Result = "finish";            

            return this.Ok(output);
            
        }

        /// <summary>修改預約記錄</summary>
        /// <param name="id">User帳號</param>
        /// <param name="booking">修改的預約內容</param>
        [HttpPut]    
        [Produces("application/json", "text/json")]
        [Consumes("application/json")]
        [BookingValidator(typeof(BookingViewModelValidator))]
        [SwaggerRequestExample(typeof(BookingViewModel), typeof(BookingViewModelExample))]
        [ProducesResponseType(typeof(BookingViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put( [FromBody] BookingViewModel booking)
        {            
            //var output = new BookingOutputModel();

            //output.Success = true;
            //output.Result = "finish";

            return this.Ok(booking);
        }

        /// <summary>刪除預約記錄</summary>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {            
        }

    }
}
