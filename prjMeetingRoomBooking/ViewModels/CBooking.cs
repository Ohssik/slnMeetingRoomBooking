using MeetingRoomBooking.Repository.Models;
using System.ComponentModel;

namespace prjMeetingRoomBooking.ViewModels
{
    public class CBooking
    {
        //private TMeeingBooking _meeingBooking;
        //public CBooking()
        //{
        //    _meeingBooking = new TMeeingBooking();
        //}
        //public TMeeingBooking MeeingBooking
        //{ 
        //    get { return _meeingBooking; } 
        //    set { _meeingBooking=value; }
        //}
        public string? RoomId
        {
            //get { return _meeingBooking.RoomId; }
            //set { _meeingBooking.RoomId=value; }
            get; set;
        }        
        public DateTime? StartTime
        {
            //get { return _meeingBooking.StartTime; }
            //set { _meeingBooking.StartTime=value; }
            get; set;
        }
        public DateTime? EndTime
        {
            //get { return _meeingBooking.EndTime; }
            //set { _meeingBooking.EndTime=value; }
            get; set;
        }
        [DisplayName("主旨")]
        public string? Subject
        {
            //get { return _meeingBooking.Subject; }
            //set { _meeingBooking.Subject=value; }
            get; set;
        }
        [DisplayName("登記人")]
        public string? BookingUserId
        {
            //get { return _meeingBooking.BookingUserId; }
            //set { _meeingBooking.BookingUserId=value; }
            get; set;
        }
        public int Id
        {
            //get { return _meeingBooking.Id; }
            //set { _meeingBooking.Id=value; }
            get; set;
        }
        [DisplayName("日期")]
        public string? startDate {
            get;set;
            
        }
        [DisplayName("開始時間")]
        public string? startT
        {
            get; set;
            
        }
        [DisplayName("日期")]
        public string? endDate
        {
            get; set;
            
        }
        [DisplayName("結束時間")]
        public string? endT {
            get; set;
            
        }
    }
}
