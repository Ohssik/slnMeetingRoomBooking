using Evertrust.Core.Common.AspNetCore.Misc;
using MeetingRoomBooking.Common.Models;
using MeetingRoomBooking.Repository.Models;

namespace MeetingRoomBooking.Repository.Interfaces
{
    public interface IBookingRepository
    {
        /// <summary>
        ///   Create New Booking
        /// </summary>
        Task<IResult> CreateAsync(MeetingBookingModel booking);

        /// <summary>
        ///   Update Booking
        /// </summary>
        Task<IResult> UpdateAsync(MeetingBookingModel booking);

        /// <summary>
        ///   Delete Booking
        /// </summary>
        /// <param name="Id"></param>
        Task<IResult> DeleteAsync(int Id);

        /// <summary>Get Bookings for checking is booked or not</summary>
        /// <param name="targetDate">Target Date</param>
        /// <param name="RoomId">Room Id</param>
        /// <param name="id"></param>
        Task<IEnumerable<MeetingBookingModel>> GetBookingsForCheckAsync(DateOnly targetDate, string roomId, int id);

        /// <summary>Get bookings in certified period</summary>
        /// <param name="periodStart">period start time</param>
        /// <param name="periodEnd">period end time</param>
        Task<IEnumerable<MeetingBookingModel>> GetAllAsync(DateTime periodStart, DateTime periodEnd);

        /// <summary>
        ///   Get booking by ID
        /// </summary>
        /// <param name="id"></param>
        Task<MeetingBookingModel> GetByIdAsync(int id);

        // <summary>Search bookings</summary>
        Task<IEnumerable<BookingViewModel>> SearchAsync(SearchQueryCmdGenerator parameters);

        // <summary>Get records counts</summary>
        Task<int> GetRecordCountsAsync(SearchQueryCmdGenerator parameters);


    }
}
