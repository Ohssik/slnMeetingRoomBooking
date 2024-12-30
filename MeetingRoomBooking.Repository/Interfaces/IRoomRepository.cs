using MeetingRoomBooking.Repository.DataModels;
using MeetingRoomBooking.Repository.ParameterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<TMeetingRoom>> GetAllAsync();
    }
}
