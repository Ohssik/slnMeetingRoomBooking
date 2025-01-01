namespace MeetingRoomBooking.Web.Parameters
{
    public class GetBookingParameter
    {
        public int Id { get; set; }
        public string RoomId { get; set; } = string.Empty;
        public DateTime? FirstDay { get; set; }
        public DateTime? LastDay { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    }
}
