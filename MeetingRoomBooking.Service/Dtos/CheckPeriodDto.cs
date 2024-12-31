namespace MeetingRoomBooking.Service.DTOs
{
    /// <summary>
    /// 取得某段時間區間內的booking記錄
    /// </summary>
    public class CheckPeriodDto
    {

        /// <summary>指定目標日期</summary>
        /// <param name="TargetDay"></param>
        /// <value>The target day.</value>
        public DateOnly TargetDay { get; set; }

        /// <summary>起始時間</summary>
        /// <param name="STime"></param>
        public string StartTime { get; set; }
        /// <summary>結束時間</summary>
        /// <param name="ETime"></param>
        public string EndTime { get; set; }
        /// <summary>會議室ID</summary>
        /// <param name="RoomId"></param>
        public string RoomId { get; set; } = "";
        /// <summary>PK</summary>
        /// <param name="Id"></param>
        public int? Id { get; set; }
    }
}
