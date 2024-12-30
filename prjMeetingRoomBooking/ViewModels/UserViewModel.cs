using System.ComponentModel;

namespace prjMeetingRoomBooking.ViewModels
{
    public class UserViewModel
    {
        [DisplayName("帳號")]
        public string UserId { get; set; } = string.Empty;
        [DisplayName("密碼")]
        public string UserPwd { get; set; } = string.Empty;

    }
}
