namespace MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Models
{
    public class ValidateError
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public string PropertyName { get; set; }
    }
}
