using System;
using System.Collections.Generic;

namespace prjMeetingRoomBooking.Models
{
    public partial class ViewManager2Room
    {
        public string? ManagerName { get; set; }
        public string RoomId { get; set; } = null!;
        public string? RoomName { get; set; }
        public string? ManagerId { get; set; }
        public string? Size { get; set; }
    }
}
