using MapsterMapper;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.ParameterModels;
using MeetingRoomBooking.Service.Dtos;
using MeetingRoomBooking.Service.Interfaces;
using MeetingRoomBooking.Service.ParameterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.Implements
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserAsync(GetUserParameterDto parameter)
        {
            var result = await _userRepository.GetUserAsync(_mapper.Map<GetUserParameterModel>(parameter));
            return _mapper.Map<UserDto>(result);
        }
    }
}
