using MeetingRoomBooking.Repository.DataModels;
using MeetingRoomBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.Implements
{
    public class RoomRepository : IRoomRepository
    {
        private readonly testContext _db;

        public RoomRepository(testContext db) 
        {
            _db = db;
        }

        public async Task<IEnumerable<TMeetingRoom>> GetAllAsync()
        {
            return await Task.Run(()=>_db.TMeetingRooms.Select(r => r).ToList()) ?? Enumerable.Empty<TMeetingRoom>();
        }

        public async Task<IEnumerable<ViewManager2Room>> GetAllRoomInfoAsync()
        {
            var managers = await Task.Run(()=> _db.ViewManager2Rooms.Select(m => m).ToList());
            return managers ?? Enumerable.Empty<ViewManager2Room>();
        }
    }
}
