using System;
using System.Collections.Generic;

namespace MeetingRoomBooking.Repository.DataModels
{
    public partial class ViewManager2Room
    {
        public string? ManagerName { get; set; } = string.Empty;
        public string RoomId { get; set; } = string.Empty;
        public string? RoomName { get; set; } = string.Empty;
        public string? ManagerId { get; set; } = string.Empty;
        public string? Size { get; set; } = string.Empty;
    }
}
