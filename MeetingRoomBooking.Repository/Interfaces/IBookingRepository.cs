using MeetingRoomBooking.Repository.ParameterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(AddBookingParameterModel parameter);

    }
}
