using MeetingRoomBooking.Repository.Helpers;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public TUser GetById(string UserId)
        {
            using (var conn = this._databaseHelper.GetConnection())
            {
                var sqlCommand = @"select UserID,UserPwd,UserName from tUser 
                                   where UserID = @UserID ";

                var parameters = new DynamicParameters();
                parameters.Add("UserID", UserId);

                var result = conn.QueryFirstOrDefault<TUser>(sqlCommand, parameters);
                return result;
            }
        }
        public bool IsExist(TUser user)
        {
            using (var conn = this._databaseHelper.GetConnection())
            {
                var sqlCommand = @"select count(UserID) from tUser 
                                   where UserID = @UserID and UserPwd = @UserPwd";

                var parameters = new DynamicParameters();
                parameters.Add("UserID", user.UserId);
                parameters.Add("UserPwd", user.UserPwd);

                var result = conn.QueryFirstOrDefault<int>(sqlCommand, parameters);
                return result==1;
            }
        }
    }
}
