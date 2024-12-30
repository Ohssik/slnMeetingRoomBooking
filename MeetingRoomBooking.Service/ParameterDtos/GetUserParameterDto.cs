using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.ParameterDtos
{
    public class GetUserParameterDto
    {
        public string UserId { get; set; } = string.Empty;
        public string UserPwd { get; set; } = string.Empty;
    }
}
