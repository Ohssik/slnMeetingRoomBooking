using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Common.Models
{    
    public class BookingViewModel
    {
        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        /// <example>
        /// 201
        /// </example>       
        public string RoomId { get; set; } = "";

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <example>
        /// 業務會議
        /// </example>
        [DisplayName("主旨")]
        public string Subject { get; set; } = "";
        /// <summary>
        /// Gets or sets the booking user identifier.
        /// </summary>
        /// <example>
        /// Amy.Chen
        /// </example>
        [DisplayName("登記人")]
        public string BookingUserId { get; set; } = "";
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <example>
        /// 2023-05-04
        /// </example>
        [DisplayName("日期")]
        public string StartDate { get; set; }
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <example>
        /// 09:00
        /// </example>
        [DisplayName("開始時間")]
        public string StartTime { get; set; }

        /// <summary>
        /// The end date
        /// </summary>
        private string? _endDate;
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <example>
        /// 2023-05-04
        /// </example>
        [DisplayName("日期")]
        public string? EndDate
        {
            get => _endDate ?? StartDate;
            set => _endDate = value;
        }
        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <example>
        /// 10:00
        /// </example>
        [DisplayName("結束時間")]
        public string EndTime { get; set; }
 
    }
}
