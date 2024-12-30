using MeetingRoomBooking.Service.Dtos;
using prjMeetingRoomBooking.Models;
using System.ComponentModel;

namespace prjMeetingRoomBooking.ViewModels
{
    public class CBooking
    {
        private BookingDto _meeingBooking;
        public CBooking()
        {
            _meeingBooking = new BookingDto();
        }
        public BookingDto MeeingBooking
        { 
            get { return _meeingBooking; } 
            set { _meeingBooking=value; }
        }
        public string? RoomId
        {
            get { return _meeingBooking.RoomId; }
            set { _meeingBooking.RoomId=value; }
        }        
        public DateTime? StartTime
        {
            get { return _meeingBooking.StartTime; }
            set { _meeingBooking.StartTime=value; }
        }
        public DateTime? EndTime
        {
            get { return _meeingBooking.EndTime; }
            set { _meeingBooking.EndTime=value; }
        }
        [DisplayName("主旨")]
        public string? Subject
        {
            get { return _meeingBooking.Subject; }
            set { _meeingBooking.Subject=value; }
        }
        [DisplayName("登記人")]
        public string? BookingUserId
        {
            get { return _meeingBooking.BookingUserId; }
            set { _meeingBooking.BookingUserId=value; }
        }
        public int Id
        {
            get { return _meeingBooking.Id; }
            set { _meeingBooking.Id=value; }
        }
        [DisplayName("日期")]
        public string? startDate { get;set; }
        [DisplayName("開始時間")]
        public string? startT { get; set; }
        
        [DisplayName("日期")]
        public string? endDate { get; set; }
       
        [DisplayName("結束時間")]
        public string? endT { get; set; }
       
    }
}
