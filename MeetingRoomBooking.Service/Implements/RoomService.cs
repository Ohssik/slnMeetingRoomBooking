using AutoMapper;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.Models;
using MeetingRoomBooking.Service.DTOs;
using MeetingRoomBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.Implements
{
    public class RoomService : IRoomService
    {
        private IMapper _mapper;
        private IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository,IMapper mapper) 
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
        }
        public async Task<IEnumerable<Manager2RoomDto>> GetAllManagersAsync()
        {
            var managers = await _roomRepository.GetAllManagersAsync();
            var result = _mapper.Map<IEnumerable<Manager2RoomDto>>(managers);
            return result;
        }

        public async Task<IEnumerable<MeetingRoomDto>> GetAllRoomsAsync()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();
            var result = _mapper.Map<IEnumerable<MeetingRoomDto>>(rooms);
            return result;
        }
    }
}
