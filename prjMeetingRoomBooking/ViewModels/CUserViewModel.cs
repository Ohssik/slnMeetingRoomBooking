using System.ComponentModel;

namespace prjMeetingRoomBooking.ViewModels
{
    public class CUserViewModel
    {
        [DisplayName("帳號")]
        public string UserId { get; set; } = null!;
        [DisplayName("密碼")]
        public string UserPwd { get; set; } = null!;
        public string? UserName { get; set; }
    }

}
