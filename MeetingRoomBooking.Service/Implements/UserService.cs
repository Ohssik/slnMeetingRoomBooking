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
    /// <summary>
    /// User Service class
    /// </summary>
    /// <seealso cref="MeetingRoomBooking.Service.Interfaces.IUserService" />
    public class UserService : IUserService
    {
        
        private IMapper _mapper;       
        private IUserRepository _userRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="mapper">The mapper.</param>
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 從User Id取得User data
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public async Task<UserDto> GetByIdAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            var result=_mapper.Map<UserDto>(user);

            return result;  
        }

        /// <summary>
        /// 檢查輸入的User帳密是否正確
        /// </summary>
        /// <param name="user">UserModel user</param>
        /// <returns></returns>
        public async Task<bool> IsExist(UserDto user)
        {            
            var userModel= _mapper.Map<UserModel>(user);

            if(await _userRepository.IsExist(userModel))
            {
                return true;
            }

            return false;
        }
    }
}
