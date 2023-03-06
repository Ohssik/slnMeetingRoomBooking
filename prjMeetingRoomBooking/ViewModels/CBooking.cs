using prjMeetingRoomBooking.Models;
using System.ComponentModel;

namespace prjMeetingRoomBooking.ViewModels
{
    public class CBooking
    {
        private TMeeingBooking _meeingBooking;
        public CBooking()
        {
            _meeingBooking = new TMeeingBooking();
        }
        public TMeeingBooking MeeingBooking
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
        public string? startDate {
            get;set;
            //get
            //{
            //    string date = "";
            //    if (StartTime!=null)
            //        date = ((DateTime)StartTime).ToString("yyyy-MM-dd");

            //    return date;
            //}
            //set { /*startDate=value;*/ } 
        }
        [DisplayName("開始時間")]
        public string? startT
        {
            get; set;
            //get
            //{
            //    string time = "";
            //    if (StartTime!=null)
            //        time = ((DateTime)StartTime).ToString("HH-mm");

            //    return time;
            //}
            //set { /*startT=value;*/ }
        }
        [DisplayName("日期")]
        public string? endDate
        {
            get; set;
            //get
            //{
            //    string date = "";
            //    if (EndTime!=null)
            //        date = ((DateTime)EndTime).ToString("yyyy-MM-dd");

            //    return date;
            //}
            //set { /*endDate=value;*/ }
        }
        [DisplayName("結束時間")]
        public string? endT {
            get; set;
            //get
            //{
            //    string time = "";
            //    if (EndTime!=null)
            //        time = ((DateTime)EndTime).ToString("HH-mm");

            //    return time;
            //}
            //set { /*endT=value;*/ }
        }
    }
}
