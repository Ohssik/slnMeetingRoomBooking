using System;
using System.Collections.Generic;

namespace prjMeetingRoomBooking.Models
{
    public partial class TMeetingRoom
    {
        public string RoomId { get; set; } = null!;
        public string? RoomName { get; set; }
        public string? ManagerId { get; set; }
        public string? Size { get; set; }
    }
}
