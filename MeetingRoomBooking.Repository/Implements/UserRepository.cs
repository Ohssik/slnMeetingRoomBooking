using MeetingRoomBooking.Repository.Helpers;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.Models;
using Dapper;

namespace MeetingRoomBooking.Repository.Implements
{
    public class UserRepository:IUserRepository
    {
        private readonly IDatabaseHelper _databaseHelper;
        
        public UserRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // <summary>
        // Get User by User ID
        // </summary>
        /// <param name="userId">User ID</param>
        public async Task<UserModel> GetByIdAsync(string UserId)
        {
            using var conn = this._databaseHelper.GetConnection();
            var sqlCommand = @"select UserID,UserPwd,UserName from tUser 
                                   where UserID = @UserID ";

            var parameters = new DynamicParameters();
            parameters.Add("UserID", UserId);

            var result = await conn.QueryFirstOrDefaultAsync<UserModel>(sqlCommand, parameters);
            return result;
        }

        // <summary>
        // Check User is Existed
        // </summary>
        /// <param name="user">UserModel user</param>
        public async Task<bool> IsExist(UserModel user)
        {
            using var conn = this._databaseHelper.GetConnection();
            var sqlCommand = @"select count(UserID) from tUser 
                                   where UserID = @UserID and UserPwd = @UserPwd";

            var parameters = new DynamicParameters();
            parameters.Add("UserID", user.UserId);
            parameters.Add("UserPwd", user.UserPwd);

            var result = await conn.QueryFirstOrDefaultAsync<int>(sqlCommand, parameters);
            return result == 1;
        }
    }
}
