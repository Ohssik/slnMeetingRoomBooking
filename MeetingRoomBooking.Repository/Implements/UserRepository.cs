using MeetingRoomBooking.Repository.DataModels;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.ParameterModels;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Repository.Implements
{
    public class UserRepository: IUserRepository
    {
        private readonly testContext _db;

        public UserRepository(testContext db)
        {
            _db = db;
        }

        public async Task<TUser> GetUserAsync(GetUserParameterModel parameter)
        {
            var result = await _db.TUsers.FirstOrDefaultAsync(
                u => u.UserId == parameter.UserId && u.UserPwd == parameter.UserPwd);
            return result ?? new TUser();
        }
    }
}
