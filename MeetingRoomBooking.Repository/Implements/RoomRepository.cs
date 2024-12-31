using Dapper;
using MeetingRoomBooking.Repository.Helpers;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.Models;
using System.Data;

namespace MeetingRoomBooking.Repository.Implements
{
    public class RoomRepository:IRoomRepository
    {
        private readonly IDatabaseHelper _databaseHelper;

        public RoomRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // <summary>Get All Meeting Rooms Info</summary>
        public async Task<IEnumerable<MeetingRoomModel>> GetAllRoomsAsync()
        {
            using IDbConnection conn = _databaseHelper.GetConnection();
            string sqlCmd = @"SELECT RoomID,RoomName FROM tMeetingRoom ORDER BY RoomID";
            var result = await conn.QueryAsync<MeetingRoomModel>(sqlCmd);
            return result;
        }

        // <summary>Get All Meeting Rooms Managers</summary>
        public async Task<IEnumerable<Manager2RoomModel>> GetAllManagersAsync()
        {
            using IDbConnection conn = _databaseHelper.GetConnection();
            string sqlCmd = @"SELECT managerName,RoomID,RoomName,ManagerID,Size
                                FROM View_Manager2Room";
            var result = await conn.QueryAsync<Manager2RoomModel>(sqlCmd);
            return result;
        }
    }
}
