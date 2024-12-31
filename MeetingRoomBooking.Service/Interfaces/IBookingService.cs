using MeetingRoomBooking.Service.DTOs;
using MeetingRoomBooking.Repository.Models;
using MeetingRoomBooking.Common.Models;
using Evertrust.Core.Common.AspNetCore.Misc;

namespace MeetingRoomBooking.Service.Interfaces
{
    public interface IBookingService
    {
        /// <summary>
        ///   檢查欲預約時段是否已經被預約
        /// </summary>
        Task<bool> IsBooked(CheckPeriodDto period);

        /// <summary>
        ///   取得某時間區段的預約紀錄
        /// </summary>
        Task<IEnumerable<BookingDto>> GetBookingsByPeriodAsync(PeriodDto period);

        /// <summary>
        ///   回傳查詢時間的預約紀錄(API)
        /// </summary>
        Task<PagedOutputModel<BookingDto>> GetPagedResultAsync(BookingInputParamater bookings);

        /// <summary>Get bookings in certified period</summary>
        /// <param name="periodStart">period start time</param>
        /// <param name="periodEnd">period end time</param>
        Task<IEnumerable<BookingDto>> GetAllAsync(DateTime periodStart, DateTime periodEnd);

        /// <summary>
        ///   Get booking by ID
        /// </summary>
        /// <param name="id"></param>
        Task<BookingDto> GetByIdAsync(int id);

        /// <summary>
        ///   Create New Booking
        /// </summary>
        Task<IResult> CreateAsync(BookingDto booking);

        /// <summary>
        ///   Update Booking
        /// </summary>
        Task<IResult> UpdateAsync(BookingDto booking);

        /// <summary>
        ///   Delete Booking
        /// </summary>
        /// <param name="Id"></param>
        Task<IResult> DeleteAsync(int Id);

    }
}
