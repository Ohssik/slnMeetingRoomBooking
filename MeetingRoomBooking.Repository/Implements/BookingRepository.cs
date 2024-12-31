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
            await _db.TMeeingBookings.AddAsync(new TMeetingBooking
            {
                RoomId = parameter.RoomId,
                Subject = parameter.Subject,
                BookingUserId = parameter.BookingUserId,
                StartTime = parameter.StartTime,
                EndTime = parameter.EndTime,
            });

            await _db.SaveChangesAsync();
        }

        public async Task<TMeetingBooking> GetBookingAsync(GetBookingParameterModel parameter)
        {
            var booking = await _db.TMeeingBookings.FirstOrDefaultAsync(b => b.Id== parameter.Id);
            return booking ?? new TMeetingBooking();
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

        public async Task<IEnumerable<TMeetingBooking>> GetBookingsForCheckAsync(GetBookingParameterModel parameter)
        {
            var startDate = parameter.FirstDay.GetValueOrDefault().ToString("yyyy-MM-dd");
            var roomId = parameter.RoomId;
            var id = parameter.Id;

            var result = await Task.Run(()=> _db.TMeeingBookings.FromSqlInterpolated(
                                                @$"SELECT RoomID, 
                                                        StartTime,
                                                        EndTime,
                                                        Subject,
                                                        BookingUserID,
                                                        id 
                                                    FROM tMeetingBooking
                                                    WHERE CONVERT(date, StartTime)={startDate} 
                                                    and RoomID={roomId} 
                                                    and id!={id}")
                                            .ToList());

            return result ?? Enumerable.Empty<TMeetingBooking>();
        }

        public async Task<IEnumerable<TMeetingBooking>> GetBookingListAsync(GetBookingParameterModel parameter)
        {
            var records = await Task.Run(() => _db.TMeeingBookings
                                            .Where(
                                                r => r.StartTime >= parameter.FirstDay && r.EndTime < parameter.LastDay)
                                            .Select(r => r)
                                            .OrderBy(r => r.RoomId)
                                            .ToList()
                                        );

            return records ?? Enumerable.Empty<TMeetingBooking>();
        }
    }
}
