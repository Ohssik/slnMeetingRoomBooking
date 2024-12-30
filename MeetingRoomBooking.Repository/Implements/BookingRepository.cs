using MeetingRoomBooking.Repository.DataModels;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.ParameterModels;

namespace MeetingRoomBooking.Repository.Implements
{
    public class BookingRepository: IBookingRepository
    {
        private readonly testContext _db;

        public BookingRepository(testContext db) 
        {
            _db = db; 
        }

        public async Task AddBookingAsync(AddBookingParameterModel parameter)
        {           
            await _db.TMeeingBookings.AddAsync(new TMeeingBooking
            {
                RoomId = parameter.RoomId,
                Subject = parameter.Subject,
                BookingUserId = parameter.BookingUserId,
                StartTime = parameter.StartTime,
                EndTime = parameter.EndTime,
            });

            await _db.SaveChangesAsync();
        }
    }
}
