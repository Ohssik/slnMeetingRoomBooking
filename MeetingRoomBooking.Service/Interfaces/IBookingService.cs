using MeetingRoomBooking.Service.Dtos;
using MeetingRoomBooking.Service.ParameterDtos;

namespace MeetingRoomBooking.Service.Interfaces
{
    public interface IBookingService
    {
        Task AddBookingAsync(AddBookingParameterDto parameter);

        Task<BookingDto> GetBookingAsync(GetBookingParameterDto parameter);
        Task<IEnumerable<BookingDto>> GetBookingListAsync(GetBookingParameterDto parameter);

        Task ModifyBookingAsync(ModifyBookingParameterDto parameter);

        Task RemoveBookingAsync(int id);

        Task<bool> IsBooked(GetBookingParameterDto parameter);
    }
}
