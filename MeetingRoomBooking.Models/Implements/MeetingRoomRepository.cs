using Dapper;
using MeetingRoomBooking.Repository.Helpers;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.Implements
{
    public class MeetingRoomRepository:IMeetingRoomRepository
    {
        private readonly IDatabaseHelper _databaseHelper;

        public MeetingRoomRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public IEnumerable<TMeetingRoom> GetAllRooms()
        {
            using(IDbConnection conn=_databaseHelper.GetConnection())
            {
                string sqlCmd = @"SELECT RoomID,RoomName FROM tMeetingRoom ORDER BY RoomID";
                var result = conn.Query<TMeetingRoom>(sqlCmd);
                return result;
            }
        }

        public IEnumerable<ViewManager2Room> GetAllManagers()
        {
            using(IDbConnection conn= _databaseHelper.GetConnection())
            {
                string sqlCmd = @"SELECT managerName,RoomID,RoomName,ManagerID,Size
                                FROM View_Manager2Room";
                var result = conn.Query<ViewManager2Room>(sqlCmd);
                return result;
            }
        }
    }
}
