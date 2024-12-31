using Swashbuckle.AspNetCore.Filters;

namespace MeetingRoomBooking.Common.Models
{
    public class BookingViewModelExample : IExamplesProvider<BookingViewModel>
    {
        public BookingViewModel GetExamples()
        {
            return new BookingViewModel
            {
                RoomId = "201",
                Subject = "MIS週會",
                BookingUserId = "Amy.Chen",
                StartDate = "2023-05-04",
                StartTime = "09:00",
                EndDate = "2023-05-04",
                EndTime = "10:00"
            };
        }
    }
}
