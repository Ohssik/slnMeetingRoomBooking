using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.ParameterModels
{
    public class GetUserParameterModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserPwd { get; set; } = string.Empty;
    }
}
