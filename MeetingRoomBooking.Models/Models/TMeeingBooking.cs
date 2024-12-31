using System;
using System.Collections.Generic;

namespace MeetingRoomBooking.Repository.Models
{
    public partial class TMeeingBooking
    {
        public string? RoomId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Subject { get; set; }
        public string? BookingUserId { get; set; }
        public int Id { get; set; }
    }
}
