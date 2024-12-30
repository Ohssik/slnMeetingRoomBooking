using System;
using System.Collections.Generic;

namespace MeetingRoomBooking.Repository.DataModels
{
    public partial class TMeetingRoom
    {
        public string RoomId { get; set; } = string.Empty;
        public string? RoomName { get; set; } = string.Empty;
        public string? ManagerId { get; set; } = string.Empty;
        public string? Size { get; set; } = string.Empty;
    }
}
