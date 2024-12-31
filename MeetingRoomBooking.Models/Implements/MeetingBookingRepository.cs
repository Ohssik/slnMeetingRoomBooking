using Dapper;
using Evertrust.Core.Common.AspNetCore.Misc;
using MeetingRoomBooking.Repository.Helpers;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.Implements
{
    public class MeetingBookingRepository:IMeetingBookingRepository
    {
        private readonly IDatabaseHelper _databaseHelper;

        public MeetingBookingRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        public IResult Create(TMeeingBooking booking)
        {
            using (var conn = this._databaseHelper.GetConnection())
            {
                var sqlCommand = @"Insert into tMeetingBooking (RoomID, StartTime,
                                    EndTime,Subject,BookingUserID) 
                                   Values (@RoomID, @StartTime,
                                    @EndTime,@Subject,@BookingUserID)"
                ;

                var executeResult = conn.Execute(sqlCommand, booking);

                IResult result = new Result(false);

                if (executeResult.Equals(1))
                {
                    result.Success = true;
                    result.AffectRows = executeResult;
                    return result;
                }

                result.Message = "資料新增錯誤";
                return result;
            }
        }
        public IResult Update(TMeeingBooking booking)
        {
            using(IDbConnection connection = this._databaseHelper.GetConnection())
            {
                string sqlCmd = @"UPDATE tMeetingBooking 
                                    SET RoomID=@RoomID,
                                    StartTime=@StartTime,
                                    EndTime=@EndTime,
                                    Subject=@Subject 
                                    WHERE id=@id";

                var executeResult = connection.Execute(sqlCmd, booking);

                IResult result = new Result(false); 
                
                if (executeResult.Equals(1)) 
                {
                    result.Success = true;
                    result.AffectRows = executeResult;
                    return result;

                }

                result.Message = "資料修改錯誤";
                return result;
            }
        }
        public IResult Delete(int Id)
        {
            using (IDbConnection connection = this._databaseHelper.GetConnection())
            {
                string sqlCmd = @"DELETE FROM tMeetingBooking                                      
                                    WHERE id=@id";

                var parameters = new DynamicParameters();
                parameters.Add("id", Id);

                var executeResult = connection.Execute(sqlCmd, parameters);

                IResult result = new Result(false);

                if (executeResult.Equals(1))
                {
                    result.Success = true;
                    result.AffectRows = executeResult;
                    return result;

                }

                result.Message = "資料刪除錯誤";
                return result;
            }
        }
        
        public TMeeingBooking GetById(int Id)
        {
            using (IDbConnection conn = this._databaseHelper.GetConnection())
            {
                string sqlCmd = @"SELECT RoomID, StartTime,
                                EndTime,Subject,BookingUserID,id 
                                FROM tMeetingBooking 
                                WHERE id=@id";

                var parameters = new DynamicParameters();
                parameters.Add("id", Id);

                var result = conn.QueryFirstOrDefault<TMeeingBooking>(sqlCmd,parameters);

                return result;
            }
        }

        public IEnumerable<ViewTmeeingBooking> GetAll(string targetDate, string RoomId, int id)
        {
            using (IDbConnection conn = this._databaseHelper.GetConnection())
            {
                string sqlCmd = @"SELECT RoomID, StartDate,StartTime,
                                EndDate,EndTime,Subject,BookingUserID,id 
                                FROM ViewTMeetingBooking
                                WHERE StartDate=@StartDate and RoomID=@RoomID and id!=@id";

                var parameters = new DynamicParameters();
                parameters.Add("StartDate", targetDate);
                parameters.Add("RoomID", RoomId);
                parameters.Add("id", id);

                var result = conn.Query<ViewTmeeingBooking>(sqlCmd, parameters);
                return result;
            }
        }

        
        public IEnumerable<TMeeingBooking> GetAll(DateTime getFirstDay, DateTime lastDay)
        {
            using (IDbConnection conn = this._databaseHelper.GetConnection())
            {
                string sqlCmd = @"SELECT RoomID,StartTime,
                                EndTime,Subject,BookingUserID,id 
                                FROM tMeetingBooking
                                WHERE StartTime>=@StartTime and EndTime<@EndTime
                                ORDER BY RoomID";
                
                var parameters = new DynamicParameters();
                parameters.Add("StartTime", getFirstDay);
                parameters.Add("EndTime", lastDay);

                var result = conn.Query<TMeeingBooking>(sqlCmd,parameters);
                return result;
            }
        }
    }
}
