using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.ParameterDtos
{
    public class ModifyBookingParameterDto
    {
        public int Id { get; set; }
        public string RoomId { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}
