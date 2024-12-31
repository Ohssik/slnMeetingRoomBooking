using MeetingRoomBooking.Service.Dtos;

namespace MeetingRoomBooking.Service.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<IEnumerable<RoomDto>> GetAllRoomInfoAsync();
    }
}
