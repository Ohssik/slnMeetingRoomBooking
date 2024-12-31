using MeetingRoomBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.Interfaces
{
    public interface IMeetingRoomRepository
    {
        IEnumerable<TMeetingRoom> GetAllRooms();
        IEnumerable<ViewManager2Room> GetAllManagers();
    }
}
