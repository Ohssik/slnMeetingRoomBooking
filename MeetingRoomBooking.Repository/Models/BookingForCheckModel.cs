namespace MeetingRoomBooking.Repository.Models
{
    public class BookingForCheckModel
    {
        public string? RoomId { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
        public string? EndDate { get; set; }
        public string? EndTime { get; set; }
        public string? Subject { get; set; }
        public string? BookingUserId { get; set; }
        public int Id { get; set; }
    }
}
