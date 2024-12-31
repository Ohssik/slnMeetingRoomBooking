using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.WebAPI.Models
{   
    /// <summary>
     /// 預約紀錄參數
     /// </summary>
    public class BookingApiModel
    {
        /// <summary>
        /// 會議室ID
        /// </summary>
        /// <example>
        /// 201
        /// </example>       
        public string RoomId { get; set; } = "";

        /// <summary>
        /// 會議主旨
        /// </summary>
        /// <example>
        /// 業務會議
        /// </example>
        [DisplayName("主旨")]
        public string Subject { get; set; } = "";
        /// <summary>
        /// 登記人
        /// </summary>
        /// <example>
        /// Amy.Chen
        /// </example>
        [DisplayName("登記人")]
        public string BookingUserId { get; set; } = "";
        
        /// <summary>
        /// 日期
        /// </summary>
        /// <example>
        /// 2023-05-04
        /// </example>
        [DisplayName("日期")]
        public string StartDate { get; set; }
        /// <summary>
        /// 開始時間
        /// </summary>
        /// <example>
        /// 09:00
        /// </example>
        [DisplayName("開始時間")]
        public string StartTime { get; set; }
        
        /// <summary>
        /// 結束時間
        /// </summary>
        /// <example>
        /// 10:00
        /// </example>
        [DisplayName("結束時間")]
        public string EndTime { get; set; }
 
    }
}
