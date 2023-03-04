using System;
using System.Collections.Generic;

namespace prjMeetingRoomBooking.Models
{
    public partial class TUser
    {
        public string UserId { get; set; } = null!;
        public string UserPwd { get; set; } = null!;
        public string? UserName { get; set; }
    }
}
