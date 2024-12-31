using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Service.DTOs
{
    public class BookingDto
    {
        /// <summary>
        ///  Meeting Room ID
        /// </summary>        
        public string? RoomId { get; set; }

        /// <summary>
        /// 會議開始時間
        /// </summary> 
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 會議結束時間
        /// </summary> 
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 會議主題
        /// </summary> 
        public string? Subject { get; set; }

        /// <summary>
        /// 申請人ID
        /// </summary> 
        public string? BookingUserId { get; set; }
        public int Id { get; set; }
    }
}
