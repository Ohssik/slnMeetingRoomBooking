using MeetingRoomBooking.Service.Dtos;
using MeetingRoomBooking.Service.ParameterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(GetUserParameterDto parameter);

    }
}
