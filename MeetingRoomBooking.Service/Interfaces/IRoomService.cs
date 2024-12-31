using MeetingRoomBooking.Repository.Models;
using MeetingRoomBooking.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.Interfaces
{
    public interface IRoomService
    {
        // <summary>Get All Meeting Rooms Info</summary>
        Task<IEnumerable<MeetingRoomDto>> GetAllRoomsAsync();

        // <summary>Get All Meeting Rooms Managers</summary>
        Task<IEnumerable<Manager2RoomDto>> GetAllManagersAsync();

        
    }
}
