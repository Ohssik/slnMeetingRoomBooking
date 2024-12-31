using MeetingRoomBooking.Repository.DataModels;
using MeetingRoomBooking.Repository.ParameterModels;

namespace MeetingRoomBooking.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(AddBookingParameterModel parameter);
        Task<TMeetingBooking> GetBookingAsync(GetBookingParameterModel parameter);
        Task ModifyBookingAsync(ModifyBookingParameterModel parameter);
        Task RemoveBookingAsync(int id);
        Task<IEnumerable<TMeetingBooking>> GetBookingsForCheckAsync(GetBookingParameterModel parameter);
        Task<IEnumerable<TMeetingBooking>> GetBookingListAsync(GetBookingParameterModel parameter);

    }
}
