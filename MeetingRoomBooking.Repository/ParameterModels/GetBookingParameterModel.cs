using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.ParameterModels
{
    public class GetBookingParameterModel
    {
        public int Id { get; set; }
        public string RoomId { get; set; } = string.Empty;
        public DateTime? FirstDay { get; set; }
        public DateTime? LastDay { get; set; }
    }
}
