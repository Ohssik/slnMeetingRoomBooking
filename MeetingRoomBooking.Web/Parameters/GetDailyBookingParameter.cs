namespace MeetingRoomBooking.Web.Parameters
{
    public class GetDailyBookingParameter
    {
        /// <summary>指定目標日期</summary>
        /// <param name="TargetDay"></param>
        /// <value>The target day.</value>
        public DateOnly TargetDay { get; set; }

        /// <summary>起始時間</summary>
        /// <param name="STime"></param>
        public string StartTime { get; set; } = string.Empty;
        /// <summary>結束時間</summary>
        /// <param name="ETime"></param>
        public string EndTime { get; set; } = string.Empty;
        /// <summary>會議室ID</summary>
        /// <param name="RoomId"></param>
        public string RoomId { get; set; } = string.Empty;
        /// <summary>PK</summary>
        /// <param name="Id"></param>
        public int? Id { get; set; }
    }
}
