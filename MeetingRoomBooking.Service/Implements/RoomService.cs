using MapsterMapper;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Service.Dtos;
using MeetingRoomBooking.Service.Interfaces;

namespace MeetingRoomBooking.Service.Implements
{
    public class RoomService: IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<RoomDto>>(await _roomRepository.GetAllAsync());
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomInfoAsync()
        {
            return _mapper.Map<IEnumerable<RoomDto>>(await _roomRepository.GetAllRoomInfoAsync());
        }
    }
}
