using Dapper;
using Evertrust.Core.Common.AspNetCore.Misc;
using MeetingRoomBooking.Common.Models;
using MeetingRoomBooking.Repository.Helpers;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.Models;
using System.Data;

namespace MeetingRoomBooking.Repository.Implements
{
    /// <summary>
    ///   class 預約紀錄CRUD
    /// </summary>
    public class BookingRepository:IBookingRepository
    {
        private readonly IDatabaseHelper _databaseHelper;

        public BookingRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        ///   Create New Booking
        /// </summary>
        public async Task<IResult> CreateAsync(MeetingBookingModel booking)
        {
            using var conn = this._databaseHelper.GetConnection();
            var sqlCommand = @"Insert into tMeetingBooking (RoomID, StartTime,
                                    EndTime,Subject,BookingUserID) 
                                   Values (@RoomID, @StartTime,
                                    @EndTime,@Subject,@BookingUserID)"
            ;

            var executeResult = await conn.ExecuteAsync(sqlCommand, booking);

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

        /// <summary>
        ///   Update Booking
        /// </summary>
        public async Task<IResult> UpdateAsync(MeetingBookingModel booking)
        {
            using IDbConnection connection = this._databaseHelper.GetConnection();
            string sqlCmd = @"UPDATE tMeetingBooking 
                                    SET RoomID=@RoomID,
                                    StartTime=@StartTime,
                                    EndTime=@EndTime,
                                    Subject=@Subject 
                                    WHERE id=@id";

            var executeResult = await connection.ExecuteAsync(sqlCmd, booking);

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

        /// <summary>
        ///   Delete Booking
        /// </summary>
        /// <param name="id"></param>
        public async Task<IResult> DeleteAsync(int Id)
        {
            using IDbConnection connection = this._databaseHelper.GetConnection();
            string sqlCmd = @"DELETE FROM tMeetingBooking                                      
                                    WHERE id=@id";

            var parameters = new DynamicParameters();
            parameters.Add("id", Id);

            var executeResult = await connection.ExecuteAsync(sqlCmd, parameters);

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

        /// <summary>
        ///   Get Booking By Id
        /// </summary>
        /// <param name="Id"></param>
        public async Task<MeetingBookingModel> GetByIdAsync(int Id)
        {
            using IDbConnection conn = this._databaseHelper.GetConnection();
            string sqlCmd = @"SELECT RoomID, StartTime,
                                EndTime,Subject,BookingUserID,id 
                                FROM tMeetingBooking 
                                WHERE id=@id";

            var parameters = new DynamicParameters();
            parameters.Add("id", Id);

            var result = await conn.QueryFirstOrDefaultAsync<MeetingBookingModel>(sqlCmd, parameters);

            return result;
        }

        /// <summary>Get Bookings for checking is booked or not</summary>
        /// <param name="targetDate">Target Date</param>
        /// <param name="RoomId">Room Id</param>
        /// <param name="id"></param>
        public async Task<IEnumerable<MeetingBookingModel>> GetBookingsForCheckAsync(DateOnly targetDate, string RoomId, int id)
        {
            using IDbConnection conn = this._databaseHelper.GetConnection();
            string sqlCmd = @"SELECT RoomID, StartTime,
                               EndTime,Subject,BookingUserID,id 
                                FROM tMeetingBooking
                                WHERE CONVERT(date, StartTime)=@StartDate and RoomID=@RoomID and id!=@id";

            var parameters = new DynamicParameters();
            parameters.Add("StartDate", targetDate.ToString("yyyy-MM-dd"));
            parameters.Add("RoomID", RoomId);
            parameters.Add("id", id);

            var result = await conn.QueryAsync<MeetingBookingModel>(sqlCmd, parameters);
            return result;
        }

        /// <summary>Get bookings in certified period</summary>
        /// <param name="periodStart">period start time</param>
        /// <param name="periodEnd">period end time</param>
        public async Task<IEnumerable<MeetingBookingModel>> GetAllAsync(DateTime periodStart, DateTime periodEnd)
        {
            using IDbConnection conn = this._databaseHelper.GetConnection();
            string sqlCmd = @"SELECT RoomID,StartTime,
                                EndTime,Subject,BookingUserID,id 
                                FROM tMeetingBooking
                                WHERE StartTime>=@StartTime and EndTime<@EndTime
                                ORDER BY RoomID";

            var parameters = new DynamicParameters();
            parameters.Add("StartTime", periodStart);
            parameters.Add("EndTime", periodEnd);

            var result = await conn.QueryAsync<MeetingBookingModel>(sqlCmd, parameters);
            return result;
        }

        // <summary>Search bookings</summary>
        public async Task<IEnumerable<BookingViewModel>> SearchAsync(SearchQueryCmdGenerator parameters)
        {
            using IDbConnection conn = this._databaseHelper.GetConnection();            

            var result = await conn.QueryAsync<BookingViewModel>(parameters.SearchParameters.Item1,parameters.SearchParameters.Item2);            
            return result;
        }

        // <summary>Get records counts</summary>
        public async Task<int> GetRecordCountsAsync(SearchQueryCmdGenerator parameters)
        {
            using IDbConnection conn = this._databaseHelper.GetConnection();
            
            var result = await conn.ExecuteScalarAsync<int>(parameters.GetRecordsCountsSql, parameters.SearchParameters.Item2);
            return result;
            
        }
    }
}
