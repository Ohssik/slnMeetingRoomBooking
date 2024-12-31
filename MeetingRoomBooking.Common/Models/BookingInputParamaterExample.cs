using Swashbuckle.AspNetCore.Filters;

namespace MeetingRoomBooking.Common.Models
{
    public class BookingInputParamaterExample : IExamplesProvider<BookingInputParamater>
    {
        public BookingInputParamater GetExamples()
        {
            return new BookingInputParamater
            {
                RoomId = "201",
                Subject = "MIS週會",
                BookingUserId = "Amy.Chen",
                Id = 25,
                TargetDate = "2023-04-21",
                SortBy = "RoomID_asc",
                PageIndex = 1,
                PageSize = 10
            };
        }
    }
}
