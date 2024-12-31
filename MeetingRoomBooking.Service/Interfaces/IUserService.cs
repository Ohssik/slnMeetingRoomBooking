using MeetingRoomBooking.Repository.Models;
using MeetingRoomBooking.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Service.Interfaces
{
    public interface IUserService
    {
        // <summary>
        // Get User by User ID
        // </summary>
        /// <param name="userId">User ID</param>
        Task<UserDto> GetByIdAsync(string userId);

        // <summary>
        // Check User is Existed
        // </summary>
        /// <param name="user">UserModel user</param>
        Task<bool> IsExist(UserDto user);
    }
}
