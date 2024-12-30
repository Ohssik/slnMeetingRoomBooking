using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.ParameterModels
{
    public class AddBookingParameterModel
    {
        public string? RoomId { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Subject { get; set; } = string.Empty;
        public string? BookingUserId { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
