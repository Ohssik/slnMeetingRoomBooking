using AutoMapper.Internal;
using System.ComponentModel;
using System.Globalization;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Common.Models
{
    public class BookingInputParamater
    {
        /// <summary>
        ///   會議室ID
        /// </summary>         
        public string? RoomId { get; set; }
        /// <summary>
        ///   申請需求主題
        /// </summary>
        public string? Subject { get; set; }
        /// <summary>
        ///   申請人ID
        /// </summary>
        public string? BookingUserId { get; set; }
        /// <summary>
        ///   ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        ///   申請日期
        /// </summary>
        public string? TargetDate { get; set; }
        /// <summary>
        ///   排序方式
        /// </summary>
        public string SortBy { get; set; } = "";        
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    
}
