using MeetingRoomBooking.Service.Dtos;
using MeetingRoomBooking.Service.ParameterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.Interfaces
{
    public interface IBookingService
    {
        Task AddBookingAsync(AddBookingParameterDto parameter);

        Task<BookingDto> GetBooking(GetBookingParameterDto parameter);

        Task ModifyBookingAsync(ModifyBookingParameterDto parameter);

        Task RemoveBookingAsync(int id);
    }
}
