using System;
using System.Collections.Generic;

namespace MeetingRoomBooking.Repository.DataModels
{
    public partial class TMeetingBooking
    {
        public string? RoomId { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Subject { get; set; } = string.Empty;
        public string? BookingUserId { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
