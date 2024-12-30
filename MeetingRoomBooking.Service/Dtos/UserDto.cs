using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.Dtos
{
    public class UserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string UserPwd { get; set; } = string.Empty;
    }
}
