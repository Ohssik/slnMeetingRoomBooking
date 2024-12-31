using MeetingRoomBooking.Repository.Models;

namespace MeetingRoomBooking.Repository.Interfaces
{
    public interface IUserRepository
    {
        // <summary>
        // Get User by User ID
        // </summary>
        /// <param name="userId">User ID</param>
        Task<UserModel> GetByIdAsync(string userId);

        // <summary>
        // Check User is Existed
        // </summary>
        /// <param name="user">UserModel user</param>
        Task<bool> IsExist(UserModel user);
    }
}
