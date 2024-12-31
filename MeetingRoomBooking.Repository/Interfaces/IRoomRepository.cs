using MeetingRoomBooking.Repository.Models;

namespace MeetingRoomBooking.Repository.Interfaces
{
    public interface IRoomRepository
    {
        // <summary>Get All Meeting Rooms Info</summary>
        Task<IEnumerable<MeetingRoomModel>> GetAllRoomsAsync();

        // <summary>Get All Meeting Rooms Managers</summary>
        Task<IEnumerable<Manager2RoomModel>> GetAllManagersAsync();
    }
}
