using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.Dtos
{
    public class RoomDto
    {
        public string RoomId { get; set; } = string.Empty;
        public string? RoomName { get; set; } = string.Empty;
        public string? ManagerName { get; set; } = string.Empty;
        public string? ManagerId { get; set; } = string.Empty;
        public string? Size { get; set; } = string.Empty;

    }
}
