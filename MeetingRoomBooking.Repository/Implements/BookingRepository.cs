using MeetingRoomBooking.Repository.DataModels;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.ParameterModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task<TMeeingBooking> GetBookingById(GetBookingParameterModel parameter)
        {
            var booking = await _db.TMeeingBookings.FirstOrDefaultAsync(b => b.Id== parameter.Id);
            return booking ?? new TMeeingBooking();
        }

        public async Task ModifyBookingAsync(ModifyBookingParameterModel parameter)
        {
            var booking = await _db.TMeeingBookings.FirstOrDefaultAsync(b=>b.Id==parameter.Id);

            if(booking != null)
            {
                booking.RoomId = parameter.RoomId;
                booking.Subject = parameter.Subject;
                booking.StartTime = parameter.StartTime;
                booking.EndTime = parameter.EndTime;

                await _db.SaveChangesAsync();
            }
        }

        public async Task RemoveBookingAsync(int id)
        {
            var booking = await _db.TMeeingBookings.FirstOrDefaultAsync(b => b.Id == id);
            if(booking != null)
            {
                await Task.Run(()=>_db.TMeeingBookings.Remove(booking));
                await _db.SaveChangesAsync();
            }
        }
    }
}
