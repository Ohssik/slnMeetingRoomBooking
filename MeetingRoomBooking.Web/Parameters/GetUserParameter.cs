using System.ComponentModel;

namespace MeetingRoomBooking.Web.Parameters
{
    public class GetUserParameter
    {
        [DisplayName("帳號")]
        public string UserId { get; set; } = string.Empty;
        [DisplayName("密碼")]
        public string UserPwd { get; set; } = string.Empty;
    }
}
